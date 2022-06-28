using Core.Utilities.Result.Abstract;
using Entities.Concrete;

namespace Business.Repositories.UserOperationClaimRepository
{
    public interface IUserOperationClaimService
    {
        public Task<IResult> Add(UserOperationClaim userOperationClaim);
        public Task<IResult> Update(UserOperationClaim userOperationClaim);
        public Task<IResult> Delete(UserOperationClaim userOperationClaim);
        public Task<IDataResult<List<UserOperationClaim>>> GetList();
        public Task<IDataResult<UserOperationClaim>> GetById(int id);

    }
}
