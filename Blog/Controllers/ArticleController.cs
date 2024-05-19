using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        private Guid UserId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            try
            {
                return Ok(await _articleService.GetAll());
            }
            catch (ArticleException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetArticle([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _articleService.Get(id));
            }
            catch (ArticleException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Route("GetArticlesByTag/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetArticlesByTag([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _articleService.GetArticlesByTag(id));
            }
            catch (ArticleException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Route("GetUserArticles")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUserArticles()
        {
            try
            {
                return Ok(await _articleService.GetUserArticles(UserId));
            }
            catch (ArticleException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("CreateArticle/{userIdArt}")]
        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody] ArticleDto articleDto, [FromRoute] string userIdArt)
        {
            try
            {
                await _articleService.Create(articleDto, userIdArt);

                return Ok();
            }
            catch (ArticleException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("AddTag/{articleId}")]
        [HttpPost]
        public async Task<IActionResult> AddTag([FromRoute] Guid articleId, [FromBody] TagDto tagDto)
        {
            try
            {
                await _articleService.AddTag(articleId, tagDto);

                return Ok();
            }
            catch (ArticleException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("EditArticle/{id}/{userIdArt}")]
        public async Task<IActionResult> UpdateArticle([FromRoute] string id, [FromRoute] string userIdArt,
            [FromBody] ArticleDto articleDto)
        {
            try
            {
                articleDto.UserId = Guid.Parse(userIdArt);

                await _articleService.Update(Guid.Parse(id), articleDto);

                return Ok();
            }
            catch (ArticleException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("DeleteArticle/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteArticle([FromRoute] Guid id)
        {
            try
            {
                await _articleService.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}