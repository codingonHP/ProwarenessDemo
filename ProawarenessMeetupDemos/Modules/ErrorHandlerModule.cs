using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Diagnostics;

namespace ProawarenessMeetupDemos.Modules
{
    public class ErrorHandlerModule : HubPipelineModule
    {
        protected override void OnIncomingError(ExceptionContext exceptionContext, IHubIncomingInvokerContext invokerContext)
        {
            Debug.WriteLine(exceptionContext.Error.StackTrace);
            base.OnIncomingError(exceptionContext, invokerContext);
        }
    }
}