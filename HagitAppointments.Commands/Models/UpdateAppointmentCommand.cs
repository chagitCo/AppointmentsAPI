﻿namespace HagitAppointments.Commands.Models
{
    public class UpdateAppointmentCommand
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
