using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjTransfer
{
    public class EnderecoInfo
    {
        public int endid { get; set; }
        public string endcep { get; set; }
        public string endcomplemento { get; set; }
        public string endreferencia { get; set; }
        public int endidcliente { get; set; }
        public string endlogradouro { get; set; }
        public string endbairro { get; set; }
        public string endcidade { get; set; }
        public string enduf { get; set; }
        public bool endpadrao { get; set; }
    }
}
