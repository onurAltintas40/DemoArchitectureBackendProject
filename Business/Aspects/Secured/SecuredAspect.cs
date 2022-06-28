using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Aspects.Secured
{
    public class SecuredAspect:MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredAspect()
        {            
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        public SecuredAspect(string roles)
        {
            _roles = roles.Split(",");
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public override void OnBefore(IInvocation invocation)
        {
            if (_roles!=null)
            {
                var roleClaims = _httpContextAccessor.HttpContext.User.ClaimsRoles();
                foreach (var role in roleClaims)
                {
                    if (roleClaims.Contains(role))
                    {
                        return;
                    }
                }

                throw new Exception("Bu işlem için yetkiniz bulunmamaktadır.");
            }
            else
            {
                var claims = _httpContextAccessor.HttpContext.User.Claims;
                if (claims.Count()>0)
                {
                    return;
                }

                throw new Exception("Bu işlem için yetkiniz bulunmamaktadır.");
            }            
        }
    }
}
