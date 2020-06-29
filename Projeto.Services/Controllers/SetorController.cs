using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Repository.Contracts;
using Projeto.Repository.Entities;
using Projeto.Services.Models;

namespace Projeto.Services.Controllers
{
    [EnableCors("DefaultPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class SetorController : ControllerBase
    {
        //atributo
        private readonly ISetorRepository setorRepository;

        //construtor com entrada de argumentos
        public SetorController(ISetorRepository setorRepository)
        {
            this.setorRepository = setorRepository;
        }

        [HttpPost]
        public IActionResult Post(SetorCadastroModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var setor = new Setor();
                    setor.Nome = model.Nome;

                    setorRepository.Inserir(setor);

                    var result = new
                    { //objeto anônimo
                        mensagem = "Setor cadastrado com sucesso.",
                        setor = setor
                    };

                    return Ok(result);
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put(SetorEdicaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var setor = setorRepository.ObterPorId(model.IdSetor);

                    //verificar se a função foi encontrada
                    if (setor != null)
                    {
                        setor.Nome = model.Nome;

                        setorRepository.Atualizar(setor);

                        var result = new
                        { //objeto anônimo
                            mensagem = "Setor atualizado com sucesso.",
                            setor = setor
                        };

                        return Ok(result);
                    }
                    else
                    {
                        return UnprocessableEntity("Id da setor não foi encontrado no sistema.");
                    }
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var setor = setorRepository.ObterPorId(id);

                if (setor != null)
                {
                    setorRepository.Excluir(setor);
                    return Ok("Setor excluído com sucesso.");
                }
                else
                {
                    return UnprocessableEntity("Id do setor não foi encontrado no sistema.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var lista = setorRepository.ConsultarTodos();

                if (lista != null && lista.Count > 0)
                {
                    return Ok(lista);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var setor = setorRepository.ObterPorId(id);

                if (setor != null)
                {
                    return Ok(setor);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}