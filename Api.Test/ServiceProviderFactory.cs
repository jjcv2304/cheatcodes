using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Api.Categories;
using Application.Utils.Interfaces;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;
using Moq;

namespace Api.Test
{
    public static class ServiceProviderFactory
    {
        public static IServiceProvider ServiceProvider { get; }

        static ServiceProviderFactory()
        {
            //Load default services
            var startup = new Startup(Configuration);
            var sc = new ServiceCollection();
            startup.ConfigureServices(sc);

            //Replace specific service/s
            ReplaceCategoryRepository(sc);

            //construct
            ServiceProvider = sc.BuildServiceProvider();
        }

        private static void ReplaceCategoryRepository(ServiceCollection sc)
        {
            var descriptor = sc.FirstOrDefault(d => d.ServiceType == typeof(ICategoryQueryRepository));
            sc.Remove(descriptor);
            var mockRepo = new Mock<ICategoryQueryRepository>();
            mockRepo.Setup(repo => repo.GetAllParents()).Returns(GetTestCategories());
            var serviceDescriptor = new ServiceDescriptor(typeof(ICategoryQueryRepository), mockRepo.Object);
            sc.Insert(sc.Count, serviceDescriptor);
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

        private static string GetApiDirectory()
        {
            string fullPath = System.Reflection.Assembly.GetAssembly(typeof(Startup)).Location;
            string theDirectory = Path.GetDirectoryName(fullPath);
            return theDirectory;
        }
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(GetApiDirectory())
            .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }
}
