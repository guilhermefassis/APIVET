using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetApi.DTOS.AnimalDTOS;
using VetApi.Services.Interfaces;

namespace VetApi.Controllers
{
    [Route("v1/[controller]")]
    public class AnimalsController : Controller
    {
       private readonly IAnimalServices _animalServices;

        public AnimalsController(IAnimalServices animalServices)
        {
            _animalServices = animalServices;
        }
        /// <summary>
        /// Metodo responsavel por retornar todos os animais, os argumentos são opcionais, porém determinam a paginação de dados
        /// </summary>
        /// <param name="skip">Parametro define qual a pagina que ira retornar</param>
        /// <param name="take">esse parametro define quantos items teremos na pagina</param>
        /// <returns>O retorno vem um objeto com o hateoas, os parametros definem qual é a quantidade e quais items serao retornados
        ///     por exemplo skip=1 take=10 ele ira pular os 0 primeiros items e ira apresentar os itens de 11 á 20.
        /// </returns>
        [HttpGet("{skip:int?}/{take:int?}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get([FromRoute] int skip = 0,
                                             [FromRoute] int take = 10)
        {
            try
            {
                var animals = await _animalServices.Get(skip, take);
                return Ok(animals);
            }
            catch(Exception)
            {
                return BadRequest("Invalid Request!");
            }
            
        }
        /// <summary>
        ///  Retorna um animal pelo seu id, posição dentro do banco de dados
        /// </summary>
        /// <param name="id">Identificador do banco de dados</param>
        /// <returns></returns>
        [HttpGet("Animal/{id}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var animal = await _animalServices.GetById(id);
                return Ok(animal);
            }
            catch(Exception)
            {
                return BadRequest("Invalid Request");
            }
        }
        /// <summary>
        /// Retornar os animais pelo seu codigo de identificação
        /// </summary>
        /// <param name="code">codigo de identificação do animal</param>
        /// <returns></returns>
        [HttpGet("Animal/Code/{code}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByAnimalCode(string code)
        {
            try
            {
                var animal = await _animalServices.GetByCode(code);
                return Ok(animal);
            }
            catch(Exception)
            {
                return BadRequest("Invalid Request");
            }
        }

        /// <summary>
        /// Retorna os animais pelo CPF do tutor, ou seja a api faz a busca pelo cpf do tutor cadastro com o animal
        /// </summary>
        /// <param name="SSN">CPF do tutor</param>
        /// <returns></returns>
        [HttpGet("Animal/Tutor/{SSN}")]
        [Authorize(Roles = "admin, regular, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByTutorSSN(string SSN)
        {
            try
            {
                var animals = await _animalServices.GetByTutorSSN(SSN);
                return Ok(animals);
            }
            catch(Exception)
            {
                return BadRequest("Invalid Request");
            }
        }
        /// <summary>
        /// Retorna os animais pelo Id do tutor
        /// </summary>
        /// <param name="id">id do tutor</param>
        /// <returns></returns>
        [HttpGet("Animal/Tutor/{id:int}")]
        [Authorize(Roles = "admin, regular, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByTutorId(int id)
        {
            try
            {
                var animals = await _animalServices.GetByTutorId(id);
                return Ok(animals);
            }
            catch(Exception)
            {
                return BadRequest("Invalid Request");
            }
        }
        /// <summary>
        /// Cria um animal e cria um tutor com um endereço novo e faz esa associação
        /// </summary>
        /// <param name="animalDTO"> Parametros necessarios para a criação de um animal e um tutor</param>
        /// <returns></returns>
        [HttpPost("CreateAnimalWithTutor")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateAnimalWithTutor([FromBody] CreateAnimalWithTutorDTO animalDTO)
        {
            try
            {
                await _animalServices.CreateWithTutor(animalDTO);
                return Created("Created", animalDTO);
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição invalida " + ex.Message);
            }
        }
        /// <summary>
        /// Retorna dados da raça do cachorro, caso a raça tenha sido cadastrada incorretamente ou alguma raça que nao seja encontrada no nosso banco sera associado o cao como SRD
        /// </summary>
        /// <param name="code">Codigo de identificação do animal</param>
        /// <returns></returns>
        [HttpGet("Animal/Breed/{code}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetBreed(string code)
        {
            try
            {
                var breed = await _animalServices.GetBreedDataToAnimalCode(code);
                return Ok(breed);
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição invalida " + ex.Message);
            }
        }
        /// <summary>
        /// Cria um novo animal passando o id de seu tutor
        /// </summary>
        /// <param name="animalDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create([FromBody] CreateAnimalDTO animalDTO)
        {
            try
            {
                await _animalServices.Create(animalDTO);
                return Created("Created", animalDTO);
            }
            catch(Exception ex)
            {
                return BadRequest("Requisição invalida " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo para atualizar dados do animal, podemos atualizar dois dados:
        /// Peso e tutor
        /// Caso nao queiram alterar o dado do tutor basta passas o parametro como 0 ou n > 0
        /// </summary>
        /// <param name="id">Id do cachorro a ser alterado</param>
        /// <param name="animalDTO">Dados que serão alterados</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAnimalDTO animalDTO)
        {
            try
            {
                await _animalServices.Update(id, animalDTO);
                return Accepted("Accepted");
            }
            catch (Exception)
            {
                return BadRequest("Not modified!");
            }
        }
        /// <summary>
        /// Faz a deleção permanente de um animal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Animal/{id:int}")]
        [Authorize(Roles = "admin, vet", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _animalServices.Delete(id);
                return Ok("Deleted");
            }
            catch (Exception)
            {
                return BadRequest("Invalid Request");
            }
        }

    }
}