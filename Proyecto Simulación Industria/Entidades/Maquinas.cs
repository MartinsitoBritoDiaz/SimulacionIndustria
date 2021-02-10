using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Simulación_Industria.Entidades
{
    public class Maquinas
    {
        public int MaquinaId { get; set; }
        public int ProduccionPorHora { get; set; }
        public int HorasHabiles { get; set; }  //recomendacion para si trabaja las 10 o extras
        public bool Estado { get; set; }
        public bool EsReparado { get; set; }

        public int ProduccionAtrasada { get; set; }

        public Maquinas(int maquinaId, int produccionPorHora, int horasHabiles, bool estado, bool esReparado, int produccionAtrasada)
        {
            MaquinaId = maquinaId;
            ProduccionPorHora = produccionPorHora;
            HorasHabiles = horasHabiles;
            Estado = estado;
            EsReparado = esReparado;
            ProduccionAtrasada = produccionAtrasada;
        }
    }
}
