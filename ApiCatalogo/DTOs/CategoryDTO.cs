using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
