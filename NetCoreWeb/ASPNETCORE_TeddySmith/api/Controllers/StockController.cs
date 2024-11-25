using api.Data;
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
        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var stock = await _context.Stocks.FindAsync(id);
        if (stock == null)
            return NotFound();
        return Ok(stock);
    }
}