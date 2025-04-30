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

            // Asegurarse de que la columna "TipoDeEntrega" se muestre
            if (!dgvPedidos.Columns.Contains("TipoDeEntrega"))
            {
                dgvPedidos.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    HeaderText = "Tipo de Entrega",
                    DataPropertyName = "TipoDeEntrega"
                });
            }

            // Ocultar la columna del objeto MetodoEntrega si se muestra
            if (dgvPedidos.Columns.Contains("MetodoEntrega"))
            {
                dgvPedidos.Columns["MetodoEntrega"].Visible = false;
            }
        }


        private void HistorialPedidosForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            // Punto de interrupción 1: Inspeccionar tipoFiltro y los pedidos
            string tipoFiltro = cmbFiltroEntrega.SelectedItem?.ToString();
            System.Diagnostics.Debug.WriteLine($"Filtro seleccionado: {tipoFiltro}");
            System.Diagnostics.Debug.WriteLine($"Número de pedidos en el registro: {RegistroPedidos.Instancia.Pedidos.Count}");
            foreach (var pedido in RegistroPedidos.Instancia.Pedidos)
            {
                System.Diagnostics.Debug.WriteLine($"Tipo de entrega del pedido: {pedido.MetodoEntrega?.TipoEntrega()}");
            }

            if (tipoFiltro == "Todos")
            {
                dgvPedidos.DataSource = RegistroPedidos.Instancia.Pedidos;
            }
            else if (!string.IsNullOrEmpty(tipoFiltro))
            {
                // Punto de interrupción 2: Inspeccionar la comparación dentro del Where
                dgvPedidos.DataSource = RegistroPedidos.Instancia.Pedidos
                    .Where(p =>
                    {
                        bool match = (p.MetodoEntrega?.TipoEntrega() == tipoFiltro);
                        System.Diagnostics.Debug.WriteLine($"Comparando: '{p.MetodoEntrega?.TipoEntrega()}' con '{tipoFiltro}', Resultado: {match}");
                        return match;
                    })
                    .ToList();

            }
    }

        private void cmbFiltroEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}


