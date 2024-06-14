using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyProject.DTOs;
using MyProject.Entities;

namespace MyProject.Repository
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        Task<Product> CreateProduct(CreateProductRequest request);
        bool DeleteProduct(int id);
    }
}