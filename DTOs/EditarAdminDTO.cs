﻿using System.ComponentModel.DataAnnotations;

namespace WebApiRestaurante2.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
