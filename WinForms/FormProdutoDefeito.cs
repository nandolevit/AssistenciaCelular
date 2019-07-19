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
    public partial class FormProdutoDefeito : Form
    {
        Form1 form1 = new Form1();
        ServicoNegocio negocioServ = new ServicoNegocio(Form1.Empresa.empconexao);
        //public string DefeitoInfo { get; set; }

        Thread thread;
        ClienteInfo infoCliente;
        AparelhoColecao colecaoAparelho;
        //IphoneCelularInfo infoCelular;
        IphoneDefeitoInfo infoDefeito;
        AparelhoInfo infoAparelho;

        public IphoneDefeitoInfo SelecionandoDefeito { get; set; }
        public AparelhoInfo SelecionadoAparelho { get; set; }

        public FormProdutoDefeito(ClienteInfo cliente)
        {
            Inicializar();
            infoCliente = cliente;

            thread = new Thread(ConsultarAparelhoCliente);
            form1.ExecutarThread(thread, progressBar1, labelBarra);

        }

        //public FormProdutoDefeito(ClienteInfo cliente, IphoneCelularInfo phone)
        //{
        //    Inicializar();
        //    infoCelular = phone;
        //    infoCliente = cliente;

        //    thread = new Thread(ConsultarAparelho);
        //    form1.ExecutarThread(thread, progressBar1, labelBarra);

        //    if (infoCelular != null)
        //    {
        //        textBoxCodProd.Text = string.Format("{0:0000}", infoAparelho.apaid);
        //        textBoxProdDescricao.Text = infoAparelho.apadescricao;
        //    }
        //}

        private void Inicializar()
        {
            InitializeComponent();
            FormFormat formFormat = new FormFormat(this);
            formFormat.formatar();
            this.FormBorderStyle = FormBorderStyle.None;
            this.AcceptButton = buttonSalvar;
            textBoxCodProd.Select();

        }

        private void ConsultarAparelhoCliente()
        {
            colecaoAparelho = negocioServ.ConsultarAparelhoClienteId(infoCliente.cliid);
            textBoxDefeito.Select();
            Form1.encerrarThread = true;
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(textBoxDefeito.Text.Trim()) || string.IsNullOrEmpty(textBoxCodProd.Text)))
            {
                Salvar();
            }
            else
                FormMessage.ShowMessegeWarning("Informar o eletrodoméstico e o defeito do aparelho!");
        }

        private void buttonFechar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FormProdutoDefeito_Load(object sender, EventArgs e)
        {
            if (colecaoAparelho != null)
            {
                if (colecaoAparelho.Count > 1)
                    AbrirListaAparelho();
                else
                {
                    infoAparelho = colecaoAparelho[0];
                    textBoxCodProd.Text = string.Format("{0:0000}", colecaoAparelho[0].apaid);
                    textBoxProdDescricao.Text = colecaoAparelho[0].apadescricao;
                }
            }
            else
            {
                FormIphoneModelo formIphoneModelo = new FormIphoneModelo(infoCliente);
                if(formIphoneModelo.ShowDialog(this) == DialogResult.Yes)
                    infoAparelho = negocioServ.ConsultarAparelhoId(formIphoneModelo.SelecionadoIphone.celid);

                textBoxCodProd.Text = string.Format("{0:00000}", infoAparelho.apaidaparelho);
                textBoxProdDescricao.Text = infoAparelho.apadescricao;
                formIphoneModelo.Dispose();
            }
        }

        private void AbrirListaAparelho()
        {
            Form_ConsultarColecao colecao = new Form_ConsultarColecao();
            foreach (AparelhoInfo aparelho in colecaoAparelho)
            {
                Form_Consultar form_Consultar = new Form_Consultar
                {
                    Cod = string.Format("{0:00000}", aparelho.apaid),
                    Descricao = aparelho.apadescricao
                };

                colecao.Add(form_Consultar);
            }

            FormConsultar_Cod_Descricao formConsultar_Cod_Descricao = new FormConsultar_Cod_Descricao(colecao, "Aparelho");
            formConsultar_Cod_Descricao.ShowDialog(this);
            formConsultar_Cod_Descricao.Dispose();

            if (formConsultar_Cod_Descricao.DialogResult == DialogResult.Yes)
            {
                Form_Consultar consultar = formConsultar_Cod_Descricao.Selecionado;
                textBoxCodProd.Text = consultar.Cod;
                textBoxProdDescricao.Text = consultar.Descricao;
                infoAparelho = negocioServ.ConsultarAparelhoId(Convert.ToInt32(consultar.Cod));
                SelecionadoAparelho = infoAparelho;
            }
        }

        private void Salvar()
        {
            PreencherDefeito();
            ExecutarSalvar();
        }

        private void ExecutarSalvar()
        {
            if (negocioServ.InsertIphoneDefeito(infoDefeito) > 0)
            {
                SelecionadoAparelho = infoAparelho;
                SelecionandoDefeito = infoDefeito;
                this.DialogResult = DialogResult.Yes;
            }
            else
                FormMessage.ShowMessegeWarning("Falha ao tentar salvar, tente novamente!");
        }

        private void PreencherDefeito()
        {
            infoDefeito = new IphoneDefeitoInfo
            {
                iphdefautofrontal = textBoxAutoFrontal.Text,
                iphdefautointerno = textBoxAutoInterno.Text,
                iphdefcamfrontal = textBoxCamFrontal.Text,
                iphdefcamtraseira = textBoxCamTraseira.Text,
                iphdefcarcaca = textBoxCarcaca.Text,
                iphdefconector = textBoxConector.Text,
                iphdefdefeito = textBoxDefeito.Text,
                iphdefflash = textBoxFlash.Text,
                iphdeffone = textBoxFone.Text,
                iphdefhome = textBoxHome.Text,
                iphdefid = 0,
                iphdefidaparelho = infoAparelho.apaid,
                iphdefmicrofone = textBoxMicro.Text,
                iphdefmicrofonetraseiro = textBoxMicroTraseira.Text,
                iphdefobs = textBoxObs.Text,
                iphdefparafuso = textBoxParafuso.Text,
                iphdefsensorprox = textBoxSensor.Text,
                iphdeftouchdisplay = textBoxDisplay.Text,
                iphdefbandeja = textBoxBandeja.Text,
                iphdefdesligar = textBoxDesligar.Text,
                iphdefsilencioso = textBoxSilencioso.Text,
                iphdefvolume = textBoxVolume.Text
            };
        }

        private void ButtonBuscarAparelho_Click(object sender, EventArgs e)
        {
            AbrirListaAparelho();
        }
    }
}
