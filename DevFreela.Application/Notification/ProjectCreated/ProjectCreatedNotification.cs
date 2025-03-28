﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Notification.ProjectCreated
{
    public class ProjectCreatedNotification : INotification
    {
        public ProjectCreatedNotification(int id, string title, decimal totalCost)
        {
            Id = id;
            Title = title;
            TotalCost = totalCost;
        }

        public int Id { get; set; }
        public string Title  { get; set; }
        public decimal TotalCost { get; set; }

    }
}
