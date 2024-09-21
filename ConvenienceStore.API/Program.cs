using ConvenienceStore.API.Data;
using ConvenienceStore.API.DTOs;
using ConvenienceStore.API.Repository.Interface;
using ConvenienceStore.API.Repository.Service;
using ConvenienceStore.API.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace ConvenienceStore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnectionString")));

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.AddScoped<IProductRepository, ProductRespository>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Convenience Store API",
                    Description = "An ASP.NET Core web API for managing Products from a store",
                    Contact = new OpenApiContact
                    {
                        Name = "Pablo Jesus Contreras Orozco",
                        Url = new Uri("https://github.com/pablocontreras89")
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Apply migrations to the database automatically when program runs.
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/", async (IProductRepository repository) => await repository.GetProductsAsync())
                .Produces<List<ProductResponseDTO>>(StatusCodes.Status200OK)
                .WithName("GetProducts")
                .WithTags("Product")
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Returns a list of products",
                    Description = "Returns a detail list of products.",
                    OperationId = "GetProducts"
                });

            app.MapGet("/{id:int:required}", async (int id, IProductRepository repository) => await repository.GetProductAsync(id))
                .Produces<ProductResponseDTO>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("GetProduct")
                .WithTags("Product")
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Returns a single products",
                    Description = "Returns a product by Id.",
                    OperationId = "GetProduct"
                });

            app.MapPost("/", async ([FromBody] AddNewProductDTO request, IProductRepository repository) => await repository.AddProductAsync(request))
                .Produces<ProductResponseDTO>(StatusCodes.Status200OK)
                .ProducesValidationProblem()
                .WithName("AddProduct")
                .WithTags("Product")
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Add a product",
                    Description = "Add a product to the database.",
                    OperationId = "AddProduct"
                });

            app.MapPut("/", async ([FromBody] UpdateProductDTO request, IProductRepository repository) => await repository.UpdateProductAsync(request))
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesValidationProblem()
                .WithName("UpdateProduct")
                .WithTags("Product")
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Update a product",
                    Description = "Update a product record in the database.",
                    OperationId = "UpdateProduct"
                });

            app.MapDelete("/{id:int:required}", async (int id, IProductRepository repository) => await repository.DeleteProductAsync(id))
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("DeleteProduct")
                .WithTags("Product")
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Delete a product",
                    Description = "Delete a product record in the database permanently.",
                    OperationId = "DeleteProduct"
                });

            app.Run();
        }
    }
}
