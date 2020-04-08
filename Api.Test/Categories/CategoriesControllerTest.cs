using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Api.Test.Framework;
using Api.Test.Framework.Builders;
using Api.Test.Framework.Mothers;
using Application.Utils.Interfaces;
using Domain;
using Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Api.Test.Categories
{
  public class CategoriesControllerTest
  {
    private static ServiceDescriptor ServiceDescriptorUOW(Mock<ICategoryCommandRepository> mockRepo)
    {
      var mockUOW = new Mock<IUnitOfWork>();
      mockUOW.Setup(uow => uow.CategoryCommandRepository).Returns(mockRepo.Object);
      var serviceDescriptorUOW = new ServiceDescriptor(typeof(IUnitOfWork), mockUOW.Object);
      return serviceDescriptorUOW;
    }

    private static ByteArrayContent GetByteContent(object dto)
    {
      var categoryCreateDto = JsonConvert.SerializeObject(dto);
      var buffer = Encoding.UTF8.GetBytes(categoryCreateDto);
      var byteContent = new ByteArrayContent(buffer);
      byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      return byteContent;
    }

    private static CategoryCreateDto GetCategoryCreateDto()
    {
      return new CategoryCreateDto();
    }

    private static CategoryUpdateDto GetCategoryUpdateDto()
    {
      return new CategoryUpdateDto();
    }

    private static IList<Category> GetTypicalCategories()
    {
      return new List<Category>
      {
          CategoryBuilder.Typical().Build(),
          CategoryBuilder.Typical().Build()
      };
    }
    private static IList<Category> GetSimpleWithIdsCategories()
    {
      return new List<Category>
      {
        CategoryBuilder.Typical().WithParentCategory(()=>CategoryBuilder.Simple().Build()).Build()
      };
    }

    [Fact]
    public async Task CreateCategory_ReturnOk_IfNoIssues()
    {
      var mockRepo = new Mock<ICategoryCommandRepository>();
      mockRepo.Setup(repo => repo.Create(It.IsAny<Category>()));
      var serviceDescriptor = new ServiceDescriptor(typeof(ICategoryCommandRepository), mockRepo.Object);

      var serviceDescriptorUOW = ServiceDescriptorUOW(mockRepo);

      using (var testServer = new ConfigurableServer(sc => sc.Replace(serviceDescriptor).Replace(serviceDescriptorUOW)))
      {
        var client = testServer.CreateClient();
        var byteContent = GetByteContent(GetCategoryCreateDto());
        var value = await client.PostAsync("api/Categories", byteContent);

        Assert.Equal(HttpStatusCode.OK, value.StatusCode);
      }
    }

    [Fact]
    public async Task DeleteCategory_ReturnOk_IfNoIssues()
    {
      var mockRepo = new Mock<ICategoryCommandRepository>();
      mockRepo.Setup(repo => repo.Delete(It.IsAny<Category>()));
      var serviceDescriptor = new ServiceDescriptor(typeof(ICategoryCommandRepository), mockRepo.Object);

      var serviceDescriptorUOW = ServiceDescriptorUOW(mockRepo);

      using (var testServer = new ConfigurableServer(sc => sc.Replace(serviceDescriptor).Replace(serviceDescriptorUOW)))
      {
        var client = testServer.CreateClient();

        var request = new HttpRequestMessage
        {
          Method = HttpMethod.Delete,
          RequestUri = new Uri("http://localhost:57243/api/Categories"),
          Content = new StringContent(JsonConvert.SerializeObject(new CategoryDeleteDto()), Encoding.UTF8,
            "application/json")
        };
        var response = await client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      }
    }

    [Fact]
    public async Task Get_ReturnOk_IfNoIssues()
    {
      var mockRepo = new Mock<ICategoryQueryRepository>();
      mockRepo.Setup(repo => repo.GetAllParents()).Returns(GetTypicalCategories());
      var serviceDescriptor = new ServiceDescriptor(typeof(ICategoryQueryRepository), mockRepo.Object);

      using (var testServer = new ConfigurableServer(sc => sc.Replace(serviceDescriptor)))
      {
        var client = testServer.CreateClient();
        var value = await client.GetAsync("api/categories");

        Assert.Equal(HttpStatusCode.OK, value.StatusCode);
      }
    }

    [Fact]
    public async Task GetCategory_ReturnOk_IfNoIssues()
    {
      var mockRepo = new Mock<ICategoryQueryRepository>();
      mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(GetTypicalCategories().First());
      var serviceDescriptor = new ServiceDescriptor(typeof(ICategoryQueryRepository), mockRepo.Object);

      using (var testServer = new ConfigurableServer(sc => sc.Replace(serviceDescriptor)))
      {
        var client = testServer.CreateClient();
        var value = await client.GetAsync("api/Categories/1");

        Assert.Equal(HttpStatusCode.OK, value.StatusCode);
      }
    }

    [Fact]
    public async Task GetChildsOf_ReturnOk_IfNoIssues()
    {
      var mockRepo = new Mock<ICategoryQueryRepository>();
      var simpleCategories = GetTypicalCategories();
      mockRepo.Setup(repo => repo.GetAllChilds(It.IsAny<int>())).Returns(simpleCategories);
      var serviceDescriptor = new ServiceDescriptor(typeof(ICategoryQueryRepository), mockRepo.Object);

      using (var testServer = new ConfigurableServer(sc => sc.Replace(serviceDescriptor)))
      {
        var client = testServer.CreateClient();
        var value = await client.GetAsync("api/Categories/GetChildsOf/1");

        Assert.Equal(HttpStatusCode.OK, value.StatusCode);
      }
    }

    [Fact]
    public async Task GetSiblingsOf_ReturnOk_IfNoIssues()
    {
      var testCategories = GetSimpleWithIdsCategories();
      var mockRepo = new Mock<ICategoryQueryRepository>();
      mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(testCategories.First);
      mockRepo.Setup(repo => repo.GetAllParents()).Returns(testCategories);
      mockRepo.Setup(repo => repo.GetAllChilds(It.IsAny<int>())).Returns(GetTypicalCategories());

      var serviceDescriptor = new ServiceDescriptor(typeof(ICategoryQueryRepository), mockRepo.Object);

      using (var testServer = new ConfigurableServer(sc => sc.Replace(serviceDescriptor)))
      {
        var client = testServer.CreateClient();
        var value = await client.GetAsync("api/Categories/GetSiblingsOf/" + GetRandom.Id());

        Assert.Equal(HttpStatusCode.OK, value.StatusCode);
      }
    }

    [Fact]
    public async Task UpdateCategory_ReturnOk_IfNoIssues()
    {
      var mockRepo = new Mock<ICategoryCommandRepository>();
      mockRepo.Setup(repo => repo.Update(It.IsAny<Category>()));
      var serviceDescriptor = new ServiceDescriptor(typeof(ICategoryCommandRepository), mockRepo.Object);

      var serviceDescriptorUOW = ServiceDescriptorUOW(mockRepo);

      using (var testServer = new ConfigurableServer(sc => sc.Replace(serviceDescriptor).Replace(serviceDescriptorUOW)))
      {
        var client = testServer.CreateClient();
        var byteContent = GetByteContent(GetCategoryUpdateDto());
        var value = await client.PutAsync("api/Categories", byteContent);

        Assert.Equal(HttpStatusCode.OK, value.StatusCode);
      }
    }
  }
}