﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Unity;

namespace Ahr
{
    public class Ioc
    {
        private static readonly UnityContainer _container;
        static Ioc()
        {
            _container = new UnityContainer();
        }

        public static void RegisterInheritedTypes(Assembly assembly, Type baseType)
        {
            _container.RegisterInheritedTypes(assembly, baseType);
        }

        public static void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _container.RegisterType<TInterface, TImplementation>();
        }

        public static T Get<T>()
        {
            return _container.Resolve<T>();
        }

    }
}
