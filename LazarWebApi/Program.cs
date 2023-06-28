using lazarData.Interfaces;
using lazarData.Repositories;
using lazarData.Repositories.Administration;
using Microsoft.EntityFrameworkCore;

namespace LazarWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors();

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null); ;
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connection = builder.Configuration.GetConnectionString("wrk");
            builder.Services.AddDbContext<lazarData.Context.LazarContext>(options => options.UseSqlServer(connection));

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            builder.Services.AddTransient<IContextRepository, ContextRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}