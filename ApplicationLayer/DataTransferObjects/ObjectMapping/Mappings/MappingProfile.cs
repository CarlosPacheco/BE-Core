using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data.TransferObjects.ObjectMapping.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(typeof(MappingProfile).Assembly);
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            IEnumerable<Type> types = assembly.GetExportedTypes().Where(t => !t.IsGenericType && t.GetInterface(nameof(IMapFrom)) != null);

            foreach (Type type in types)
            {
                type.GetMethod(nameof(IMapFrom.Mapping))?.Invoke(Activator.CreateInstance(type), new object[] { this });
            }
        }
    }
}