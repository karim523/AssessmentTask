﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Demo.BLL.Dtos.Employees
{
    public class CreatedEmployeeDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }

        public int? ManagerId { get; set; }

        public IFormFile? Image { get; set; }
    }
}
