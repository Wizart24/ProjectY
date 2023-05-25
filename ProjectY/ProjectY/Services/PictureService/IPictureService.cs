using ProjectY.Dtos.Picture;
using ProjectY.Models;

namespace ProjectY.Services.PictureService
{
	public interface IPictureService
	{
		Task<ServiceResponse<List<GetPictureDto>>> GetAllPictures(int userId);
		Task<ServiceResponse<GetPictureDto>> GetPictureById(int id);
		Task<ServiceResponse<List<GetPictureDto>>> AddPicture(AddPictureDto picture);
		Task<ServiceResponse<GetPictureDto>> UpdatePicture(UpdatePictureDto picture);
		Task<ServiceResponse<List<GetPictureDto>>> DeletePicture(int id);
	}
}
