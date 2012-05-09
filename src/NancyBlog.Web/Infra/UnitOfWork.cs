using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NancyBlog.Domain;
using System.Data;
using ServiceStack.OrmLite;

namespace NancyBlog.Infra
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected IDbTransaction Transaction { get; set; }
        protected IDbCommand Command { get; set; }

        public UnitOfWork(IDbCommand command)
        {
            Command = command;
            Transaction = command.BeginTransaction();
        }

        public void Create(Entity entity)
        {
            Command.Insert(entity);
        }

        public void Update(Entity entity)
        {
            Command.Update(entity);
        }

        public void Delete(Entity entity)
        {
            Command.Delete(entity);
        }

        public void Commit()
        {
            Transaction.Commit();           
        }

        public bool IsRolledBack { get; set; }

        public void Rollback()
        {
            Transaction.Rollback();
            IsRolledBack = true;
        }

        public void Dispose()
        {
            if (!IsRolledBack)
                Transaction.Commit();
        }
    }
}
