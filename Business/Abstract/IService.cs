using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IService<T>
    {
        IDataResult<List<T>> GetById(int id);
        IDataResult<List<T>> GetAll();
        IResult Add(T entity);
        IResult Update(T entity);
        IResult Delete(T entity);
    }
}
