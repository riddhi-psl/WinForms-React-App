using Microsoft.Web.WebView2.Core;
using System;

namespace Sentry.Protocol
{
    internal class CoreWebView2
    {
        public static Action<object, CoreWebView2WebMessageReceivedEventArgs> WebMessageReceived { get; internal set; }
    }
}