using System.ComponentModel.DataAnnotations;

namespace server.Main.DTO
{
    public class HouseDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Streetname is required")]
        [RegularExpression(@"^[A-Za-z_. ]{3,70}$", ErrorMessage = "Streetname is not valid")]
        public string StreetName { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Housenumber is required")]
        public int HouseNumber { get; set; }
        [RegularExpression(@"^[A-Za-z_ ]{1,2}$", ErrorMessage = "Addition is not valid")]
        public string Addition { get; set; } = string.Empty;
        [Required(ErrorMessage = "Zip is required")]
        [RegularExpression(@"^[1-9][0-9]{3} ?(?!sa|sd|ss|SA|SD|SS)[A-Za-z]{2}$", ErrorMessage = "Zip is not valid")]
        public string Zip { get; set; } = string.Empty;
        [Required(ErrorMessage = "City is required")]
        [RegularExpression(@"^[A-Za-z_ ]{3,40}$", ErrorMessage = "City is not valid")]
        public string City { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Price is required")]
        public int Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Size is required")]
        public int Size { get; set; }
        public bool HasGarage { get; set; } = false;
        [Range(1, int.MaxValue, ErrorMessage = "Bedrooms is required")]
        public int Bedrooms { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Bathrooms is required")]
        public int Bathrooms { get; set; }
        [Range(1800, int.MaxValue, ErrorMessage = "Construction Year is required")]
        public int ConstructionYear { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;
        public bool MadeByMe { get; set; } = false;
    }
}
