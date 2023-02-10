using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Repositories;
using SistemaDeTarefas.Repositories.Interfaces;

namespace SistemaDeTarefas
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddEntityFrameworkSqlServer()
				.AddDbContext<SistemaTarefasDBContex>(
					options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
				);
			// basicamente configura injeção de dependencias
			// toda vez que chamarem essa interface vamos saber que a classe que vai resolver e implementar essa interface é a UsuarioRepositorio
			builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositorio>();
			builder.Services.AddScoped<ITarefaRepository, TarefaRepositorio>();
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}