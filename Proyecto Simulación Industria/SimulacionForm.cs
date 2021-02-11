﻿using Proyecto_Simulación_Industria.Entidades;
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
        double probabilidadDañarse1 = 0.025;

        public void Maquina1()
        {
            Random random = new Random();
            double Aleatorio;
            double metodoRecuperacion; //Determina entre el a y el B
            bool ExisteRepuesto = true;
            

            if (!maquina1.Estado)
            {
                tiempodañado1++;
                textBox1.Text = Convert.ToString(tiempodañado1);
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
                Maquina1richTextBox.Visible = false;
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
                    if(maquina1.HorasHabiles!=12)
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

            if (maquina1.Estado && Aleatorio < probabilidadDañarse1)
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
                
                AuxtextBox.Text = Convert.ToString(metodoRecuperacion);
                maquina1.EsReparado = true;
                if (metodoRecuperacion > 0.50)
                {
                    maquina1.HorasHabiles = 12;
                }
                else
                {
                    if(maquina1.ProduccionPorHora==50)
                        maquina1.ProduccionPorHora = Convert.ToInt32(maquina1.ProduccionPorHora * 1.20);
                }
                probabilidadDañarse1 += 0.0025;
            }
        }

        int tiempodañado2=0;
        double probabilidadDañarse2 = 0.01;
        public void Maquina2()
        {
            Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            double Aleatorio;
            double metodoRecuperacion; //Determina entre el a y el B
            bool ExisteRepuesto = random.NextDouble() >= 0.5;


            if (!maquina2.Estado)
            {
                tiempodañado2++;

                if (ExisteRepuesto)
                {
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
                Funcionando2PictureBox.Visible = true;
                Error2PictureBox.Visible = false;

                if (maquina2.ProduccionAtrasada == 0)
                {
                    maquina2.ProduccionPorHora = 40;
                    maquina2.HorasHabiles = 10;
                }
                else
                {
                    if (maquina2.HorasHabiles != 12)
                        maquina2.ProduccionAtrasada -= Convert.ToInt32(40*0.20);//Si esta produciendo el 20% mas ira restandlo
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
            {
                maquina2.EsReparado = true;
                if (metodoRecuperacion > 0.50)
                {
                    maquina2.HorasHabiles = 12;
                }
                else
                {
                    if (maquina2.ProduccionPorHora == 40)
                        maquina2.ProduccionPorHora = Convert.ToInt32(maquina2.ProduccionPorHora * 1.20);
                }
                probabilidadDañarse2 += 0.0025;
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
