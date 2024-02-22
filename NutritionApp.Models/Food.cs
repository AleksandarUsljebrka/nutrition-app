using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NutritionApp.Models
{

    public class Food
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Food Name")]
        [StringLength(25, MinimumLength = 2)]

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only characters are alowed!")]
        [Required]
        public string FoodName { get; set; }
        [Required]
        [DisplayName("Type of Food")]
        public string TypeOfFood { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public float Proteins { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public float Carbs { get; set; }
        [Required(ErrorMessage = "This field is required!")]

        public float Fat { get; set; }

    }
}
