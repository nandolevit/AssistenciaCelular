using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using ObjTransfer;
using Negocios;
using System.Diagnostics;

namespace WinForms
{
    public partial class FormCadastroPessoa : Form
    {
        Form1 form1 = new Form1();
        int modSave;
        int idSave;
        int idEnd;
        bool config = false;
        string cpf;
        int codCargo;

        Thread thread;
        FuncInfo infoFunc;
        ClienteInfo infoCliente;
        ClienteColecao colecaoCliente;
        EnderecoInfo infoEnd;
        FornecedorInfo infoFornecedor;
        EnderecoColecao colecaoEndereco;

        ClienteNegocios negocioCliente = new ClienteNegocios(Form1.Empresa.empconexao);
        FuncNegocios funcNegocios = new FuncNegocios(Form1.Empresa.empconexao);
        EmpresaNegocios empresaNegocios = new EmpresaNegocios(Form1.Empresa.empconexao);
        FornecedorNegocios fornecedorNegocios = new FornecedorNegocios(Form1.Empresa.empconexao);
        

        //public FormCadastroPessoa()
        //{
        //    Inicializar();
        //}

        public FormCadastroPessoa(UnidadeInfo unidadeInfo)
        {
            Inicializar();
            modSave = 4;
            config = true;
            FuncForm();
        }

        public FormCadastroPessoa(FornecedorInfo fornecedor)
        {
            Inicializar();
            modSave = 3;
            this.Text = "FORNECEDOR";
            this.labelTitle.Text = "FORNECEDOR";
            buttonBuscarCargo.Visible = false;
            labelUnidade.Visible = false;
            labelCargoDescricao.Visible = false;
        }

        public FormCadastroPessoa(FuncInfo func)
        {
            Inicializar();
            modSave = 2;
            FuncForm();
        }

        public FormCadastroPessoa(ClienteInfo cli)
        {
            Inicializar();
            modSave = 0;
            this.Text = "CLIENTE";
            this.labelTitle.Text = "CLIENTE";
            labelCargoDescricao.Visible = false;
            labelUnidade.Visible = false;
            buttonBuscarCargo.Visible = false;
            infoCliente = cli;
            idSave = cli.cliid;
            if (cli.cliid > 0)
                PreencherFormCliente();
        }

        private void Inicializar()
        {
            InitializeComponent();
            FormFormat formFormat = new FormFormat(this);
            formFormat.formatar();

            if (Form1.Offline == true)
            {
                buttonEnd.Visible = false;
                linkLabelCep.Visible = false;
            }

            this.AcceptButton = buttonSalvar;
        }

        private void FuncForm()
        {
            this.Text = "FUNCIONÁRIO";
            this.labelTitle.Text = "FUNCIONÁRIO";
            labelCargoDescricao.Visible = true;
            labelUnidade.Visible = true;
            buttonBuscarCargo.Visible = true;
        }

        private void ConsultarEndCliente()
        {
            colecaoEndereco = negocioCliente.ConsultarEnderecoPorIdCliente(infoCliente.cliid);

            if (colecaoEndereco != null)
                infoEnd = colecaoEndereco[0];

            Form1.encerrarThread = true;
        }

        private void PreencherFormEnd()
        {
            idEnd = infoEnd.endid;
            textBoxBairro.Text = infoEnd.endbairro;
            maskedTextBoxCep.Text = infoEnd.endcep;
            textBoxCidade.Text = infoEnd.endcidade;
            textBoxComplemento.Text = infoEnd.endcomplemento;
            textBoxPontoReferencia.Text = infoEnd.endreferencia;
            textBoxLogradouro.Text = infoEnd.endlogradouro;
            textBoxUF.Text = infoEnd.enduf;
            
        }

        private void PreencherFormClienteTeste()
        {
            textBoxId.Text = "0";
            maskedTextBoxCpf.Text = "66440141000133";
            textBoxEmail.Text = "faleconosco@lauraebetinafilmagensltda.com.br";
            textBoxNome.Text = "Laura e Betina Filmagens Ltda";
            textBoxNiver.Text = "06/12/1987";

            maskedTextBoxTel1.Text = "7528634952";
            maskedTextBoxTel2.Text = "75983967082";

            textBoxBairro.Text = "Santo Antônio dos Prazeres";
            maskedTextBoxCep.Text = "44071700";
            textBoxCidade.Text = "Feira de Santana";
            textBoxComplemento.Text = "Santo Antônio dos Prazeres";
            textBoxPontoReferencia.Text = "Santo Antônio dos Prazeres";
            textBoxLogradouro.Text = "2ª Travessa Quito";
            textBoxUF.Text = "ba";
        }

