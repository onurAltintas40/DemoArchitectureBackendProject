﻿

using Core.Utilities.Result.Abstract;
using Entities.Concrete;

namespace Business.Repositories.EmailParameterRespository
{
    public interface IEmailParameterService
    {
        Task<IResult> Add(EmailParameter emailParameter);
        Task<IResult> Update(EmailParameter emailParameter);
        Task<IResult> Delete(EmailParameter emailParameter);
        Task<IDataResult<List<EmailParameter>>> GetList();
        Task<IDataResult<EmailParameter>> GetById(int id);
        Task<IResult> SendEmail(EmailParameter emailParameter,string body, string subject, string emails);
    }
}
