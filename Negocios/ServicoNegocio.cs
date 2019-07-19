using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ObjTransfer;
using AccessDB;
using System.Data;

namespace Negocios
{
    public class ServicoNegocio
    {
        private static string EmpConexao { get; set; }

        public ServicoNegocio(string conexao)
        {
            EmpConexao = conexao;
        }

        AccessDbMySql accessDbMySql = new AccessDbMySql();

        public int UpdateIphoneDefeito(IphoneDefeitoInfo defeito)
        {
            if (accessDbMySql.Conectar(EmpConexao))
            {
                accessDbMySql.AddParametrosMySql("@autofrontal", defeito.iphdefautofrontal);
                accessDbMySql.AddParametrosMySql("@autointerno", defeito.iphdefautointerno);
                accessDbMySql.AddParametrosMySql("@camfrontal", defeito.iphdefcamfrontal);
                accessDbMySql.AddParametrosMySql("@camtraseira", defeito.iphdefcamtraseira);
                accessDbMySql.AddParametrosMySql("@carcaca", defeito.iphdefcarcaca);
                accessDbMySql.AddParametrosMySql("@conector", defeito.iphdefconector);
                accessDbMySql.AddParametrosMySql("@defeito", defeito.iphdefdefeito);
                accessDbMySql.AddParametrosMySql("@flash", defeito.iphdefflash);
                accessDbMySql.AddParametrosMySql("@fone", defeito.iphdeffone);
                accessDbMySql.AddParametrosMySql("@home", defeito.iphdefhome);
                accessDbMySql.AddParametrosMySql("@id", defeito.iphdefid);
                accessDbMySql.AddParametrosMySql("@aparelho", defeito.iphdefidaparelho);
                accessDbMySql.AddParametrosMySql("@microfone", defeito.iphdefmicrofone);
                accessDbMySql.AddParametrosMySql("@microfonetraseiro", defeito.iphdefmicrofonetraseiro);
                accessDbMySql.AddParametrosMySql("@obs", defeito.iphdefobs);
                accessDbMySql.AddParametrosMySql("@parafuso", defeito.iphdefparafuso);
                accessDbMySql.AddParametrosMySql("@sensorprox", defeito.iphdefsensorprox);
                accessDbMySql.AddParametrosMySql("@display", defeito.iphdeftouchdisplay);
                accessDbMySql.AddParametrosMySql("@volume", defeito.iphdefvolume);
                accessDbMySql.AddParametrosMySql("@silencioso", defeito.iphdefsilencioso);
                accessDbMySql.AddParametrosMySql("@desligar", defeito.iphdefdesligar);
                accessDbMySql.AddParametrosMySql("@bandeja", defeito.iphdefbandeja);

                return accessDbMySql.ExecutarScalarMySql("spUpdateIphoneDefeito");
            }
            else
                return 0;
        }

        public int InsertIphoneDefeito(IphoneDefeitoInfo defeito)
        {
            if (accessDbMySql.Conectar(EmpConexao))
            {
                accessDbMySql.AddParametrosMySql("@autofrontal", defeito.iphdefautofrontal);
                accessDbMySql.AddParametrosMySql("@autointerno", defeito.iphdefautointerno);
                accessDbMySql.AddParametrosMySql("@camfrontal", defeito.iphdefcamfrontal);
                accessDbMySql.AddParametrosMySql("@camtraseira", defeito.iphdefcamtraseira);
                accessDbMySql.AddParametrosMySql("@carcaca", defeito.iphdefcarcaca);
                accessDbMySql.AddParametrosMySql("@conector", defeito.iphdefconector);
                accessDbMySql.AddParametrosMySql("@defeito", defeito.iphdefdefeito);
                accessDbMySql.AddParametrosMySql("@flash", defeito.iphdefflash);
                accessDbMySql.AddParametrosMySql("@fone", defeito.iphdeffone);
                accessDbMySql.AddParametrosMySql("@home", defeito.iphdefhome);
                accessDbMySql.AddParametrosMySql("@id", defeito.iphdefid);
                accessDbMySql.AddParametrosMySql("@phone", defeito.iphdefidaparelho);
                accessDbMySql.AddParametrosMySql("@microfone", defeito.iphdefmicrofone);
                accessDbMySql.AddParametrosMySql("@microfonetraseiro", defeito.iphdefmicrofonetraseiro);
                accessDbMySql.AddParametrosMySql("@obs", defeito.iphdefobs);
                accessDbMySql.AddParametrosMySql("@parafuso", defeito.iphdefparafuso);
                accessDbMySql.AddParametrosMySql("@sensor", defeito.iphdefsensorprox);
                accessDbMySql.AddParametrosMySql("@display", defeito.iphdeftouchdisplay);
                accessDbMySql.AddParametrosMySql("@volume", defeito.iphdefvolume);
                accessDbMySql.AddParametrosMySql("@silencioso", defeito.iphdefsilencioso);
                accessDbMySql.AddParametrosMySql("@desligar", defeito.iphdefdesligar);
                accessDbMySql.AddParametrosMySql("@bandeja", defeito.iphdefbandeja);

                return accessDbMySql.ExecutarScalarMySql("spInsertIphoneDefeito");
            }
            else
                return 0;
        }

