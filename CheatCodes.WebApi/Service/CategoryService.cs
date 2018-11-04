using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CheatCodes.WebApi.Models;

namespace CheatCodes.WebApi.Service
{
    public class CategoryService: ICategoryService
    {
        private readonly CheatCodesDb _db;

        public CategoryService(CheatCodesDb db)
        {
            _db = db;
        }


        public void Add(Category category)
        {
            _db.Add(category);
            _db.SaveChanges();
        }

        public IList<Category> Get()
        {
            return _db.Categories.ToList();
        }
    }
}