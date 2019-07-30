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
        EmailNegocio negocioEmail = new EmailNegocio();
        EmailInfo infoEmail;
        public FormEmail()
        {
            InitializeComponent();
            this.AcceptButton = buttonEnviar;
            textBoxPara.Select();
            //FormFormat formFormat = new FormFormat(this);
            //formFormat.formatar();
        }

        private void ButtonEnviar_Click(object sender, EventArgs e)
        {
            if (CampoObrigatorio())
            {
                PreencherEmail();
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
    }
}
