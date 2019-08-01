﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AccessDB;
using ObjTransfer;

namespace Negocios
{
    public class EmailNegocio
    {
        Email infoEmail;
        EmpresaEmailInfo empresa;

        public EmailNegocio(EmpresaEmailInfo emp)
        {
            empresa = emp;
        }

        public bool EnviarEmailGmail(EmailInfo emailInfo)
        {
            infoEmail = new Email("smtp.gmail.com", empresa.emaillogin, empresa.emailsenha);
            return infoEmail.EnviarEmail(emailInfo);
        }

        public bool EnviarEmailBasico(string mail, string assunto, string mensagem)
        {
            EmailInfo email = new EmailInfo
            {
                emailAssunto = assunto,
                emailMessage = mensagem,
                emailTo = new string[] {mail},
                emailCC = new string[0],
                emailCCo = new string[0],
                emailAnexo = new string[0]
            };

            return EnviarEmailGmail(email);
        }
    }
}
