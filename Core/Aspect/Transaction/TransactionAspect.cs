using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System.Transactions;

namespace Core.Aspect.Transaction
{
    public class TransactionAspect:MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transactionScope.Complete();

                }
                catch (Exception)
                {
                    transactionScope.Dispose();
                    throw;
                }
            }            
        }
    }
}
