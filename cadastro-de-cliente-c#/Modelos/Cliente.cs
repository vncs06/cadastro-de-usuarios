using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
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

    public static void exibirDadosDoCliente(string nome)
    {
        string nomeDoArquivo = "lista-de-clientes";
        string jsonArquivo = File.ReadAllText(nomeDoArquivo);
        var clientesLista = JsonSerializer.Deserialize<List<Cliente>>(jsonArquivo);
        var clientesCadastrados = clientesLista.Find(t => t.Nome == nome);


        if (listaDeCliente.Count == 0 && jsonArquivo == string.Empty)
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

    public static void VerificarDuplicidadeNaLista(Cliente cliente, string nome, string email)
    {
        if (!listaDeCliente.Any(t => t.Nome == nome || t.Email == email))
        {
            string nomeDoArquivo = "lista-de-clientes";
            string jsonExistete = File.ReadAllText(nomeDoArquivo);
            if (!string.IsNullOrEmpty(jsonExistete))
            {
                var clientesCadastrados = JsonSerializer.Deserialize<List<Cliente>>(jsonExistete) ?? new List<Cliente>();

                if (!clientesCadastrados.Any(t => t.Nome == nome || t.Email == email))
                {
                    listaDeCliente.Add(cliente);
                    Console.WriteLine("Cliente cadastrado com sucesso :)");

                }
                else
                {
                    Console.WriteLine("O arquivo nao foi cadastrado com sucesso, verifique se o nome ou email ja estao em uso.");

                }

            }
            else
            {
                listaDeCliente.Add(cliente);
                Console.WriteLine("Cliente cadastrado com sucesso :)");

            }

        }
        else
        {
            Console.WriteLine("O arquivo nao foi cadastrado com sucesso, verifique se o nome ou email ja estao em uso.");

        }
        


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
