using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// API Controller para gerenciar operações relacionadas às categorias.
    /// </summary>

    [EnableCors("AllowSpecificOrigins")]
    [Route("/api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Inicializa uma nova instância do <see cref="CategoriesController"/>.
        /// </summary>
        /// <param name="categoryService">Serviço para operações de categoria.</param>

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Obtém a lista de todas as categorias.
        /// </summary>
        /// <returns>Uma lista de categorias.</returns>

        [HttpGet(Name ="GetCategories")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get() 
        {
            var categories = await _categoryService.GetCategories();
            if(categories== null)
            {
                return NotFound("Categories not found");
            }
            return Ok(categories);
        }

        /// <summary>
        /// Obtém os detalhes de uma categoria específica pelo ID.
        /// </summary>
        /// <param name="id">ID da categoria.</param>
        /// <returns>Os detalhes da categoria.</returns>

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if(category== null)
            {
                return NotFound("Category not Found");
            }
            return Ok(category);
        }

        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        /// <param name="categoryDTO">Objeto DTO contendo os dados da categoria.</param>
        /// <returns>A categoria recém-criada.</returns>

        [HttpPost(Name ="Create Category")]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            if(categoryDTO == null)
            {
                return BadRequest("Invalid Data");
            }
            var category = await _categoryService.Add(categoryDTO);

            return new CreatedAtRouteResult("GetCategory", 
                new { id = category.Id }, categoryDTO);
        }

        /// <summary>
        /// Atualiza os dados de uma categoria existente.
        /// </summary>
        /// <param name="id">ID da categoria a ser atualizada.</param>
        /// <param name="categoryDTO">Objeto DTO contendo os novos dados da categoria.</param>
        /// <returns>Resultado da atualização.</returns>

        [HttpPut(Name ="Update Category")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if(categoryDTO == null)
            {
                return BadRequest("Update Data Invalid");
            }

            await _categoryService.Update(categoryDTO);

            return Ok(categoryDTO);
        }

        /// <summary>
        /// Remove uma categoria pelo ID.
        /// </summary>
        /// <param name="id">ID da categoria a ser removida.</param>
        /// <returns>A categoria removida.</returns>

        [HttpDelete("{id:int}", Name ="Delete Category")]
        public async Task<ActionResult<CategoryDTO>> Detele(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if(category == null) 
            {
                return NotFound("Category not found");
            }

            await _categoryService.Remove(id);

            return Ok(category);
        }
    }
}
