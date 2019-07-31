using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Negocios;
using ObjTransfer;

namespace WinForms
{
    public partial class FormFuncConsultar : Form
    {
        FuncNegocios funcNegocios = new FuncNegocios(Form1.Empresa.empconexao);
        
        public FormFuncConsultar()
        {
            InitializeComponent();
            FormFormat formFormat = new FormFormat(this);
            formFormat.formatar();
            PreencherGrid();
        }

        private void PreencherGrid()
        {
            PessoaColecao funcColecao = new PessoaColecao();

            funcColecao = funcNegocios.ConsultarPessoaPorTipo(EnumPessoaTipo.Funcionario);

            dataGridViewFunc.DataSource = null;
            dataGridViewFunc.DataSource = funcColecao;
        }
    }
}
