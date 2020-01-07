using System;
using System.Collections.Generic;
using System.Linq;
using Api.Categories;
using Api.Utils;
using Application.Utils;
using Application.Utils.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Persistance;
using Xunit;

namespace Api.Test.Categories
{
    public class CategoriesControllerTest
    {

        [Fact]
        public void Test1()
        {
            var messages = (Messages)ServiceProviderFactory.ServiceProvider.GetServices(typeof(Messages)).Single();

            var controller = new CategoriesController(messages);
            var result = controller.Get();
            Assert.IsType(result.GetType(), typeof(OkObjectResult));
        }

        private IList<Category> GetTestCategories()
        {
            return new List<Category>
            {
                new Category()
                {
                    Name = "Cat test name 1",
                    Description = "Cat test description 1"
                }
            };

        }
    }
}


