using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using ObjTransfer;
using Negocios;

namespace WinForms
{
    public partial class FormServico : Form
    {
        Form1 form1 = new Form1();
        Thread thread;
        ClienteInfo infoCliente;
        ServicoInfo infoServ;
        FuncInfo responsavel;
        FuncColecao colecaofunc;
        ServicoColecao colecaoServ;
        ServicoIphoneInfo infoServIphone;
        ServicoIphoneColecao colecaoServIphone;
        IphoneCelularInfo infoCelular;

        ClienteNegocios negociosCliente = new ClienteNegocios(Form1.Empresa.empconexao);
        ServicoNegocio negocioServ = new ServicoNegocio(Form1.Empresa.empconexao);
        FuncNegocios negocioFunc = new FuncNegocios(Form1.Empresa.empconexao);

        int idSave;
        bool saved; //confirma se a OS foi salva, para quando fechar a janela atualizar a lista

        public FormServico(ClienteInfo cliente)
        {
            Inicializar();
            infoCliente = cliente;
            PreencherForm();
        }

        //public FormServico(ClienteInfo cliente, IphoneCelularInfo celular)
        //{
        //    Inicializar();
        //    infoCliente = cliente;
        //    infoCelular = celular;
        //    PreencherForm();
        //}

        public FormServico()
        {
            Inicializar();
        }

        private void Inicializar()
        {
            InitializeComponent();
            FormFormat formFormat = new FormFormat(this);
            formFormat.formatar();
            textBoxCaracteristica.CharacterCasing = CharacterCasing.Normal;
            colecaoServ = new ServicoColecao();
        }

        private void PreencherForm()
        {
            textBoxNome.Text = infoCliente.cliid + " - " + infoCliente.clinome;

            thread = new Thread(PreencherFormThread);
            form1.ExecutarThread(thread, progressBar1, labelBarra);

            if (colecaofunc.Count == 1)
            {
                responsavel = colecaofunc[0];
                textBoxCodTec.Text = string.Format("{0:000}", responsavel.funId);
                textBoxResponsavel.Text = responsavel.funNome;
            }
            else
                ConsultarResponsavel(colecaofunc);

            buttonAdd.Enabled = true;
            buttonAdd.Select();
            buttonCliente.Enabled = false;
        }

        private void PreencherFormThread()
        {
            colecaofunc = negocioFunc.ConsultarFuncTecnico();
            Form1.encerrarThread = true;
        }

        private void buttonFechar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AbrirDefeito();
        }

        private void AbrirDefeito()
        {
            FormProdutoDefeito formProdutoDefeito = new FormProdutoDefeito(infoCliente);
            if (formProdutoDefeito.ShowDialog(this) == DialogResult.Yes)
            {
                infoCelular = formProdutoDefeito.SelecionadoCelular;
                infoServIphone = formProdutoDefeito.SelecionandoDefeito;
                colecaoServIphone.Add(infoServIphone);

                textBoxObs.Text = infoServIphone.iphdefobs;
                textBoxDescricao.Text = infoCelular.ToString();
                textBoxDefeito.Text = infoServIphone.iphdefdefeito;
                textBoxCaracteristica.Text = infoServIphone.ToString();
                buttonAdd.Enabled = false;
                buttonSalvar.Enabled = true;
                buttonSalvar.Select();
                buttonCliente.Enabled = false;
            }
            formProdutoDefeito.Dispose();
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            if (FormMessage.ShowMessegeQuestion("Deseja salvar este registro?") == DialogResult.Yes)
            {
                groupBoxServico.Enabled = false;
                infoServ = new ServicoInfo
                {
                    serid = 000000000,
                    serdataagend = dateTimePickerData.Value.Date,
                    seridunid = Form1.Unidade.uniid,
                    seridfunc = Form1.User.useidfuncionario,
                    seridstatus = 1,
                    seridtec_resp = responsavel.funId,
                    seridaparelho = infoCelular.celid,
                    serdescricao = infoCelular.ToString(),
                    seridtipoaparelho = 1,
                };

                thread = new Thread(new ThreadStart(Salvar));
                form1.ExecutarThread(thread, progressBar1, labelBarra);
                PreencherGrid();
            }
        }

        private void Limpar()
        {
            infoServ = null;
            infoServIphone = null;
            idSave = 0;
            textBoxCaracteristica.Clear();
            textBoxDescricao.Clear();
            textBoxDefeito.Clear();
            textBoxObs.Clear();
            buttonAdd.Enabled = true;
        }

