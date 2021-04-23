using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Concrete.EntityFramework.Abstract;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICustomerDal:IEntityRepository<Customer>
    {
    }
}
