﻿using FluentValidation;
using Cod3rsGrowth.Dominio.Entidades;
using Cod3rsGrowth.Dominio.Interfaces;
using Cod3rsGrowth.Servicos.Validadores;

namespace Cod3rsGrowth.Servicos.Servicos
{
    public class ServicoCarro
    {
        private ValidacoesCarro _validadorCarro;
        private readonly IRepositorioCarro _repositorioCarro;

        public ServicoCarro(IRepositorioCarro repositorioCarro, ValidacoesCarro validadorCarro)
        {
            _validadorCarro = validadorCarro;
            _repositorioCarro = repositorioCarro;
        }

        public List<Carro> ObterTodos(FiltroCarro? carro = null)
        {
            return _repositorioCarro.ObterTodos(carro);
        }

        public Carro ObterPorId(int IdDoItem)
        {
            return _repositorioCarro.ObterPorId(IdDoItem);
        }

        public Carro Criar(Carro carro)
        {
            var retornoValidacaoCarro = _validadorCarro.Validate(carro);

            if (!retornoValidacaoCarro.IsValid)
            {
                var erros = string.Join(Environment.NewLine, retornoValidacaoCarro.Errors.Select(x => x.ErrorMessage));
                throw new ValidationException(erros);
            }

            return _repositorioCarro.Criar(carro);
        }

        public Carro Editar(Carro carro)
        {
            var resultado = _validadorCarro.Validate(carro);

            if (!resultado.IsValid)
            {
                var erros = string.Join(Environment.NewLine, resultado.Errors.Select(x => x.ErrorMessage));
                throw new ValidationException(erros);
            }

            return _repositorioCarro.Editar(carro);
        }

        public void Remover(int IdDeRemocao)
        {
            _repositorioCarro.Remover(IdDeRemocao);
        }
    }
}
