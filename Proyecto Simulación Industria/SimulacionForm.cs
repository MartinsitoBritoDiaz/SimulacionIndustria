using Proyecto_Simulación_Industria.Entidades;
using System;
using System.Windows.Forms;

namespace Proyecto_Simulación_Industria
{
    public partial class SimulacionForm : Form
    {
        int materiaPrima = 2000;

        Pedido pedido = new Pedido();
        Maquinas maquina1 = new Maquinas(1, 50, 10, true, false, 0);
        
        Maquinas maquina2 = new Maquinas(1, 40, 10, true, false, 0);
        //maquinaId, ProduccionHora, HorasHabiles, Estado, EsReparado, ProdAtrasada}

        int TotalProducido = 0;

        bool horasNormales;
        bool horasExtras;

        int ProducidoMaquina1 = 0;
        int ProducidoMaquina2 = 0;

        int HorasTrabjadas = 0;
        int diasTrabajado = 0;
        int mesesTrabajados = 0;


        public void horario() //para determinar si ninguna maquina trabaja horas extras, para asi los diaas duren mas
        {
            if (maquina1.HorasHabiles == 10 && maquina2.HorasHabiles == 10)
            {
                horasNormales = true;
            }
            else
            {
                if (maquina1.HorasHabiles == 10 && maquina2.HorasHabiles == 10)
                {
                    horasNormales = false;
                    horasExtras = true;
                }
            }
               
        }

        public SimulacionForm()
        {
            InitializeComponent();
            pictureBox1.Visible = false;
        }


        int i = 0;
        int tiempodañado1 = 0;
        double probabilidadDañarse1 = 0.01;

        public void Maquina1()
        {
            Random random = new Random();
            double Aleatorio;
            double metodoRecuperacion; //Determina entre el a y el B
            bool ExisteRepuesto = true;
            

            if (!maquina1.Estado)
            {
                tiempodañado1++;
                if (ExisteRepuesto)
                {
                    Maquina1richTextBox.Visible = true;
                    Maquina1richTextBox.Text = "La reparacion de la maquina demorará 2 dias";
                    if (horasNormales && tiempodañado1 == 20)
                    {
                        maquina1.Estado = true;
                        tiempodañado1 = 0;
                    }
                    else
                       if (horasExtras && tiempodañado1 == 24)
                        {
                            maquina1.Estado = true;
                            tiempodañado1 = 0;
                        }
                }
                else
                {
                    Maquina1richTextBox.Visible = true;
                    Maquina1richTextBox.Text = "No hay repuestos disponibles, la reparacion de la maquina demorará 3 dias";
                    if (horasNormales && tiempodañado1 == 30)
                    {
                        maquina1.Estado = true;
                        tiempodañado1 = 0;
                    }
                    else
                       if (horasExtras && tiempodañado1 == 36)
                        {
                            maquina1.Estado = true;
                            tiempodañado1 = 0;
                        }
                }
                
                maquina1.ProduccionAtrasada += 50;
            }


            if (maquina1.Estado)
            {
                maquina1.Reparando = false;
                Maquina1richTextBox.Visible = false;
                MetodoRecuperacion1label.Visible = false;
                ExisteRepuesto = random.NextDouble() >= 0.5;
                Funcionando1PictureBox.Visible = true;
                Error1PictureBox.Visible = false;

                if (maquina1.ProduccionAtrasada == 0)
                {
                    maquina1.ProduccionPorHora = 50;
                    maquina1.HorasHabiles = 10;
                }
                else
                {
                    if(maquina1.HorasHabiles==12 || maquina1.ProduccionPorHora > 50)
                        maquina1.ProduccionAtrasada -= 10;//Si esta produciendo el 20% mas ira restando los 10 de mas que produce
                }

                HorasATrabajar1label.Text = Convert.ToString(maquina1.HorasHabiles);
                ProduccionHora1label.Text = Convert.ToString(maquina1.ProduccionPorHora);
                ProduccionAtrasada1label.Text= Convert.ToString(maquina1.ProduccionAtrasada);
            }
            else
            {
                Funcionando1PictureBox.Visible = false;
                Error1PictureBox.Visible = true;
            }

            Aleatorio = random.NextDouble();
            metodoRecuperacion = random.NextDouble();

            if (maquina1.Estado)
                if(probabilidadDañarse1 > Aleatorio)
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
            {
                if (!maquina1.Reparando)
                {
                    maquina1.Reparando = true;
                    if (metodoRecuperacion > 0.50)
                    {
                        MetodoRecuperacion1label.Visible = true;
                        MetodoRecuperacion1label.Text = "Aumento Horas";
                        maquina1.HorasHabiles = 12;
                        probabilidadDañarse1 += 0.001;
                    }
                    else
                    {
                        MetodoRecuperacion1label.Visible = true;
                        MetodoRecuperacion1label.Text = "Aumento Producción";

                        if (maquina1.ProduccionPorHora == 50)
                            maquina1.ProduccionPorHora = Convert.ToInt32(maquina1.ProduccionPorHora * 1.20);
                        probabilidadDañarse1 += 0.001;
                    }
                }
                
            }
        }

