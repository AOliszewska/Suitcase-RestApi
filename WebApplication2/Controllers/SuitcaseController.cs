using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Suitcase.DTO.Request;
using Suitcase.Repositories.Interfaces;

namespace Suitcase.Controllers
{
    [ApiController]
    [Route("api")]
    public class SuitcaseController: Controller
    {
        private readonly ISuitcaseDbRepository _suitcaseDbRepository;
        public SuitcaseController(ISuitcaseDbRepository suitcaseDbRepository)
        {
            _suitcaseDbRepository = suitcaseDbRepository;
        }

        [HttpGet ("suitcase/{CityName}")]
        public async Task<IActionResult> GetSuitcases([FromRoute]  string cityName)
        {
            if (cityName == null)
            {
                return NotFound("cityName is null");
            }
            var suitcaseFromDb = await _suitcaseDbRepository.GetSuitcaseFromDbAsync(cityName);
            if (suitcaseFromDb.Count==0)
            {
                return NotFound("No suitcase was found");
            }
            return Ok(suitcaseFromDb);
        }

        [HttpGet("{text}")]
        public async Task<IActionResult> GetSuitcasesByText([FromRoute] string text)
        {
            if (text == null)
            {
                return NotFound("text is null");
            }
            var suitcaseFromDb = await _suitcaseDbRepository.GetSuitcaseByTextDbAsync(text);
            if (suitcaseFromDb == null)
            {
                return NotFound("No suitcase was found");
            }
            return Ok(suitcaseFromDb);
        }

        [HttpPost ("{idSuitcase}")]
        public async Task<IActionResult> AddSuitcase([FromBody]ItemRequestDto itemRequestDto, [FromRoute] int idSuitcase)
        {
            var addItem = await _suitcaseDbRepository.PostItemFromDbAsync(itemRequestDto, idSuitcase);
            if (addItem != "OK"){
                return BadRequest(addItem);
            }

            return Ok("Correctly added new item");
        }
        [HttpDelete("item/{idItem}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int idItem)
        {
            var deleteItem = await _suitcaseDbRepository.DeleteItem(idItem);
            if (deleteItem=="No item was found")
                return NotFound("The specified item was not found");
            return Ok(deleteItem);
        }
   
        [HttpDelete("suitcase/{idSuitcase}")]
        public async Task<IActionResult> DeleteSuitcase([FromRoute] int idSuitcase)
        {
            var deleteSuitcase = await _suitcaseDbRepository.DeleteSuitcase(idSuitcase);
            if (deleteSuitcase=="SuitcaseApi does not exist")
                return NotFound("SuitcaseApi does not exist");
            return Ok(deleteSuitcase);
        }
   
        [HttpPut("item/{idItem}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int idItem)
        {
            var updateItem = await _suitcaseDbRepository.UpdateItem(idItem);
            if (updateItem == "No item was found")
                return NotFound("Item does not exist");
            return Ok(updateItem);
        }
    }
}