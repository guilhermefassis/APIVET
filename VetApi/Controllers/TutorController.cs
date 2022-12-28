using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetApi.DTOS.AddreessDTOS;
using VetApi.DTOS.TutorsDTOS;
using VetApi.Services.Interfaces;

namespace VetApi.Controllers
{
    [Route("v1/[controller]")]
    public class TutorController : Controller
    {
        private readonly ITutorServices _tutorServices;


       public TutorController(ITutorServices tutorServices)
        {
            _tutorServices = tutorServices;
        }
        /// <summary>
        /// Cria um tutor com um novo endereço
        /// </summary>
        /// <param name="tutorDto"></param>
        /// <returns></returns>
        [HttpPost("CreateWithNewAddress")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateWithNewAddress([FromBody] CreateTutorWithAddressDTO tutorDto)
        {
            try
            {
                var tutor = await _tutorServices.CreateWithNewAddress(tutorDto);
                return Created("Created", tutor);
            }
            catch(Exception ex)
            {
                return BadRequest("Not Created! " + ex.Message);
            }
        }
        /// <summary>
        /// Cria um novo tutor e caso ele ja exista retorna o tutor com o mesmo cpf que temos no banco de dados
        /// </summary>
        /// <param name="tutorDto"></param>
        /// <returns></returns>
        [HttpPost("CreateOrReturn")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateOrReturn([FromBody] CreateTutorWithAddressDTO tutorDto)
        {
            try
            {
                var tutor = await _tutorServices.CreateOrReturnTutor(tutorDto);
                return Ok(tutor);
            }
            catch(Exception ex)
            {
                return BadRequest("Not Created! " + ex.Message);
            }
        }
        /// <summary>
        /// Cria um novo tutor e é atrelado um endereço ja existente
        /// </summary>
        /// <param name="tutorDto"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create([FromBody] CreateTutorDTO tutorDto)
        {
            try
            {
                await _tutorServices.Create(tutorDto);
                
                return Ok("Create");
            }
            catch(Exception ex)
            {
                return BadRequest("Not Created! " + ex.Message);
            }
        }
        /// <summary>
        /// Retorna todos os tutores com paginação de dados
        /// </summary>
        /// <param name="skip">pagina</param>
        /// <param name="take">quantidade de conteudo</param>
        /// <returns></returns>
        [HttpGet("{skip:int?}/{take:int?}")]
        [Authorize(Roles = "admin, regular, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAll([FromRoute] int skip = 0, [FromRoute] int take = 10)
        {
             try
            {
                var tutors = await _tutorServices.GetAll();
                return Ok(tutors);
            }
            catch(Exception ex)
            {
                return BadRequest("Not Created! " + ex.Message);
            }
        }
        /// <summary>
        /// Retorna o tutor pela sua identificação no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Tutor/{id:int}")]
        [Authorize(Roles = "admin, regular, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var tutor = await _tutorServices.GetById(id);
                return Ok(tutor);
            }
            catch(Exception)
            {
                return BadRequest("Invalid Request");
            }
        }
        /// <summary>
        /// Retorna o tutor pelo seu CPF
        /// </summary>
        /// <param name="SSN"></param>
        /// <returns></returns>
        [HttpGet("Tutor/{SSN}")]
        [Authorize(Roles = "admin, regular, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetBySSN(string SSN)
        {
            try
            {
                var tutor = await _tutorServices.GetBySSN(SSN);
                return Ok(tutor);
            }
            catch(Exception)
            {
                return BadRequest("Invalid Request");
            }
        }
        /// <summary>
        /// Retorna o tutor pelo id de seu animal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Tutor/Animal/{id:int}")]
        [Authorize(Roles = "admin, regular, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByAnimalId(int id)
        {
            try
            {
                var tutor = await _tutorServices.GetByAnimalId(id);
                return Ok(tutor);
            }
            catch(Exception)
            {
                return BadRequest("Invalid Request");
            }
        }
        /// <summary>
        /// Retorna o tutor pelo codigo de identificação de seu animal
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("Tutor/Animal/{code}")]
        [Authorize(Roles = "admin, regular, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByAnimalCode(string code)
        {
            try
            {
                var tutor = await _tutorServices.GetByAnimalCode(code);
                return Ok(tutor);
            }
            catch(Exception)
            {
                return BadRequest("Invalid Request");
            }
        }
        /// <summary>
        /// Atualiza o endereço do tutor, e seus dados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tutor"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateTutorDTO tutor)
        {
             try
            {
                await _tutorServices.Update(id, tutor);
                return Accepted("Modificado", tutor);
            }
            catch(Exception)
            {
                return BadRequest("Invalid Request");
            }
        }
        /// <summary>
        /// Atualza para um novo endereço e apaga o antigo do banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPut("NewAddress/{id:int}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateWithNewAddress(int id, [FromBody]CreateAddressDTO address)
        {
             try
            {
                await _tutorServices.UpdateWithNewAddress(id, address);
                return Accepted("Modificado", address);
            }
            catch(Exception ex)
            {
                return BadRequest("Invalid Request " + ex.Message);
            }
        }
        /// <summary>
        /// Deleta o tutor permanentemente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Tutor/{id:int}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _tutorServices.Delete(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid Request " + ex.Message);
            }
        }
    }
}