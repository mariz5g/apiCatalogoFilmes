using ApiCatalogoFilmes.Entities;

namespace ApiCatalogoFilmes.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private static Dictionary<Guid, Filme> filmes = new Dictionary<Guid, Filme>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Filme{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "Fifa 21", Produtora = "EA", Preco = 200} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Filme{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome = "Fifa 20", Produtora = "EA", Preco = 190} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Filme{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome = "Fifa 19", Produtora = "EA", Preco = 180} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Filme{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Nome = "Fifa 18", Produtora = "EA", Preco = 170} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Filme{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Nome = "Street Fighter V", Produtora = "Capcom", Preco = 80} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Filme{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Nome = "Grand Theft Auto V", Produtora = "Rockstar", Preco = 190} }
        };

        public Task<List<Filme>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(filmes.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Filme> Obter(Guid id)
        {
            if (!filmes.ContainsKey(id))
                return Task.FromResult<Filme>(null);

            return Task.FromResult(filmes[id]);
        }

        public Task<List<Filme>> Obter(string nome, string produtora)
        {
            return Task.FromResult(filmes.Values.Where(filme => filme.Nome.Equals(nome) && filme.Produtora.Equals(produtora)).ToList());
        }

        public Task<List<Filme>> ObterSemLambda(string nome, string produtora)
        {
            var retorno = new List<Filme>();

            foreach (var filme in filmes.Values)
            {
                if (filme.Nome.Equals(nome) && filme.Produtora.Equals(produtora))
                    retorno.Add(filme);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Filme filme)
        {
            filmes.Add(filme.Id, filme);
            return Task.CompletedTask;
        }

        public Task Atualizar(Filme filme)
        {
            filmes[filme.Id] = filme;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            filmes.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        { }
    }
}
