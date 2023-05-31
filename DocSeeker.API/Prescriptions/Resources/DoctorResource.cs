﻿namespace DocSeeker.API.Prescriptions.Resources;

public class DoctorResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }
    public int Description { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}