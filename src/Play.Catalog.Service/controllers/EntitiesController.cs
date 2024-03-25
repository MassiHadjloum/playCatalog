using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Model;
using Play.Catalog.Service.Repositories;

namespace Play.Catalog.Service.Controllers
{

  //localhost:port/items
  [ApiController]
  [Route("entities")]
  public class EntitiesController : ControllerBase
  {

    private readonly IItemsRepository itemsRepository;

    public EntitiesController(IItemsRepository itemsRepository) {
      this.itemsRepository = itemsRepository;
    }    

    // GET /items
    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetAsync()
    {
      var items = (await itemsRepository.GetAllAsync())
                  .Select(item => item.AsDto());
      return items;
    }

    // GET /items/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
    {
      var item = await itemsRepository.GetAsync(id);
      if(item == null) {
        return NotFound();
      }
      return item.AsDto();
    }

    // POST /items
    [HttpPost]
    public async Task<ActionResult<ItemDto>> AddItemAsync(CreateItemDto createItemDto)
    {
      var item = Extensions.AsItem(createItemDto);
      var itemm = new Item {
        Name = createItemDto.Name,
        Description = createItemDto.Description,
        Price = createItemDto.Price,
        CreatedDate = DateTimeOffset.UtcNow
      };
      await itemsRepository.CreateAsync(item);
      // at runtime GetByIdAsync Async suffix will be removed
      // options.SuppressAsyncSuffixInActionNames = false; on builder.Services.AddControllers
      return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
    }

    // PUT items/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto updateItemDto)
    {
      var exestingItem = await itemsRepository.GetAsync(id);
      if(exestingItem == null) {
        return NotFound();
      }
      exestingItem.Name = updateItemDto.Name;
      exestingItem.Description = updateItemDto.Description;
      exestingItem.Price = updateItemDto.Price;
      // await itemsRepository.UpdateAsync(Extensions.AsItem(updateItemDto));
      await itemsRepository.UpdateAsync(exestingItem);

      return NoContent();
    }

    // DELETE /items/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItemAsync(Guid id) {
      var exestingItem = itemsRepository.GetAsync(id);
      if(exestingItem == null) {
        return NotFound();
      }
      await itemsRepository.RemoveAsync(id);
      return NoContent();
    }
  }
}