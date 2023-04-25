using System.ComponentModel.DataAnnotations;

namespace server.Main.Models
{
  public class House
  {
    public int Id { get; set; }
    public string StreetName { get; set; } = string.Empty;
    public int HouseNumber { get; set; }
    public string Addition { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int Price { get; set; }
    public int Size { get; set; }
    public bool HasGarage { get; set; } = false;
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public int ConstructionYear { get; set; }
    public string Description { get; set; } = string.Empty;
    public User? User { get; set; }
  }
}