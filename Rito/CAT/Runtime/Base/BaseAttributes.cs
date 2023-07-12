using UnityEngine;

namespace Rito.CAT
{
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public abstract class DropDownAttributeBase : PropertyAttribute
    {
    }

    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public abstract class HeaderAttributeBase : PropertyAttribute
    {
    }
}