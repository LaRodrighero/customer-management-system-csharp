using System;


namespace SistemaClientes.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }

        public Cliente(int id, string nome, string cpf, DateTime dataNascimento)
        {
            this.Id = id;
            this.Nome = nome;
            this.CPF = cpf;
            this.DataNascimento = dataNascimento;
        }

        public void Exibir()
        // Método público que não retorna nada (void)
        // Serve para mostrar as informações do cliente na tela
        {
            Console.WriteLine($"ID: {Id}| Nome: {Nome}| CPF: {CPF} | Data Nasc.: {DataNascimento:dd/MM/yyyy}");
            //Escreve na tela as propriedades do cliente formatada
            //DataNascimento:dd/MM/yyyy formata a data no padrão dia/mês/ano
        }

        public override string ToString()
        {
            return $"ID: {Id} | Nome: {Nome} | CPF: {CPF} | Data Nasc: {DataNascimento:dd/MM/yyyy}";
        }
    }
}