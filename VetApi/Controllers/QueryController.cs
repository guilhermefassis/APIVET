using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetApi.DTOS.QueryDTOS;
using VetApi.Services.Interfaces;

namespace VetApi.Controllers
{
    [Route("v1/[controller]")]
    public class QueryController : Controller
    {
       private readonly IQueryServices _queryServices;

        public QueryController(IQueryServices queryServices)
        {
            _queryServices = queryServices;
        }
        /// <summary>
        /// Cria uma nova consulta, assim vc passa os dados do dia do animal, o CRVM do medico e o codigo de identificação do animal
        /// </summary>
        /// <param name="queryDTO"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create([FromBody] CreateQueryDTO queryDTO)
        {
            try
            {
                await _queryServices.Create(queryDTO);
                return Created("Criado", queryDTO);
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição Invalida "+ ex.Message);
            }
        }
        /// <summary>
        /// Faz a requisição dos dados da api com paginação de daos
        /// </summary>
        /// <param name="skip">Numero da pagina</param>
        /// <param name="take">Quantidade de items</param>
        /// <returns></returns>
        [HttpGet("Consultas/{skip:int?}/{take:int?}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAll([FromRoute] int skip = 0, [FromRoute] int take = 10)
        {
            try
            {
                var querys = await _queryServices.GetAll(skip, take);
                return Ok(querys);
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição Invalida "+ ex.Message);
            }
        }
        /// <summary>
        /// Retorna uma consulta por id da consulta
        /// </summary>
        /// <param name="id">Identificador de posição do banco de dados</param>
        /// <returns></returns>
        [HttpGet("Consulta/{id}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var consulta = await _queryServices.GetById(id);
                return Ok(consulta);
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição invalid " + ex.Message);
            }
        }
        /// <summary>
        /// Esse metodo retorna todas as consultas de um veterinario especifico
        /// </summary>
        /// <param name="CRVM">Numero de identificação do veterinario</param>
        /// <returns></returns>
        [HttpGet("Consulta/Veterinarian/{CRVM}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByVeterinarianCRVM(string CRVM)
        {
            try
            {
                var consulta = await _queryServices.GetByVeterinarianCRVM(CRVM);
                return Ok(consulta);
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição invalid " + ex.Message);
            }
        }
        /// <summary>
        /// Retorna as consulta pelo id do veterinario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Consulta/Veterinarian/{id:int}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByVeterinarianId(int id)
        {
            try
            {
                var consulta = await _queryServices.GetByVeterinarianId(id);
                return Ok(consulta);
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição invalid " + ex.Message);
            }
        }
        /// <summary>
        /// Retorna as consultas pelo id do tutor, caso o tutor tenha mais de um animal retornara todas as consultas de todos os seus caes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Consulta/Tutor/{id:int}")]
        [Authorize(Roles = "admin, regular, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByTutorId(int id)
        {
            try
            {
                var consulta = await _queryServices.GetByTutorId(id);
                return Ok(consulta);
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição invalid " + ex.Message);
            }
        }
        /// <summary>
        /// Retorna as consultas pelo CPF do tutor
        /// </summary>
        /// <param name="SSN"></param>
        /// <returns></returns>
        [HttpGet("Consulta/Tutor/{SSN}")]
        [Authorize(Roles = "admin, regular, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByTutorSSN(string SSN)
        {
            try
            {
                var consulta = await _queryServices.GetByTutorSSN(SSN);
                return Ok(consulta);
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição invalid " + ex.Message);
            }
        }
        /// <summary>
        /// Retornas as consultas pelo id do animal, retorna todo o historico das consultas daquele animal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Consulta/Animal/{id:int}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByAnimalId(int id)
        {
            try
            {
                var consulta = await _queryServices.GetByAnimalId(id);
                return Ok(consulta);
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição invalid " + ex.Message);
            }
        }
        /// <summary>
        /// Retorna As consulta pelo codigo de identificação do animal
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("Consulta/Animal/{code}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByAnimalCode(string code)
        {
            try
            {
                var consulta = await _queryServices.GetByAnimalCode(code);
                return Ok(consulta);
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição invalid " + ex.Message);
            }
        }
        /// <summary>
        /// Retornas as consultas de uma data especifica
        /// </summary>
        /// <param name="Date">data no formato => dd-MM-yyyy</param>
        /// <returns></returns>
        [HttpGet("Consulta/Date/{Date}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByDate(string Date)
        {
            try
            {
                DateTime date = DateTime.ParseExact(Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                var consulta = await _queryServices.GetByDate(date);
                return Ok(consulta);
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição invalid " + ex.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de uma consulta
        /// </summary>
        /// <param name="id"></param>
        /// <param name="queryDTO"></param>
        /// <returns></returns>
        [HttpPut("Consulta/{id}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateQueryDTO queryDTO)
        {
            try
            {
                await _queryServices.Update(id, queryDTO);
                return Accepted("Modificado");
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição invalid " + ex.Message);
            }
        }
        /// <summary>
        /// Faz a deleção permanente de uma consulta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Consulta/{id}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _queryServices.Delete(id);
                return Accepted("Modificado");
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição invalid " + ex.Message);
            }
        }

    }
}