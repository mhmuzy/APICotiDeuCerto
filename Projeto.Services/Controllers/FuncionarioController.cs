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
    public class FuncionarioController : ControllerBase
    {
        //atributo
        private readonly IFuncionarioRepository funcionarioRepository;

        //construtor com entrada de argumentos
        public FuncionarioController(IFuncionarioRepository funcionarioRepository)
        {
            this.funcionarioRepository = funcionarioRepository;
        }

        [HttpPost]
        public IActionResult Post(FuncionarioCadastroModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var funcionario = new Funcionario();
                    funcionario.Nome = model.Nome;
                    funcionario.Salario = model.Salario;
                    funcionario.DataAdmissao = model.DataAdmissao;
                    funcionario.IdSetor = model.IdSetor;
                    funcionario.IdFuncao = model.IdFuncao;

                    funcionarioRepository.Inserir(funcionario);

                    var result = new
                    { 
                        mensagem = "Funcionário cadastrado com sucesso.",
                        funcionario = funcionario
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
        public IActionResult Put(FuncionarioEdicaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {                    
                    var funcionario = funcionarioRepository.ObterPorId(model.IdFuncionario);
                                        
                    if (funcionario != null)
                    {
                        funcionario.Nome = model.Nome;
                        funcionario.Salario = model.Salario;
                        funcionario.DataAdmissao = model.DataAdmissao;
                        funcionario.IdSetor = model.IdSetor;
                        funcionario.IdFuncao = model.IdFuncao;

                        funcionarioRepository.Atualizar(funcionario);

                        var result = new
                        { 
                            mensagem = "Funcionário atualizado com sucesso.",
                            funcionario = funcionario
                        };

                        return Ok(result);
                    }
                    else
                    {
                        return UnprocessableEntity("Id do funcionário não foi encontrado no sistema.");
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
                var funcionario = funcionarioRepository.ObterPorId(id);

                if (funcionario != null)
                {
                    funcionarioRepository.Excluir(funcionario);
                    return Ok("Funcionário excluído com sucesso.");
                }
                else
                {
                    return UnprocessableEntity("Id da funcionário não foi encontrado no sistema.");
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
                var lista = funcionarioRepository.ConsultarTodos();

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
                var funcionario = funcionarioRepository.ObterPorId(id);

                if (funcionario != null)
                {
                    return Ok(funcionario);
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