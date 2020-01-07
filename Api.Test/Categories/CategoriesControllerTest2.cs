using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Utils.Interfaces;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit;

namespace Api.Test.Categories
{
    public class CategoriesControllerTest2
    {
        [Fact]
        public async Task ShouldGetValue()
        {
            var mockRepo = new Mock<ICategoryQueryRepository>();
            mockRepo.Setup(repo => repo.GetAllParents()).Returns(GetTestCategories());
            var serviceDescriptor = new ServiceDescriptor(typeof(ICategoryQueryRepository), mockRepo.Object);

            using (var testServer = new ConfigurableServer(sc => sc.Replace(serviceDescriptor)))
            {
                var client = testServer.CreateClient();
                var value = await client.GetStringAsync("api/categories");
                Assert.Equal("Hello mockworld", value);
            }

        }

        private static IList<Category> GetTestCategories()
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
    //private TestServer CreateTestServer()
    //{
    //    var builder = new WebHostBuilder()
    //        .UseStartup<Startup>();
    //    return new TestServer(builder);
    //}
}
