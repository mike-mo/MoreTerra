namespace WorldView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Reflection;

    public static class Util
    {
        public static Type GetResourceAssemblyType()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type t in assembly.GetTypes())
                {
                    if (t.Name == "Resources")
                    {
                        return t;
                    }
                }
            }
            throw new ApplicationException("Cannot find the Resource Assembly");
        }
    }
}
