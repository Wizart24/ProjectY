using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectY.Dtos.Picture;
using ProjectY.Models;
using ProjectY.Services.PictureService;

namespace ProjectY.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PictureController : ControllerBase
	{
		private readonly IPictureService _pictureService;

		public PictureController(IPictureService pictureService)
        {
			_pictureService = pictureService;
		}

        [HttpGet]
		public async Task<ActionResult<ServiceResponse<List<GetPictureDto>>>> GetAll()
		{
			return Ok(await _pictureService.GetAllPictures());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ServiceResponse<GetPictureDto>>> GetSingle(int id)
		{
			return Ok(await _pictureService.GetPictureById(id));
		}

		[HttpPost]
		public async Task<ActionResult<ServiceResponse<List<GetPictureDto>>>> AddPicture(AddPictureDto picture)
		{
			return Ok(await _pictureService.AddPicture(picture));
		}

		[HttpPut]
		public async Task<ActionResult<ServiceResponse<List<GetPictureDto>>>> UpdatePicture(UpdatePictureDto picture)
		{
			var response = await _pictureService.UpdatePicture(picture);
			if (response.Data == null)
				return NotFound(response);
			return Ok(response);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<ServiceResponse<GetPictureDto>>> DeletePicture(int id)
		{
			var response = await _pictureService.DeletePicture(id);
			if (response.Data == null)
				return NotFound(response);
			return Ok(response);
		}
	}
}