        private void PreencherFormCliente()
        {
            thread = new Thread(ConsultarEndCliente);
            form1.ExecutarThread(thread, progressBar1, labelBarra);
            this.Activate();

            textBoxId.Text = string.Format("{0:000000}", infoCliente.cliid);
            maskedTextBoxCpf.Text = infoCliente.clicpf;
            textBoxEmail.Text = infoCliente.cliemail;
            textBoxNome.Text = infoCliente.clinome;
            textBoxNiver.Text = infoCliente.cliniver.ToString("ddd, dd 'de' MMMM 'de' yyyy").ToUpper();

            if (infoCliente.clitel.IndexOf('/') > 0)
            {
                string[] tel = infoCliente.clitel.Split('/');
                maskedTextBoxTel1.Text = tel[0];
                maskedTextBoxTel2.Text = tel[1];
            }
            else
                maskedTextBoxTel1.Text = infoCliente.clitel;

            PreencherFormEnd();

            maskedTextBoxCpf.Enabled = false;
        }

        private void SalvarUser(FuncInfo func)
        {
            UserNegocio userNegocio = new UserNegocio(Form1.Empresa.empconexao);
            UserInfo userInfo = new UserInfo
            {
                useidfuncionario = func.funId,
                uselogin = func.funCpf,
                usesenha = func.funCpf
            };

            userNegocio.InsertUser(userInfo);
        }

        private void SalvarFornecedor()
        {
            if (!string.IsNullOrEmpty(textBoxNome.Text))
            {
                PreencherFornecedorInfo();
                SalvarThread();
            }
            else
                FormMessage.ShowMessegeWarning("Insira no mínimo o nome do fornecedor!");
        }

        private void SalvarFunc()
        {
            if (!(string.IsNullOrEmpty(textBoxNome.Text) || string.IsNullOrEmpty(maskedTextBoxCpf.Text)))
            {
                if (string.IsNullOrEmpty(labelCargoDescricao.Text))
                {
                    FormMessage.ShowMessegeWarning("Defina o cargo do funcionário");
                    buttonBuscarCargo.Select();
                    return;
                }

                PreencherFuncInfo();
                SalvarThread();

                if (idSave > 0)
                {
                    if (modSave == 4)
                    {
                        FornecedorInfo forn = new FornecedorInfo
                        {
                            forNome = "PADRAO",
                            forIdFunc = idSave,
                            forpadrao = 1
                        };

                        fornecedorNegocios.InsertFornecedor(forn);
                    }

                    infoFunc.funId = idSave;
                    SalvarUser(infoFunc);
                    FormMessage.ShowMessegeInfo("Registro salvo com sucesso!");
                    FormMessage.ShowMessegeInfo("Ao logar pela primeira vez, deverá inserir o proprio CPF, como LOGIN E SENHA!");
                    buttonSalvar.Enabled = false;
                }
            }
            else
            {
                FormMessage.ShowMessegeWarning("Insira no mínimo o nome e o cpf do funcionário e selecione uma unidade!");
            }
        }

        private void SalvarCliente()
        {
            if (CamposObrigatorios())
            {
                PreencherClienteInfo();
                SalvarThread();

                //FormMessage.ShowMessegeInfo("Registro salvo com sucesso!");

                FormServicoTipo formServicoTipo = new FormServicoTipo();
                if (formServicoTipo.ShowDialog(this) == DialogResult.Yes)
                {
                    FormIphoneModelo formIphoneModelo = new FormIphoneModelo(infoCliente);
                    if(formIphoneModelo.ShowDialog(this) == DialogResult.Yes)
                    {
                        formIphoneModelo.Dispose();
                        FormServico formServico = new FormServico(infoCliente);
                        formServico.ShowDialog(this);
                        formServico.Dispose();
                    }
                }

                formServicoTipo.Dispose();
                this.Close();
            }
        }

