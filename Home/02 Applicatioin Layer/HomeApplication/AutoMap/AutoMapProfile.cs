using AutoMapper;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.AutoMap
{
   public  static class AutoMapProfile
    {
        static IMapper mapper;
        public static void Reg()
        {
            if (mapper != null) return;
            var config = new MapperConfiguration(cfg =>
            {

                cfg.AddProfile<ToDtoProfile>();
                cfg.AddProfile<ToEntityProfile>();
            });
            mapper = config.CreateMapper();
        }

        public static T AsEntity<T>(this Dto dto)
        {
            return mapper.Map<T>(dto);
        }

        public static T AsDto<T>(this Entity entity)
        {
            return mapper.Map<T>(entity);
        }

        public static IList<T> AsList<T>(this IEnumerable<Dto> dtos)
        {
            return mapper.Map<List<T>>(dtos);
        }

        public static IList<T> AsList<T>(this IEnumerable<Entity> entities)
        {
            return mapper.Map<List<T>>(entities);
        }
    }
}
