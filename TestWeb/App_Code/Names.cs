

namespace TestWeb
{
    public static class Names
    {
        public static class Views
        {
            public static string DEFAULT = "Default.aspx";
            public static string PAGE_1 = "Page_1.aspx";
            public static string PAGE_2 = "Page_2.aspx";
        }

        public static class Contexts
        {
            private static string CONTEXT = ".Context";
            public static string DEFAULT = string.Concat(Views.DEFAULT, Contexts.CONTEXT);
            public static string PAGE_1 = string.Concat(Views.PAGE_1, Contexts.CONTEXT);
            public static string PAGE_2 = string.Concat(Views.PAGE_2, Contexts.CONTEXT);
        }

        public static class Flows
        {
            private static string FLOW = ".Flow";
            public static string DEFAULT = string.Concat(Views.DEFAULT, Flows.FLOW);
            public static string PAGE_1 = string.Concat(Views.PAGE_1, Flows.FLOW);
            public static string PAGE_2 = string.Concat(Views.PAGE_2, Flows.FLOW);
        }
    }
}