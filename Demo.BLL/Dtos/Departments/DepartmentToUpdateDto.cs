﻿namespace Demo.BLL.Dtos.Departments
{
    public class DepartmentToUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Code { get; set; } = null!;
    }
}
