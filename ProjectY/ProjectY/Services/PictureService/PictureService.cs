using AutoMapper;
using ProjectY.Dtos.Picture;
using ProjectY.Models;

namespace ProjectY.Services.PictureService
{
	public class PictureService : IPictureService
	{
		private static List<Picture> pictures = new List<Picture>
		{
			new Picture(),
			new Picture {Id = 2, Name = "Dog"}
		};
		private readonly IMapper _mapper;

		public PictureService(IMapper mapper)
        {
			_mapper = mapper;
		}

        public async Task<ServiceResponse<List<GetPictureDto>>> AddPicture(AddPictureDto picture)
		{
			var serviceResponse = new ServiceResponse<List<GetPictureDto>>();
			var newPicture = _mapper.Map<Picture>(picture);

			newPicture.Id = pictures.Max(x => x.Id) + 1;
			pictures.Add(newPicture);
			serviceResponse.Data = pictures.Select(x => _mapper.Map<GetPictureDto>(x)).ToList();

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetPictureDto>>> GetAllPictures()
		{
			var serviceResponse = new ServiceResponse<List<GetPictureDto>>();

			serviceResponse.Data = pictures.Select(x => _mapper.Map<GetPictureDto>(x)).ToList();
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetPictureDto>> GetPictureById(int id)
		{
			var serviceResponse = new ServiceResponse<GetPictureDto>();

			var picture = pictures.FirstOrDefault(x => x.Id == id);
			serviceResponse.Data = _mapper.Map<GetPictureDto>(picture);
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetPictureDto>> UpdatePicture(UpdatePictureDto picture)
		{
			var serviceResponse = new ServiceResponse<GetPictureDto>();

			try
			{
				var updatePicture = pictures.FirstOrDefault(x => x.Id == picture.Id);
				if (picture == null)
				{
					throw new Exception($"Picture with ID '{picture.Id}' not found.");
				}

				updatePicture.Name = picture.Name;
				updatePicture.Description = picture.Description;
				updatePicture.Price = picture.Price;
				updatePicture.Quantity = picture.Quantity;
				updatePicture.UploadTime = picture.UploadTime;

				serviceResponse.Data = _mapper.Map<GetPictureDto>(updatePicture);
			}
			catch (Exception ex) 
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetPictureDto>>> DeletePicture(int id)
		{
			var serviceResponse = new ServiceResponse<List<GetPictureDto>>();

			try
			{
				var deletePicture = pictures.First(x => x.Id == id);
				if (deletePicture == null)
				{
					throw new Exception($"Picture with ID '{id}' not found.");
				}

				pictures.Remove(deletePicture);

				serviceResponse.Data = pictures.Select(x => _mapper.Map<GetPictureDto>(x)).ToList();
			}
			catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
			}

			return serviceResponse;
		}
	}
}
