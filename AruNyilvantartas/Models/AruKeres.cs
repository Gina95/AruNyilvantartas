using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AruNyilvantartas.Models
{
    public class AruKeres
    {
        public List<Aru> nyilvantartas { get; set; }
        public string  keresElnevezes { get; set; }
        public string keresKategoria { get; set; }
        public SelectList Kategoria { get; set; }
        public SelectList Elnevezes { get; set; }
    }
}
