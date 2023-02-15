﻿namespace InstaMonitor
{
    internal class Update
    {
        public Usuario Usuario { get; set; }
        public Status Status { get; set; }
        public DateTime DateTime { get; set; }

        public Update(Usuario usuario, Status status, DateTime dateTime)
        {
            Usuario = usuario;
            Status = status;
            DateTime = dateTime;
        }
    }
}
