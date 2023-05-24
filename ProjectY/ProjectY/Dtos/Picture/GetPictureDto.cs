namespace ProjectY.Dtos.Picture
{
	public class GetPictureDto
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		public DateTime UploadTime { get; set; } = DateTime.UtcNow;
	}
}
