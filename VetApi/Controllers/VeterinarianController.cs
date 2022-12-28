using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetApi.DTOS.VeterinarianDTOS;
using VetApi.Services.Interfaces;

namespace VetApi.Controllers
{
    [Route("v1/[controller]")]
    [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VeterinarianController : Controller
    {
        private readonly IVeterinarianServices _veterinarianServices;

        public VeterinarianController(IVeterinarianServices veterinarianServices)
        {
            _veterinarianServices = veterinarianServices;
        }
        /// <summary>
        /// Retorna os veterinarios com paginação de dados
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet("Veterinarian/{skip:int?}/{take:int?}")]
        public async Task<IActionResult> GetAll([FromRoute] int skip = 0, [FromRoute] int take = 10)
        {
            try
            {
                var veterinarians = await _veterinarianServices.GetAll(skip, take);
                return Ok(veterinarians);
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição Invalida " + ex.Message);
            }
        }   
        /// <summary>
        /// Retorna o veterinario pelo seu codigo de identificação
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Veterinarian/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var Veterinarian = await _veterinarianServices.GetById(id);
                return Ok(Veterinarian);
            }
            catch(Exception)
            {
                return BadRequest("Requisição invalida");
            }
        }
        /// <summary>
        /// Cria um novo veterinario e um novo endereço para ele, ao criar um veterinario e automaticamente criado um novo email para o mesmo que  consiste em seu nome tudo minusculo e sem espaços @vet.com e asenha e Vet@1234
        /// </summary>
        /// <param name="veterinarianDto"></param>
        /// <returns></returns>
        [HttpPost("CreateWithNewAddress")]
        public async Task<IActionResult> CreateWithNewAddress([FromBody] CreateVeterinarianWithAddressDTO veterinarianDto)
        {
            try
            {
                var veterinarian = await _veterinarianServices.CreateWithAddress(veterinarianDto);
                return Created("Criado", veterinarian);
            }
            catch(Exception ex)
            {
                return BadRequest("Não Criado! " + ex.Message);
            }
        }
        /// <summary>
        /// Consulta se o veterinario a ser criado exite, e caso exista ele o retorna, ao criar um veterinario e automaticamente criado um novo email para o mesmo que  consiste em seu nome tudo minusculo e sem espaços @vet.com e asenha e Vet@1234
        /// </summary>
        /// <param name="veterinarianDTO"></param>
        /// <returns></returns>
        [HttpPost("CreateOrReturn")]
        public async Task<IActionResult> CreateOrReturn([FromBody] CreateVeterinarianWithAddressDTO veterinarianDTO)
        {
            try
            {
                var veterinarian = await _veterinarianServices.CreateOrReturnTutor(veterinarianDTO);
                return Ok(veterinarian);
            }
            catch(Exception ex)
            {
                return BadRequest("Not Created! " + ex.Message);
            }
        }
        /// <summary>
        /// Retorna um veterinario pelo seu cpf
        /// </summary>
        /// <param name="SSN"></param>
        /// <returns></returns>
        [HttpGet("Veterinarian/{SSN}")]
        public async Task<IActionResult> GetBySSN(string SSN)
        {
            try
            {
                var veterinarian = await _veterinarianServices.GetBySSN(SSN);
                return Ok(veterinarian);
            }
            catch(Exception)
            {
                return BadRequest("Invalid Request");
            }
        }
        /// <summary>
        /// Retorna um veterinario pelo id da sua especialidade que é um enum
        /// </summary>
        /// <param name="SpecialtyId"></param>
        /// <returns></returns>
        [HttpGet("Specialty/{SpecialtyId}")]
        public async Task<IActionResult> GetBySpecialty([FromRoute] int SpecialtyId)
        {
            try
            {
                var veterinarians = await _veterinarianServices.GetBySpecialty(SpecialtyId);
                return Ok(veterinarians);
            }
            catch(Exception)
            {
                return BadRequest("Requisição invalida");
            }
        }
        /// <summary>
        /// Cria um novo usuario, ao criar um veterinario e automaticamente criado um novo email para o mesmo que  consiste em seu nome tudo minusculo e sem espaços @vet.com e asenha e Vet@1234
        /// </summary>
        /// <param name="veterinarianDTO"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateVeterinarianDTO veterinarianDTO)
        {
            try
            {
                await _veterinarianServices.Create(veterinarianDTO);
                return Ok("Create");
            }
            catch(Exception ex)
            {
                return BadRequest("Não Criado! " + ex.Message);
            }
        }
        /// <summary>
        /// Atualiza o veterinario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="veterinarianDTO"></param>
        /// <returns></returns>
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVeterinarianDTO veterinarianDTO)
        {
            try
            {
                await _veterinarianServices.Update(id, veterinarianDTO);
                return Accepted("Modificado");
            }
            catch(Exception)
            {
                return BadRequest("Não Modificado!");
            }
        }
        /// <summary>
        /// Deleta o Veterinario permanentemente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Veterinarian/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _veterinarianServices.Delete(id);
                return Ok("Deletado!");
            }
            catch(Exception)
            {
                return BadRequest("Não foi possivel deletar");
            }
        }
    }
}