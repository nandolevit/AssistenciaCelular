using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Negocios;
using ObjTransfer;

namespace WinForms
{
    public partial class FormIphoneModelo : Form
    {
        Thread thread;
        Form1 form1 = new Form1();
        IphoneModeloColecao colecaoIphone;
        IphoneModeloColecao colecaoIpad;
        IphoneModeloInfo infoIphone;
        ServicoNegocio negocioServ = new ServicoNegocio(Form1.Empresa.empconexao);
        IphoneModeloCorColecao colecaoCor;
        IphoneModeloCorColecao colecaoCorSelecionada;
        CodDescricaoColecao colecaoCores;
        ClienteInfo infoCliente;
        IphoneCelularInfo infoCelular;
        public IphoneCelularInfo SelecionadoIphone { get; set; }
        int cod = 0;
       
        public FormIphoneModelo()
        {
            Inicializar();
        }

        public FormIphoneModelo(ClienteInfo cliente)
        {
            Inicializar();
            infoCliente = cliente;
        }

        private void Inicializar()
        {
            InitializeComponent();
            FormFormat formFormat = new FormFormat(this);
            formFormat.formatar();
            this.FormBorderStyle = FormBorderStyle.None;
            this.AcceptButton = buttonSalvar;

            comboBoxModelo.ValueMember = "iphmodid";
            comboBoxModelo.DisplayMember = "iphmoddescricao";
        }

        private void FormIphoneModelo_Load(object sender, EventArgs e)
        {
            colecaoCores = negocioServ.ConsultarIphoneCorColecao();

            colecaoIpad = new IphoneModeloColecao();
            colecaoIphone = new IphoneModeloColecao();

            foreach (IphoneModeloInfo phone in Form1.IphoneColecao)
            {
                if (phone.iphmodipad)
                    colecaoIpad.Add(phone);
                else
                    colecaoIphone.Add(phone);
            }

            colecaoCor = Form1.IphoneCorColecao;
            comboBoxModelo.DataSource = colecaoIphone;
            comboBoxModelo.SelectedIndex = -1;
            cod = 1;

            if (colecaoCor == null)
            {
                FormMessage.ShowMessegeWarning("O formulário ainda não está pronto, tente mais tarde!");
                this.DialogResult = DialogResult.Cancel;
            }

        }

        private void ComboBoxModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            colecaoCorSelecionada = new IphoneModeloCorColecao();

            if (cod > 0)
            {
                int dex = Convert.ToInt32(comboBoxModelo.SelectedValue);
                textBoxModelo.Text = comboBoxModelo.Text;
                textBoxCap.Clear();
                textBoxNumMod.Clear();
                textBoxCor.Clear();

                if (radioButtonIphone.Checked)
                {
                    foreach (IphoneModeloInfo phone in colecaoIphone)
                    {
                        if (dex == phone.iphmodid)
                        {
                            infoIphone = phone;
                            break;
                        }
                    }

                    foreach (IphoneModeloCorInfo cor in colecaoCor)
                    {
                        if (cor.modcoridiphone == infoIphone.iphmodid)
                            colecaoCorSelecionada.Add(cor);
                    }
                }
                else
                {
                    foreach (IphoneModeloInfo phone in colecaoIpad)
                    {
                        if (dex == phone.iphmodid)
                        {
                            infoIphone = phone;
                            break;
                        }
                    }
                }
                

                labelTitle.Text = infoIphone.iphmoddescricao;
                textBoxDetalhes.Text = strConver(infoIphone.iphmoddetalhes);
                textBoxAno.Text = infoIphone.iphmodlancamento.ToString();
                textBoxDimensoes.Text = strConver(infoIphone.iphmodpesodimensoes);
                textBoxTela.Text = strConver(infoIphone.iphmodtela);
                textBoxCameraT.Text = strConver(infoIphone.iphmodcameratraseira);
                textBoxCamF.Text = strConver(infoIphone.iphmodcamerafrontal);
                textBoxTv.Text = strConver(infoIphone.iphmodtvvideo);
                textBoxControle.Text = strConver(infoIphone.iphmodbotoescontroles);
                textBoxEnergia.Text = strConver(infoIphone.iphmodenergiabateria);
                textBoxSensores.Text = strConver(infoIphone.iphmodsensores);
                textBoxCaixa.Text = strConver(infoIphone.iphmodconteudocaixa);
                textBoxGravacao.Text = strConver(infoIphone.iphmodgravacao);
                textBoxResistente.Text = strConver(infoIphone.iphmodresistente);

                comboBoxNumMod.Text = string.Empty;
                comboBoxCapacidade.Text = string.Empty;
                comboBoxCor.Text = string.Empty;

                comboBoxNumMod.Items.Clear();
                comboBoxCapacidade.Items.Clear();
                comboBoxCor.Items.Clear();

                comboBoxNumMod.Items.AddRange(infoIphone.iphmodnum);
                comboBoxCapacidade.Items.AddRange(infoIphone.iphmodcapacidade);
                comboBoxCor.Items.AddRange(infoIphone.iphmodcor);

                panelPrincipal.Enabled = true;
                ConvertImagem(infoIphone.iphmodfoto);
            }
        }

