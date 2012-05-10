using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace NancyBlog.Domain
{
    public interface IRepository
    {
        IQueryable<T> Query<T>(Expression<Func<T, bool>> predicate) where T: Entity, new();
        T Find<T>(Guid id) where T : Entity, new();
        T First<T>(Expression<Func<T, bool>> predicate) where T : Entity, new();

        IUnitOfWork Start();        
    }
}
