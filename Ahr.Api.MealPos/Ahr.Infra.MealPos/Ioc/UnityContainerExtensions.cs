using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;

namespace Ahr
{
    public static class UnityContainerExtensions
    {
        public static void RegisterInheritedTypes(this Unity.IUnityContainer container, System.Reflection.Assembly assembly, System.Type baseType)
        {
            var allTypes = assembly.GetTypes();
            var baseInterfaces = baseType.GetInterfaces();

            foreach (var type in allTypes)
            {
                var test = type.BaseType;
                if (type.BaseType != null && type.BaseType.GenericEq(baseType))
                {
                    var typeInterface = type.GetInterfaces().FirstOrDefault(x => !baseInterfaces.Any(bi => bi.GenericEq(x)));
                    if (typeInterface == null)
                    {
                        continue;
                    }
                    container.RegisterType(typeInterface, type);
                    //container.RegisterSingleton(typeInterface, type);
                }
            }

            var ok = assembly.GetTypes();
        }
    }
}
