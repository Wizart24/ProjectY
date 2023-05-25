using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjectY.Data;
using ProjectY.Services.PictureService;
using Swashbuckle.AspNetCore.Filters;

namespace ProjectY
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(x =>
			{
				x.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme{
					Description = """Standart Authorization header using the Bearer scheme. Example "bearer {token}" """,
					In = ParameterLocation.Header,
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
			});

				x.OperationFilter<SecurityRequirementsOperationFilter>();
			});

			builder.Services.AddAutoMapper(typeof(Program).Assembly);

			builder.Services.AddScoped<IPictureService, PictureService>();
			builder.Services.AddScoped<IAuthRepository, AuthRepository>();

			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
			{
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
					.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}