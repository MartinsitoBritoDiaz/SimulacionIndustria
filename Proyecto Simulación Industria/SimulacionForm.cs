using Proyecto_Simulación_Industria.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Simulación_Industria
{
    public partial class SimulacionForm : Form
    {
        Pedido pedido = new Pedido();
        Maquinas maquina1 = new Maquinas(1, 50, 10, true, false, 0);
        
        Maquinas maquina2 = new Maquinas(1, 40, 10, true, false, 0);
        //maquinaId, ProduccionHora, HorasHabiles, Estado, EsReparado, ProdAtrasada
       
        public SimulacionForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }


        int i = 0;
        public void Ejecucion()
        {
            Random random = new Random();
            double aux;
            double metodoReparacion; //Determina entre el a y el B
            double probabilidadDañarse = 0.10;
           
            if (i < pedido.CantidadPedido)
            //for (int i = 0; i <= pedido.CantidadPedido; i++)
            {
                textBox1.Text = Convert.ToString(i);
                aux = random.NextDouble();
                metodoReparacion = random.NextDouble();

                if (aux < probabilidadDañarse)
                    maquina1.Estado = false;
                MateriaPrimatextBox.Text = maquina1.EsReparado.ToString();
                if (maquina1.Estado && maquina1.EsReparado==false) {
                    pedido.CantidadFabricada += maquina1.ProduccionPorHora;
                    Produccion1Label.Text = Convert.ToString(pedido.CantidadFabricada);
                    
                }
                else
                    if (!maquina1.EsReparado)
                    {
                        maquina1.EsReparado = true;
                        if (metodoReparacion > 0.50)
                        {
                            probabilidadDañarse -= 0.025;
                            maquina1.ProduccionPorHora += Convert.ToInt32(maquina1.ProduccionPorHora * 1.20);
                    }
                        else
                        {
                            probabilidadDañarse -= 0.05;
                            maquina1.HorasHabiles += 2;
                        }
                    }
                i++;
            }

            
        }

        private void IniciarButton_Click(object sender, EventArgs e)
        {
            if (PedidoTextBox.Text == null)
                pedido.CantidadPedido = 0;

            //  Ejecucion();
            timer.Enabled = true;
       
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (PedidoTextBox.Text == null)
                pedido.CantidadPedido = 0;

            pedido.CantidadPedido = Convert.ToInt32(PedidoTextBox.Text);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            
            Ejecucion();
            
        }
    }
}
