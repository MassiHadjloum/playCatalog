using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Model;

namespace Play.Catalog.Service
{
  public static class Extensions
  {
    public static ItemDto AsDto(this Item item)
    {
      return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
    }

    public static Item AsItem(IItemDto itemDto, DateTimeOffset? dateTimeOffset = null, string? mode = null)
    {
      return new Item
      {
        Name = itemDto.Name,
        Description = itemDto.Description,
        Price = itemDto.Price,
        CreatedDate = mode == "POST" ? DateTimeOffset.UtcNow : dateTimeOffset ?? DateTimeOffset.UtcNow  
      };
    }
  }
}