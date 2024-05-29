﻿using Cod3rsGrowth.Dominio.Entities;
using Cod3rsGrowth.Infra.Interfaces;
using Cod3rsGrowth.Infra.Repositorios;

namespace Cod3rsGrowth.Testes
{
    public class RepositorioVendaMock : IRepositorioVenda
    {
        private List<Venda> _repositorioVenda = ListaSingleton.Instance.RepositorioVenda;
        private int _novoId = 0;

        public List<Venda> ObterTodos()
        {
            return _repositorioVenda;
        }

        public Venda ObterPorId(int IdDeBusca)
        {
            return _repositorioVenda.Find(objeto => objeto.Id == IdDeBusca)
                ?? throw new Exception($"A venda com ID {IdDeBusca} não foi encontrada");
        }

        public void Criar(Venda venda)
        {
            venda.Id = _novoId;
            _novoId++;
            _repositorioVenda.Add(venda);
        }

        public void Editar(Venda venda)
        {
            var listaDoBanco = ObterTodos();
            var indexDesejado = venda.Id;

            if (venda.Nome != null) listaDoBanco[indexDesejado].Nome = venda.Nome;
            if (venda.Email != null) listaDoBanco[indexDesejado].Email = venda.Email;
            if (venda.Cpf != null) listaDoBanco[indexDesejado].Cpf = venda.Cpf;
            if (venda.Telefone != null) listaDoBanco[indexDesejado].Telefone = venda.Telefone;

            listaDoBanco[venda.Id] = venda;
            _repositorioVenda = listaDoBanco;
        }
    }
}
