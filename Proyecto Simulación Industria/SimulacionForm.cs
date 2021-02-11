using Proyecto_Simulación_Industria.Entidades;
using System;
using System.Windows.Forms;

namespace Proyecto_Simulación_Industria
{
    public partial class SimulacionForm : Form
    {
        Pedido pedido = new Pedido();
        Maquinas maquina1 = new Maquinas(1, 50, 10, true, false, 0);
        
        Maquinas maquina2 = new Maquinas(1, 40, 10, true, false, 0);
        //maquinaId, ProduccionHora, HorasHabiles, Estado, EsReparado, ProdAtrasada}

        int TotalProducido = 0;
        int ProducidoRetrasoMaquina1 = 1000;
        int ProducidoRetrasodaMaquina2 = 800;

        int ProducionInicialMaquina2 = 40; 
     

        int ProducidoMaquina1 = 0;
        int ProducidoMaquina2 = 0;

        int HorasTrabjadas = 0;
        int diasTrabajado = 0;
        int mesesTrabajados = 0;

        public SimulacionForm()
        {
            InitializeComponent();
            pictureBox1.Visible = false;
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
            double Aleatorio;
            double metodoReparacion; //Determina entre el a y el B
            double probabilidadDañarse = 0.025;

            int contadorHorasAumentado = 0; //Contador para saber la cantidad de horas que han pasado luego que se reparo


            if (!maquina1.Estado)
            {
                tiempodañado1++;
                textBox1.Text = Convert.ToString(tiempodañado1);

                if (tiempodañado1 == 20)
                {
                    maquina1.Estado = true;
                    tiempodañado1 = 0;
                }
                maquina1.ProduccionAtrasada += 50;
            }


            if (maquina1.Estado)
            {
                Funcionando1PictureBox.Visible = true;
                Error1PictureBox.Visible = false;
                

                
               
                if (maquina1.ProduccionAtrasada == 0)
                {
                    maquina1.ProduccionPorHora = 50;
                    maquina1.HorasHabiles = 10;
                }
                else
                {
                    if(maquina1.HorasHabiles!=12)
                        maquina1.ProduccionAtrasada -= 10;//Si esta produciendo el 20% mas ira restando los 10 de mas que produce
                }
                

                ProduccionHora1label.Text = Convert.ToString(maquina1.ProduccionPorHora);
                ProduccionAtrasada1label.Text= Convert.ToString(maquina1.ProduccionAtrasada);
            }
            else
            {
                Funcionando1PictureBox.Visible = false;
                Error1PictureBox.Visible = true;
            }

            Aleatorio = random.NextDouble();
            metodoReparacion = random.NextDouble();

            if (maquina1.Estado && Aleatorio < probabilidadDañarse)
                maquina1.Estado = false;


            if (maquina1.Estado)
            {
                ProducidoMaquina1 += maquina1.ProduccionPorHora;
                pedido.CantidadFabricada += maquina1.ProduccionPorHora;

                Produccion1Label.Text = Convert.ToString(ProducidoMaquina1);
                TotalProducido = pedido.CantidadFabricada;
                TotalProducidoTextbox.Text = Convert.ToString(TotalProducido);
            }
            else
                if (!maquina1.EsReparado)
                {
                    maquina1.EsReparado = true;
                    if (metodoReparacion > 0.50)
                    {
                        probabilidadDañarse += 0.01;
                        maquina1.HorasHabiles += 2;
                    }
                    else
                    {
                        probabilidadDañarse += 0.01;
                        maquina1.ProduccionPorHora = Convert.ToInt32(maquina1.ProduccionPorHora * 1.20);
                        contadorHorasAumentado = 0;

                    }
            }

            contadorHorasAumentado++;
        }

        int tiempodañado2=0;
        public void Maquina2()
        {
            Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            double Aleatorio;
            double metodoReparacion; //Determina entre el a y el B
            double probabilidadDañarse2 = 0.025;

            int contadorHorasAumentado = 0; //Contador para saber la cantidad de horas que han pasado luego que se reparo

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

                if (40 < maquina2.ProduccionPorHora)
                {
                    if (contadorHorasAumentado == 100)
                        maquina2.ProduccionPorHora = 40;
                }
            }
            else
            {
                Funcionando2PictureBox.Visible = false;
                Error2PictureBox.Visible = true;
            }

            Aleatorio = random.NextDouble();
            metodoReparacion = random.NextDouble();

            if (maquina1.Estado && Aleatorio < probabilidadDañarse2)
                maquina2.Estado = false;


            if (maquina2.Estado)
            {
                ProducidoMaquina2 += maquina2.ProduccionPorHora;
                pedido.CantidadFabricada += maquina2.ProduccionPorHora;

                Produccion2Label.Text = Convert.ToString(ProducidoMaquina2);
                TotalProducido = pedido.CantidadFabricada;
                TotalProducidoTextbox.Text = Convert.ToString(TotalProducido);
            }
            else
                if (maquina2.EsReparado == false)
                    {
                        maquina2.EsReparado = true;
                        if (metodoReparacion > 0.50)
                        {
                            probabilidadDañarse2 += 0.01;
                            maquina2.HorasHabiles += 2;
                        }
                        else
                        {
                            probabilidadDañarse2 += 0.01;
                            maquina2.ProduccionPorHora = Convert.ToInt32(maquina2.ProduccionPorHora * 1.20);
                            contadorHorasAumentado = 0;
                        }
                    }
            contadorHorasAumentado++;
        }

       
        public void Ejecucion()
        {
            HorasTrabjadas++; //aumenta 1 hora

            if (HorasTrabjadas == 10)
            {
                if(maquina1.HorasHabiles == 10 && maquina2.HorasHabiles == 10)
                {
                    diasTrabajado++;
                    HorasTrabjadas = 0;
                }
                else
                {
                    if (maquina1.HorasHabiles == 12 || maquina2.HorasHabiles == 12)
                        diasTrabajado++;
                        HorasTrabjadas = 0;
                }

                if (diasTrabajado == 30)
                {
                    mesesTrabajados++;
                    MesesTrabajadostextBox.Text = Convert.ToString(mesesTrabajados);
                    diasTrabajado = 0;
                }

            }


            HorasTrabajadastextBox.Text = Convert.ToString(HorasTrabjadas);
            DiastrabajadostextBox.Text = Convert.ToString(diasTrabajado);

            if ((pedido.CantidadPedido > pedido.CantidadFabricada))
            {
                Maquina1();

                ///////////////////////////////////////////////////////////////////////////////////////////

                Maquina2();

                i++;
            }
            else
            {
                timer.Stop();
                TotalProducido = 0;
            }


        }

        private void IniciarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PedidoTextBox.Text))
            {
                pedido.CantidadPedido = 0;
            }
            else
            {
                pedido.CantidadPedido = Convert.ToInt32(PedidoTextBox.Text);
                pictureBox1.Visible = true;

                timer.Enabled = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (PedidoTextBox.Text == null)
            {
                pedido.CantidadPedido = 0;
            }
            else
                pedido.CantidadPedido = Convert.ToInt32(PedidoTextBox.Text);

            timer.Enabled = true;
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
