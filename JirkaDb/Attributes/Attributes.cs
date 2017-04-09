using System;
using JetBrains.Annotations;

namespace JirkaDb.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    [MeansImplicitUse]
    public class ForUiAttribute : Attribute
    {

    }
}