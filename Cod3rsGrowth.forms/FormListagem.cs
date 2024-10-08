using Cod3rsGrowth.Forms;
using Cod3rsGrowth.Dominio.Enums;
using Cod3rsGrowth.Dominio.Entidades;
using Cod3rsGrowth.Servicos.Servicos;
using Microsoft.IdentityModel.Tokens;

namespace Cod3rsGrowth.forms
{
    public partial class FormListagem : Form
    {

        private ServicoCarro _servicoCarro;
        private ServicoVenda _servicoVenda;
        private FiltroCarro _filtroCarro = new FiltroCarro();
        private FiltroVenda _filtroVenda = new FiltroVenda();

        public FormListagem(ServicoCarro servicoCarro, ServicoVenda servicoVenda)
        {
            _servicoCarro = servicoCarro;

            _servicoVenda = servicoVenda;

            InitializeComponent();
        }

        private void AoCarregarFormListagem(object sender, EventArgs e)
        {
            CarregarListasAtualizadas();
            CarregarEnums();
            LimparComboBox();
        }

        private void CarregarListasAtualizadas()
        {
            TabelaVenda.DataSource = _servicoVenda.ObterTodos();
            TabelaCarro.DataSource = _servicoCarro.ObterTodos();
        }

        private void CarregarEnums()
        {
            selecionarCor.DataSource = Enum.GetValues(typeof(Cores));
            selecionarMarca.DataSource = Enum.GetValues(typeof(Marcas));
        }

        private void LimparComboBox()
        {
            selecionarCor.SelectedItem = null;
            selecionarMarca.SelectedItem = null;
        }

