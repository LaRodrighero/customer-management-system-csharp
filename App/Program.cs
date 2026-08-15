using System;
using System.Collections.Generic;
using System.Linq;
using SistemaClientes.Models;
using SistemaClientes.Repositories;
using SistemaClientes.Services;
using SistemaClientes.Utils;


namespace SistemaClientes.App
{
    class Program
    {
        static IRepositorioCli repositorio = new RepositorioCliJson();
        static RepositorioUsuarioJson repositorioUsuarios = new RepositorioUsuarioJson();


        static void Main()
        {

            Usuario? usuarioLogado = null;
            bool sair = false;

            while (!sair && usuarioLogado == null)
            {
                Console.WriteLine("\n==== MENU INICIAL ====");
                Console.WriteLine("1 - Login");
                Console.WriteLine("2 - Cadastrar Novo Usuário");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");
                string? escolha = Console.ReadLine();

                switch (escolha)
                {
                    case "1":
                        usuarioLogado = FazerLogin();
                        break;
                    case "2":
                        CadastrarUsuario();
                        break;
                    case "0":
                        sair = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
            if (sair) return;

            // proteção extra. se ninguém logou, também encerro
            if (usuarioLogado == null) return;
            
            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("\n====MENU====");
                Console.WriteLine("1 - Cadastrar Cliente");
                Console.WriteLine("2 - Listar Clientes");
                Console.WriteLine("3 - Editar Cliente");
                Console.WriteLine("4 - Remover Cliente");
                Console.WriteLine("5 - Buscar Cliente");
                Console.WriteLine("6 - Ordenar Clientes");
                Console.WriteLine("7 - Sair");
                Console.Write("Escolha uma opção: ");
                var opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        CadastrarCliente();
                        break;
                    case "2":
                        ListarClientes();
                        break;
                    case "3":
                        EditarCliente();
                        break;
                    case "4":
                        RemoverCliente();
                        break;
                    case "5":
                        BuscarCliente();
                        break;
                    case "6":
                        Console.WriteLine("ORDENAÇÃO DE CLIENTES");
                        Console.WriteLine("1 - Ordenar por Nome");
                        Console.WriteLine("2 - Ordenar por Data de Nascimento");
                        Console.Write("Escolha uma opção: ");
                        string? escolhaCampoTxt = Console.ReadLine();
                        if (!int.TryParse(escolhaCampoTxt, out int escolhaCampo) || (escolhaCampo != 1 && escolhaCampo != 2))
                        {
                            Console.WriteLine("Opção inválida.");
                            return;
                        }

                        Console.WriteLine();
                        Console.WriteLine("1 - Ordem Crescente (A-Z ou mais velho primeiro)");
                        Console.WriteLine("2 - Ordem Decrescente (Z-A ou mais novo primeiro)");
                        Console.Write("Escolha a ordem: ");
                        string? ordemTxt = Console.ReadLine();
                        if (!int.TryParse(ordemTxt, out int ordem) || (ordem != 1 && ordem != 2))
                        {
                            Console.WriteLine("Opção de ordem inválida.");
                            return;
                        }

                        OrdenadorClientes.Ordenar(repositorio, escolhaCampo, ordem);
                        break;
                    case "7":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }
        static void CadastrarCliente()
        {
            Console.Write("Digite o nome do cliente: ");
            string? nome = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome inválido. O cliente não foi cadastrado.");
                return;
            }

            Console.Write("Digite o CPF do cliente: ");
            string? cpf = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cpf) || !Validador.CpfValido(cpf))
            {
                Console.WriteLine("CPF inválido, use o formato xxx.xxx.xxx-xx.");
                return;
            }
            bool cpfExistente = repositorio.ExisteCpf(cpf);
            if (cpfExistente)
            {
                Console.WriteLine("Este CPF já está cadastrado.");
                return;
            }

