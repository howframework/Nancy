using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NancyBlog.Domain;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using System.Data;
using System.Linq.Expressions;

namespace NancyBlog.Infra
{
    public class Repository : IRepository
    {
        static IDbConnectionFactory DbFactory = new OrmLiteConnectionFactory(
                                @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True",
                                SqlServerOrmLiteDialectProvider.Instance);

        public IDbCommand Command()
        {
            return DbFactory.OpenDbConnection().CreateCommand();
        }

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate) where T : Entity, new()
        {
            return Command().Select<T>(predicate).AsQueryable();
        }

        public T Find<T>(Guid id) where T : Entity, new()
        {
            return Command().QueryById<T>(id);
        }

        public T First<T>(Expression<Func<T, bool>> predicate) where T : Entity, new()
        {
            return Command().Select<T>(predicate).FirstOrDefault();
        }

        public IUnitOfWork Start()
        {
            return new UnitOfWork(Command());
        }
    }
}