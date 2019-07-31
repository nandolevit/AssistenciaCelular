using System;
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
        Email email;
        EmpresaEmailInfo empresa;

        public EmailNegocio(EmpresaEmailInfo emp)
        {
            empresa = emp;
        }

        public bool EnviarEmailGmail(EmailInfo emailInfo)
        {
            if (empresa != null)
            {
                email = new Email("smtp.gmail.com", empresa.emaillogin, empresa.emailsenha);
                return email.EnviarEmail(emailInfo);
            }
            else
                return false;
        }
    }
}
