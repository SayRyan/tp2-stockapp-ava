using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Application.Services;
using StockApp.Domain.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// API Controller para gerenciar operações relacionadas aos fornecedores.
    /// </summary>
    [EnableCors("AllowSpecificOrigins")]
    [Route("/api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        [HttpPost("send-sms")]
        public async Task<IActionResult> SendSms([FromQuery] string phoneNumber, [FromQuery] string message, [FromServices] ISmsService smsService)
        {
            await smsService.SendSmsAsync(phoneNumber, message);
            return Ok("SMS enviado com sucesso!");
        }

        /// <summary>
        /// Inicializa uma nova instância do <see cref="SuppliersController"/>.
        /// </summary>
        /// <param name="supplierService">Serviço para operações de fornecedores.</param>

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        /// <summary>
        /// Obtém a lista de todos os fornecedores.
        /// </summary>
        /// <returns>Uma lista de fornecedores.</returns>

        [HttpGet(Name = "GetSuppliers")]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> Get()
        {
            var suppliers = await _supplierService.GetSuppliers();
            if (suppliers == null)
            {
                return NotFound("Suppliers not found");
            }
            return Ok(suppliers);
        }

        /// <summary>
        /// Obtém os detalhes de um fornecedor específico pelo ID.
        /// </summary>
        /// <param name="id">ID do fornecedor.</param>
        /// <returns>Os detalhes do fornecedor.</returns>

        [HttpGet("{id:int}", Name = "GetSupplier")]
        public async Task<ActionResult<SupplierDTO>> Get(int id)
        {
            var supplier = await _supplierService.GetSupplierById(id);
            if (supplier == null)
            {
                return NotFound("Supplier not found");
            }
            return Ok(supplier);
        }

        /// <summary>
        /// Cria um novo fornecedor.
        /// </summary>
        /// <param name="supplierDto">Objeto DTO contendo os dados do fornecedor.</param>
        /// <returns>O fornecedor recém-criado.</returns>

        [HttpPost(Name = "Create Supplier")]
        public async Task<ActionResult> CreatedSupplier([FromBody] SupplierDTO supplierDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var supplier = await _supplierService.Add(supplierDto);
            return CreatedAtAction(nameof(Get), new { id = supplier.Id }, supplierDto);
        }

        /// <summary>
        /// Atualiza os dados de um fornecedor existente.
        /// </summary>
        /// <param name="id">ID do fornecedor a ser atualizado.</param>
        /// <param name="supplierDto">Objeto DTO contendo os novos dados do fornecedor.</param>
        /// <returns>Resultado da atualização.</returns>

        [HttpPut(Name = "Update Supplier")]
        public async Task<ActionResult> Put(int id, [FromBody] SupplierDTO supplierDto)
        {
            if (supplierDto == null)
            {
                return BadRequest("Update Data Invalid");
            }

            await _supplierService.Update(supplierDto);
            return Ok(supplierDto);
        }

        /// <summary>
        /// Remove um fornecedor pelo ID.
        /// </summary>
        /// <param name="id">ID do fornecedor a ser removido.</param>
        /// <returns>O fornecedor removido.</returns>

        [HttpDelete("{id:int}", Name = "Delete Supplier")]
        public async Task<ActionResult<SupplierDTO>> Delete(int id)
        {
            var supplier = await _supplierService.GetSupplierById(id);
            if (supplier == null)
            {
                return NotFound("Supplier not found");
            }

            await _supplierService.Remove(id);

            return Ok(supplier);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> SearchSuppliers(
            [FromQuery] string name,
            [FromQuery] string contactEmail,
            [FromQuery] string phoneNumber)
        {
            var suppliers = await _supplierService.SearchSuppliersAsync(name, contactEmail, phoneNumber);
            return Ok(suppliers);
        }
    }
}
