using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Concrete.EntityFramework.Abstract;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
    }
}