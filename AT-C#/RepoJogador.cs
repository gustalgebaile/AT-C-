using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using AT;

public class RepoJogadorMemoria : IRepoJogador
{
    private List<Jogador> listaJogadores = new List<Jogador>();

    public void AdicionarJogador()
    {
        var jogador = new Jogador();

        Console.WriteLine("Digite o nome do jogador:");
        jogador.Nome = Console.ReadLine();

        Console.WriteLine("Digite o último sobrenome do jogador:");
        jogador.UltSobrenome = Console.ReadLine();

        Console.WriteLine("Digite o time do jogador:");
        jogador.Time = Console.ReadLine();

        Console.WriteLine("Digite a data de nascimento do jogador (yyyy-MM-dd):");
        jogador.DataDeNasc = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("O jogador foi convocado? (S/N):");
        jogador.Convocado = Console.ReadLine().Equals("S", StringComparison.OrdinalIgnoreCase);

        Console.WriteLine("Quantos gols o jogador fez?");
        jogador.Gols = double.Parse(Console.ReadLine());

        Console.WriteLine("Qual é o número da camisa do jogador?");
        jogador.NumCamisa = ushort.Parse(Console.ReadLine());

        jogador.Id = Guid.NewGuid();
        listaJogadores.Add(jogador);
    }

        public List<Jogador> ProcurarJogadorPorNome(string nome)
    {
        return listaJogadores
            .Where(d => d.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Jogador> ObterUltimosCinco()
    {
        if (listaJogadores.Count <= 0)
        {
            Console.WriteLine("Nenhum jogador cadastrado.");
        }
        else if (listaJogadores.Count <= 5)
        {
            Console.WriteLine("Últimos cinco cadastramentos:");
            return new List<Jogador>(listaJogadores);
        }

        return listaJogadores.Skip(Math.Max(0, listaJogadores.Count - 5)).ToList();
    }

    public void AlterarJogador()
    {
        Console.Write("Digite o nome do jogador que deseja alterar: ");
        string nomeParaAlterar = Console.ReadLine();

        var jogadoresEncontrados = ProcurarJogadorPorNome(nomeParaAlterar);

        if (jogadoresEncontrados.Count == 0)
        {
            Console.WriteLine("Nenhum jogador encontrado com esse nome.");
            return;
        }

        Console.WriteLine("Jogadores encontrados:");
        for (int i = 0; i < jogadoresEncontrados.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Nome: {jogadoresEncontrados[i].Nome}, Time: {jogadoresEncontrados[i].Time}");
        }

        Console.Write("Escolha o número do jogador que deseja alterar: ");
        if (int.TryParse(Console.ReadLine(), out int escolha) && escolha >= 1 && escolha <= jogadoresEncontrados.Count)
        {
            var jogadorSelecionado = jogadoresEncontrados[escolha - 1];

            Console.WriteLine("Digite os novos dados do jogador:");

            Console.Write("Qual o novo nome do jogador? ");
            string novoNome = Console.ReadLine();

            Console.Write("Qual o novo último sobrenome do jogador? ");
            string novoUltSobrenome = Console.ReadLine();

            Console.Write("Qual o novo time do jogador? ");
            string novoTime = Console.ReadLine();

            Console.WriteLine("Qual a data de nascimento do jogador? (yyyy-MM-dd):");
            DateTime novoDataDeNasc = DateTime.Parse(Console.ReadLine());

            Console.Write("O jogador foi convocado? S/N: ");
            bool novoConvocado = Console.ReadLine().Equals("S", StringComparison.OrdinalIgnoreCase);

            double novoGols;
            while (true)
            {
                Console.WriteLine("Quantos gol(s) o jogador fez?");
                if (double.TryParse(Console.ReadLine(), out novoGols))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Valor inválido. Digite um número válido.");
                }
            }

            ushort novoNumCamisa;
            while (true)
            {
                Console.WriteLine("Qual o número da camisa do jogador?");
                if (ushort.TryParse(Console.ReadLine(), out novoNumCamisa))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Número inválido. Digite um número válido.");
                }
            }

            jogadorSelecionado.Nome = novoNome;
            jogadorSelecionado.UltSobrenome = novoUltSobrenome;
            jogadorSelecionado.Time = novoTime;
            jogadorSelecionado.DataDeNasc = novoDataDeNasc;
            jogadorSelecionado.Convocado = novoConvocado;
            jogadorSelecionado.Gols = novoGols;
            jogadorSelecionado.NumCamisa = novoNumCamisa;
            jogadorSelecionado.DataDeCadastro = DateTime.Now;

            Console.WriteLine("Jogador alterado com sucesso.");
        }
        else
        {
            Console.WriteLine("Opção inválida.");
        }
    }