        private void ConvertImagem(byte[] foto)
        {
            if (foto != null)
            {
                MemoryStream memoryStream = new MemoryStream(foto);
                pictureBoxImagem.Image = Image.FromStream(memoryStream);
            }
            else
                pictureBoxImagem.Image = null;
        }

        private string strConver(string[] texto)
        {
            string novotxt = string.Empty;

            foreach (string txt in texto)
                novotxt += " - " + txt + Environment.NewLine;

            return novotxt;
        }

        private void ComboBoxCor_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxCor.Text = comboBoxCor.Text;

            if (radioButtonIphone.Checked)
            {
                foreach (IphoneModeloCorInfo cor in colecaoCorSelecionada)
                {
                    if (comboBoxCor.Text == cor.iphcordescricao)
                    {
                        ConvertImagem(cor.modcorfoto);
                        break;
                    }
                }
            }
        }

        private void ComboBoxNumMod_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxNumMod.Text = comboBoxNumMod.Text;
        }

        private void ComboBoxCapacidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxCap.Text = comboBoxCapacidade.Text;
        }

        private void ButtonFechar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void ButtonSalvar_Click(object sender, EventArgs e)
        {
            if (FormMessage.ShowMessegeQuestion("Deseja salvar?") == DialogResult.Yes)
            {
                buttonSalvar.Enabled = false;
                PreencherCelular();
                labelBarra.Visible = true;
                thread = new Thread(Salvar);
                form1.ExecutarThread(thread, progressBar1, labelBarra);
            }
        }

        private void Salvar()
        {
            infoCelular.celid = negocioServ.InsertIphoneCelular(infoCelular);
            SelecionadoIphone = infoCelular;
            Form1.encerrarThread = true;
            this.DialogResult = DialogResult.Yes;
        }

        private void PreencherCelular()
        {
            infoCelular = new IphoneCelularInfo
            {
                celanocompra = textBoxAnoCompra.Text,
                celcapacidade = textBoxCap.Text,
                celcor = textBoxCor.Text,
                celid = 0,
                celidcliente = infoCliente.cliid,
                celidmodiphone = infoIphone.iphmodid,
                celimei = textBoxImei.Text,
                celmodelo = textBoxNumMod.Text,
                celobs = textBoxObs.Text,
                celserie = textBoxSerie.Text,
                celiphonedescricao = textBoxModelo.Text
            };
        }

        private void RadioButtonIphone_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonIphone.Checked)
            {
                comboBoxModelo.ValueMember = "iphmodid";
                comboBoxModelo.DisplayMember = "iphmoddescricao";
                comboBoxModelo.DataSource = colecaoIphone;
                tabControl1.TabPages.Add(tabPage2);
                textBoxDetalhes.ScrollBars = ScrollBars.None;
            }
            else
            {
                comboBoxModelo.ValueMember = "iphmodid";
                comboBoxModelo.DisplayMember = "iphmoddescricao";
                comboBoxModelo.DataSource = colecaoIpad;
                tabControl1.TabPages.RemoveAt(1);
                textBoxDetalhes.ScrollBars = ScrollBars.Vertical;
            }
        }

        private void ButtonFoto_Click(object sender, EventArgs e)
        {
            FormIphoneSalvarFoto formIphoneSalvarFoto = new FormIphoneSalvarFoto(colecaoIphone);
            formIphoneSalvarFoto.ShowDialog(this);
            formIphoneSalvarFoto.Dispose();
        }
    }
}
