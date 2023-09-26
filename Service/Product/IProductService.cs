﻿using Service.User;

namespace Service.Product
{
    public interface IProductService
    {
        IProductRepository _productRepository { get; set; }
        public void AddProduct(Product Product);
        public void DeleteProduct(Product mockProduct);
        public Product GetProductByName(string productName);

        public Product[] GetAllProducts();
        public void UpdateProduct(Product mockProduct);
        public void Reset();
    }
}