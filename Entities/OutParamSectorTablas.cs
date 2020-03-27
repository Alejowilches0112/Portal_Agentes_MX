﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamSectorTablas
    {
        public double secuencia { get; set; }
        public string sector { get; set; }
        public double ind_estado { get; set; }
    }
    public class OutParamSectorTablas
    {
        public List<ParamSectorTablas> ListSectorTablas { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
