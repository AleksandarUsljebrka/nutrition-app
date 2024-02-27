using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NutritionApp.Models
{

    public class Food
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Naziv Hrane")]
        [StringLength(25, MinimumLength = 2)]

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Samo su slova dozvoljena!")]
        [Required]
        public string FoodName { get; set; }
        [Required]
        [DisplayName("Tip Hrane")]
        public string TypeOfFood { get; set; }
        
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
		[DisplayName("Proteini(g)")]
		public float Proteins { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        [DisplayName("Ugljeni hidrati(g)")]
		public float Carbs { get; set; }

		[DisplayName("Masti(g)")]
		[Required(ErrorMessage = "Ovo polje je obavezno!")]
        public float Fat { get; set; }

        public ICollection<FoodInDiary>? FoodInDiaries { get; set; }
    }
}
