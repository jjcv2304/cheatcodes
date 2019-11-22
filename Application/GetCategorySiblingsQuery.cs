﻿using System.Collections.Generic;
using Application.Utils;
using Application.Utils.Interfaces;
using Dtos;

namespace Application
{
    public sealed class GetCategorySiblingsQuery : IQuery<List<CategoryDto>>
    {
        public int Id { get; }

        public GetCategorySiblingsQuery(int Id)
        {
            Id = Id;
        }

        internal sealed class GetCategorySiblingsHandler : IQueryHandler<GetCategorySiblingsQuery, List<CategoryDto>>
        {
            private ICategoryQueryRepository _categoryQueryRepository;

            public GetCategorySiblingsHandler(ICategoryQueryRepository categoryQueryRepository)
            {
                _categoryQueryRepository = categoryQueryRepository;
            }

            public List<CategoryDto> Handle(GetCategorySiblingsQuery query)
            {
                var categories = _categoryQueryRepository.GetSiblingsOf(query.Id);
                var categoryDtos = MapService.Map(categories);

                return categoryDtos;
            }
        }
    }
}
