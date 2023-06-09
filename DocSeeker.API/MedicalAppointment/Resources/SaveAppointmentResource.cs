﻿using System.ComponentModel.DataAnnotations;

namespace DocSeeker.API.MedicalAppointment.Resources;

public class SaveAppointmentResource
{
    [Required] public DateTime Date { get; set; }

    [Required] public int DoctorId { get; set; }

    [Required] public int PatientId { get; set; }
}