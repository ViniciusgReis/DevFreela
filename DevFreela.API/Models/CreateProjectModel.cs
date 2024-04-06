﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Models
{
    public class CreateProjectModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; private set; }
    }
}
