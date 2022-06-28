
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.Aspect.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;

        public PerformanceAspect()
        {
            _interval = 3;
        }

        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        public override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        public override void OnAfter(IInvocation invocation)
        {
            double totalSecond = _stopwatch.Elapsed.TotalSeconds;
            if (totalSecond > _interval)
            {
                //Mail Kodları
                //Database Kodları

                Debug.WriteLine($"Performans Raporu:{invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}==>{totalSecond}");
            }

            _stopwatch.Reset();
        }
    }
}
