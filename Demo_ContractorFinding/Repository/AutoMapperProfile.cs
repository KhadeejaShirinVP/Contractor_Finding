using AutoMapper;
using Domain;
using Domain.Models;

namespace Repository
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Userview, UserDisplayV2>();
        }
    }
}