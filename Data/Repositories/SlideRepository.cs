﻿using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface ISlideRepository : IRepository<Slide>
    {
    }

    public class SlideRepository : RepositoryBase<Slide>, ISlideRepository
    {
        public SlideRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}