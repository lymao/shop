﻿using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
    }

    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}