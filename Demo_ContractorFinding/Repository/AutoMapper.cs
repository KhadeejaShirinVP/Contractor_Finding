using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AutoMapper<Tsource, TDestination>
    {
        private static Mapper _mapper = new Mapper(new MapperConfiguration(
            cfg => cfg.CreateMap<Tsource, TDestination>()));

        public static List<TDestination> Maplist(List<Tsource> source)
        {

            var result = source.Select(a => _mapper.Map<TDestination>(a)).ToList();
            return result;
        }

        //public static TDestination Map (Tsource source)
        //{
        //    return _mapper.Map<TDestination>(source);
        //}

        //public static List<TDestination>Maplist(List<Tsource> source)
        //{
        //    var result= new List<TDestination>();
        //    source.ForEach(a => { result.Add(Map(a)); });
        //    return result;
        //}
    }
}
