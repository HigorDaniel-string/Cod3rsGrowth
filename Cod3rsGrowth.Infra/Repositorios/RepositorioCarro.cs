﻿using LinqToDB;
using Cod3rsGrowth.Dominio.Entidades;
using Cod3rsGrowth.Servicos.Servicos;
using Cod3rsGrowth.Dominio.Interfaces;
using Cod3rsGrowth.Infra.ConexaoComBanco;

namespace Cod3rsGrowth.Infra.Repositorios
{
    public class RepositorioCarro : IRepositorioCarro
    {
        private MeuContextoDeDados _conexao;
        private ServicoVenda _servicoVenda;

        public RepositorioCarro(MeuContextoDeDados meuContextoDeDados, ServicoVenda servicoVenda)
        {
            _conexao = meuContextoDeDados;
            _servicoVenda = servicoVenda;
        }

        public List<Carro> ObterTodos(FiltroCarro? filtroCarro = null)
        {
            var query = FiltroParaBusca(filtroCarro);
            var carrosFiltrados = query.ToList();

            return carrosFiltrados;
        }

        public Carro ObterPorId(int IdDeBusca)
        {
            var query = from p in _conexao.Carro
                        where p.Id == IdDeBusca
                        select p;

            var resultado = query.FirstOrDefault()
                ?? throw new Exception($"Carro com ID {IdDeBusca} não encontrado.");

            return resultado;
        }

        public Carro Criar(Carro carro)
        {
            carro.Id = _conexao.InsertWithInt32Identity(carro);
            return carro;
        }

        public Carro Editar(Carro carroAtualizado)
        {
            var carroDesejado = ObterPorId(carroAtualizado.Id);

            _conexao.Update(carroAtualizado);
            return carroAtualizado;
        }

        public void Remover(int Id)
        {
                _conexao.Carro
                 .Where(carro => carro.Id == Id)
                 .Delete();
        }

        private IQueryable<Carro> FiltroParaBusca(FiltroCarro? filtroCarro)
        {
            IQueryable<Carro> query = _conexao.Carro;

            if (filtroCarro is null) return query;

            if (!string.IsNullOrEmpty(filtroCarro.Modelo))
                query = query.Where(d => d.Modelo.Contains(filtroCarro.Modelo));

            if (filtroCarro.Cor.HasValue)
                query = query.Where(d => d.Cor == filtroCarro.Cor);

            if (filtroCarro.Marca.HasValue)
                query = query.Where(d => d.Marca == filtroCarro.Marca);

            if (filtroCarro.Flex.HasValue)
                query = query.Where(d => d.Flex == filtroCarro.Flex);

            if(filtroCarro.Disponiveis.HasValue){
                var vendas = _servicoVenda.ObterTodos();

                vendas.ForEach(x => {
                    query = query
                    .Where(c => c.Id != x.IdDoCarroVendido);
                });
            }
            return query;
        }
    }
}
