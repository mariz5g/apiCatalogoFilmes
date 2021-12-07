using ApiCatalogoFilmes.Entities;

namespace ApiCatalogoFilmes.Repositories
{
    public interface IFilmeRepository : IDisposable
    {
        Task<List<Filme>> Obter(int pagina, int quantidade);

        Task<Filme> Obter(Guid id);

        Task<List<Filme>> Obter(string nome, string produtora);

        Task Inserir(Filme filme);

        Task Atualizar(Filme filme);

        Task Remover(Guid id);
    }
}
