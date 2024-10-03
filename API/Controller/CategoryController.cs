﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.CategoryRepo;
using Standard.DTOs;
using Standard.Entities;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericRepository<Category> _repo;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _cp;
        public CategoryController(IGenericRepository<Category> repo,
            IMapper mapper,
            ICategoryRepository cp)
        {
            _repo = repo;
            _mapper = mapper;
            _cp = cp;
        }
        [HttpGet("GetCategories")]
        public async Task<ActionResult<List<CategoriesHirarichy>>> GetCategories()
        {
            var cat = await _cp.GetCategories();

            var catDtos = _mapper.Map<List<CategoriesHirarichy>>(cat);

            return Ok(catDtos);
        }

        [HttpPost("CreateCategory")]
        public async Task<ActionResult> CreateCategory(CategoryDto category)
        {
            var newCategory = _mapper.Map<Category>(category);

            await _repo.CreateNew(newCategory);
            return Ok("Category Created Successfully");

        }

        [HttpGet("GetCategoryById/{id}")]
        public async Task<ActionResult<CategoriesHirarichy>> GetCategoryById(int id)
        {

            var category = await _cp.GetCategoryById(id);


            if (category == null)
            {
                return NotFound("Category not found or has been deleted.");
            }

            // Map the entity to a DTO and return it
            return Ok(_mapper.Map<CategoriesHirarichy>(category));

        }

        [HttpPut("UpdateCategory/{id}")]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] CategoryDto category)
        {
            try
            {
                var existingItem = await _repo.GetById(id);

                if (existingItem == null)
                {
                    return NotFound($"Category with ID {id} not found");
                }

                // Update only the fields that are provided in the DTO
                if (!string.IsNullOrEmpty(category.CatNameAr))
                {
                    existingItem.CatNameAr = category.CatNameAr;
                }

                if (!string.IsNullOrEmpty(category.CatNameEn))
                {
                    existingItem.CatNameEn = category.CatNameEn;
                }

                if (!string.IsNullOrEmpty(category.CatDesAr))
                {
                    existingItem.CatDesAr = category.CatDesAr;
                }

                if (!string.IsNullOrEmpty(category.CatDesEn))
                {
                    existingItem.CatDesEn = category.CatDesEn;
                }

                await _repo.Update(existingItem);

                return Ok("Category updated successfully");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Error updating Category: {ex.Message}");
            }
        }
    }
}