        public IphoneDefeitoInfo ConsultarIphoneDefeitoId(int id)
        {
            if (accessDbMySql.Conectar(EmpConexao))
            {
                accessDbMySql.AddParametrosMySql("@phone", id);
                DataTable dataTable = accessDbMySql.dataTableMySql("spConsultarIphoneDefeitoId");
                if (dataTable != null)
                    return PreencherIphoneDefeito(dataTable)[0];
                else
                    return null;
            }
            else
                return null;
        }

        private IphoneDefeitoColecao PreencherIphoneDefeito(DataTable dataTable)
        {
            IphoneDefeitoColecao colecao = new IphoneDefeitoColecao();
            foreach (DataRow row in dataTable.Rows)
            {
                IphoneDefeitoInfo defeito = new IphoneDefeitoInfo
                {
                    iphdefautofrontal = Convert.ToString(row["iphdefautofrontal"]),
                    iphdefautointerno = Convert.ToString(row["iphdefautointerno"]),
                    iphdefcamfrontal = Convert.ToString(row["iphdefcamfrontal"]),
                    iphdefcamtraseira = Convert.ToString(row["iphdefcamtraseira"]),
                    iphdefcarcaca = Convert.ToString(row["iphdefcarcaca"]),
                    iphdefconector = Convert.ToString(row["iphdefconector"]),
                    iphdefdefeito = Convert.ToString(row["iphdefdefeito"]),
                    iphdefflash = Convert.ToString(row["iphdefflash"]),
                    iphdeffone = Convert.ToString(row["iphdeffone"]),
                    iphdefhome = Convert.ToString(row["iphdefhome"]),
                    iphdefid = Convert.ToInt32(row["iphdefid"]),
                    iphdefidaparelho = Convert.ToInt32(row["iphdefidaparelho"]),
                    iphdefmicrofone = Convert.ToString(row["iphdefmicrofone"]),
                    iphdefmicrofonetraseiro = Convert.ToString(row["iphdefmicrofonetraseiro"]),
                    iphdefobs = Convert.ToString(row["iphdefobs"]),
                    iphdefparafuso = Convert.ToString(row["iphdefparafuso"]),
                    iphdefsensorprox = Convert.ToString(row["iphdefsensorprox"]),
                    iphdeftouchdisplay = Convert.ToString(row["iphdeftouchdisplay"])
                };

                colecao.Add(defeito);
            }

            return colecao;
        }

        public AparelhoInfo ConsultarAparelhoId(int id)
        {
            if (accessDbMySql.Conectar(EmpConexao))
            {
                accessDbMySql.AddParametrosMySql("@id", id);
                DataTable dataTable = accessDbMySql.dataTableMySql("spConsultarAparelhoId");

                if (dataTable != null)
                    return PreencherAparelho(dataTable)[0];
                else
                    return null;
            }
            else
                return null;
        }

        public AparelhoColecao ConsultarAparelhoClienteId(int id)
        {
            if (accessDbMySql.Conectar(EmpConexao))
            {
                accessDbMySql.AddParametrosMySql("@id", id);
                DataTable dataTable = accessDbMySql.dataTableMySql("spConsultarAparelhoClienteId");

                if (dataTable != null)
                    return PreencherAparelho(dataTable);
                else
                    return null;
            }
            else
                return null;
        }

