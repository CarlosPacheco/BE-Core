using System;

namespace CrossCutting.Typewriter.Attributes
{
    /// <summary>
    /// Specifies that TypeWriter will not create the public method. This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed  class TypewriterIgnoreAttribute : Attribute
    {
    }
}
