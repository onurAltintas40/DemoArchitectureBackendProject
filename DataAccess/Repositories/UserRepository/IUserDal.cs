﻿using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Repositories.UserRepository
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetUserOperationClaims(int userId);
    }
}
