using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using cMenu.Metadata.Attributes;

namespace cMenu.Metadata
{
    public static class CMetadataHelperExt
    {
        #region REFLECTION
        public static object GetPropertyValue(this object Object, string PropertyName)
        {
            return CMetadataHelper.sGetPropertyValue(Object.GetType(), PropertyName, Object);
        }
        public static object GetPropertyValue(this Type T, string PropertyName, object Object = null)
        {
            return CMetadataHelper.sGetPropertyValue(T, PropertyName, Object);
        }
        public static int SetPropertyValue(this object Object, string PropertyName, object Value)
        {
            return CMetadataHelper.sSetPropertyValue(Object.GetType(), PropertyName, Value, Object);
        }
        public static int SetPropertyValue(this Type T, string PropertyName, object Value, object Object = null)
        {
            return CMetadataHelper.sSetPropertyValue(T, PropertyName, Value, Object);
        }
        public static object ExecuteFunction(this object Object, string FunctionName, object[] Parameters = null)
        {
            return CMetadataHelper.sExecuteFunction(Object.GetType(), FunctionName, Object, Parameters);
        }
        public static object ExecuteFunction(this Type T, string FunctionName, object Object = null, object[] Parameters = null)
        {
            return CMetadataHelper.sExecuteFunction(T, FunctionName, Object, Parameters);
        }

        public static object CreateInstance(this Type T, object[] Parameters = null)
        {
            return Activator.CreateInstance(T, Parameters);
        }
        #endregion

        #region METADATA
        public static bool IsBrowseableByMetadataManager(this Type T)
        {
            return CMetadataHelper.sIsTypeBrowseableByMetadataManager(T);
        }
        public static CMetaClassAttribute GetMetadata(this Type T)
        {
            return CMetadataHelper.sGetTypeMetadata(T);
        }
        public static CMetaPropertyBindingAttribute GetMetadata(this PropertyInfo Property)
        {
            return CMetadataHelper.sGetPropertyMetadata(Property);
        }
        #endregion
    }

    public class CMetadataHelper
    {
        #region STATIC FUNCTIONS
        public static bool sIsTypeBrowseableByMetadataManager(Type T)
        {
            var Attributes = T.GetCustomAttributes(true);
            foreach (object Attribute in Attributes)
                if (Attribute is CMetaClassAttribute)
                    return (Attribute as CMetaClassAttribute).VisibleForMetadataManager;
            return false;
        }
        public static CMetaClassAttribute sGetTypeMetadata(Type T)
        {
            var Attributes = T.GetCustomAttributes(true);
            foreach (object Attribute in Attributes)
                if (Attribute is CMetaClassAttribute)
                    return (CMetaClassAttribute)Attribute;

            return null;
        }
        public static CMetaFunctionAttribute sGetFunctionMetadata(Type T)
        {
            var Attributes = T.GetCustomAttributes(true);
            foreach (object Attribute in Attributes)
                if (Attribute is CMetaFunctionAttribute)
                    return (CMetaFunctionAttribute)Attribute;
            return null;
        }
        public static CMetaPropertyBindingAttribute sGetPropertyMetadata(PropertyInfo Property)
        {
            var Attributes = Property.GetCustomAttributes(true);
            foreach (object Attribute in Attributes)
                if (Attribute is CMetaPropertyBindingAttribute)
                    return (CMetaPropertyBindingAttribute)Attribute;

            return null;
        }

        public static object sExecuteFunction(Type T, string FunctionName, object Object = null, object[] Parameters = null)
        {
            object R = null;
            var Function = T.GetMethod(FunctionName);
            if (Function.Equals(null))
                return null;
            R = Function.Invoke(Object, Parameters);
            return R;
        }
        public static object sGetPropertyValue(Type T, string PropertyName, object Object = null)
        {
            var Property = T.GetProperty(PropertyName);
            if (Property.Equals(null))
                return null;

            return Property.GetValue(Object, null);
        }
        public static int sSetPropertyValue(Type T, string PropertyName, object Value, object Object = null)
        {
            var Property = T.GetProperty(PropertyName);
            if (Property.Equals(null))
                return -2;

            Property.SetValue(Object, Value, null);
            return -1;
        }

        public static Type sGetTypeFromAssembly(Assembly Assembly, string TypeName)
        {
            var Type = from T in Assembly.GetTypes()
                       where T.Name == TypeName
                       select T;
            return Type.ToList()[0];
        }
        public static Type sGetTypeFromAssembly(Assembly Assembly, string TypeName, Type ImplementedInterface)
        {
            var Type = from T in Assembly.GetTypes()
                       where T.GetInterfaces().Contains(ImplementedInterface) && T.Name == TypeName
                       select T;
            return Type.ToList()[0];
        }
        public static List<Type> sGetTypesFromAssembly(Assembly Assembly, Type ImplementedInterface)
        {
            var Type = from T in Assembly.GetTypes()
                       where T.GetInterfaces().Contains(ImplementedInterface)
                       select T;
            return Type.ToList();
        }
        #endregion
    }
}
