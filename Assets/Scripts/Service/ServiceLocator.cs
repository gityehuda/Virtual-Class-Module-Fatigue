using System;
using System.Collections.Generic;

namespace Binus.WebGL.Service
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static void RegisterService<T>(T service)
        {
            var type = typeof(T);
            if (services.ContainsKey(type))
            {
                services[type] = service;
            }
            else
            {
                services.Add(type, service);
            }
        }

        public static T GetService<T>()
        {
            var type = typeof(T);
            if (services.TryGetValue(type, out var service))
            {
                return (T)service;
            }
            throw new Exception($"Service {type} not found. Did you forget to register it?");
        }
    }
}
