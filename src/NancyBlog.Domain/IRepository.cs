using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NancyBlog.Domain
{
    public interface IRepository
    {
        IQueryable<T> Query<T>();
        T Find<T>(Guid id);
        T First<T>();

        IUnitOfWork Start();        
    }
}