        private bool CamposObrigatorios()
        {
            string Nome, Tel, Email, Cep, Logradouro, Referencia, Complemento, Cidade, Bairro, Uf;
            Nome = textBoxNome.Text.Trim();
            Tel = maskedTextBoxTel1.Text.Trim();
            Email = textBoxEmail.Text.Trim();
            Cep = maskedTextBoxCep.Text.Trim();
            Logradouro = textBoxLogradouro.Text.Trim();
            Referencia = textBoxPontoReferencia.Text.Trim();
            Complemento  = textBoxComplemento.Text.Trim();
            Cidade = textBoxCidade.Text.Trim();
            Bairro = textBoxBairro.Text.Trim();
            Uf = textBoxUF.Text.Trim();


            if (String.IsNullOrEmpty(Nome) || String.IsNullOrEmpty(Tel) || String.IsNullOrEmpty(Email) || 
                String.IsNullOrEmpty(Cep) || String.IsNullOrEmpty(Logradouro) || String.IsNullOrEmpty(Referencia) ||
                String.IsNullOrEmpty(Complemento) || String.IsNullOrEmpty(Bairro) || String.IsNullOrEmpty(Cidade) ||
                String.IsNullOrEmpty(Uf))
            {
                if (String.IsNullOrEmpty(Nome))
                    labelNome.ForeColor = Color.Red;

                if (String.IsNullOrEmpty(Tel))
                    labelTel.ForeColor = Color.Red;

                if (String.IsNullOrEmpty(Email))
                    labelEmail.ForeColor = Color.Red;

                if (String.IsNullOrEmpty(Cep))
                    labelCep.ForeColor = Color.Red;

                if (String.IsNullOrEmpty(Logradouro))
                    labelLogradouro.ForeColor = Color.Red;

                if (String.IsNullOrEmpty(Referencia))
                    labelPontoReferencia.ForeColor = Color.Red;

                if (String.IsNullOrEmpty(Complemento))
                    labelComplemento.ForeColor = Color.Red;

                if (String.IsNullOrEmpty(Bairro))
                    labelBairro.ForeColor = Color.Red;

                if (String.IsNullOrEmpty(Cidade))
                    labelCidade.ForeColor = Color.Red;

                if (String.IsNullOrEmpty(Uf))
                    labelUf.ForeColor = Color.Red;

                MessageBox.Show("Todos os campos, em vermelho, devem ser preenchidos!", "ADVERTÊNCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }
            else
                return true;
        }


        private void PreencherFornecedorInfo()
        {
            infoFornecedor = new FornecedorInfo
            {
                forBairro = textBoxBairro.Text,
                forCep = maskedTextBoxCep.Text,
                forCidade = textBoxCidade.Text,
                forCnpj = maskedTextBoxCpf.Text,
                forComplemento = textBoxComplemento.Text,
                forEmail = textBoxEmail.Text,
                forLogradouro = textBoxLogradouro.Text,
                forNome = textBoxNome.Text,
                forTelefone = maskedTextBoxTel1.Text + (string.IsNullOrEmpty(maskedTextBoxTel2.Text) ? "" : "/" + maskedTextBoxTel2.Text),
                forUf = textBoxUF.Text,
                forIdFunc = Form1.User.useidfuncionario,
            };
        }

        private void PreencherFuncInfo()
        {
            UnidadeColecao unidadeColecao = empresaNegocios.ConsultarUnidade();
            infoFunc = new FuncInfo
            {
                funBairro = textBoxBairro.Text,
                funCep = maskedTextBoxCep.Text,
                funCidade = textBoxCidade.Text,
                funCpf = maskedTextBoxCpf.Text,
                funComplemento = textBoxComplemento.Text,
                funEmail = textBoxEmail.Text,
                funLogradouro = textBoxLogradouro.Text,
                funNome = textBoxNome.Text,
                funTelefone = maskedTextBoxTel1.Text + (string.IsNullOrEmpty(maskedTextBoxTel2.Text) ? "" : "/" + maskedTextBoxTel2.Text),
                funUf = textBoxUF.Text,
                funNiver = string.IsNullOrEmpty(textBoxNiver.Text) ? DateTime.Now.Date : Convert.ToDateTime(textBoxNiver.Text).Date,
                funIdUnidade = Form1.Unidade.uniid,
                funidcargo = codCargo
            };
        }

        private void PreencherClienteInfo()
        {
            infoCliente = new ClienteInfo
            {
                cliid = idSave,
                clicpf = maskedTextBoxCpf.Text,
                cliemail = textBoxEmail.Text,
                cliiduser = Form1.User.useidfuncionario,
                cliniver = string.IsNullOrEmpty(textBoxNiver.Text) ? DateTime.Now.Date : Convert.ToDateTime(textBoxNiver.Text).Date,
                clinome = textBoxNome.Text,
                clitel = maskedTextBoxTel1.Text + (string.IsNullOrEmpty(maskedTextBoxTel2.Text) ? "" : "/" + maskedTextBoxTel2.Text)
            };

            infoEnd = new EnderecoInfo
            {
                endid = idEnd,
                endcep = maskedTextBoxCep.Text,
                endcomplemento = textBoxComplemento.Text,
                endbairro = textBoxBairro.Text,
                endcidade = textBoxCidade.Text,
                endlogradouro = textBoxLogradouro.Text,
                enduf = textBoxUF.Text,
                endidcliente = idSave,
                endreferencia = textBoxPontoReferencia.Text,
                endpadrao = true
            };
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            switch (modSave)
            {
                case 0:
                    SalvarCliente();
                    break;
                case 1:
                    break;
                case 2:
                    SalvarFunc();
                    break;
                case 3:
                    SalvarFornecedor();
                    break;
                case 4:
                    SalvarFunc();
                    break;
                default:
                    break;
            }
        }

        private void SalvarThread()
        {
            thread = new Thread(Salvar);
            form1.ExecutarThread(thread, progressBar1, labelBarra);
        }

        private void Salvar()
        {
            switch (modSave)
            {
                case 0:
                    if (infoCliente.cliid == 0)
                    {
                        idSave = negocioCliente.Inserir(infoCliente);

                        if (idSave > 0)
                        {
                            infoCliente.cliid = idSave;
                            infoEnd.endidcliente = idSave;
                            negocioCliente.InserirEndereco(infoEnd);
                        }
                    }
                    else
                    {
                        idSave = negocioCliente.UpdateCliente(infoCliente);
                        negocioCliente.UpdateEndereco(infoEnd);
                    }

                    break;
                case 1:
                    //idSave = autorizadaNegocios.InsertAutorizada(infoAutorizada);
                    break;
                case 2:
                    idSave = funcNegocios.InsertFunc(infoFunc);
                    break;
                case 3:
                    idSave = fornecedorNegocios.InsertFornecedor(infoFornecedor);
                    break;
                case 4:
                    idSave = funcNegocios.InsertFunc(infoFunc);
                    break;
                default:
                    break;
            }

            Form1.encerrarThread = true;

            if (idSave > 0 && modSave != 0)
                FormMessage.ShowMessegeInfo("Registro salvo com sucesso!");
        }

        private void Alterar()
        {
            switch (modSave)
            {
                case 0:
                    idSave = negocioCliente.Inserir(infoCliente);

                    if (idSave > 0)
                    {
                        infoCliente.cliid = idSave;
                        infoEnd.endidcliente = idSave;
                        negocioCliente.InserirEndereco(infoEnd);
                    }
                    break;
                case 1:
                    //idSave = autorizadaNegocios.InsertAutorizada(infoAutorizada);
                    break;
                case 2:
                    idSave = funcNegocios.InsertFunc(infoFunc);
                    break;
                case 3:
                    idSave = fornecedorNegocios.InsertFornecedor(infoFornecedor);
                    break;
                case 4:
                    break;
                default:
                    break;
            }

            Form1.encerrarThread = true;

            //if (idSave > 0)
            //    FormMessage.ShowMessegeInfo("Registro salvo com sucesso!");
        }

        private void linkLabelCep_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Process.Start("http://www.buscacep.correios.com.br/sistemas/buscacep/");
        }

        private void maskedTextBoxCpf_Leave(object sender, EventArgs e)
        {

            cpf = maskedTextBoxCpf.Text;


            //preencher o formulário com os meus dados para testes
            if (cpf == "71992776512")
            {
                PreencherFormClienteTeste();
                return;
            }

            ValidarCpfCnpj validarCpfCnpj = new ValidarCpfCnpj(cpf);

            if (cpf != "00000000000")
            {
                if (cpf.Length >= 11)
                {
                    if (cpf.Length > 11)
                    {
                        if (validarCpfCnpj.CpfCpnjValido())
                        {
                            maskedTextBoxCpf.Mask = "00,000,000/0000-00";
                            ConsultarCpf();
                        }
                        else
                        {
                            FormMessage.ShowMessegeWarning("CNPJ inválido! Tente novamente...");
                            maskedTextBoxCpf.Clear();
                            maskedTextBoxCpf.Focus();
                        }
                    }
                    else
                    {
                        if (validarCpfCnpj.CpfCpnjValido())
                        {
                            maskedTextBoxCpf.Mask = "000,000,000-00";
                            ConsultarCpf();
                        }
                        else
                        {
                            FormMessage.ShowMessegeWarning("CPF inválido! Tente novamente...");
                            maskedTextBoxCpf.Clear();
                            maskedTextBoxCpf.Focus();
                        }
                    }

                }
                else
                    maskedTextBoxCpf.Clear();
            }
        }
        private void ConsultarCpf()
        {
            if (modSave == 0)
            {
                colecaoCliente = negocioCliente.ConsultarPorCpf(cpf);

                if (colecaoCliente != null)
                {
                    infoCliente = colecaoCliente[0];
                    PreencherFormCliente();
                }
            }
        }

        private void maskedTextBoxCpf_Enter(object sender, EventArgs e)
        {
            maskedTextBoxCpf.Mask = null;
        }

        private void buttonFechar_Click(object sender, EventArgs e)
        {
            if (config)
                Application.Exit();
            else
                this.Close();
        }

        private void pictureBoxBuscarUnidade_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            CepInfo cepInfo = new CepInfo();

            cepInfo = negocioCliente.ConsultarCep(maskedTextBoxCep.Text);

            if (cepInfo != null)
            {
                textBoxLogradouro.Text = cepInfo.Logradouro;
                textBoxBairro.Text = cepInfo.Bairro;
                textBoxCidade.Text = cepInfo.Cidade;
                textBoxUF.Text = cepInfo.Uf;
                textBoxComplemento.Select();
            }
            else
            {
                FormMessage.ShowMessegeWarning("CEP não encontrado, tente outro CEP!");
            }
        }