        int tiempodañado2=0;
        double probabilidadDañarse2 = 0.01;
        public void Maquina2()
        {
            Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            double Aleatorio;
            double metodoRecuperacion; //Determina entre el a y el B
            bool ExisteRepuesto = true;


            if (!maquina2.Estado)
            {
                tiempodañado2++;

                if (ExisteRepuesto)
                {
                    Maquina2richTextBox.Visible = true;
                    Maquina2richTextBox.Text = "La reparacion de la maquina demorará 2 dias";
                    if (horasNormales && tiempodañado2 == 20)
                    {
                        maquina2.Estado = true;
                        tiempodañado2 = 0;
                    }
                    else
                       if (horasExtras && tiempodañado2 == 24)
                        {
                            maquina2.Estado = true;
                            tiempodañado2 = 0;
                        }
                }
                else
                {
                    Maquina2richTextBox.Visible = true;
                    Maquina2richTextBox.Text = "No hay repuestos disponibles, la reparacion de la maquina demorará 3 dias";
                    if (horasNormales && tiempodañado2 == 30)
                    {
                        maquina2.Estado = true;
                        tiempodañado2 = 0;
                    }
                    else
                       if (horasExtras && tiempodañado2 == 36)
                        {
                            maquina2.Estado = true;
                            tiempodañado2 = 0;
                        }
                }
               
                maquina2.ProduccionAtrasada += 40;
            }

            if (maquina2.Estado)
            {
                maquina2.Reparando = false;
                Maquina2richTextBox.Visible = false;
                MetodoRecuperacion2label.Visible = false;   
                Funcionando2PictureBox.Visible = true;
                Error2PictureBox.Visible = false;

                if (maquina2.ProduccionAtrasada <= 0)
                {
                    maquina2.ProduccionPorHora = 40;
                    maquina2.HorasHabiles = 10;
                    maquina2.ProduccionAtrasada = 0;
                }
                else
                {
                    if (maquina2.HorasHabiles == 12 || maquina2.ProduccionPorHora > 40)
                        maquina2.ProduccionAtrasada -= 8;//Si esta produciendo el 20% mas ira restandlo
                }

                HorasATrabajar2label.Text = Convert.ToString(maquina2.HorasHabiles);
                ProduccionHora2label.Text = Convert.ToString(maquina2.ProduccionPorHora);
                ProduccionAtrasada2label.Text = Convert.ToString(maquina2.ProduccionAtrasada);
            }
            else
            {
                Funcionando2PictureBox.Visible = false;
                Error2PictureBox.Visible = true;
            }

            Aleatorio = random.NextDouble();
            metodoRecuperacion = random.NextDouble();

            if (maquina2.Estado)
                if (probabilidadDañarse2 > Aleatorio)
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
            {
                if (!maquina2.Reparando)
                {
                    maquina2.Reparando = true;
                    if (metodoRecuperacion > 0.50)
                    {
                        MetodoRecuperacion2label.Visible = true;
                        MetodoRecuperacion2label.Text = "Aumento horas";
                        maquina2.HorasHabiles = 12;

                        probabilidadDañarse2 += 0.001;
                    }
                    else
                    {
                        MetodoRecuperacion2label.Visible = true;
                        MetodoRecuperacion2label.Text = "Aumento Producción";

                        if (maquina2.ProduccionPorHora == 40)
                            maquina2.ProduccionPorHora = Convert.ToInt32(maquina2.ProduccionPorHora * 1.20);

                        probabilidadDañarse2 += 0.001;
                    }
                }
            }
        }

       
        public void Ejecucion()
        {
            horario();
            HorasTrabjadas++; //aumenta 1 hora

            if (HorasTrabjadas == 10)
            {
                if(horasNormales)
                {
                    diasTrabajado++;
                    HorasTrabjadas = 0;
                }
                else
                {
                    if (horasExtras)
                        diasTrabajado++;
                        HorasTrabjadas = 0;
                }

                

            }

            if (diasTrabajado == 30)
            {
                mesesTrabajados++;
                MesesTrabajadostextBox.Text = Convert.ToString(mesesTrabajados);
                diasTrabajado = 0;
            }


            HorasTrabajadastextBox.Text = Convert.ToString(HorasTrabjadas);
            DiastrabajadostextBox.Text = Convert.ToString(diasTrabajado);

            if ((pedido.CantidadPedido > pedido.CantidadFabricada))
            {

                Maquina1();
                ///////////////////////////////////////////////////////////////////////////////////////////

                Maquina2();

                if(maquina1.Estado == true || maquina2.Estado == true)
                {
                    MateriaPrimatextBox.Text = Convert.ToString(materiaPrima);

                    if (materiaPrima <= 0)
                    {
                        timer.Stop();
                        materiaPrima = 0;
                        MessageBox.Show("Se esta suministrando materia prima!!");
                        materiaPrima = 2000;
                        timer.Enabled = true;
                    }

                    if (maquina1.Estado)
                        materiaPrima -= 30;

                    if (maquina2.Estado)
                        materiaPrima -= 20;
                }

                i++;
            }
            else
            {
                timer.Stop();
                TotalProducido = 0;
                MessageBox.Show("Pedido finalizado!!");
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
            PedidoTextBox.Focus();
        }

       
    }
}
