﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class PaisBE : BEBase
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Sigla { get; set; }
    }
}
