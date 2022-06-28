

using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Repositories.UserRepository
{
    public class EfUserDal : EfEntityRepositoryBase<User, SimpleContextDb>, IUserDal
    {
        public List<OperationClaim> GetUserOperationClaims(int userId)
        {
            using (var context = new SimpleContextDb())
            {
                var result = from userOperationClaim in context.UserOperationClaims.Where(p => p.UserId == userId)
                             join operatonClaim in context.OperationClaims on userOperationClaim.OperationClaimId equals operatonClaim.Id
                             select new OperationClaim
                             {
                                 Id = operatonClaim.Id,
                                 Name = operatonClaim.Name
                             };
                return result.OrderBy(p=>p.Name).ToList();
            }
        }
    }
}
