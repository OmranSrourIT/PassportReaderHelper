using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace PassportReaderHelper
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {

                string ServiceURLPath = ConfigurationManager.AppSettings["UrlSelfHosting"];

                if (ServiceURLPath != "" || ServiceURLPath != null)
                {
                    var config = new HttpSelfHostConfiguration(ServiceURLPath);
                    config.EnableCors();
                    config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

                    config.MaxReceivedMessageSize = 2147483647; // use config for this value


                    config.Routes.MapHttpRoute(
                        name: "API",
                        routeTemplate: "api/{controller}/{action}/{id}",
                        defaults: new { controller = "AFISHome", id = RouteParameter.Optional }
                        );
                  
                    HttpSelfHostServer server = new HttpSelfHostServer(config);
                    server.OpenAsync().Wait();

                    //WriteToFile("api http://localhost:12334 in done to calling" + DateTime.Now);

                }



            }
            catch (Exception ex)
            {

                var stackTrace = new StackTrace(ex, true);
                var frame = stackTrace.GetFrame(0);
                var line = frame.GetFileLineNumber();

            }
        }

        protected override void OnStop()
        {
        }
    }
}
