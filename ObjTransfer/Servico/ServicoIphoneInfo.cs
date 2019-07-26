using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjTransfer
{
    public class ServicoIphoneInfo : ServicoInfo
    {
        public int iphdefid { get; set; }
        public int iphdefidservico { get; set; }
        public string iphdefdefeito { get; set; }
        public string iphdefobs { get; set; }
        public string iphdeftouchdisplay { get; set; }
        public string iphdefcamfrontal { get; set; }
        public string iphdefsensorprox { get; set; }
        public string iphdefhome { get; set; }
        public string iphdefautofrontal { get; set; }
        public string iphdefconector { get; set; }
        public string iphdeffone { get; set; }
        public string iphdefautointerno { get; set; }
        public string iphdefmicrofone { get; set; }
        public string iphdefparafuso { get; set; }
        public string iphdefcarcaca { get; set; }
        public string iphdefcamtraseira { get; set; }
        public string iphdefmicrofonetraseiro { get; set; }
        public string iphdefflash { get; set; }
        public string iphdefvolume { get; set; }
        public string iphdefbandeja { get; set; }
        public string iphdefdesligar { get; set; }
        public string iphdefsilencioso { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.IsNullOrEmpty(iphdeftouchdisplay) ? "" : "**Touch/Display: " + iphdeftouchdisplay);
            sb.AppendLine(string.IsNullOrEmpty(iphdefcamfrontal) ? "" : "**Câmera frontal: " + iphdefcamfrontal);
            sb.AppendLine(string.IsNullOrEmpty(iphdefsensorprox) ? "" : "**Sensor de proximidade: " + iphdefsensorprox);
            sb.AppendLine(string.IsNullOrEmpty(iphdefhome) ? "" : "**Botão home/Touch ID: " + iphdefhome);
            sb.AppendLine(string.IsNullOrEmpty(iphdefautofrontal) ? "" : "**Auto-falante frontal: " + iphdefautofrontal);
            sb.AppendLine(string.IsNullOrEmpty(iphdefconector) ? "" : "**Conector Lightning: " + iphdefconector);
            sb.AppendLine(string.IsNullOrEmpty(iphdeffone) ? "" : "**Miniconector de fone de ouvido: " + iphdeffone);
            sb.AppendLine(string.IsNullOrEmpty(iphdefautointerno) ? "" : "**Auto-falante interno: " + iphdefautointerno);
            sb.AppendLine(string.IsNullOrEmpty(iphdefmicrofone) ? "" : "**Microfone: " + iphdefmicrofone);
            sb.AppendLine(string.IsNullOrEmpty(iphdefparafuso) ? "" : "**Parafuso da carcaça: " + iphdefparafuso);
            sb.AppendLine(string.IsNullOrEmpty(iphdefparafuso) ? "" : "**Parafuso da carcaça: " + iphdefparafuso);
            sb.AppendLine(string.IsNullOrEmpty(iphdefcarcaca) ? "" : "**Estado da carcaça: " + iphdefcarcaca);
            sb.AppendLine(string.IsNullOrEmpty(iphdefcamtraseira) ? "" : "**Câmera traseira: " + iphdefcamtraseira);
            sb.AppendLine(string.IsNullOrEmpty(iphdefmicrofonetraseiro) ? "" : "**Microfone traseiro: " + iphdefmicrofonetraseiro);
            sb.AppendLine(string.IsNullOrEmpty(iphdefflash) ? "" : "**Flash: " + iphdefflash);
            sb.AppendLine(string.IsNullOrEmpty(iphdefautointerno) ? "" : "**Auto-falante interno: " + iphdefautointerno);
            sb.AppendLine(string.IsNullOrEmpty(iphdefvolume) ? "" : "**Botão de Volume: " + iphdefvolume);
            sb.AppendLine(string.IsNullOrEmpty(iphdefbandeja) ? "" : "**Bandeja de Chip: " + iphdefbandeja);
            sb.AppendLine(string.IsNullOrEmpty(iphdefdesligar) ? "" : "**Botão Ligar/Desligar: " + iphdefdesligar);
            sb.AppendLine(string.IsNullOrEmpty(iphdefsilencioso) ? "" : "**Botão Tocar/Silencioso: " + iphdefsilencioso);

            return sb.ToString();
        }
    }
}
