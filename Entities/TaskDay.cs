﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AllowanceFunctions.Entities
{
    [Table("taskdays")]
    public class TaskDay
    {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public int StatusId { get; set; }
        public int TaskWeekId { get; set; }
        public decimal Value { get; set; }
        public int AccountId { get; set; }
    }
}