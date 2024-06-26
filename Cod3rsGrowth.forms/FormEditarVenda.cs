﻿using Cod3rsGrowth.Dominio.Entidades;
using Cod3rsGrowth.Servicos.Servicos;
using Cod3rsGrowth.Servicos.Validadores;
using Microsoft.IdentityModel.Tokens;

namespace Cod3rsGrowth.Forms
{
    public partial class FormEditarVenda : Form
    {
        private int _idDaEdicao;
        private ServicoCarro _servico;
        private ServicoVenda _servicoVenda;
        private ValidacoesVenda _validacoesVenda;
        private List<Carro> carro = new List<Carro>();
        private FiltroCarro _filtro = new FiltroCarro();
        private FiltroVenda _filtroVenda = new FiltroVenda();
        private List<string> comboBoxSelecionarCarro = new List<string>();
        private string _nome;
        private string _cpf;
        private string _telefone;
        private string _email;
        private string _data;
        public FormEditarVenda(int IdVenda, ServicoCarro servicoCarro, ServicoVenda servico, ValidacoesVenda validacoes, string nome, string cpf, string telefone, string email, string data)
        {
            _idDaEdicao = IdVenda;
            _servicoVenda = servico;
            _servico = servicoCarro;
            _validacoesVenda = validacoes;

            _nome = nome;
            _cpf = cpf;
            _telefone = telefone;
            _email = email;
            _data = data;

            InitializeComponent();
        }

        private void AoCarregarFomEditarVenda(object sender, EventArgs e)
        {
            CarregarComboBoxCarro();
            CarregarDados();
        }

        private void AoClicarNoBotaoCancelarEditarVenda(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Erro cancelar Edicao");
            }
        }

        private void AoClicarNoBotaoSalvarEdicaoVenda(object sender, EventArgs e)
        {
            try
            {
                carro = _servico.ObterTodos(_filtro);
                var IdDoCarroComprado = carro[selecionandoCarro.SelectedIndex].Id;
                var valorPago = carro[selecionandoCarro.SelectedIndex].ValorDoVeiculo;
                var vendaNova = new Venda
                {
                    Id = _idDaEdicao,
                    Cpf = txtCpf.Text,
                    Email = txtEmail.Text,
                    IdDoCarroVendido = IdDoCarroComprado,
                    ValorTotal = valorPago,
                    DataDeCompra = DateTime.Parse(_data),
                    Nome = txtNome.Text,
                    Telefone = txtTelefone.Text
                };

                _servicoVenda.Editar(vendaNova);
                MessageBox.Show($"Venda {_idDaEdicao} editada com successo!", "Editando venda");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        private void CarregarComboBoxCarro()
        {
            try
            {
                IEnumerable<Carro> obter;
                var carros = _servico.ObterTodos(_filtro);
                var venda = _servicoVenda.ObterTodos(_filtroVenda);
                if (venda.Count != 0 && venda.FirstOrDefault().Id != _idDaEdicao)
                    obter = carros.Where(x => x.Id != venda.FirstOrDefault().IdDoCarroVendido);
                else
                    obter = carros;

                foreach (var car in obter)
                {
                    carro.Add(car);
                    comboBoxSelecionarCarro.Add($"ID: {car.Id} Modelo: {car.Modelo} Cor: {car.Cor}");
                }
                selecionandoCarro.DataSource = comboBoxSelecionarCarro;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        private void CarregarDados()
        {
            txtCpf.Text = _cpf;
            txtEmail.Text = _email;
            txtNome.Text = _nome;
            txtTelefone.Text = _telefone;
        }

        private void AoSelecionarCarroComprado(object sender, EventArgs e)
        {
            try
            {
                var var = selecionandoCarro.SelectedItem;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }
    }
}