        private void buttonBuscarUnidade_Click(object sender, EventArgs e)
        {
            CodDescricaoColecao codDescricaoColecao = funcNegocios.ConsultarCargos();
            Form_ConsultarColecao form_ConsultarColecao = new Form_ConsultarColecao();

            if (codDescricaoColecao != null)
            {
                foreach (CodDescricaoInfo cod in codDescricaoColecao)
                {
                    Form_Consultar form_Consultar = new Form_Consultar
                    {
                        Cod = string.Format("{0:000}", cod.cod),
                        Descricao = cod.descricao
                    };

                    form_ConsultarColecao.Add(form_Consultar);
                }
            }

            FormConsultar_Cod_Descricao formConsultar_Cod_Descricao = new FormConsultar_Cod_Descricao(form_ConsultarColecao, "Unidades");
            formConsultar_Cod_Descricao.ShowDialog(this);

            if (formConsultar_Cod_Descricao.DialogResult == DialogResult.Yes)
            {
                codCargo = Convert.ToInt32(formConsultar_Cod_Descricao.Selecionado.Cod);
                labelCargoDescricao.Text = formConsultar_Cod_Descricao.Selecionado.Descricao;
            }
        }

        private void buttonAddNiver_Click(object sender, EventArgs e)
        {
            FormAddData formAddData = new FormAddData();
            formAddData.ShowDialog(this);
            formAddData.Dispose();

            if (formAddData.DialogResult == DialogResult.Yes)
            {
                DateTime data = Convert.ToDateTime(formAddData.textoData);
                textBoxNiver.Text = data.ToString("ddd, dd 'de' MMMM 'de' yyyy").ToUpper();
                maskedTextBoxCep.Select();
            }

        }

        private void TextBoxNiver_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormCadastroPessoa_Load(object sender, EventArgs e)
        {
            maskedTextBoxCpf.Select();
        }
    }
}
