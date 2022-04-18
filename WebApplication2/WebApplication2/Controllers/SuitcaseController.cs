using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTO;
using WebApplication2.Repositories.Interfaces;

namespace WebApplication2.Controllers
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
            if (suitcaseFromDb==null)
            {
                return NotFound("No suitcase was found for this city");
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
                return NotFound("No suitcases for the text");
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
            return Ok("A new item has been assigned to the suitcase");
             
        }

        [HttpDelete("item/{idItem}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int idItem)
        {
            var deleteItem = await _suitcaseDbRepository.DeleteItem(idItem);
            if (deleteItem=="not found item")
                return NotFound("The specified item does not exist");
            return Ok(deleteItem);
        }

        [HttpDelete("suitcase/{idSuitcase}")]
        public async Task<IActionResult> DeleteSuitcase([FromRoute] int idSuitcase)
        {
            var deleteSuitcase = await _suitcaseDbRepository.DeleteSuitcase(idSuitcase);
            if (deleteSuitcase=="not found suitcase")
                return NotFound("The specified suitcase does not exist");
            return Ok(deleteSuitcase);
        }

        [HttpPut("item/{idItem}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int idItem)
        {
            var updateItem = await _suitcaseDbRepository.UpdateItem(idItem);
            if (updateItem == "not found item")
                return NotFound("The specified item does not exist");
            return Ok(updateItem);
        }
    }
}