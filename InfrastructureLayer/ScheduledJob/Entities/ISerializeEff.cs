using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduledJob.Entities
{
    public interface ISerializeEff<T>
    {
        void Serialize(T entity);

        T Deserialize(EffFile effFile);
       
    }
}
