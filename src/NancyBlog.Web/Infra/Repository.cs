using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NancyBlog.Domain;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using System.Data;

namespace NancyBlog.Infra
{
    public class Repository : IRepository
    {
        static IDbConnectionFactory DbFactory = new OrmLiteConnectionFactory(
                                @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\App_Data\Database1.mdf;Integrated Security=True;User Instance=True",
                                SqlServerOrmLiteDialectProvider.Instance);

        protected IDbCommand Command()
        {
            return DbFactory.OpenDbConnection().CreateCommand();
        }

        public IQueryable<T> Query<T>()
        {            
            throw new NotImplementedException();
        }

        public T Find<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        public T First<T>()
        {
            throw new NotImplementedException();
        }

        public IUnitOfWork Start()
        {
            return new UnitOfWork(Command());
        }
    }
}