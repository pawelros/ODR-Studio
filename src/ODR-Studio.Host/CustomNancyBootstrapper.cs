using System;
using log4net;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using ODR_Studio.WebApi.Client;

namespace ODRStudio.Host
{
    public class CustomNancyBootstrapper: DefaultNancyBootstrapper
    {
        private readonly string hostUrl;
        private ILog log;

        public CustomNancyBootstrapper(string hostUrl, ILog log)
        {
            this.hostUrl = hostUrl;
            this.log = log;
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            container.Register(log);
           container.Register(new WebApiClient(this.hostUrl));
        }
    }
}

