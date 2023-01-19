using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PatronApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("[action]")]
        public IActionResult RecipeList()
        {
            var data = _recipeService.GetAllList();
            if (data.Success)
            {
                return Ok(data);
            }
            return BadRequest(data);
        }

        [HttpPost("[action]")]
        public IActionResult RecipeUpdate(Recipe recipe)
        {
            var data = _recipeService.Update(recipe);
            if (data.Success)
            {
                return Ok(data);
            }
            return BadRequest(data);
        }
    }
}
