using System.Collections;
using System.Collections.Generic;
using CheatCodes.WebApi.Models;

namespace CheatCodes.WebApi.Service
{
    public interface ICategoryService
    {

        void Add(Category category);
        IList<Category> Get();
        
        
    }
}