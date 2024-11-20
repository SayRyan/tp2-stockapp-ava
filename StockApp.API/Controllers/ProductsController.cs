using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// API Controller para gerenciar operações relacionadas aos produtos.
    /// </summary>

    [EnableCors("AllowSpecificOrigins")]
    [Route("/api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Inicializa uma nova instância do <see cref="ProductsController"/>.
        /// </summary>
        /// <param name="productService">Serviço para operações de produto.</param>

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Obtém a lista de todos os produtos.
        /// </summary>
        /// <returns>Uma lista de produtos.</returns>

        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _productService.GetProducts();
            if (products == null)
            {
                return NotFound("Products not found.");
            }

            return Ok(products);
        }

        /// <summary>
        /// Obtém os detalhes de um produto específico pelo ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <returns>Os detalhes do produto.</returns>

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(product);
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="productDto">Objeto DTO contendo os dados do produto.</param>
        /// <returns>O produto recém-criado.</returns>

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] ProductDTO productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productService.Add(productDto);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, productDto);
        }

        /// <summary>
        /// Atualiza os dados de um produto existente.
        /// </summary>
        /// <param name="id">ID do produto a ser atualizado.</param>
        /// <param name="productDto">Objeto DTO contendo os novos dados do produto.</param>
        /// <returns>Resultado da atualização.</returns>

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ProductDTO productDto)
        {
            if (id != 5)
                return BadRequest("ID mismatch.");
            if (!ModelState.IsValid)
                return BadRequest("ID mismatch.");
            await _productService.Update(productDto);
            return Ok(productDto);
        }

        /// <summary>
        /// Remove um produto pelo ID.
        /// </summary>
        /// <param name="id">ID do produto a ser removido.</param>
        /// <returns>Resultado da remoção.</returns>

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound();
            await _productService.Remove(id);
            return Ok();
        }
    }
}
