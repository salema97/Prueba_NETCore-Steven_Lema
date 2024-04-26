﻿using System.ComponentModel.DataAnnotations;

namespace Shop.Core.Dto
{
    public class CategoryDto
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class ListCategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
