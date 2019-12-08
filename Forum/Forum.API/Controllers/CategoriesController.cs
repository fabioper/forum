using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Forum.API.ViewModels;
using Forum.Domain.Entities;
using Forum.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly ISectionsRepository _sectionsRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoriesRepository categoriesRepository, ISectionsRepository sectionsRepository, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _sectionsRepository = sectionsRepository;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoriesRepository.GetAllAsync();

            return Ok(new ResponseMessage
            {
                Code = 200,
                Data = categories
            });
        }

        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> Index([FromBody] CreateGategoryViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseMessage
                {
                    Code = 400,
                    Message = "Formatação inválida",
                    Errors = ModelState.Values.Select(v => v.Errors)
                });

            var sectionExist = await _sectionsRepository.Contains(vm.SectionId);

            if (sectionExist)
            {
                var category = _mapper.Map<Category>(vm);

                await _categoriesRepository.AddAsync(category);

                return Ok(new ResponseMessage
                {
                    Code = 200,
                    Message = "Categoria adicionado com sucesso"
                });
            }

            return BadRequest(new ResponseMessage
            {
                Code = 400,
                Message = "Seção não existe"
            });
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var category = await _categoriesRepository.GetByIdAsync(id);

            if (category != null)
                return Ok(new ResponseMessage
                {
                    Code = 200,
                    Data = category
                });

            return NotFound(new ResponseMessage
            {
                Code = 404,
                Message = "Categoria não encontrada"
            });
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Edit(long id, [FromBody] EditCategoryViewModel vm)
        {
            var category = await _categoriesRepository.GetByIdAsync(id);

            if (category != null)
            {
                category.Name = vm.Name;
                category.Description = vm.Description;
                category.SectionId = vm.SectionId;

                await _categoriesRepository.UpdateAsync(category);

                return Ok(new ResponseMessage
                {
                    Code = 200,
                    Message = $"Categoria \"{category.Name}\" atualizada com sucesso"
                });
            }

            return BadRequest(new ResponseMessage
            {
                Code = 400,
                Message = "Categoria não encontrada"
            });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Remove(long id)
        {
            var categoryToBeDeleted = await _categoriesRepository.GetByIdAsync(id);

            if (categoryToBeDeleted != null)
            {
                await _categoriesRepository.RemoveAsync(categoryToBeDeleted);

                return Ok(new ResponseMessage
                {
                    Code = 200,
                    Message = $"Categoria {categoryToBeDeleted.Name} deletada com sucesso"
                });
            }

            return BadRequest(new ResponseMessage
            {
                Code = 400,
                Message = "Categoria não encontrada"
            });
        }
    }
}