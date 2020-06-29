using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient; //biblioteca para o SqlServer
using Dapper; //biblioteca para executar os comandos SQL
using Projeto.Repository.Entities; //importando
using Projeto.Repository.Contracts; //importando
using System.Linq;

namespace Projeto.Repository.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        //atributo
        private readonly string connectionString;

        //construtor para receber o valor da connectionstring
        public FuncionarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Inserir(Funcionario entity)
        {
            var query = "insert into Funcionario(Nome, Salario, DataAdmissao, IdSetor, IdFuncao) values(@Nome, @Salario, @DataAdmissao, @IdSetor, @IdFuncao) SELECT SCOPE_IDENTITY()";

            using (var connection = new SqlConnection(connectionString))
            {
                entity.IdFuncionario = connection.QueryFirstOrDefault<int>(query, entity);
            }
        }

        public void Atualizar(Funcionario entity)
        {
            var query = "update Funcionario set Nome = @Nome, Salario = @Salario, DataAdmissao = @DataAdmissao, IdSetor = @IdSetor, IdFuncao = @IdFuncao where IdFuncionario = @IdFuncionario";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Excluir(Funcionario entity)
        {
            var query = "delete from Funcionario where IdFuncionario = @IdFuncionario";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Funcionario> ConsultarTodos()
        {
            var query = "select * from Funcionario f "
                      + "inner join Setor s on f.IdSetor = s.IdSetor "
                      + "inner join Funcao fn on fn.IdFuncao = f.IdFuncao";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query(query, (Funcionario f, Setor s, Funcao fn) 
                        => 
                        {
                            f.Setor = s; //agregando Setor ao -> Funcionario
                            f.Funcao = fn; //agregando Funcao ao -> Funcionario
                            return f;
                        }, 
                        splitOn: "IdSetor, IdFuncao") //chaves estrangeiras
                        .ToList();
            }
        }

        public Funcionario ObterPorId(int id)
        {
            var query = "select * from Funcionario f "
                     + "inner join Setor s on f.IdSetor = s.IdSetor "
                     + "inner join Funcao fn on fn.IdFuncao = f.IdFuncao "
                     + "where f.IdFuncionario = @IdFuncionario";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query(query, (Funcionario f, Setor s, Funcao fn)
                    =>
                    {
                        f.Setor = s;
                        f.Funcao = fn;
                        return f;
                    },                    
                    new { IdFuncionario = id }, 
                    splitOn: "IdSetor, IdFuncao")
                    .FirstOrDefault();
            }
        }
    }
}
