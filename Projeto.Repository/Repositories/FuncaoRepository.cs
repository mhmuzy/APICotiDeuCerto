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
    public class FuncaoRepository : IFuncaoRepository
    {
        //atributo
        private readonly string connectionString;

        //construtor para receber o valor da connectionstring
        public FuncaoRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Inserir(Funcao entity)
        {
            var query = "insert into Funcao(Descricao) values(@Descricao) SELECT SCOPE_IDENTITY()";

            using (var connection = new SqlConnection(connectionString))
            {
                entity.IdFuncao = connection.QueryFirstOrDefault<int>(query, entity);
            }
        }

        public void Atualizar(Funcao entity)
        {
            var query = "update Funcao set Descricao = @Descricao where IdFuncao = @IdFuncao";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Excluir(Funcao entity)
        {
            var query = "delete from Funcao where IdFuncao = @IdFuncao";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Funcao> ConsultarTodos()
        {
            var query = "select * from Funcao";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Funcao>(query).ToList();
            }
        }

        public Funcao ObterPorId(int id)
        {
            var query = "select * from Funcao where IdFuncao = @IdFuncao";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Funcao>(query, new { IdFuncao = id }).FirstOrDefault();
            }
        }
    }
}
