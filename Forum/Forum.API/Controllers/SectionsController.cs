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
    [Route("api/sections")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly ISectionsRepository _sectionsRepository;

        public SectionsController(ISectionsRepository sectionsRepository, IMapper mapper)
        {
            _sectionsRepository = sectionsRepository;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var sections = await _sectionsRepository.GetAllAsync();

            return Ok(new ResponseMessage
            {
                Code = 200,
                Data = sections
            });
        }

        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> Index([FromBody] CreateSectionViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseMessage
                {
                    Code = 400,
                    Message = "Formatação inválida",
                    Errors = ModelState.Values.Select(v => v.Errors)
                });

            var section = _mapper.Map<Section>(vm);

            await _sectionsRepository.AddAsync(section);

            return Ok(new ResponseMessage
            {
                Code = 200,
                Message = "Seção adicionada com sucesso"
            });
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var section = await _sectionsRepository.GetByIdAsync(id);

            if (section != null)
                return Ok(new ResponseMessage
                {
                    Code = 200,
                    Data = section
                });

            return NotFound(new ResponseMessage
            {
                Code = 404,
                Message = "Seção não encontrada"
            });
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Edit(long id, [FromBody] EditSectionViewModel vm)
        {
            var section = await _sectionsRepository.GetByIdAsync(id);

            if (section != null)
            {
                section.Name = vm.Name;
                await _sectionsRepository.UpdateAsync(section);

                return Ok(new ResponseMessage
                {
                    Code = 200,
                    Message = $"Seção \"{section.Name}\" atualizada com sucesso"
                });
            }

            return BadRequest(new ResponseMessage
            {
                Code = 400,
                Message = "Seção não encontrada"
            });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Remove(long id)
        {
            var sectionToBeDeleted = await _sectionsRepository.GetByIdAsync(id);

            if (sectionToBeDeleted != null)
            {
                await _sectionsRepository.RemoveAsync(sectionToBeDeleted);

                return Ok(new ResponseMessage
                {
                    Code = 200,
                    Message = $"Seção {sectionToBeDeleted.Name} deletada com sucesso"
                });
            }

            return BadRequest(new ResponseMessage
            {
                Code = 400,
                Message = "Seção não encontrada"
            });
        }
    }
}