using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NancyBlog.Domain
{
    public interface IUnitOfWork
    {
        void Create(Entity entity);
        void Update(Entity entity);
        void Delete(Entity entity);

        void Commit();
        void Rollback();
    }
}
