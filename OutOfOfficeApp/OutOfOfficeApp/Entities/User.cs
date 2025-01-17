﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOfficeApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PasswordHash { get; set; }
        public virtual Employee? Employee { get; set; }
        public int? EmployeeId { get; set; }
        public int? RoleId { get; set; }
        public virtual Role? Role {  get; set; }

    }
}
