using System;
using System.Collections.Generic;

using System.Configuration;
using System.Web;
using System.Web.UI;


namespace FW.HttpFlow
{
    /// <summary>
    /// Provides functionality to navigate in the user pathways.
    /// Includes context store/restore
    /// Includes flow passing parameters
    /// </summary>
    /// <see cref="FW.HttpFlow.IContextCare"/>
    public sealed class HttpFlowSystem : IHttpModule
    {
        #region Miembros de IHttpModule

        /// <summary>
        /// Initializes the module, preparing it to control requests and care about context and flow. 
        /// </summary>
        /// <param name="context">
        /// System.Web.HttpApplication object to access methods, properties and common event to the existing ASP.NET application objects.
        /// </param>
        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(Application_PreRequestHandlerExecute);
        }

        /// <summary>
        /// Delete resources (different from memory) used by the module implementing System.Web.IHttpModule.
        /// </summary>
        public void Dispose()
        {
            // Nothing at the moment
        }

        #endregion

        /// <summary>
        /// Occurs just before ASP.NET starts executing an event handler (for example, a page or an XML Web service).
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event args</param>
        private void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            if (HttpContext.Current.Handler is Page)
            {
                Page page = (HttpContext.Current.Handler as Page);
                page.PreLoad += new EventHandler(Page_PreLoad);
            }
            else
            {
                // No tracking of component that are not 'Page'
            }
        }

        /// <summary>
        /// Handles the PreLoad event of the Page control.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event args</param>
        private void Page_PreLoad(object sender, EventArgs e)
        {
            if (null != HttpFlowTracker.Current)
            {
                HttpFlowTracker.Current.Page_PreLoad(sender, e);
                Page page = (sender as Page);
                page.PreLoad -= new EventHandler(Page_PreLoad);
            }
            else
            {
                // No HttpFlowTracker.CurrentFlow because of no session
            }
        }

        /// <summary>
        /// The help appsetting key for the configuration file
        /// </summary>
        private const string HTTPFLOW_APPSETTING_CONFIGURATION = "FW.HttpFlow.HttpFlowSystem.StartPage";

        private static string _startPagePath = string.Empty;
        /// <summary>
        /// Gets the start page path.
        /// </summary>
        /// <value>El Start page path.</value>
        private static string StartPagePath
        {
            get
            {
                if (string.IsNullOrEmpty(_startPagePath))
                {
                    if (null != ConfigurationManager.AppSettings.Get(HttpFlowSystem.HTTPFLOW_APPSETTING_CONFIGURATION))
                        _startPagePath = ConfigurationManager.AppSettings.Get(HttpFlowSystem.HTTPFLOW_APPSETTING_CONFIGURATION);
                    else
                        throw new ConfigurationErrorsException("The HttpFlowSystem is not configured.");
                }
                return _startPagePath;
            }
        }

        /// <summary>
        /// Redirects the client forward, to a the given URL.
        /// </summary>
        /// <param name="url">The URL address to move forward.</param>
        /// <exception cref="System.Web.HttpException">Redirection is tried when HTTP headers were already sent.
        /// </exception>
        public static void Forward(string url)
        {
            HttpFlowSystem.Forward(url, new HttpFlowItemCollection());
        }

        /// <summary>
        /// Redirects the client forward, to a the given URL.
        /// </summary>
        /// <param name="url">The URL address to move forward.</param>
        /// <param name="endResponse">Indicates whether the execution of the current page must finish or not.</param>
        /// <exception cref="System.Web.HttpException">Redirection is tried when HTTP headers were already sent.
        /// </exception>
        public static void Forward(
            string url,
            bool endResponse)
        {
            HttpFlowSystem.Forward(url, new HttpFlowItemCollection(), endResponse);
        }

        /// <summary>
        /// Redirects the client forward, to a the given URL.
        /// </summary>
        /// <param name="url">The URL address to move forward.</param>
        /// <param name="flow">The flow information to pass to the next page.</param>
        /// <exception cref="System.Web.HttpException">Redirection is tried when HTTP headers were already sent.
        /// </exception>
        public static void Forward(string url, HttpFlowItemCollection flow)
        {
            HttpFlowTracker.Current.Forward(url, flow);
        }

        /// <summary>
        /// Redirects the client forward, to a the given URL.
        /// </summary>
        /// <param name="url">The URL address to move forward.</param>
        /// <param name="flow">The flow information to pass to the next page.</param>
        /// <param name="endResponse">Indicates whether the execution of the current page must finish or not.</param>
        /// <exception cref="System.Web.HttpException">Redirection is tried when HTTP headers were already sent.
        /// </exception>
        public static void Forward(
            string url,
            HttpFlowItemCollection flow,
            bool endResponse)
        {
            HttpFlowTracker.Current.Forward(
                url,
                flow,
                endResponse);
        }

        /// <summary>
        /// Redirects the client backward to the last stored URL of the user navigation tracking.
        /// </summary>
        /// <exception cref="System.Web.HttpException">Redirection is tried when HTTP headers were already sent.
        /// </exception>
        public static void Backward()
        {
            HttpFlowSystem.Backward(new HttpFlowItemCollection());
        }

        /// <summary>
        /// Redirects the client backward to the last stored URL of the user navigation tracking.
        /// </summary>
        /// <param name="flow">The flow information to pass to the page we are returning to.</param>
        /// <exception cref="System.Web.HttpException">Redirection is tried when HTTP headers were already sent.
        /// </exception>
        public static void Backward(HttpFlowItemCollection flow)
        {
            HttpFlowTracker.Current.Backward(flow);
        }
                
        private static object _syncClass = new object();
        /// <summary>
        /// Gets the class level synchronization object.
        /// </summary>
        /// <value>The class level synchronization object.</value>
        public static object SyncClass
        {
            get { return _syncClass; }
        }

        private object _syncRoot = new object();
        /// <summary>
        /// Gets the object level synchronization object.
        /// </summary>
        /// <value>The object level synchronization object.</value>
        public object SyncRoot
        {
            get { return _syncRoot; }
        }

        #region Inner classes

        /// <summary>
        /// Class to navigate on application pages. Provides functionality to go Forward, Backward and 
        /// integrates navigation with <see cref="FW.HttpFlow.IContextCare"/> pages.
        /// </summary>
        /// <seealso cref="FW.HttpFlow.IContextCare"/>
        internal sealed class HttpFlowTracker
        {
            /// <summary>
            /// The flow direction 
            /// </summary>
            private enum eFlowDirection : byte
            {
                /// <summary>
                /// Going back
                /// </summary>
                backward = 1,

                /// <summary>
                /// Going forward
                /// </summary>
                forward = 2,

                /// <summary>
                /// Not moving
                /// </summary>
                notMoving = 3
            }

            /// <summary>
            /// Creates an HttpFlowTracker instance
            /// </summary>
            internal HttpFlowTracker()
            {
                this.FlowTrackStack = new Stack<KeyValuePair<string, HttpContextItemCollection>>();
                this.ResetFlowTrack();
            }

            /// <summary>
            /// Destroys the HttpFlowTracker instance
            /// </summary>
            ~HttpFlowTracker()
            {
                this.FlowTrackStack.Clear();
                this.FlowTrackStack = null;
            }

            /// <summary>
            /// Get the current HttpFlow
            /// </summary>
            internal static HttpFlowTracker Current
            {
                get
                {
                    const string HTTP_FLOW_TRACKER = "FW.HttpFlow.HttpFlow";
                    if (null != HttpContext.Current.Session)
                    {
                        if (null == HttpContext.Current.Session[HTTP_FLOW_TRACKER])
                        {
                            lock (SyncClass)
                            {
                                if (null == HttpContext.Current.Session[HTTP_FLOW_TRACKER])
                                {
                                    HttpContext.Current.Session[HTTP_FLOW_TRACKER] = new HttpFlowTracker();
                                }
                            }
                        }
                        return HttpContext.Current.Session[HTTP_FLOW_TRACKER] as HttpFlowTracker;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            /// <summary>
            /// Handles the PreLoad event of the Page control.
            /// </summary>
            /// <param name="sender">The sender of the event</param>
            /// <param name="e">The event args</param>
            internal void Page_PreLoad(object sender, EventArgs e)
            { 
                // Keep tracking only on pages
                if (!(HttpContext.Current.Handler is Page))
                    return;

                // Last load from page doing Forward or Backward
                if ((0 != this.FlowTrackStack.Count) && (HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Equals(this.FlowTrackStack.Peek().Key)))
                    return;

                // Moving from menu or other external link -> start the flow track
                if ((!(HttpContext.Current.Handler as Page).IsPostBack) && (eFlowDirection.notMoving == this.FlowDirection))
                {
                    this.ResetFlowTrack();
                    return;
                }

                lock (this.SyncRoot)
                {
                    //TODO: Verify the flow belongs to what it is requested by the application.
                    switch (this.FlowDirection)
                    {
                        case eFlowDirection.forward:
                            if (HttpContext.Current.Handler is HttpFlow.IFlowCare)
                            {
                                (HttpContext.Current.Handler as HttpFlow.IFlowCare).SetFlowFromPrevious(this.CurrentFlow);
                            }
                            break;

                        case eFlowDirection.backward:
                            if (HttpContext.Current.Handler is HttpFlow.IFlowCare)
                            {
                                (HttpContext.Current.Handler as HttpFlow.IFlowCare).SetFlowFromPrevious(this.CurrentFlow);
                            }
                            if (HttpContext.Current.Handler is HttpFlow.IContextCare)
                            {
                                (HttpContext.Current.Handler as HttpFlow.IContextCare).SetContext(this.CurrentContext);
                            }
                            break;

                        case eFlowDirection.notMoving:
                            break;

                        default:
                            throw new InvalidFlowException(
                                String.Format(
                                    "The flow direction {0} is not recognized",
                                    this.FlowDirection.ToString()));
                    }

                    this.FlowDirection = eFlowDirection.notMoving;
                }
            }

            /// <summary>
            /// Redirects the client forward, to a the given URL.
            /// </summary>
            /// <param name="url">The URL address to move forward.</param>
            /// <param name="flow">The flow information to pass to the next page.</param>
            /// <exception cref="System.Web.HttpException">Redirection is tried when HTTP headers were already sent.
            /// </exception>
            internal void Forward(string url, HttpFlowItemCollection flow)
            {
                this.Forward(
                    url,
                    flow,
                    false);
            }

            /// <summary>
            /// Redirects the client forward, to a the given URL.
            /// </summary>
            /// <param name="url">The URL address to move forward.</param>
            /// <param name="flow">The flow information to pass to the next page.</param>
            /// <param name="endResponse">Indicates whether the execution of the current page must finish or not.</param>
            /// <exception cref="System.Web.HttpException">Redirection is tried when HTTP headers were already sent.
            /// </exception>
            internal void Forward(
                string url,
                HttpFlowItemCollection flow,
                bool endResponse)
            {
                lock (this.SyncRoot)
                {
                    // Get the context
                    this.CurrentContext = (HttpContext.Current.Handler is IContextCare) ?
                        (HttpContext.Current.Handler as IContextCare).GetContext() :
                        new HttpContextItemCollection();

                    // Get the flow
                    this.CurrentFlow = (null != flow) ? 
                        flow : 
                        new HttpFlowItemCollection();

                    this.FlowTrackStack.Push(
                        new KeyValuePair<string, HttpContextItemCollection>(
                                HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath,
                                this.CurrentContext));

                    this.FlowDirection = eFlowDirection.forward;

                    HttpContext.Current.Response.Redirect(
                        url,
                        endResponse);
                }
            }

            /// <summary>
            /// Redirects the client backward to the last stored URL of the user navigation tracking.
            /// </summary>
            /// <param name="flow">The flow information to pass to the page we are returning to.</param>
            /// <exception cref="System.Web.HttpException">Redirection is tried when HTTP headers were already sent.
            /// </exception>
            internal void Backward(HttpFlowItemCollection flow)
            {
                lock (this.SyncRoot)
                {
                    if (0 != this.FlowTrackStack.Count)
                    {
                        KeyValuePair<string, HttpContextItemCollection> back = this.FlowTrackStack.Pop();

                        // Get the context
                        this.CurrentContext = back.Value;

                        // Get the flow
                        this.CurrentFlow = (null != flow) ?
                            flow :
                            new HttpFlowItemCollection();

                        this.FlowDirection = eFlowDirection.backward;

                        HttpContext.Current.Response.Redirect(back.Key);
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect(HttpFlowSystem.StartPagePath);
                    }
                }
            }

            /// <summary>
            /// Resets the flow.
            /// </summary>
            private void ResetFlowTrack()
            {
                this.FlowTrackStack.Clear();
            }

            private Stack<KeyValuePair<string, HttpContextItemCollection>> _flowTrackStack = new Stack<KeyValuePair<string, HttpContextItemCollection>>();
            /// <summary>
            /// Flow track stack to follow up the user pathways
            /// </summary>
            private Stack<KeyValuePair<string, HttpContextItemCollection>> FlowTrackStack
            {
                get { return _flowTrackStack; }
                set { _flowTrackStack = value; }
            }

            private eFlowDirection _flowDirection = eFlowDirection.notMoving;
            /// <summary>
            /// The current flow direction
            /// </summary>
            private eFlowDirection FlowDirection
            {
                get { return _flowDirection; }
                set { _flowDirection = value; }
            }

            private const string CURRENT_CONTEXT = "FW.HttpFlowSystem.CurrentContext";
            /// <summary>
            /// The current context passing back to the previous page, allways backward
            /// </summary>
            private HttpContextItemCollection CurrentContext
            {
                get { return HttpContext.Current.Session[HttpFlowTracker.CURRENT_CONTEXT] as HttpContextItemCollection; }
                set { HttpContext.Current.Session[HttpFlowTracker.CURRENT_CONTEXT] = value; }
            }

            private const string CURRENT_FLOW = "FW.HttpFlowSystem.CurrentFlow";
            /// <summary>
            /// The current flow passing to the next page, either forward or backward
            /// </summary>
            private HttpFlowItemCollection CurrentFlow
            {
                get { return HttpContext.Current.Session[HttpFlowTracker.CURRENT_FLOW] as HttpFlowItemCollection; }
                set { HttpContext.Current.Session[HttpFlowTracker.CURRENT_FLOW] = value; }
            }

            private static object _syncClass = new object();
            /// <summary>
            /// Gets the class level synchronization object.
            /// </summary>
            /// <value>The class level synchronization object.</value>
            public static object SyncClass
            {
                get { return _syncClass; }
            }

            private object _syncRoot = new object();
            /// <summary>
            /// Gets the object level synchronization object.
            /// </summary>
            /// <value>The object level synchronization object.</value>
            public object SyncRoot
            {
                get { return _syncRoot; }
            }

            #endregion 
        }
    }
}
