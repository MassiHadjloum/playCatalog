using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;

namespace Play.Catalog.Service.Controllers
{

  //localhost:port/items
  [ApiController]
  [Route("items")]
  public class ItemController : ControllerBase
  {
    private static readonly List<ItemDto> items = new(){
      new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
      new ItemDto(Guid.NewGuid(), "Antidote", "Cures poisen", 7, DateTimeOffset.UtcNow),
      new ItemDto(Guid.NewGuid(), "Bronze sword", "Deals a small amount of damage", 20, DateTimeOffset.UtcNow),
    };

    // GET /items
    [HttpGet]
    public IEnumerable<ItemDto> Get()
    {
      return items;
    }

    // GET /items/{id}
    [HttpGet("{id}")]
    public ItemDto GetById(Guid id)
    {
      var item = items.Where(item => item.Id == id).SingleOrDefault();
      return item!;
    }

    // POST /items
    [HttpPost]
    public ActionResult<ItemDto> AddItem(CreateItemDto createItemDto)
    {
      var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
      items.Add(item);

      return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    // PUT items/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateItem(Guid id, UpdateItemDto updateItemDto)
    {
      var existingItem = items.Find(item => item.Id == id);
      var updatedItem = existingItem! with
      {
        Name = updateItemDto.Name,
        Description = updateItemDto.Description,
        Price = updateItemDto.Price
      };

      int index = items.FindIndex(item => item.Id == updatedItem.Id);
      items[index] = updatedItem;

      return NoContent();
    }

    // DELETE /items/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteItem(Guid id) {
      var existingItem = items.Find(item => item.Id == id);
      if(existingItem != null) items.Remove(existingItem);

      return NoContent();
    }
  }
}