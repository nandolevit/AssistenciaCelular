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
    public partial class FormPessoa : Form
    {
        Form1 form1 = new Form1();
        string cpf;
        int codCargo;

        EnumPessoaTipo enumPessoa = new EnumPessoaTipo();
        PessoaInfo infoPessoa;
        public PessoaInfo SelecionadoPessoa { get; set; }

        ClienteNegocios negocioCliente = new ClienteNegocios(Form1.Empresa.empconexao);
        FuncNegocios funcNegocios = new FuncNegocios(Form1.Empresa.empconexao);
        EmpresaNegocios empresaNegocios = new EmpresaNegocios(Form1.Empresa.empconexao);


        public FormPessoa(EnumPessoaTipo pessoa)
        {
            Inicializar();
            enumPessoa = pessoa;
        }

        public FormPessoa(PessoaInfo pessoa)
        {
            Inicializar();
            infoPessoa = pessoa;
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

        private bool CamposObrigatorios()
        {
            string Nome, Tel, Email;
            Nome = textBoxNome.Text.Trim();
            Tel = maskedTextBoxTel1.Text.Trim();
            Email = textBoxEmail.Text.Trim();


            if (String.IsNullOrEmpty(Nome) || String.IsNullOrEmpty(Tel) || String.IsNullOrEmpty(Email))
            {
                if (String.IsNullOrEmpty(Nome))
                    labelNome.ForeColor = Color.Red;

                if (String.IsNullOrEmpty(Tel))
                    labelTel.ForeColor = Color.Red;

                if (String.IsNullOrEmpty(Email))
                    labelEmail.ForeColor = Color.Red;

                MessageBox.Show("Todos os campos, em vermelho, devem ser preenchidos!", "ADVERTÊNCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }
            else
                return true;
        }


        private void PreencherPessoaInfo()
        {
            infoPessoa = new PessoaInfo
            {
                pssendbairro = textBoxBairro.Text,
                pssendcep = maskedTextBoxCep.Text,
                pssendcidade = textBoxCidade.Text,
                psscpf = maskedTextBoxCpf.Text,
                pssendcomplemento = textBoxComplemento.Text,
                pssemail = textBoxEmail.Text,
                pssendlogradouro = textBoxLogradouro.Text,
                pssnome = textBoxNome.Text,
                psstelefone = maskedTextBoxTel1.Text + (string.IsNullOrEmpty(maskedTextBoxTel2.Text) ? "" : "/" + maskedTextBoxTel2.Text),
                pssenduf = textBoxUF.Text,
                pssiduser = Form1.User.useidfuncionario,
                pssidtipo = enumPessoa,
                pssniver = string.IsNullOrEmpty(textBoxNiver.Text) ? DateTime.Now.Date : Convert.ToDateTime(textBoxNiver.Text).Date                
            };

            SelecionadoPessoa = infoPessoa;
        }

        

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            if (CamposObrigatorios())
            {
                PreencherPessoaInfo();
                this.DialogResult = DialogResult.Yes;
            }
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
            //colecaoCliente = negocioCliente.ConsultarPorCpf(cpf);

            //if (colecaoCliente != null)
            //{
            //    infoCliente = colecaoCliente[0];
            //    PreencherFormCliente();
            //}
        }

        private void maskedTextBoxCpf_Enter(object sender, EventArgs e)
        {
            maskedTextBoxCpf.Mask = null;
        }

        private void buttonFechar_Click(object sender, EventArgs e)
        {
            //if (config)
            //    Application.Exit();
            //else
            //    this.Close();
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
