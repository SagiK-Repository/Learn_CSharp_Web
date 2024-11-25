using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers;
public static class StockMappers
{
    public static StockDto ToStockDto(this Stock stockModel) => new StockDto
    {
        Id = stockModel.Id,
        Symbol = stockModel.Symbol,
        CompanyName = stockModel.CompanyName,
        Purchase = stockModel.Purchase,
        LastDiv = stockModel.LastDiv,
        Industry = stockModel.Industry,
        MarketCap = stockModel.MarketCap,
    };

    public static Stock ToStockFromCreateDTO(this CreateStockRequestDto request) => new Stock
    {
        Symbol = request.Symbol,
        CompanyName = request.CompanyName,
        Purchase = request.Purchase,
        LastDiv = request.LastDiv,
        Industry = request.Industry,
        MarketCap = request.MarketCap,
    };
}