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

        public override string ToString()
        {
            string descricao = string.Empty;

            descricao += string.IsNullOrEmpty(celiphonedescricao)? "": celiphonedescricao;
            descricao += string.IsNullOrEmpty(celmodelo) ? "" : ", Modelo: " + celmodelo;
            descricao += string.IsNullOrEmpty(celimei) ? "" : ", IMEI: " + celimei;
            descricao += string.IsNullOrEmpty(celserie) ? "" : ", SÉRIE: " + celserie;
            descricao += string.IsNullOrEmpty(celanocompra) ? "" : ", Ano: " + celanocompra;
            descricao += string.IsNullOrEmpty(celcapacidade) ? "" : ", Capacidade: " + celcapacidade;
            descricao += string.IsNullOrEmpty(celcor) ? "" : ", Cor: " + celcor;
            descricao += string.IsNullOrEmpty(celobs) ? "" :", Obs: " + celobs;

            return descricao;
        }
    }
}
