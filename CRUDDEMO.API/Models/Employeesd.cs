using System;
using System.Collections.Generic;

namespace CRUDDEMO.API.Models;

public partial class Employeesd
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Salary { get; set; }

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Age { get; set; }

    public string Department { get; set; } = null!;

    public DateOnly JoiningDate { get; set; }
}
