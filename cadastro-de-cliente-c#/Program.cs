using System.Runtime.CompilerServices;
using cadastro_de_cliente.Modelos;
using System.Text.Json;
using (HttpClient client = new HttpClient())
{
    void Menu()
    {
        Console.WriteLine("Bem vindo ao Sistema de Cadastro de clientes. :)\n");
        Console.WriteLine("1 - Cadastrar um cliente: ");
        Console.WriteLine("2 - Ver dados de um cliente: ");
        Console.Write("Digite a opcao que voce deseja: ");
        int numeroEscolhido = int.Parse(Console.ReadLine()!);

        switch (numeroEscolhido)
        {
            case 1:
                adicionarCliente();
                break;
            case 2:
                ExibirDados();
                break;
            default:
                Console.WriteLine("Escolha uma opcao valida!!!");
                break;
        }

    }

    Menu();

    void adicionarCliente()
    {
        Console.Clear();
        Console.Write("Digite o nome do cliente: ");
        string nome = Console.ReadLine()!;
        Console.Write("Digite a idade do cliente: ");
        int idade = int.Parse(Console.ReadLine()!);
        Console.Write("Digite o melhor email do cliente: ");
        string email = Console.ReadLine()!;
        if (string.IsNullOrEmpty(email))
        {
            Console.WriteLine("Digite um email valido!!! (Ex: email@email.com)");
            Console.ReadKey();
            Console.Clear();
            Menu();
            return;

        }

        Cliente cliente = new(nome, idade, email);
        Cliente.VerificarDuplicidadeNaLista(cliente, nome, email);
        Cliente.GerarArquivoJson();

        Console.Write("pressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
        Console.Clear();
        Menu();

    }

    void ExibirDados()
    {
        Console.Clear();
        Console.Write("Digite o nome do cliente que deseja ver os dados: ");
        string nomeCliente = Console.ReadLine()!;
        Cliente.exibirDadosDoCliente(nomeCliente);
        Console.Write("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
        Console.Clear();
        Menu();

    }

}