        private AparelhoColecao PreencherAparelho(DataTable dataTable)
        {
            AparelhoColecao colecao = new AparelhoColecao();
            foreach (DataRow row in dataTable.Rows)
            {
                AparelhoInfo aparelho = new AparelhoInfo
                {
                    apadescricao = Convert.ToString(row["apadescricao"]),
                    apaid = Convert.ToInt32(row["apaid"]),
                    apaidaparelho = Convert.ToInt32(row["apaidaparelho"]),
                    apaidcliente = Convert.ToInt32(row["apaidcliente"]),
                    apaidtipoaparelho = Convert.ToInt32(row["apaidtipoaparelho"])
                };

                colecao.Add(aparelho);
            }

            return colecao;
        }

        public int InsertIphoneCelular(IphoneCelularInfo phone)
        {
            if (accessDbMySql.Conectar(EmpConexao))
            {
                accessDbMySql.AddParametrosMySql("@phone", phone.celidmodiphone);
                accessDbMySql.AddParametrosMySql("@compra", phone.celanocompra);
                accessDbMySql.AddParametrosMySql("@capacidade", phone.celcapacidade);
                accessDbMySql.AddParametrosMySql("@cor", phone.celcor);
                accessDbMySql.AddParametrosMySql("@cliente", phone.celidcliente);
                accessDbMySql.AddParametrosMySql("@imei", phone.celimei);
                accessDbMySql.AddParametrosMySql("@modelo", phone.celmodelo);
                accessDbMySql.AddParametrosMySql("@obs", phone.celobs);
                accessDbMySql.AddParametrosMySql("@serie", phone.celserie);
                accessDbMySql.AddParametrosMySql("@descricao", phone.celiphonedescricao);

                return accessDbMySql.ExecutarScalarMySql("spInsertIphoneCelular");
            }
            else
                return 0;
        }

