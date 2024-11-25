using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    public StockController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> GetStocks()
    {
        var stocks = await _context.Stocks.ToListAsync();
        var dtos = stocks.Select(stock => stock.ToStockDto());
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var stock = await _context.Stocks.FindAsync(id);
        if (stock == null)
            return NotFound();
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDTO();
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id} , stockModel.ToStockDto());
    }
}