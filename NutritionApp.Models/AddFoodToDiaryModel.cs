using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionApp.Models
{
    public class AddFoodToDiaryModel
    {
        public AddFoodToDiaryModel(int id, string name, float grams, float proteins, float carbs, float fat, string tof)
        {
            this.Id = id;
            FoodName = name;
            Grams = grams;
            Proteins = proteins;
            Carbs = carbs;
            Fat = fat;
            TypeOfFood = tof;
        }
        public AddFoodToDiaryModel()
        {
            
        }
        public int Id { get; set; }

		public string FoodName { get; set; }

        public string TypeOfFood { get; set; }

        public float Proteins { get; set; }

        public float Carbs { get; set; }

        public float Fat { get; set; }

        [Required(ErrorMessage = "Polje Količina (g) je obavezno.")]
        [Range(0.01, 999.99, ErrorMessage = "Unesite broj između {1} i {2}.")]
		[RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Polje Količina (g) mora sadržati brojeve sa najviše dve decimalne tačke.")]
		public float Grams { get; set; }
    }
}
