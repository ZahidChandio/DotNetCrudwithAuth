using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;


        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo) {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            // ModelState is coming from the ControllerBase
            // To check the validations in the Dto
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var comments = await _commentRepo.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet]
        // [Route("{id}")]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            var comment = await _commentRepo.GetByIdAsync(id);
            
            if(comment == null) {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }
        
        // [HttpPost("{stockId}")]
        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto) {
            // ModelState is coming from the ControllerBase
            // To check the validations in the Dto
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var stockExists = await _stockRepo.StockExists(stockId);
            
            if(!stockExists) {
                return BadRequest("Stock does not exist");
            }

            var commentModel = commentDto.ToCommentFromCreate(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        // [Route("{id}")]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto) {
            var comment = await _commentRepo.UpdateAsync(id, updateDto.ToCommentFromUpdate());
            
            if(comment == null) {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        // [Route("{id}")]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
             // ModelState is coming from the ControllerBase
            // To check the validations in the Dto
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var commentModel = await _commentRepo.DeleteAsync(id);
            
            if(commentModel == null) {
                return NotFound("Comment does not exist.");
            }

            return Ok(commentModel);
        }
    }
}