        public IphoneModeloCorColecao ConsultarIphoneModeloCorFotoId(int id)
        {
            if (accessDbMySql.Conectar(DadosDB.ConexaoSys))
            {
                accessDbMySql.AddParametrosMySql("@phone", id);
                DataTable dataTable = accessDbMySql.dataTableMySql("spConsultarIphoneModeloCorFotoId");

                if (dataTable != null)
                {
                    IphoneModeloCorColecao colecao = new IphoneModeloCorColecao();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        IphoneModeloCorInfo cor = new IphoneModeloCorInfo
                        {
                            modcorfoto = DBNull.Value.Equals(row["modcorfoto"]) ? null : (byte[])row["modcorfoto"],
                            modcorid = Convert.ToInt32(row["modcorid"]),
                            modcoridcor = Convert.ToInt32(row["modcoridcor"]),
                            modcoridiphone = Convert.ToInt32(row["modcoridiphone"]),
                            iphcordescricao = Convert.ToString(row["iphcordescricao"])
                        };

                        colecao.Add(cor);
                    }

                    return colecao;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public IphoneModeloCorColecao ConsultarIphoneModeloCorFoto()
        {
            if (accessDbMySql.Conectar(DadosDB.ConexaoSys))
            {
                DataTable dataTable = accessDbMySql.dataTableMySql("spConsultarIphoneModeloCorFoto");

                if (dataTable != null)
                {
                    IphoneModeloCorColecao colecao = new IphoneModeloCorColecao();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        IphoneModeloCorInfo cor = new IphoneModeloCorInfo
                        {
                            modcorfoto = DBNull.Value.Equals(row["modcorfoto"]) ? null : (byte[])row["modcorfoto"],
                            modcorid = Convert.ToInt32(row["modcorid"]),
                            modcoridcor = Convert.ToInt32(row["modcoridcor"]),
                            modcoridiphone = Convert.ToInt32(row["modcoridiphone"]),
                            iphcordescricao = Convert.ToString(row["iphcordescricao"])
                        };

                        colecao.Add(cor);
                    }

                    return colecao;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public int UpdateFotoIphoneModelo(IphoneModeloCorInfo cor)
        {
            if (accessDbMySql.Conectar(DadosDB.ConexaoSys))
            {
                accessDbMySql.AddParametrosMySql("@phone", cor.modcoridiphone);
                accessDbMySql.AddParametrosMySql("@foto", cor.modcorfoto);

                return accessDbMySql.ExecutarScalarMySql("spUpdateFotoIphoneModelo");
            }
            else
                return 0;
        }

        public int InsertIphoneModeloCor(IphoneModeloCorInfo cor)
        {
            if (accessDbMySql.Conectar(DadosDB.ConexaoSys))
            {
                accessDbMySql.AddParametrosMySql("@phone", cor.modcoridiphone);
                accessDbMySql.AddParametrosMySql("@cor", cor.modcoridcor);
                accessDbMySql.AddParametrosMySql("@foto", cor.modcorfoto);

                return accessDbMySql.ExecutarScalarMySql("spInsertIphoneModeloCor");
            }
            else
                return 0;
        }

        public CodDescricaoColecao ConsultarIphoneCorColecao()
        {
            if (accessDbMySql.Conectar(DadosDB.ConexaoSys))
            {
                DataTable dataTable = accessDbMySql.dataTableMySql("spConsultarIphoneCor");

                if (dataTable != null)
                {
                    CodDescricaoColecao colecao = new CodDescricaoColecao();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        CodDescricaoInfo cod = new CodDescricaoInfo
                        {
                            cod = Convert.ToInt32(row["iphcorid"]),
                             descricao = Convert.ToString(row["iphcordescricao"])
                        };

                        colecao.Add(cod);
                    }

                    return colecao;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public IphoneModeloColecao ConsultarIphoneColecao()
        {
            if (accessDbMySql.Conectar(DadosDB.ConexaoSys))
            {
                DataTable dataTable = accessDbMySql.dataTableMySql("spConsultarIphoneColecao");

                if (dataTable != null)
                    return PreencherIphone(dataTable);
                else
                    return null;
            }
            else
                return null;
        }

        public IphoneModeloInfo ConsultarIphoneId(string id)
        {
            if (accessDbMySql.Conectar(DadosDB.ConexaoSys))
            {
                accessDbMySql.AddParametrosMySql("@cod", id);
                DataTable dataTable = accessDbMySql.dataTableMySql("spConsultarIphoneId");

                if (dataTable != null)
                    return PreencherIphone(dataTable)[0];
                else
                    return null;
            }
            else
                return null;
        }

        public IphoneModeloColecao PreencherIphone(DataTable dataTable)
        {
            IphoneModeloColecao colecao = new IphoneModeloColecao();
            foreach (DataRow iphone in dataTable.Rows)
            {
                IphoneModeloInfo phone = new IphoneModeloInfo
                {
                    iphmodbotoescontroles = Convert.ToString(iphone["iphmodbotoescontroles"]).Split(';'),
                    iphmodcamerafrontal = Convert.ToString(iphone["iphmodcamerafrontal"]).Split(';'),
                    iphmodcameratraseira = Convert.ToString(iphone["iphmodcameratraseira"]).Split(';'),
                    iphmodcapacidade = Convert.ToString(iphone["iphmodcapacidade"]).Split(';'),
                    iphmodconteudocaixa = Convert.ToString(iphone["iphmodconteudocaixa"]).Split(';'),
                    iphmodcor = Convert.ToString(iphone["iphmodcor"]).Split(';'),
                    iphmoddetalhes = Convert.ToString(iphone["iphmoddetalhes"]).Split(';'),
                    iphmodenergiabateria = Convert.ToString(iphone["iphmodenergiabateria"]).Split(';'),
                    iphmodgravacao = Convert.ToString(iphone["iphmodgravacao"]).Split(';'),
                    iphmodid = Convert.ToInt32(iphone["iphmodid"]),
                    iphmodlancamento = Convert.ToInt32(iphone["iphmodlancamento"]),
                    iphmodnum = Convert.ToString(iphone["iphmodnum"]).Split(';'),
                    iphmodpesodimensoes = Convert.ToString(iphone["iphmodpesodimensoes"]).Split(';'),
                    iphmodresistente = Convert.ToString(iphone["iphmodresistente"]).Split(';'),
                    iphmodsensores = Convert.ToString(iphone["iphmodsensores"]).Split(';'),
                    iphmodtela = Convert.ToString(iphone["iphmodtela"]).Split(';'),
                    iphmodtvvideo = Convert.ToString(iphone["iphmodtvvideo"]).Split(';'),
                    iphmoddescricao = Convert.ToString(iphone["iphmoddescricao"]),
                    iphmodfoto = DBNull.Value.Equals(iphone["iphmodfoto"]) ? null : (byte[])iphone["iphmodfoto"],
                    iphmodipad = Convert.ToBoolean(iphone["iphmodipad"])
                };
                colecao.Add(phone);
            }

            return colecao;
        }
    }
}
