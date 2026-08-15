using System;
using System.Collections.Generic;
using SistemaClientes.Models;
using SistemaClientes.Repositories;

namespace SistemaClientes.Services
{
    public class OrdenadorClientes
    {
        public static void Ordenar(IRepositorioCli repositorio, int escolhaCampo, int ordem)
        {
            // Pega a lista atual dos clientes
            List<Cliente> listaOriginal = repositorio.Listar();

            // Cria uma instância do repositório de ordenação com a lista atual
            RepOrdenacao repOrdenar = new RepOrdenacao(listaOriginal);

            List<Cliente> listaOrdenada;

            if (escolhaCampo == 1) // Por nome
            {
                listaOrdenada = ordem == 1 ? repOrdenar.OrdenarPorNomeCres() : repOrdenar.OrdenarPorNomeDecres();
            }
            else if (escolhaCampo == 2) // Por data de nascimento
            {
                listaOrdenada = ordem == 1 ? repOrdenar.OrdenarPorDataCres() : repOrdenar.OrdenarPorDataDecres();
            }
            else
            {
                Console.WriteLine("Opção de ordenação inválida.");
                return;
            }

            // Exibe a lista ordenada
            Console.WriteLine("\nClientes ordenados:\n");
            foreach (var cliente in listaOrdenada)
            {
                cliente.Exibir();
            }
        }
    }
}
