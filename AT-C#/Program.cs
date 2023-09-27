using AT;
using System;
using System.Collections.Generic;

class Program
{
    private static IRepoJogador repoJogador;

    public static void Main(string[] args)
    {
        repoJogador = InicializarRepositorio();

        while (true)
        {
            Console.WriteLine("Gerenciador de Jogador de Futebol");
            Console.WriteLine("Selecione uma das opções abaixo:");
            Console.WriteLine("[1] - Pesquisar Jogador(es)");
            Console.WriteLine("[2] - Adicionar novo Jogador");
            Console.WriteLine("[3] - Alterar Jogador");
            Console.WriteLine("[4] - Excluir Jogador");
            Console.WriteLine("[5] - Sair");
            int Menu = int.Parse(Console.ReadLine());
            switch (Menu)
            {
                case 1:
                    Console.Write("Digite o nome para consulta: ");
                    string procurarNome = Console.ReadLine();
                    var resultadoProcura = repoJogador.ProcurarJogadorPorNome(procurarNome);

                    if (resultadoProcura.Count > 0)
                    {
                        Console.WriteLine("Resultados da pesquisa:");
                        for (int i = 0; i < resultadoProcura.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. Nome: {resultadoProcura[i].Nome}, Sobrenome: {resultadoProcura[i].UltSobrenome}, Time: {resultadoProcura[i].Time}");
                        }

                        Console.Write("Escolha o número do Jogador para ver detalhes: ");
                        int choiceIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                        if (choiceIndex >= 0 && choiceIndex < resultadoProcura.Count)
                        {
                            var jogadorSelecionado = resultadoProcura[choiceIndex];
                            Console.WriteLine($"Detalhes do Jogador: Nome: {jogadorSelecionado.Nome}, Sobrenome: {jogadorSelecionado.UltSobrenome}, Time: {jogadorSelecionado.Time}, Idade: {jogadorSelecionado.CalcularIdade()}, Convocado: {jogadorSelecionado.Convocado}, Gols na Temporada: {jogadorSelecionado.Gols}, Número da Camisa: {jogadorSelecionado.NumCamisa}");
                        }
                        else
                        {
                            Console.WriteLine("Opção inválida.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nenhum Jogador encontrado com esse nome.");
                    }
                    break;

                case 2:
                    Console.WriteLine("Quantos jogador(es) serão cadastrado(s)?");
                    int numCadastros = int.Parse(Console.ReadLine());
                    for (int i = 0; i < numCadastros; i++)
                    {
                        AdicionarJogador();
                    }
                    break;

                case 3:
                    AlterarJogador();
                    break;

                case 4:
                    ExcluirJogador();
                    break;

                case 5:
                    Console.WriteLine("Saindo...");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    private static IRepoJogador InicializarRepositorio()
    {
        var repoMemoriaJson = new RepoJogadorJson();
        var ultimosCinco = repoMemoriaJson.ObterUltimosCinco();
        if (ultimosCinco.Count < 5 && ultimosCinco.Count != 0)
        {
            Console.WriteLine("Todos os jogadores cadastrados:");
            foreach (var jogador in ultimosCinco)
            {
                Console.WriteLine($"Nome: {jogador.Nome}, Time: {jogador.Time}");
            }
        }
        if(ultimosCinco.Count >= 5)
        {
            Console.WriteLine("Últimos 5 jogadores cadastrados:");
            foreach (var jogador in ultimosCinco)
            {
                Console.WriteLine($"Nome: {jogador.Nome}, Time: {jogador.Time}");
            }
        }
        Console.WriteLine("Escolha o tipo de memória:");
        Console.WriteLine("[1] - Memória");
        Console.WriteLine("[2] - JSON");
        int escolhaMemoria = int.Parse(Console.ReadLine());

        switch (escolhaMemoria)
        {
            case 1:
                return new RepoJogadorMemoria();
            case 2:
                return new RepoJogadorJson();
            default:
                Console.WriteLine("Opção inválida. Usando memória por padrão.");
                return new RepoJogadorMemoria();
        }
    }

    private static void AdicionarJogador()
    {
        repoJogador.AdicionarJogador();
    }

    private static void AlterarJogador()
    {
        repoJogador.AlterarJogador();
    }

    private static void ExcluirJogador()
    {
        repoJogador.ExcluirJogador();
    }
}
