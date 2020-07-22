using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TransactionUploader.Common.Extensions
{
    public static class AppDomainExtensions
    {
        public static IEnumerable<Type> GetImplementationsTypes(this AppDomain domain, string @namespace, string typePrefix)
        {
            var types = domain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => !string.IsNullOrWhiteSpace(type.Namespace) &&
                               type.Namespace.StartsWith(@namespace))
                .Where(type => !type.GetTypeInfo().IsAbstract)
                .Where(type => type.Name.EndsWith(typePrefix));

            return types;
        }
    }
}
