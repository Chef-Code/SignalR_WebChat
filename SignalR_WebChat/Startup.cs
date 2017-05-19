using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartupAttribute(typeof(SignalR_WebChat.Startup))]
namespace SignalR_WebChat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //Documentation: https://docs.microsoft.com/en-us/aspnet/signalr/overview/deployment/tutorial-signalr-self-host

            app.Map("/signalr", map =>
            {
                // Setup the cors middleware to run before SignalR.
                // By default this will allow all origins. You can 
                // configure the set of origins and/or http verbs by
                // providing a cors options with a different policy.
                map.UseCors(CorsOptions.AllowAll);

                var hubConfiguration = new HubConfiguration
                {
                    // You can enable JSONP by uncommenting line below.
                    // JSONP requests are insecure but some older browsers (and some
                    // versions of IE) require JSONP to work cross domain

                    // EnableJSONP = true
                };

                hubConfiguration.EnableDetailedErrors = true;
                //hubConfiguration.EnableJavaScriptProxies = false;

                // Run the SignalR pipeline. We're not using app.MapSignalR()
                // since this branch is already runs under the "/signalr"
                // path.
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}
