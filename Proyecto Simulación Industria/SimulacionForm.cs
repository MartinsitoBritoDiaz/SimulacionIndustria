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
        //maquinaId, ProduccionHora, HorasHabiles, Estado, EsReparado, ProdAtrasada}

        int ProducidoMaquina1 = 0;
        int ProducidoMaquina2 = 0;

        int HorasTrabjadas = 0;
        int diasTrabajado = 0;
        int mesesTrabajados = 0;

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
        int tiempodañado1 = 0;
        public void Maquina1()
        {
            Random random = new Random();
            double aux;
            double metodoReparacion; //Determina entre el a y el B
            double probabilidadDañarse = 0.10;

            if (!maquina1.Estado)
            {
                tiempodañado1++;
                textBox1.Text = Convert.ToString(tiempodañado1);
                if (tiempodañado1 == 20)
                {
                    maquina1.Estado = true;
                    tiempodañado1 = 0;

                }

            }

            if (maquina1.Estado)
            {
                Funcionando1PictureBox.Visible = true;
                Error1PictureBox.Visible = false;
            }
            else
            {
                Funcionando1PictureBox.Visible = false;
                Error1PictureBox.Visible = true;

            }



            aux = random.NextDouble();
            metodoReparacion = random.NextDouble();

            if (maquina1.Estado && aux < probabilidadDañarse)
                maquina1.Estado = false;

            MateriaPrimatextBox.Text = maquina1.Estado.ToString(); //quitar luego

            if (maquina1.Estado)
            {
                ProducidoMaquina1 += maquina1.ProduccionPorHora;
                pedido.CantidadFabricada += maquina1.ProduccionPorHora;

                Produccion1Label.Text = Convert.ToString(ProducidoMaquina1);
                TotalProducidoTextbox.Text = Convert.ToString(pedido.CantidadFabricada);
            }
            else
                if (maquina1.EsReparado == false)
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
        }

        int tiempodañado2=0;
        public void Maquina2()
        {
            Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            double aux;
            double metodoReparacion; //Determina entre el a y el B
            double probabilidadDañarse2 = 0.10;


            if (!maquina2.Estado)
            {
                tiempodañado2++;
                MateriaPrimatextBox.Text = Convert.ToString(tiempodañado2);
                if (tiempodañado2 == 20)
                {
                    maquina2.Estado = true;
                    tiempodañado2 = 0;

                }
            }
           

            if (maquina2.Estado)
            {
                Funcionando2PictureBox.Visible = true;
                Error2PictureBox.Visible = false;
            }
            else
            {
                Funcionando2PictureBox.Visible = false;
                Error2PictureBox.Visible = true;
            }

            

            aux = random.NextDouble();
                metodoReparacion = random.NextDouble();

                if (aux < probabilidadDañarse2)
                    maquina2.Estado = false;

                textBox5.Text = maquina2.EsReparado.ToString(); //quitar luego

                if (maquina2.Estado)
                {
                    ProducidoMaquina2 += maquina2.ProduccionPorHora;
                    pedido.CantidadFabricada += maquina2.ProduccionPorHora;

                    Produccion2Label.Text = Convert.ToString(ProducidoMaquina2);
                    TotalProducidoTextbox.Text = Convert.ToString(pedido.CantidadFabricada);
                }
                else
                    if (maquina2.EsReparado == false)
                    {
                        maquina2.EsReparado = true;
                        if (metodoReparacion > 0.50)
                        {
                            probabilidadDañarse2 -= 0.025;
                            maquina2.ProduccionPorHora += Convert.ToInt32(maquina2.ProduccionPorHora * 1.20);
                        }
                        else
                        {
                            probabilidadDañarse2 -= 0.05;
                            maquina2.HorasHabiles += 2;
                        }
                    }


              
            
        }

       
        public void Ejecucion()
        {

            HorasTrabjadas++; //aumenta 1 hora

            if (HorasTrabjadas == 10)
            {
                diasTrabajado++;
                HorasTrabjadas= 0;

                if (diasTrabajado == 30)
                {
                    mesesTrabajados++;
                    MesesTrabajadostextBox.Text= Convert.ToString(mesesTrabajados);
                }
                    
            }
            HorasTrabajadastextBox.Text = Convert.ToString(HorasTrabjadas);
            DiastrabajadostextBox.Text = Convert.ToString(diasTrabajado);

            if (i <= pedido.CantidadPedido)
            //for (int i = 0; i <= pedido.CantidadPedido; i++)
            {
                Maquina1();

                ///////////////////////////////////////////////////////////////////////////////////////////


                Maquina2();





                i++;
            }
            else
            {
                timer.Stop();
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

        private void SimulacionForm_Load(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