    public void ExcluirJogador()
    {
        Console.Write("Digite o nome do jogador que deseja excluir: ");
        string nomeParaExcluir = Console.ReadLine();

        var jogadoresEncontrados = ProcurarJogadorPorNome(nomeParaExcluir);

        if (jogadoresEncontrados.Count == 0)
        {
            Console.WriteLine("Nenhum jogador encontrado com esse nome.");
            return;
        }

        Console.WriteLine("Jogadores encontrados:");
        for (int i = 0; i < jogadoresEncontrados.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Nome: {jogadoresEncontrados[i].Nome}, Time: {jogadoresEncontrados[i].Time}");
        }

        Console.Write("Escolha o número do jogador que deseja excluir: ");
        if (int.TryParse(Console.ReadLine(), out int escolha) && escolha >= 1 && escolha <= jogadoresEncontrados.Count)
        {
            var jogadorSelecionado = jogadoresEncontrados[escolha - 1];

            // Exiba os detalhes do jogador selecionado antes de confirmar a exclusão
            Console.WriteLine($"Você está prestes a excluir o jogador: Nome: {jogadorSelecionado.Nome}, Time: {jogadorSelecionado.Time}");

            Console.Write("Tem certeza que deseja excluir este jogador? (S/N): ");
            string confirmacao = Console.ReadLine();

            if (confirmacao.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                listaJogadores.Remove(jogadorSelecionado);
                Console.WriteLine("Jogador excluído com sucesso.");
            }
            else
            {
                Console.WriteLine("Exclusão cancelada.");
            }
        }
        else
        {
            Console.WriteLine("Opção inválida.");
        }
    }
}
public class RepoJogadorJson : IRepoJogador
{
    private const string arquivoJson = "jogadores.json";
    private readonly string caminhoArquivo = "jogadores.json";
    private List<Jogador> listaJogadores = new List<Jogador>();

    public RepoJogadorJson()
    {
        CarregarJogadores();
    }
    private void SalvarJogadores()
    {
        string json = JsonConvert.SerializeObject(listaJogadores);
        File.WriteAllText(caminhoArquivo, json);
    }

