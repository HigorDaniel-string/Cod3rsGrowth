﻿namespace Cod3rsGrowth.Dominio.Entities
{
    class Vendas
    {
        public string Nome { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        public int Id { get; set; }
        public int Total { get; set; }
        public List<Carro> CarrinhoDeCompra { get; set; }
        public DateTime DataDeCompra { get; set; }

        public Vendas(string nome, DateTime dataDeNascimento, string telefone, string cPF, int id, int total, DateTime dataDeCompra)
        {
            Nome = nome;
            DataDeNascimento = dataDeNascimento;
            Telefone = telefone;
            CPF = cPF;
            Id = id;
            Total = total;
            DataDeCompra = dataDeCompra;
        }
    }
}
