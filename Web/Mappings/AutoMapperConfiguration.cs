﻿using AutoMapper;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>

            {

                cfg.CreateMap<PostCategory, PostCategoryViewModel>();

                cfg.CreateMap<Post, PostViewModel>();

                cfg.CreateMap<PostTag, PostTagViewModel>();

                cfg.CreateMap<Tag, TagViewModel>();

                cfg.CreateMap<ProductCategory, ProductCategoryViewModel>();

                cfg.CreateMap<Product, ProductViewModel>();

            });
            //For version of Automapper <5.0
            //Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            //Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            //Mapper.CreateMap<Tag, TagViewModel>();

        }
    }
}