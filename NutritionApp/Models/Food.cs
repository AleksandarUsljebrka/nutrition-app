using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NutritionApp.Models
{
    public class Food
    {
        [Key] 
        public int Id { get; set; }
        
        [Required]
        [DisplayName("Food Name")]
        public string FoodName { get; set; }
        [Required]
        [DisplayName("Type of Food")]
        public string TypeOfFood { get; set; }
        [Required]
        [DisplayName("Proteins")]
        public float Proteins { get; set;}
        [Required]
        [DisplayName("Carbs")]
        public float Carbs { get; set;}
        [Required]
        [DisplayName("Fat")]
        public float Fat{ get; set;}

    }
}
