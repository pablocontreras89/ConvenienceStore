using ConvenienceStore.API.DTOs;
using ConvenienceStore.API.Models;

namespace ConvenienceStore.API.Repository.Interface
{
    public interface IProductRepository
    {
        Task<IResult> GetProductsAsync();
        Task<IResult> GetProductAsync(int productId);
        Task<IResult> AddProductAsync(AddNewProductDTO product);
        Task<IResult> UpdateProductAsync(UpdateProductDTO product);
        Task<IResult> DeleteProductAsync(int productId);
    }
}
