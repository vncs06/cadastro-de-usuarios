using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Text.Json;
namespace cadastro_de_cliente.Modelos;

class Cliente
{
    public static List<Cliente> listaDeCliente = new();

    public Cliente(string nome, int idade, string email)
    {
        Nome = nome;
        Idade = idade;
        Email = email;

    }

    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Email { get; set; }

    public static bool verificarEmail(string email)
    {
        string padrao = @"^[\w\.-]+@[\w\.-]+\.\w{2,}$";
        if (string.IsNullOrWhiteSpace(email))
        {
            return true;

        }

        if (!Regex.IsMatch(email, padrao))
        {
            return true;

        }

        return false;

    }

    public static void exibirDadosDoCliente(string nome)
    {
        string nomeDoArquivo = "lista-de-clientes";

        if (!File.Exists(nomeDoArquivo))
        {
            Console.WriteLine("Nao existem dados de clientes :(");
            return;

        }

        string jsonArquivo = File.ReadAllText(nomeDoArquivo);
        var clientesLista = JsonSerializer.Deserialize<List<Cliente>>(jsonArquivo);
        var clientesCadastrados = clientesLista.Find(t => t.Nome == nome);


        if (listaDeCliente.Count == 0 && string.IsNullOrEmpty(jsonArquivo))
        {
            Console.WriteLine("Nao existem clientes cadastrados :( Voltando ao menu principal...");
            Thread.Sleep(5000);
            Console.Clear();

        }
        else
        {
            if (clientesCadastrados != null)
            {
                Console.WriteLine($"Nome: {clientesCadastrados.Nome}");
                Console.WriteLine($"Idade: {clientesCadastrados.Idade}");
                Console.WriteLine($"Email: {clientesCadastrados.Email}");

            }
            else
            {
                Console.WriteLine("Cliente nao encontrado :( pressione qualquer tecla para voltar ao menu...");
                Console.ReadKey();

            }
            
        }
        

    }

    public static bool VerificarDuplicidadeNaLista(Cliente cliente)
    {
        if(listaDeCliente.Any(t => t.Nome.Equals(cliente.Nome) || t.Email.Equals(cliente.Email)))
        {
            return true;

        }

        string nomeDoArquivo = "lista-de-clientes";
        if (File.Exists(nomeDoArquivo))
        {
            string jsonExistente = File.ReadAllText(nomeDoArquivo);
            var clientesLista = JsonSerializer.Deserialize<List<Cliente>>(jsonExistente) ?? new List<Cliente>();
            if(clientesLista.Any(t => t.Nome.Equals(cliente.Nome) || t.Email.Equals(cliente.Email)))
            {
                return true;

            }

        }

        return false;

    }

    public static void GerarArquivoJson()
    {
        string nomeDoArquivo = "lista-de-clientes";

        if (File.Exists(nomeDoArquivo))
        {
            string jsonExistente = File.ReadAllText(nomeDoArquivo);
            if (!string.IsNullOrEmpty(jsonExistente))
            {
                var clientesJaCadastrados = JsonSerializer.Deserialize<List<Cliente>>(jsonExistente) ?? new List<Cliente>();
                foreach(var cliente in clientesJaCadastrados)
                {
                    if (!listaDeCliente.Any(c => c.Nome == cliente.Nome && c.Email == cliente.Email))
                    {
                        listaDeCliente.Add(cliente);

                    }

                }

            }

        }
        string json = JsonSerializer.Serialize(listaDeCliente, new JsonSerializerOptions { WriteIndented = true});
        File.WriteAllText(nomeDoArquivo, json);

    }

}
