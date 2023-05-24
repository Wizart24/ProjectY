using AutoMapper;
using ProjectY.Dtos.Picture;
using ProjectY.Models;

namespace ProjectY
{
	public class AutoMapperProfile : Profile
	{
        public AutoMapperProfile()
        {
            CreateMap<Picture, GetPictureDto>();
            CreateMap<AddPictureDto, Picture>();
        }
    }
}
