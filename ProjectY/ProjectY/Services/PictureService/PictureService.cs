using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectY.Data;
using ProjectY.Dtos.Picture;
using ProjectY.Models;

namespace ProjectY.Services.PictureService
{
	public class PictureService : IPictureService
	{
		private readonly IMapper _mapper;
		private readonly DataContext _context;

		public PictureService(IMapper mapper, DataContext context)
        {
			_mapper = mapper;
			_context = context;
		}

        public async Task<ServiceResponse<List<GetPictureDto>>> AddPicture(AddPictureDto picture)
		{
			var serviceResponse = new ServiceResponse<List<GetPictureDto>>();
			var newPicture = _mapper.Map<Picture>(picture);

			_context.Pictures.Add(newPicture);
			await _context.SaveChangesAsync();

			serviceResponse.Data = await _context.Pictures.Select(x => _mapper.Map<GetPictureDto>(x)).ToListAsync();

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetPictureDto>>> GetAllPictures()
		{
			var serviceResponse = new ServiceResponse<List<GetPictureDto>>();

			var dbPictures = await _context.Pictures.ToListAsync();

			serviceResponse.Data = dbPictures.Select(x => _mapper.Map<GetPictureDto>(x)).ToList();
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetPictureDto>> GetPictureById(int id)
		{
			var serviceResponse = new ServiceResponse<GetPictureDto>();

			var picture = await _context.Pictures.FirstOrDefaultAsync(x => x.Id == id);
			serviceResponse.Data = _mapper.Map<GetPictureDto>(picture);
			return serviceResponse;
		}

		public async Task<ServiceResponse<GetPictureDto>> UpdatePicture(UpdatePictureDto picture)
		{
			var serviceResponse = new ServiceResponse<GetPictureDto>();

			try
			{
				var updatePicture = await _context.Pictures.FirstOrDefaultAsync(x => x.Id == picture.Id);
				if (picture == null)
				{
					throw new Exception($"Picture with ID '{picture.Id}' not found.");
				}

				updatePicture.Name = picture.Name;
				updatePicture.Description = picture.Description;
				updatePicture.Price = picture.Price;
				updatePicture.Quantity = picture.Quantity;
				//updatePicture.UploadTime = picture.UploadTime;

				await _context.SaveChangesAsync();
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
				var deletePicture = await _context.Pictures.FirstOrDefaultAsync(x => x.Id == id);
				if (deletePicture == null)
				{
					throw new Exception($"Picture with ID '{id}' not found.");
				}

				_context.Pictures.Remove(deletePicture);
				await _context.SaveChangesAsync();

				serviceResponse.Data = await _context.Pictures.Select(x => _mapper.Map<GetPictureDto>(x)).ToListAsync();
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
