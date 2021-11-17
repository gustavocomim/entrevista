using Dapper;
using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;
using Projeto.Core.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace Projeto.Core.Services
{
    public class TarefaService : ICrudService<Tarefa>
    {
        private SQLiteConnection _connection;

        /// <summary>
        /// TODO: melhorar a forma com que essa classe recebe a injeção de dependencia
        /// </summary>
        public void InjetarConexao(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void Delete(Tarefa entity)
        {
            throw new NotImplementedException();
        }

        public Tarefa GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Tarefa entity)
        {
            _connection.Execute("INSERT INTO tarefa (tipo, descricao, concluida) VALUES(@Tipo, @Descricao, @Concluida)", entity);
        }

        public void Update(Tarefa entity)
        {
            _connection.Execute("UPDATE tarefa SET concluida = @Concluida WHERE id = @Id", entity);
        }
    }
}
