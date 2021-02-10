using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Simulación_Industria.Entidades
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public int CantidadPedido { get; set; }
        public int CantidadFabricada { get; set; } = 0; //Lo que se ha hecho
    }
}
