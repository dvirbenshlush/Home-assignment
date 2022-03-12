﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Home_Assignment.Models
{
    //[Keyless]
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public double age { get; set; }
        public double gpa { get; set; }
        public string school_of_name { get; set; }
        public string school_address { get; set; }
    }
}
