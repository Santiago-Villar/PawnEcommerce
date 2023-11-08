using Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.Exception;
using System.Threading.Tasks;
using Service.Filter.ConcreteFilter;
using Service.Filter;
using Service.DTO.Product;

namespace Service.Product
{
    public class ProductService : IProductService
    {
        public IProductRepository _productRepository { get; set; }


        public IColorService _colorService { get; set; }
        public ProductService(IProductRepository repo, IColorService colorService)
        {
            _productRepository = repo;
            _colorService = colorService;
        }

        public Product AddProduct(Product Product)
        {
            if (_productRepository.Exists(Product)) {
                throw new ServiceException("Product " + Product.Name + " already exists.");
            }
            else return _productRepository.AddProduct(Product);
        }

        public void DeleteProduct(int id)
        {
            if (_productRepository.Exists(id))
            {
                _productRepository.DeleteProduct(id);
            }
            else throw new RepositoryException("Product with id:" + id + " does not exist.");
        }

        public Product GetProductByName(string productName)
        {
            if (string.IsNullOrEmpty(productName))
            {
                throw new ServiceException("Product name cannot be null or empty.");
            }

            var product = _productRepository.GetProductByName(productName);
            if (product == null)
            {
                throw new RepositoryException($"Product {productName} does not exist.");
            }

            return product;
        }
        public Product Get(int id)
        {
            var product = _productRepository.Get(id);
            if (product == null)
            {
                throw new RepositoryException($"Product with id:{id} does not exist.");
            }

            return product;
        }
        public Product[] GetAllProducts(FilterQuery filter)
        {
            return _productRepository.GetAllProducts(filter);
        }
        public bool NewNameIsValid(string newName, Product Product)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            _productRepository.Reset();
        }

        public void UpdateProduct(Product newProductVersion)
        {
            if (newProductVersion == null)
            {
                throw new ServiceException($"New version of a product cannot be null");
            }
            if (_productRepository.Exists(newProductVersion.Id))
            {
                _productRepository.UpdateProduct(newProductVersion);
            }
            else { throw new RepositoryException($"Product {newProductVersion.Id} does not exist."); }
        }


        public void DecreaseStock(int productId, int quantity)
        {
            var product = _productRepository.Get(productId);
            if (product == null)
            {
                throw new ServiceException($"Product with id:{productId} does not exist.");
            }

            if (!product.IsStockAvailable(quantity))
            {
                throw new ServiceException($"Not enough stock for product with id:{productId}.");
            }

            product.DecreaseStock(quantity);
            _productRepository.UpdateProduct(product);
        }

        public void IncreaseStock(int productId, int quantity)
        {
            var product = _productRepository.Get(productId);
            if (product == null)
            {
                throw new ServiceException($"Product with id:{productId} does not exist.");
            }

            product.IncreaseStock(quantity);
            _productRepository.UpdateProduct(product);
        }

        public (Product[] UpdatedCart, List<Product> RemovedProducts) VerifyAndUpdateCart(Product[] cartProducts)
        {
            List<Product> updatedCart = new List<Product>();
            List<Product> removedProducts = new List<Product>();

            FilterQuery filter = new FilterQuery
            {
                ProductIds = new IdsFilterCriteria { Ids = cartProducts.Select(p => p.Id).ToList() }
            };

            Product[] latestProducts = GetAllProducts(filter);

            foreach (var cartProduct in cartProducts)
            {
                var latestProduct = latestProducts.FirstOrDefault(p => p.Id == cartProduct.Id);

                if (latestProduct == null)
                {
                    removedProducts.Add(cartProduct);
                    continue;
                }

                var productCountInCart = cartProducts.Count(p => p.Id == cartProduct.Id);

                if (latestProduct.IsStockAvailable(productCountInCart))
                {
                    updatedCart.Add(latestProduct);
                }
                else
                {
                    removedProducts.Add(cartProduct);
                    cartProducts = cartProducts.Where(p => p.Id != cartProduct.Id).ToArray();
                }
            }

            return (updatedCart.ToArray(), removedProducts);
        }

        public string GenerateRemovalNotification(List<Product> removedProducts)
        {
            if (!removedProducts.Any())
                return string.Empty;

            var productNames = string.Join(", ", removedProducts.Select(p => p.Name));
            return $"The following products were removed from your cart due to insufficient stock or being no longer available: {productNames}.";
        }
     




        public Product UpdateProductUsingDTO(int id, ProductUpdateModel productDto)
        {
            var existingProduct = _productRepository.Get(id);

            if (productDto.Name != null) existingProduct.Name = productDto.Name;
            if (productDto.Description != null) existingProduct.Description = productDto.Description;
            if (productDto.Price.HasValue) existingProduct.Price = productDto.Price.Value;
            if (productDto.BrandId.HasValue) existingProduct.BrandId = productDto.BrandId.Value;
            if (productDto.CategoryId.HasValue) existingProduct.CategoryId = productDto.CategoryId.Value;
            if (productDto.Colors != null)
            {
                existingProduct.ProductColors.Clear();
                foreach (var colorId in productDto.Colors.Distinct())
                {
                    var color = _colorService.Get(colorId);
                    existingProduct.AddColor(color);
                }
            }

            return _productRepository.UpdateProduct(existingProduct);
        }

    }
}

