using System;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [Route("GetTag/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetTag([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _tagService.Get(id));
            }
            catch (TagException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            try
            {
                return Ok(await _tagService.GetAll());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Route("CreateTag")]
        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] TagDto tagDto)
        {
            try
            {
                await _tagService.Create(tagDto);
                return Ok();
            }
            catch (TagException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("UpdateTag/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateTag([FromRoute] Guid id, [FromBody] TagDto tagDto)
        {
            try
            {
                await _tagService.Update(id, tagDto);

                return Ok();
            }
            catch (TagException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("DeleteTag/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTag([FromRoute] Guid id)
        {
            try
            {
                await _tagService.Delete(id);

                return Ok();
            }
            catch (TagException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}