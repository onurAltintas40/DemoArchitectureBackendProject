using Business.Aspects.Secured;
using Business.Repositories.OperationClaimRepository.Constans;
using Core.Aspect.Performance;
using Core.Utilities.Business;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.OperationClaimRepository;
using Entities.Concrete;

namespace Business.Repositories.OperationClaimRepository
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;
        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        public async Task<IResult> Add(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(await IsNameAvaibleForAdd(operationClaim.Name));
            if (result != null)
            {
                return result;
            }

            await _operationClaimDal.Add(operationClaim);
            return new SuccessResult(OperationClaimMessages.Added);
        }
        public async Task<IResult> Update(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(await IsNameAvaibleForUpdate(operationClaim));
            if (result != null)
            {
                return result;
            }

            await _operationClaimDal.Update(operationClaim);
            return new SuccessResult(OperationClaimMessages.Updated);
        }

        public async Task<IResult> Delete(OperationClaim operationClaim)
        {
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(OperationClaimMessages.Deleted);
        }

        public async Task<IDataResult<OperationClaim>> GetById(int id)
        {
            return new SuccessDataResult<OperationClaim>(await _operationClaimDal.Get(p=>p.Id==id));
        }

        [SecuredAspect("Admin")]
        [PerformanceAspect(5)]
        public async Task<IDataResult<List<OperationClaim>>> GetList()
        {
            
            return new SuccessDataResult<List<OperationClaim>>(await _operationClaimDal.GetAll());
        }        

        private async Task<IResult> IsNameAvaibleForAdd(string name)
        {
            var result = await _operationClaimDal.Get(p => p.Name == name);
            if (result != null)
            {
                return new ErrorResult(OperationClaimMessages.NameIsAvaible);
            }
            return new SuccessResult();
        }

        private async Task<IResult> IsNameAvaibleForUpdate(OperationClaim operationClaim)
        {
            var currentOperationClaim = await _operationClaimDal.Get(p => p.Id == operationClaim.Id);
            if (currentOperationClaim.Name != operationClaim.Name)
            {
                var result = _operationClaimDal.Get(p => p.Name == operationClaim.Name);
                if (result != null)
                {
                    return new ErrorResult(OperationClaimMessages.NameIsAvaible);
                }
            }
            
            return new SuccessResult();
        }
    }
}