    private void CarregarJogadores()
    {
        if (File.Exists(caminhoArquivo))
        {
            string json = File.ReadAllText(caminhoArquivo);
            listaJogadores = JsonConvert.DeserializeObject<List<Jogador>>(json);
        }
        else
        {
            listaJogadores = new List<Jogador>();
        }
    }
    public void AdicionarJogador()
    {
        var jogador = new Jogador();

        Console.WriteLine("Digite o nome do jogador:");
        jogador.Nome = Console.ReadLine();

        Console.WriteLine("Digite o último sobrenome do jogador:");
        jogador.UltSobrenome = Console.ReadLine();

        Console.WriteLine("Digite o time do jogador:");
        jogador.Time = Console.ReadLine();

        Console.WriteLine("Digite a data de nascimento do jogador (yyyy-MM-dd):");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime dataDeNasc))
        {
            jogador.DataDeNasc = dataDeNasc;
        }
        else
        {
            Console.WriteLine("Data de nascimento inválida. O jogador será cadastrado sem essa informação.");
        }

        Console.WriteLine("O jogador foi convocado? (S/N):");
        jogador.Convocado = Console.ReadLine().Equals("S", StringComparison.OrdinalIgnoreCase);

        Console.WriteLine("Quantos gols o jogador fez?");
        if (double.TryParse(Console.ReadLine(), out double gols))
        {
            jogador.Gols = gols;
        }
        else
        {
            Console.WriteLine("Número de gols inválido. O jogador será cadastrado com 0 gols.");
        }

        Console.WriteLine("Qual é o número da camisa do jogador?");
        if (ushort.TryParse(Console.ReadLine(), out ushort numCamisa))
        {
            jogador.NumCamisa = numCamisa;
        }
        else
        {
            Console.WriteLine("Número da camisa inválido. O jogador será cadastrado sem essa informação.");
        }

        jogador.Id = Guid.NewGuid();
        listaJogadores.Add(jogador);
        SalvarJogadores();

        Console.WriteLine("Jogador cadastrado com sucesso.");
    }

    public List<Jogador> ProcurarJogadorPorNome(string nome)
    {
        return listaJogadores
            .Where(d => d.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Jogador> ObterUltimosCinco()
    {
        if (listaJogadores.Count <= 0)
        {
            Console.WriteLine("Nenhum jogador cadastrado.");
            return new List<Jogador>();
        }

        var ultimosCinco = listaJogadores
            .OrderByDescending(j => j.DataDeCadastro)
            .Take(5)
            .ToList();

        return ultimosCinco;
    }

    public void AlterarJogador()
    {
        Console.Write("Digite o nome do jogador que deseja alterar: ");
        string nomeParaAlterar = Console.ReadLine();

        var jogadoresEncontrados = ProcurarJogadorPorNome(nomeParaAlterar);

        if (jogadoresEncontrados.Count == 0)
        {
            Console.WriteLine("Nenhum jogador encontrado com esse nome.");
            return;
        }

        Console.WriteLine("Jogadores encontrados:");
        for (int i = 0; i < jogadoresEncontrados.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Nome: {jogadoresEncontrados[i].Nome}, Time: {jogadoresEncontrados[i].Time}");
        }

        Console.Write("Escolha o número do jogador que deseja alterar: ");
        if (int.TryParse(Console.ReadLine(), out int escolha) && escolha >= 1 && escolha <= jogadoresEncontrados.Count)
        {
            var jogadorSelecionado = jogadoresEncontrados[escolha - 1];

            Console.WriteLine("Digite os novos dados do jogador:");

            Console.Write("Qual o novo nome do jogador? ");
            string novoNome = Console.ReadLine();

            Console.Write("Qual o novo último sobrenome do jogador? ");
            string novoUltSobrenome = Console.ReadLine();

            Console.Write("Qual o novo time do jogador? ");
            string novoTime = Console.ReadLine();

            Console.WriteLine("Qual a data de nascimento do jogador? (yyyy-MM-dd):");
            DateTime novoDataDeNasc = DateTime.Parse(Console.ReadLine());

            Console.Write("O jogador foi convocado? S/N: ");
            bool novoConvocado = Console.ReadLine().Equals("S", StringComparison.OrdinalIgnoreCase);

            double novoGols;
            while (true)
            {
                Console.WriteLine("Quantos gol(s) o jogador fez?");
                if (double.TryParse(Console.ReadLine(), out novoGols))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Valor inválido. Digite um número válido.");
                }
            }

            ushort novoNumCamisa;
            while (true)
            {
                Console.WriteLine("Qual o número da camisa do jogador?");
                if (ushort.TryParse(Console.ReadLine(), out novoNumCamisa))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Número inválido. Digite um número válido.");
                }
            }

            jogadorSelecionado.Nome = novoNome;
            jogadorSelecionado.UltSobrenome = novoUltSobrenome;
            jogadorSelecionado.Time = novoTime;
            jogadorSelecionado.DataDeNasc = novoDataDeNasc;
            jogadorSelecionado.Convocado = novoConvocado;
            jogadorSelecionado.Gols = novoGols;
            jogadorSelecionado.NumCamisa = novoNumCamisa;
            jogadorSelecionado.DataDeCadastro = DateTime.Now;

            Console.WriteLine("Jogador alterado com sucesso.");
        }
        else
        {
            Console.WriteLine("Opção inválida.");
        }
    }

    public void ExcluirJogador()
    {
        Console.Write("Digite o nome do jogador que deseja excluir: ");
        string nomeParaExcluir = Console.ReadLine();

        var jogadoresEncontrados = ProcurarJogadorPorNome(nomeParaExcluir);

        if (jogadoresEncontrados.Count == 0)
        {
            Console.WriteLine("Nenhum jogador encontrado com esse nome.");
            return;
        }

        Console.WriteLine("Jogadores encontrados:");
        for (int i = 0; i < jogadoresEncontrados.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Nome: {jogadoresEncontrados[i].Nome}, Time: {jogadoresEncontrados[i].Time}");
        }

        Console.Write("Escolha o número do jogador que deseja excluir: ");
        if (int.TryParse(Console.ReadLine(), out int escolha) && escolha >= 1 && escolha <= jogadoresEncontrados.Count)
        {
            var jogadorSelecionado = jogadoresEncontrados[escolha - 1];

            Console.WriteLine($"Você está prestes a excluir o jogador: Nome: {jogadorSelecionado.Nome}, Sobrenome: {jogadorSelecionado.UltSobrenome}, Time: {jogadorSelecionado.Time}, Idade: {jogadorSelecionado.CalcularIdade()}, Convocado: {jogadorSelecionado.Convocado}, Gols na Temporada: {jogadorSelecionado.Gols}, Número da Camisa: {jogadorSelecionado.NumCamisa}");

            Console.Write("Tem certeza que deseja excluir este jogador? (S/N): ");
            string confirmacao = Console.ReadLine();

            if (confirmacao.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                listaJogadores.Remove(jogadorSelecionado);
                Console.WriteLine("Jogador excluído com sucesso.");
            }
            else
            {
                Console.WriteLine("Exclusão cancelada.");
            }
        }
        else
        {
            Console.WriteLine("Opção inválida.");
        }
    }
}