            Console.Write("Digite a data de nascimento (formato: dd/mm/aaaa): ");
            string? dataTexto = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(dataTexto) || !Validador.ConvertData(dataTexto, out DateTime dataNascimento))
            {
                Console.WriteLine("Data inválida.");
                return;
            }
            if (Validador.DataFuturo(dataNascimento))
            {
                Console.WriteLine("A data de nascimento não pode estar no futuro.");
                return;
            }
            var cliente = new Cliente(0, nome, cpf, dataNascimento);
            repositorio.Adicionar(cliente);
            Console.WriteLine("Cliente adicionado com sucesso!");
        }
        static void ListarClientes()
        {
            var listaClientes = repositorio.Listar();
            if (listaClientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
            }
            else
            {
                Console.WriteLine("\nClientes cadastrados.");
                foreach (var cliente in listaClientes)
                {
                    cliente.Exibir();
                }
            }
        }
        static void EditarCliente()
        {
            Console.Write("Digite o ID do cliente que deseja alterar: ");
            string? idTexto = Console.ReadLine();
            if (!int.TryParse(idTexto, out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var cliente = repositorio.BuscarPorId(id);
            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado");
                return;
            }

            Console.WriteLine($"Nome atual: {cliente.Nome}");
            Console.Write("Digite o novo nome (ou deixe em branco para manter o atual): ");
            string? novoNome = Console.ReadLine();

            Console.WriteLine($"CPF atual: {cliente.CPF}");
            Console.Write("Insira o novo CPF (ou deixe em branco para manter): ");
            string? novoCpf = Console.ReadLine();

            Console.WriteLine($"Data de nascimento atual: {cliente.DataNascimento:dd/MM/yyyy}");
            Console.Write("Digite a nova data de nascimento (ou deixe em branco para manter): ");
            string? novaDataTexto = Console.ReadLine();
            DateTime? novaData = null;

            if (!string.IsNullOrWhiteSpace(novaDataTexto))
            {
                if (!Validador.ConvertData(novaDataTexto, out DateTime dataConvertida))
                {
                    Console.WriteLine("Data inválida.");
                    return;
                }
                novaData = dataConvertida;
            }

            bool atualizado = repositorio.Editar(id, novoNome, novoCpf, novaData);
            if (atualizado)
            {
                Console.WriteLine("Cliente atualizado com sucesso.");
            }
            else
            {
                Console.WriteLine("Não foi possível atualizar o cliente. Verifique os dados e tente novamente.");
            }
        }

        static void RemoverCliente()
        {
            Console.Write("Digite o ID que deseja remover: ");
            string? removerId = Console.ReadLine();
            if (!int.TryParse(removerId, out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            bool removido = repositorio.Remover(id);
            if (!removido)
            {
                Console.WriteLine("Cliente não encontrado");
            }
            else
            {
                Console.WriteLine("Cliente removido.");
            }
        }

        static void BuscarCliente()
        {
            Console.Write("Digite o nome ou CPF do cliente para busca: ");
            string? termoBusca = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(termoBusca))
            {
                Console.WriteLine("Nenhum termo informado. A busca foi cancelada.");
                return;
            }

            var resultados = repositorio.Buscar(termoBusca);

            if (resultados.Count == 0)
            {
                Console.WriteLine("Nenhum cliente foi localizado.");
            }
            else
            {
                Console.WriteLine("Cliente localizado: ");
                foreach (var cliente in resultados)
                {
                    cliente.Exibir();
                }
            }
        }
        static Usuario? FazerLogin()
        {
            Console.WriteLine("=== LOGIN ===");
            Console.Write("E-mail: ");
            string? email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("E-mail inválido.");
                return null;
            }

            Console.Write("Senha: ");
            string senha = "";
            ConsoleKeyInfo tecla;
            do
            {
                tecla = Console.ReadKey(true);
                if (tecla.Key != ConsoleKey.Backspace && tecla.Key != ConsoleKey.Enter)
                {
                    senha += tecla.KeyChar;
                    Console.Write("*");
                }
                else if (tecla.Key == ConsoleKey.Backspace && senha.Length > 0)
                {
                    senha = senha.Substring(0, senha.Length - 1);
                    Console.Write("\b \b");
                }
            } while (tecla.Key != ConsoleKey.Enter);

            Console.WriteLine();

            var usuario = repositorioUsuarios.BuscarPorEmail(email);
            if (usuario != null && usuario.Senha == senha)
            {
                Console.WriteLine($"\nBem-vindo, {usuario.Nome}!");
                return usuario;
            }

            Console.WriteLine("\nLogin inválido. Tente novamente.");
            return null;
        }
        static void CadastrarUsuario()
        {
            Console.WriteLine("\n=== Cadastro de Novo Usuário ===");

            Console.Write("Nome: ");
            string? nome = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome inválido.");
                return;
            }

            Console.Write("Email: ");
            string? email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email inválido.");
                return;
            }

            if (repositorioUsuarios.Listar().Any(u => u.Email == email))
            {
                Console.WriteLine("Esse e-mail já está cadastrado.");
                return;
            }

            Console.Write("Senha: ");
            string senha = LerSenha();

            Console.Write("Confirme a senha: ");
            string confirmarSenha = LerSenha();

            // Validação de confirmação
            if (senha != confirmarSenha)
            {
                Console.WriteLine("As senhas não conferem.");
                return;
            }

            // Cria e adiciona o novo usuário à lista
            Usuario novoUsuario = new Usuario(nome, email, senha);
            repositorioUsuarios.Adicionar(novoUsuario);

            Console.WriteLine("Usuário cadastrado com sucesso!");
        }

        static string LerSenha()
        {
            string senha = "";
            ConsoleKeyInfo tecla;

            do
            {
                tecla = Console.ReadKey(true);
                if (tecla.Key != ConsoleKey.Backspace && tecla.Key != ConsoleKey.Enter)
                {
                    senha += tecla.KeyChar;
                    Console.Write("*");
                }
                else if (tecla.Key == ConsoleKey.Backspace && senha.Length > 0)
                {
                    senha = senha.Substring(0, senha.Length - 1);
                    Console.Write("\b \b");
                }
            } while (tecla.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return senha;
        }


    }
}