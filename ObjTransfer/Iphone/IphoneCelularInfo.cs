using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjTransfer
{
    public class IphoneCelularInfo
    {
        public int celid { get; set; }
        public int celidmodiphone { get; set; }
        public string celiphonedescricao { get; set; }
        public int celidcliente { get; set; }
        public string celcor { get; set; }
        public string celcapacidade { get; set; }
        public string celmodelo { get; set; }
        public string celimei { get; set; }
        public string celanocompra { get; set; }
        public string celserie { get; set; }
        public string celobs { get; set; }
        public string celsenha { get; set; }
        public string celicloudlogin { get; set; }
        public string celicloudsenha { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.IsNullOrEmpty(celiphonedescricao) ? "" : celiphonedescricao);
            sb.AppendLine(string.IsNullOrEmpty(celmodelo) ? "" : ", Modelo: " + celmodelo);
            sb.AppendLine(string.IsNullOrEmpty(celimei) ? "" : ", IMEI: " + celimei);
            sb.AppendLine(string.IsNullOrEmpty(celserie) ? "" : ", SÉRIE: " + celserie);
            sb.AppendLine(string.IsNullOrEmpty(celanocompra) ? "" : ", Ano: " + celanocompra);
            sb.AppendLine(string.IsNullOrEmpty(celcapacidade) ? "" : ", Capacidade: " + celcapacidade);
            sb.AppendLine(string.IsNullOrEmpty(celcor) ? "" : ", Cor: " + celcor);

            return sb.ToString();
        }
    }
}
