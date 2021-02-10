using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Simulación_Industria.Entidades
{
    class Maquinas
    {
        public int MaquinaId { get; set; }
        public int ProduccionHora { get; set; }
        public bool Estado { get; set; }
        public bool EsReparado { get; set; }
    }
}
