using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetApi.Services.Interfaces;

namespace VetApi.Controllers
{
    [Route("v1/[controller]")]
    public class DogApiController : Controller
    {

        private readonly ITheDogAPI _dogApi;

        public DogApiController(ITheDogAPI dogApi)
        {
            _dogApi = dogApi;
        }
        /// <summary>
        /// Retornar todas as raças com paginação de dados
        /// </summary>
        /// <param name="limit">Quantidade de itemns da pagina</param>
        /// <param name="page">numero da pagina</param>
        /// <returns></returns>
        [HttpGet("{limit:int}/{pag:int}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAll(int limit, int page)
        {
            try
            {
                var racas = await _dogApi.GetAll(page, limit);
                return Ok(racas);
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição invalida " + ex.Message);
            }
        }
        /// <summary>
        /// Retorna uma raça pelo nome
        /// </summary>
        /// <param name="breed">Nome da raça</param>
        /// <returns></returns>
        [HttpGet("{breed}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetBreedByName(string breed)
        {
             try
            {
                var racas = await _dogApi.GetByName(breed);
                return Ok(racas);
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição invalida " + ex.Message);
            }
        }
        /// <summary>
        /// Retorna todos os registros do banco de dados sobre raças
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllBreeds()
        {
            try
            {
                var racas = await _dogApi.GetAll();
                return Ok(racas);
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição invalida " + ex.Message);
            }
        }
    }
}