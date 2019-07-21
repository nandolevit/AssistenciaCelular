using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ObjTransfer;
using Negocios;
using System.Threading;

namespace WinForms
{
    public partial class FormServicoListar : Form
    {
        Form1 form1 = new Form1();
        Thread thread;
        bool selecionado;
        string palavraPesquisa;
        string status;
        string atend;
        string garant;
        DateTime dataIni;
        DateTime dataFim;
        ServicoNegocio servicoNegocio = new ServicoNegocio(Form1.Empresa.empconexao);
        
        public FormServicoListar()
        {
            InitializeComponent();
            FormFormat formFormat = new FormFormat(this);
            formFormat.formatar();
        }

        private void FormConsultarServico_Load(object sender, EventArgs e)
        {
            if (Form1.Offline)
                checkBoxDetalhada.Enabled = false;

            textBoxPesquisar.Select();
        }

        private void PreencherComboboxAtendente()
        {
            FuncNegocios funcNegocios = new FuncNegocios(Form1.Empresa.empconexao);
            FuncColecao colecao = new FuncColecao();
            FuncColecao funcColecao = funcNegocios.ConsultatFuncTotal();

            FuncInfo funcInfo = new FuncInfo
            {
                funId = 0,
                funNome = "*TODOS ATENDENTES*"
            };

            colecao.Add(funcInfo);

            foreach (FuncInfo func in funcColecao)
                colecao.Add(func);

            comboBoxAtendente.DisplayMember = "funnome";
            comboBoxAtendente.ValueMember = "funid";
            comboBoxAtendente.DataSource = colecao;
        }

        private void PreencherComboboxStatus()
        {
            //CodDescricaoColecao colecao = servicoNegocio.ConsultarStatus();
            CodDescricaoColecao codDescricao = new CodDescricaoColecao();

            CodDescricaoInfo descricao = new CodDescricaoInfo
            {
                cod = 0,
                descricao = "*TODOS STATUS*"
            };
            codDescricao.Add(descricao);
            //foreach (CodDescricaoInfo cod in colecao)
            //    codDescricao.Add(cod);
            
            comboBoxStatus.DisplayMember = "descricao";
            comboBoxStatus.ValueMember = "cod";
            comboBoxStatus.DataSource = codDescricao;
        }

        private void buttonFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxDetalhada_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDetalhada.Checked)
            {
                groupBoxDetalhada.Enabled = true;
                groupBoxTipo.Enabled = false;

                PreencherComboboxStatus();
                PreencherComboboxAtendente();

            }
            else
            {
                groupBoxDetalhada.Enabled = false;
                groupBoxTipo.Enabled = true;
            }
        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {

        }


        private void radioButtonOs_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPesquisar.Select();
            textBoxPesquisar.Clear();
        }

        private void FormConsultarServico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (checkBoxDetalhada.Checked)
                {

                }
                else
                {

                }
            }
        }

        private void buttonSelecionar_Click(object sender, EventArgs e)
        {
        }

        private void buttonPesquisarDetalhada_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(comboBoxStatus.SelectedValue) == 0)
                status = "%";
            else
                status = Convert.ToString(comboBoxStatus.SelectedValue);

            if (Convert.ToInt32(comboBoxAtendente.SelectedValue) == 0)
                atend = "%";
            else
                atend = Convert.ToString(comboBoxAtendente.SelectedValue);

            if (!checkBoxGarantia.Checked)
                garant = "%";
            else
                garant = "1";

            dataIni = dateTimePickerIni.Value;
            dataFim = dateTimePickerFim.Value;
        }

        private void checkBoxGarantia_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxAtendente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePickerIni_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePickerFim_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPesquisar_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonNome_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewConsultar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ButtonAdicionar_Click(object sender, EventArgs e)
        {
            FormServico formServico = new FormServico();
            if (formServico.ShowDialog(this) == DialogResult.Yes)
            {

            }
        }
    }
}
