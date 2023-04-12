﻿using System.ComponentModel.DataAnnotations;

namespace AppLookUp.Models.ViewModels
{
    public class UserVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string Role { get; set; }
    }
}
