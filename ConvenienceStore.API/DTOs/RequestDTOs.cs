namespace ConvenienceStore.API.DTOs
{
    public record ProductsListDTO(int Id, string UPC, string Description, decimal Price, int Quantity);

    public record AddNewProductDTO(string UPC, string Description, decimal Price, int Quantity);

    public record UpdateProductDTO(int Id, string UPC, string Description, decimal Price, int Quantity);

    public record ProductResponseDTO(int Id, string UPC, string Description, decimal Price, int Quantity);
}