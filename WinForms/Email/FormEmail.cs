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
    public partial class FormEmail : Form
    {
        EmailNegocio negocioEmail;
        EmailInfo infoEmail;
        PessoaInfo infoPessoa;
        List<string> listAnexo = new List<string>();
        public FormEmail()
        {
            InitializeComponent();
            this.AcceptButton = buttonEnviar;
            textBoxPara.Select();
            MensagemPadrao();
        }

        private void MensagemPadrao()
        {
            string mensagem = string.Empty;

            mensagem += "Prezado(a) cliente, ";

            for (int i = 0; i < 5; i++)
                mensagem += Environment.NewLine;

            mensagem += "Att," + Environment.NewLine;
            mensagem += "Equipe " + Form1.Empresa.empfantasia + ".";

            textBoxMessage.Text = mensagem;
        }

        private void ButtonEnviar_Click(object sender, EventArgs e)
        {
            if (CampoObrigatorio())
            {
                PreencherEmail();

                negocioEmail = new EmailNegocio(Form1.EmpresaEmail);

                if (negocioEmail.EnviarEmailGmail(infoEmail))
                    FormMessage.ShowMessegeWarning("Mensagem enviada por email");
            }
        }

        private void PreencherEmail()
        {
            infoEmail = new EmailInfo
            {
                emailTo = string.IsNullOrEmpty(textBoxPara.Text) ? new string[0] : textBoxPara.Text.Split(';'),
                emailAssunto = textBoxAssunto.Text,
                emailCC = string.IsNullOrEmpty(textBoxCC.Text) ? new string[0] : textBoxCC.Text.Split(';'),
                emailCCo = string.IsNullOrEmpty(textBoxCCo.Text) ? new string[0] : textBoxCCo.Text.Split(';'),
                emailMessage = textBoxMessage.Text
            };

            if (listBoxAnexo.Items.Count > 0)
            {
                foreach (string item in listBoxAnexo.Items)
                    listAnexo.Add(item);

                infoEmail.emailAnexo = listAnexo.ToArray();
            }
            else
                infoEmail.emailAnexo = new string[0];
        }

        private bool CampoObrigatorio()
        {
            if (string.IsNullOrEmpty(textBoxPara.Text))
            {
                FormMessage.ShowMessegeWarning("Informe o email de destino!");
                textBoxPara.Select();
                return false;
            }

            if (string.IsNullOrEmpty(textBoxAssunto.Text))
            {
                FormMessage.ShowMessegeWarning("Informe o assunto!");
                textBoxAssunto.Select();
                return false;
            }

            if (string.IsNullOrEmpty(textBoxMessage.Text))
            {
                FormMessage.ShowMessegeWarning("Informe a mensagem que deseja enviar!");
                textBoxMessage.Select();
                return false;
            }

            return true;
        }

        private void ButtonTo_Click(object sender, EventArgs e)
        {
            Consultar(textBoxPara);
        }

        private void Consultar(object txt)
        {
            TextBox box = (TextBox)txt;


            FormPessoaConsultar formPessoaConsultar = new FormPessoaConsultar(true);
            if (formPessoaConsultar.ShowDialog(this) == DialogResult.Yes)
            {
                infoPessoa = formPessoaConsultar.SelecionadoCliente;

                if (infoPessoa.pssemail == "sem@email.com")
                {
                    FormMessage.ShowMessegeWarning("A pessoa selecionada não possui e-mail cadastrado!");
                    return;
                }

                if (string.IsNullOrEmpty(box.Text))
                    box.Text = infoPessoa.pssemail;
                else
                    box.Text += ";" + infoPessoa.pssemail;
            }

            
        }

        private void ButtonCC_Click(object sender, EventArgs e)
        {
            Consultar(textBoxCC);
        }

        private void ButtonCCo_Click(object sender, EventArgs e)
        {
            Consultar(textBoxCCo);
        }

        private void TextBoxMessage_TextChanged(object sender, EventArgs e)
        {

        }

        private void ButtonAnexo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = true;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (string item in dialog.FileNames)
                        listBoxAnexo.Items.Add(item);
                }
            }
        }

        private void FormEmail_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.DialogResult = DialogResult.Cancel;
                    break;
                default:
                    break;
            }
        }

        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxAnexo.SelectedIndex > 0)
                listBoxAnexo.Items.RemoveAt(listBoxAnexo.SelectedIndex);
            else
                FormMessage.ShowMessegeWarning("Selecione o anexo que deseja remover!");
        }
    }
}
