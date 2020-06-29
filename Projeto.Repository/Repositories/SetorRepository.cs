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
    public class SetorRepository : ISetorRepository
    {
        //atributo
        private readonly string connectionString;

        //construtor para receber o valor da connectionstring
        public SetorRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Inserir(Setor entity)
        {
            var query = "insert into Setor(Nome) values(@Nome) SELECT SCOPE_IDENTITY()";

            using (var connection = new SqlConnection(connectionString))
            {
                entity.IdSetor = connection.QueryFirstOrDefault<int>(query, entity);
            }
        }

        public void Atualizar(Setor entity)
        {
            var query = "update Setor set Nome = @Nome where IdSetor = @IdSetor";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Excluir(Setor entity)
        {
            var query = "delete from Setor where IdSetor = @IdSetor";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Setor> ConsultarTodos()
        {
            var query = "select * from Setor";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Setor>(query).ToList();
            }
        }

        public Setor ObterPorId(int id)
        {
            var query = "select * from Setor where IdSetor = @IdSetor";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Setor>(query, new { IdSetor = id }).FirstOrDefault();
            }
        }
    }
}
