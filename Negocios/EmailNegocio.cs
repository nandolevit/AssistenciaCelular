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

        public bool EnviarEmailGmail(EmailInfo emailInfo)
        {
            email = new Email("smtp.gmail.com", "nandolevit2012@gmail.com", "wizykovisc");
            return email.EnviarEmail(emailInfo);
        }
    }
}
