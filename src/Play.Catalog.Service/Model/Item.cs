using Play.Common;

namespace Play.Catalog.Service.Model
{

    public class Item : IEntity
  {
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public DateTimeOffset CreatedDate { get; set; }

    // public Item(string Name, string Description, decimal Price, DateTimeOffset CreatedDate)
    // {
    //   Id = Guid.NewGuid(); 
    //   this.Name = Name; 
    //   this.Description = Description; 
    //   this.Price = Price; 
    //   this.CreatedDate = CreatedDate;
    // }
  }
}