using AutoMapper;
using ConvenienceStore.API.Data;
using ConvenienceStore.API.DTOs;
using ConvenienceStore.API.Models;
using ConvenienceStore.API.Repository.Interface;
using ConvenienceStore.API.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConvenienceStore.API.Repository.Service
{
    public class ProductRespository : IProductRepository
    {
        private readonly ILogger<ProductRespository> _logger;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ProductRespository(ILogger<ProductRespository> logger, IMapper mapper, AppDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IResult> GetProductsAsync()
        {
            List<Product> list = await _context.Products.ToListAsync();
            if (list.Count() == 0)
                return Results.NotFound();

            List<ProductResponseDTO> result = _mapper.Map<List<Product>,List<ProductResponseDTO>>(list);

            return Results.Ok(result);
        }

        public async Task<IResult> GetProductAsync(int productId)
        {
            var record = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (record == null)
                return Results.NotFound($"Product with Id : {productId} not found");

            var result = _mapper.Map<ProductResponseDTO>(record);
            return Results.Ok(result);
        }

        public async Task<IResult> AddProductAsync(AddNewProductDTO product)
        {
            var validator = new AddProductValidator();
            var validationResult = await validator.ValidateAsync(product);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            Product record = _mapper.Map<Product>(product);

            await _context.AddAsync(record);
            await _context.SaveChangesAsync();
            return Results.Ok(_mapper.Map<ProductResponseDTO>(record));
        }

        public async Task<IResult> UpdateProductAsync(UpdateProductDTO product)
        {
            var validator = new UpdateProductValidator();
            var validationResult = await validator.ValidateAsync(product);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            Product? entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            if (entity == null)
                return Results.NotFound($"Product with Id : {product.Id} not found");

            _mapper.Map<UpdateProductDTO, Product>(product, entity);

            await _context.SaveChangesAsync();
            return Results.NoContent();
        }

        public async Task<IResult> DeleteProductAsync(int productId)
        {
            Product? entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (entity == null)
                return Results.NotFound($"Product with Id : {productId} not found");

            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
            return Results.NoContent();
        }
    }
}