        private void AoClicarNoBotaoFiltrarDaTabelaCarro(object sender, EventArgs e)
        {
            try
            {
                if (selecionarMarca != null && selecionarMarca.SelectedItem != null)
                {
                    var indexDesejado = selecionarMarca.SelectedIndex;
                    _filtroCarro.Marca = (Marcas)indexDesejado;
                }

                if (!txtProcurar.Text.IsNullOrEmpty())
                {
                    _filtroCarro.Modelo = txtProcurar.Text;
                }

                if (selecionarCor != null && selecionarCor.SelectedItem != null)
                {
                    var indexdesejado = (Cores)selecionarCor.SelectedIndex;
                    _filtroCarro.Cor = indexdesejado;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Erro ao tentar fitrar carros");
            }

            TabelaCarro.DataSource = _servicoCarro.ObterTodos(_filtroCarro);
        }

        private void AoClicarNoBotaoLimparFiltroDaTabelaCarro(object sender, EventArgs e)
        {
            try
            {
                if (_filtroCarro.Marca != null)
                {
                    _filtroCarro.Marca = null;
                }

                if (_filtroCarro.Modelo != null)
                {
                    _filtroCarro.Modelo = null;
                }

                if (_filtroCarro.Cor != null)
                {
                    _filtroCarro.Cor = null;
                }

                txtProcurar.Clear();
                selecionarCor.SelectedItem = null;
                selecionarMarca.SelectedItem = null;

                TabelaCarro.DataSource = _servicoCarro.ObterTodos(_filtroCarro);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Erro ao tentar limpar filtro carro");
            }
        }

        private void AoClicarNoBotaoFiltrarNaTabelaVenda(object sender, EventArgs e)
        {
            var mascaraData = "  /  /";
            var mascaraCpf = "   .   .   -";
            try
            {
                if (!txtProcurarNome.Text.IsNullOrEmpty())
                {
                    _filtroVenda.Nome = txtProcurarNome.Text;
                }

                if (!textBoxDataInicialVenda.Text.IsNullOrEmpty() && textBoxDataInicialVenda.Text != mascaraData)
                {
                    _filtroVenda.DataDeCompraInicial = textBoxDataInicialVenda.Text;
                    
                }

                if (!textBoxDataFinalVenda.Text.IsNullOrEmpty() && textBoxDataFinalVenda.Text != mascaraData)
                {
                    _filtroVenda.DataDeCompraFinal = textBoxDataFinalVenda.Text;
                }

                if (!txtProcurarEmail.Text.IsNullOrEmpty())
                {
                    _filtroVenda.Email = txtProcurarEmail.Text;
                }

                if (!procurarCpf.Text.IsNullOrEmpty() && procurarCpf.Text != mascaraCpf)
                {
                    _filtroVenda.Cpf = procurarCpf.Text;
                }

                TabelaVenda.DataSource = _servicoVenda.ObterTodos(_filtroVenda);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Erro ao tentar filtrar venda");
            }
        }

        private void AoClicarNoBotaoLimparFiltroDaTabelaVenda(object sender, EventArgs e)
        {
            try
            {
                if (_filtroVenda.Nome != null)
                    _filtroVenda.Nome = null;
                txtProcurarNome.Clear();

                if (_filtroVenda.Cpf != null)
                    _filtroVenda.Cpf = null;
                procurarCpf.Clear();

                if (_filtroVenda.Email != null)
                    _filtroVenda.Email = null;
                txtProcurarEmail.Clear();

                if (_filtroVenda.DataDeCompraFinal != null)
                    _filtroVenda.DataDeCompraFinal = null;
                textBoxDataFinalVenda.Clear();

                if (_filtroVenda.DataDeCompraInicial != null)
                    _filtroVenda.DataDeCompraInicial = null;
                textBoxDataInicialVenda.Clear();

                TabelaVenda.DataSource = _servicoVenda.ObterTodos(_filtroVenda);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Erro ao tentar limpar filtro venda");
            }
        }

        private void AoClicarNoBotaoCriarVenda(object sender, EventArgs e)
        {
            var FormVenda = new FormModificarVenda(_servicoVenda, _servicoCarro);
            FormVenda.CarregarComboBoxCarroCriar();
            FormVenda.AdicionarEventoDeCriar();
            FormVenda.ShowDialog();
            CarregarListasAtualizadas();
        }

        private void AoClicarNoBotaoCriarCarro(object sender, EventArgs e)
        {
            var FormCarro = new FormModificarCarro(_servicoCarro);
            FormCarro.AdicionarEventoDeCriar();
            FormCarro.CarregarEnums();
            FormCarro.LimparComboBox();
            FormCarro.ShowDialog();
            CarregarListasAtualizadas();
        }

        private void AoClicarNoBotaoRemoverVenda(object sender, EventArgs e)
        {
            try
            {
                if (TabelaVenda.Rows.Count != 0)
                {
                    var colunaIdVenda = "ColunaIdVenda";
                    var colunaIdCarro = "IdDoCarroVendido";
                    var linhaSelecionada = TabelaVenda.CurrentCell.RowIndex;
                    var colunaDesejadaIdVenda = TabelaVenda.Columns[colunaIdVenda].Index;

                    var idDaVendaSelecionada = ObterIdSelecionado(colunaIdVenda, linhaSelecionada, TabelaVenda);
                    var idDoCarroSelecionado = ObterIdSelecionado(colunaIdCarro, linhaSelecionada, TabelaVenda);

                    DialogResult resultado = MessageBox.Show($"Deseja excluir permanentemente a venda do ID {idDaVendaSelecionada}?", "Remover Venda", MessageBoxButtons.YesNo);

                    DialogResult resultadoRemoverCarro = MessageBox.Show($"Deseja excluir tamb�m permanentemente o carro ID {idDoCarroSelecionado} que est� associado a venda ID {idDaVendaSelecionada}?", "Remover Carro", MessageBoxButtons.YesNo);

                    if (resultado == DialogResult.Yes && resultadoRemoverCarro == DialogResult.Yes)
                    {
                        _servicoVenda.Remover(idDaVendaSelecionada);
                        _servicoCarro.Remover(idDoCarroSelecionado);
                    }
                    else if (resultado == DialogResult.Yes)
                    {
                        _servicoVenda.Remover(idDaVendaSelecionada);
                    }

                    CarregarListasAtualizadas();
                }
                else
                {
                    MessageBox.Show("N�o � poss�vel remover, pois, a lista est� vazia.", "Erro ao remover Venda");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Erro ao tentar remover venda");
            }
        }

        private void AoClicarNoBotaoRemoverCarro(object sender, EventArgs e)
        {
            try
            {
                if (TabelaCarro.Rows.Count != 0)
                {
                    var linhaSelecionada = TabelaCarro.CurrentCell.RowIndex;
                    var colunaIdCarro = "ColunaIdCarro";
                    var idDoCarroSelecionado = ObterIdSelecionado(colunaIdCarro, linhaSelecionada, TabelaCarro);

                    DialogResult resultadoRemoverCarro = MessageBox.Show($"Deseja excluir permanentemente o carro ID {idDoCarroSelecionado}?", "Remover Carro", MessageBoxButtons.YesNo);

                    if (resultadoRemoverCarro == DialogResult.Yes)
                    {
                        _servicoCarro.Remover(idDoCarroSelecionado);
                    }

                    CarregarListasAtualizadas();
                }
                else
                {
                    MessageBox.Show("N�o � poss�vel remover, pois, a lista est� vazia.", "Erro ao remover Carro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Erro ao tentar remover carro");
            }
        }

        private void AoClicarNoBotaoEditarVenda(object sender, EventArgs e)
        {
            try
            {
                if (TabelaVenda.Rows.Count != 0)
                {
                    var colunaDesejada = "ColunaIdVenda";
                    var linhaSelecionada = TabelaVenda.CurrentCell.RowIndex;
                    var idDaVendaSelecionada = ObterIdSelecionado(colunaDesejada, linhaSelecionada, TabelaVenda);

                    var formVenda = new FormModificarVenda(_servicoVenda, _servicoCarro);
                    formVenda.CarregarValoresDaVenda(idDaVendaSelecionada);
                    formVenda.AdicionarEventoDeEditar(idDaVendaSelecionada);
                    formVenda.CarregarComboBoxCarroEditar(idDaVendaSelecionada);
                    formVenda.Text = "Editando Venda";
                    formVenda.ShowDialog();
                    CarregarListasAtualizadas();
                }
                else
                {
                    MessageBox.Show("N�o � poss�vel editar, pois, a lista est� vazia.", "Erro ao editar Venda");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Erro ao tentar abrir tela de edi��o de venda");
            }
        }

        private void AoClicarNoBotaoEditarCarro(object sender, EventArgs e)
        {
            try
            {
                if (TabelaCarro.Rows.Count != 0)
                {
                    var colunaID = "ColunaIdCarro";
                    var linhaSelecionada = TabelaCarro.CurrentCell.RowIndex;
                    var idSelecionado = ObterIdSelecionado(colunaID, linhaSelecionada, TabelaCarro);

                    var formCarro = new FormModificarCarro(_servicoCarro);
                    formCarro.CarregarEnums();
                    formCarro.CarregarValoresDoCarro(idSelecionado);
                    formCarro.AdicionarEventoDeEditar(idSelecionado);
                    formCarro.Text = "Editando Carro";
                    formCarro.ShowDialog();
                    AtualizarValorDoVeiculoNaVenda(idSelecionado);
                    CarregarListasAtualizadas();
                }
                else
                {
                    MessageBox.Show("N�o � poss�vel editar, pois, a lista est� vazia.", "Erro ao editar Carro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Erro ao tentar abrir tela de edi��o carro");
            }
        }

        private void AtualizarValorDoVeiculoNaVenda(int idDoCarro)
        {
            var carro = _servicoCarro.ObterPorId(idDoCarro);
            var valorDoCarro = carro.ValorDoVeiculo;
            _filtroVenda.IdDoCarroVendido = idDoCarro;
            List<Venda> vendas = _servicoVenda.ObterTodos(_filtroVenda);

            if (vendas.Count != 0)
            {
                var primeiraVenda = vendas[0];
                primeiraVenda.ValorTotal = valorDoCarro;
                _servicoVenda.Editar(primeiraVenda);
            }
            else
            {
                return;
            }

            _filtroVenda = null;
        }

        private int ObterIdSelecionado(string coluna, int linha, DataGridView data)
        {
            var idSelecionado = Convert.ToInt32(data.Rows[linha].Cells[coluna].Value);

            return idSelecionado;
        }

    }
}
