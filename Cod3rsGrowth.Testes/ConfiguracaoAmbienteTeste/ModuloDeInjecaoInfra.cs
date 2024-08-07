﻿using Cod3rsGrowth.Servicos.Servicos;
using Cod3rsGrowth.Dominio.Interfaces;
using Cod3rsGrowth.Testes.Repositorios;
using Cod3rsGrowth.Servicos.Validadores;
using Microsoft.Extensions.DependencyInjection;

namespace Cod3rsGrowth.Testes.ConfiguracaoAmbienteTeste
{
    public static class ModuloDeInjecaoInfra
    {
        public static void BindService(ServiceCollection servicos)
        {
            servicos.AddScoped<ServicoCarro>();
            servicos.AddScoped<ServicoVenda>();
            servicos.AddScoped<ValidacoesCarro>();
            servicos.AddScoped<ValidacoesVenda>();
            servicos.AddScoped<IRepositorioCarro, RepositorioCarroMock>();
            servicos.AddScoped<IRepositorioVenda, RepositorioVendaMock>();
        }
    }
}
