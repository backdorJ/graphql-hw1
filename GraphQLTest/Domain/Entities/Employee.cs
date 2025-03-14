﻿namespace Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly Birth { get; set; }

    public int Age { get; set; }
    public string Phone { get; set; }
}