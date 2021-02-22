using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.TransferObjects.ObjectMapping.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            IEnumerable<Type> types = typeof(MappingProfile).Assembly.GetExportedTypes().Where(t => !t.IsGenericType && t.GetInterface(nameof(IMapFrom)) != null);

            foreach (Type type in types)
            {
                type.GetMethod(nameof(IMapFrom.Mapping))?.Invoke(Activator.CreateInstance(type), new object[] { this });
            }
        }
    }
}