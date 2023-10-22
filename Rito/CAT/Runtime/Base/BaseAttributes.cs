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

    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public abstract class IfAttributeBase : PropertyAttribute
    {
        public string TargetField { get; set; }
        public object Value { get; set; }

        public IfAttributeBase(string targetField, object value)
        {
            TargetField = targetField;
            Value = value;
        }
    }
}