using System;
using System.Collections.Generic;
using AT;

public interface IRepoJogador
{
    void AdicionarJogador();
    List<Jogador> ProcurarJogadorPorNome(string nome);
    List<Jogador> ObterUltimosCinco();
    void AlterarJogador();
    void ExcluirJogador();
}