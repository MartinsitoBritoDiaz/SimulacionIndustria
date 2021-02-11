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
    public partial class InicioForm : Form
    {
        public InicioForm()
        {
            InitializeComponent();
        }

        private void InicioForm_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void InicioButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Este es un simulador de una industria que posee dos maquinas, la primera maquina produce 50 productos por hora, la segunda maquina produce 40 productos por hora. Estas maquinas tienen una posibilidad de dañarse de 1 porciento");
           
            SimulacionForm Simulacion = new SimulacionForm();
            Simulacion.Show();
        }
    }
}
