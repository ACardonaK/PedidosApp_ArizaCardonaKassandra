using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PedidosApp
{
    public partial class HistorialPedidosForm : Form
    {
        public HistorialPedidosForm()
        {
            InitializeComponent();
            CargarHistorialPedidos();
        }

        private void CargarHistorialPedidos()
        {
            dgvPedidos.DataSource = RegistroPedidos.Instancia.Pedidos;
        }


        private void HistorialPedidosForm_Load(object sender, EventArgs e)
        {
            InitializeComponent();
            cmbFiltroEntrega.Items.Add("Todos");
            cmbFiltroEntrega.Items.Add("Dron");
            cmbFiltroEntrega.Items.Add("Motocicleta");
            cmbFiltroEntrega.Items.Add("Camión");
            cmbFiltroEntrega.Items.Add("Bicicleta");
            cmbFiltroEntrega.SelectedIndex = 0; // Seleccionar "Todos" por defecto
            CargarHistorialPedidos();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string tipoFiltro = cmbFiltroEntrega.SelectedItem?.ToString();

            if (tipoFiltro == "Todos")
            {
                dgvPedidos.DataSource = RegistroPedidos.Instancia.Pedidos;
            }
            else if (!string.IsNullOrEmpty(tipoFiltro))
            {
                dgvPedidos.DataSource = RegistroPedidos.Instancia.Pedidos
                    .Where(p => p.MetodoEntrega.TipoEntrega() == tipoFiltro)
                    .ToList();
            }

        }
    }
}
