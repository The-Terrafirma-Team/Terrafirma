using System;

namespace Terrafirma.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class ResetDefaultsAttribute : Attribute
    {
        public object Default { get;}
        public ResetDefaultsAttribute(object reset)
        {
            Default = reset;
        }
    }
}