        private void Salvar()
        {
            idSave = negocioServ.InsertServico(infoServ);

            if (idSave > 0)
            {
                infoServ.serid = idSave;
                infoServIphone.iphdefidservico = idSave;
                negocioServ.InsertServicoIphone(infoServIphone);
                colecaoServ.Add(infoServ);
                colecaoServIphone.Add(infoServIphone);
                Form1.encerrarThread = true;
            }
        }

        private void PreencherGrid()
        {
            dataGridViewServico.DataSource = null;
            dataGridViewServico.DataSource = colecaoServ;

            Limpar();
            groupBoxServico.Enabled = true;
            buttonSalvar.Enabled = false;
            buttonImprimir.Visible = true;
            buttonImprimir.Select();
        }

        private void buttonImprimir_Click(object sender, EventArgs e)
        {
            Selecionado();

            if (infoServ != null)
            {
                //FormRelatorioFicha formRelatorioFicha = new FormRelatorioFicha(infoServ);
                //formRelatorioFicha.Show(this);
            }
        }

        private void Selecionado()
        {
            if (dataGridViewServico.SelectedRows.Count > 0)
                infoServ = (ServicoInfo)dataGridViewServico.SelectedRows[0].DataBoundItem;
        }
        
        private void buttonFechar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxTaxa_TextChanged(object sender, EventArgs e)
        {
            FormTextoFormat.MoedaFormat((TextBox)sender);
        }

        private void FrmServico_Load(object sender, EventArgs e)
        {
            buttonCliente.Select();
            colecaoServIphone = new ServicoIphoneColecao();
                colecaoServ = new ServicoColecao();

            if (infoCliente != null)
                AbrirDefeito();
        }

        private void buttonBuscarTec_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCodTec.Text))
            {
                ConsultarResponsavel(colecaofunc);
            }
            else
            {
                //responsavel = funcNegocios.ConsultarFuncPorId(ConvertNum(textBoxCodTec));

                if (responsavel != null)
                {
                    textBoxCodTec.Text = string.Format("{0:000}", responsavel.funId);
                    textBoxResponsavel.Text = responsavel.funNome;
                }
                else
                {
                    FormMessage.ShowMessegeWarning("Código inválido, tente outro!");
                    textBoxCodTec.Clear();
                    textBoxCodTec.Select();
                }
            }
        }

        private void ConsultarResponsavel(FuncColecao funcColecao)
        {
            Form_ConsultarColecao form_ConsultarColecao = new Form_ConsultarColecao();

            foreach (FuncInfo func in funcColecao)
            {
                Form_Consultar form_Consultar = new Form_Consultar
                {
                    Cod = string.Format("{0:000}", func.funId),
                    Descricao = func.funNome
                };

                form_ConsultarColecao.Add(form_Consultar);
            }

            AbrirFormConsultar(form_ConsultarColecao);
        }

        private void AbrirFormConsultar(Form_ConsultarColecao form_ConsultarColecao)
        {
            FormConsultar_Cod_Descricao formConsultar_Cod_Descricao = new FormConsultar_Cod_Descricao(form_ConsultarColecao, "Técnico Responsável");
            formConsultar_Cod_Descricao.ShowDialog(this);

            if (formConsultar_Cod_Descricao.DialogResult == DialogResult.Yes)
            {
                textBoxCodTec.Text = formConsultar_Cod_Descricao.Selecionado.Cod;
                textBoxResponsavel.Text = formConsultar_Cod_Descricao.Selecionado.Descricao;
                
            }
        }

        private void textBoxCodTec_TextChanged(object sender, EventArgs e)
        {
            textBoxResponsavel.Clear();
        }

        private void ButtonCliente_Click(object sender, EventArgs e)
        {
            ServicoInfo servico = new ServicoInfo();
            FormClienteConsultar formClienteConsultar = new FormClienteConsultar(true);
            formClienteConsultar.ShowDialog(this);
            formClienteConsultar.Dispose();

            if (formClienteConsultar.DialogResult == DialogResult.Yes)
            {
                infoCliente = formClienteConsultar.SelecionadoCliente;
                PreencherForm();
            }
        }

        private void TextBoxObs_Leave(object sender, EventArgs e)
        {
            buttonSalvar.Select();
        }

        private void FrmServico_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (saved)
                this.DialogResult = DialogResult.Yes;
        }
    }
}
