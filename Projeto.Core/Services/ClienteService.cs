using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;
using Projeto.Core.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Core.Services
{
    public class ClienteService : ICrudService<Cliente>
    {

        private void Validate(Cliente entity)
        {
            // TODO: não deixe que clientes com o mesmo e-mail sejam inseridos na base de dados
            /*
            if (...)
            {
                throw new BusinessException("O e-mail informado já está cadastrado");
            }
            */
        }

        public void Delete(Cliente entity)
        {
            throw new NotImplementedException();
        }

        public Cliente GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Cliente entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Cliente entity)
        {
            throw new NotImplementedException();
        }
    }
}
