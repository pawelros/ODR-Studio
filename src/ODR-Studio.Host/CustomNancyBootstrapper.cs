using System;
using Nancy;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;
using log4net;

namespace ODRStudio.Host
{
    public class CustomNancyBootstrapper: DefaultNancyBootstrapper
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            container.Register<ILog>(log);
        }
    }
}

