using Projeto.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Core.Infrastructure.Database
{
    /// <summary>
    /// TODO: comente o que essa interface representa.
    /// </summary>
    public interface ICrudService<T> 
        where T: IEntity
    {
        public T GetById(int id);
        public void Insert(T entity);

        public void Update(T entity);

        public void Delete(T entity);
    }
}
