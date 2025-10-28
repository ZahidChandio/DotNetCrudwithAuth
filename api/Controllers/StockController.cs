using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("/api/stock")]
    [ApiController]
    public class StockController: ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepository;
        
        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
            _context = context;
        }
        [HttpGet]
        // Normal call
        // public async Task<IActionResult> GetAll() {
        // Filtering
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query) {

            // ModelState is coming from the ControllerBase
            // To check the validations, added in the Dto
            if(!ModelState.IsValid) return BadRequest(ModelState);

            // ToList() - Deffered execution
            // var stocks = _context.Stocks.ToList().Select(s => s.ToStockDto());
            // var stocks = await _context.Stocks.ToListAsync();
           
            // Repository pattern
            // var stocks = await _stockRepository.GetAllAsync();   

            // Filtering by params
            var stocks = await _stockRepository.GetAllAsync(query);

            var stockDto = stocks.Select(s => s.ToStockDto());

            return Ok(stocks);
        }
        
        // [HttpGet("{id}")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            // ModelState is coming from the ControllerBase
            // To check the validations in the Dto
            if(!ModelState.IsValid) return BadRequest(ModelState);

            // var stock = await _context.Stocks.FindAsync(id);
            var stock = await _stockRepository.GetByIdAsync(id); // Repository pattern


            if(stock == null) {
                return NotFound();
            }
            
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto) {
            // ModelState is coming from the ControllerBase
            // To check the validations in the Dto
            if(!ModelState.IsValid) return BadRequest(ModelState);

            // [FromBody] will parse the body
            var stockModel = stockDto.ToStockFromCreateDTO();
            // await _context.Stocks.AddAsync(stockModel);
            // await _context.SaveChangesAsync();
            await _stockRepository.CreateAsync(stockModel); // Repository pattern

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        // [HttpPut("{id}")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto) {
            // ModelState is coming from the ControllerBase
            // To check the validations in the Dto
            if(!ModelState.IsValid) return BadRequest(ModelState);

            // [FromBody] will parse the body
            // var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            var stockModel = await _stockRepository.UpdateAsync(id, updateDto); // Repository pattern
           
            if(stockModel == null) {
                return NotFound();
            }

            // stockModel.Symbol = updateDto.Symbol;
            // stockModel.CompanyName = updateDto.CompanyName;
            // stockModel.Purchase = updateDto.Purchase;
            // stockModel.LastDiv = updateDto.LastDiv;
            // stockModel.Industry = updateDto.Industry;
            // stockModel.MarketCap = updateDto.MarketCap;

            // await _context.SaveChangesAsync();
            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        // [Route("{id}")]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            // ModelState is coming from the ControllerBase
            // To check the validations in the Dto
            if(!ModelState.IsValid) return BadRequest(ModelState);

            // [FromBody] will parse the body
            // var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            var stockModel = await _stockRepository.DeleteAsync(id);

           
            if(stockModel == null) {
                return NotFound();
            }

            // _context.Stocks.Remove(stockModel);

            // await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}