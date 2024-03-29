﻿using Microsoft.AspNetCore.Identity;

namespace NutritionApp.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public ICollection<UserDiary> UserDiaries { get; set; }
    }
}
