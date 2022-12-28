using System;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using VetApi.Data;
using VetApi.DTO;
using VetApi.Services.Interfaces;

namespace SanclerAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AutorizationController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IEmailServices _emailServices;

        public AutorizationController(IUserServices userServices,
                                      IEmailServices emailServices)
        {
            _userServices = userServices;
            _emailServices = emailServices;
        }
        /// <summary>
        /// Retorna informação do login
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "AutorizationController :: Access in : " + DateTime.Now.ToLongDateString();
        }
        /// <summary>
        /// Faz um registro de usuario e manda um email para ele, para confirmação
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }

            Result result = await _userServices.RegisterUser(model);
            if(result.IsFailed) return BadRequest();
            return Ok(result.Successes);
            
        }
        /// <summary>
        /// Faz o login de um novo usuario
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>Token</returns>
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDTO userInfo)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }
            var result = await _userServices.LogIn(userInfo);
            if(result.IsFailed) return Unauthorized(result.Errors);
            return Ok(result.Successes);
            
        }
        /// <summary>
        /// Confirma sua conta, é usado pelo email
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("Confirm")]
        public async Task<IActionResult> ComfirmAccount([FromQuery] ConfirmEmailRequest request)
        {
            var result = await _emailServices.ConfirmAccount(request);
            if (result.IsSuccess) return Ok("your account have be a confirmed!");
            return BadRequest(result.Errors);
        }

        /// <summary>
        ///  Realiza o Lgout com segurança do usuario
        /// </summary>
        /// <returns></returns>
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            Result result = _userServices.Logout();
            if(result.IsSuccess) return Ok(result);
            return Unauthorized("Log out fail!");
        }
        /// <summary>
        /// Envia o email com um token para o usuario, onde a url tem uma parte do token e esse token que recebe pelo email em conjunto se torna o token completo para poder trocar a senha
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("request-reset")]
        public IActionResult RequestReset(RequestReset request)
        {
            Result result = _userServices.RequestResetPassword(request);
            if(result.IsFailed) return Unauthorized(result.Errors);
            return Ok(result.Successes);
        }
        /// <summary>
        /// nesse metodo vove rebera uma url contendo o login e deve usar o metodo post para passar a nova senha e o token de verificação para poder raealizar a troca, esse token tem validação de 2 horas.
        /// </summary>
        /// <param name="T"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("make-reset")]
        public IActionResult MakeResetPassword([FromQuery] string T,
                                               [FromBody] MakeResetRequest request)
        {
            var Token = T + request.Token;
            request.Token = Token;
            Result result = _userServices.MakeResetPassword(request);
            if(result.IsFailed) return Unauthorized(result.Errors);
            return Ok(result.Successes);
        }

    }
}
