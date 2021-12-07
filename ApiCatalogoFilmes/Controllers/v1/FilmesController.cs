using ApiCatalogoFilmes.Exceptions;
using ApiCatalogoFilmes.InputModel;
using ApiCatalogoFilmes.Services;
using ApiCatalogoFilmes.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiCatalogoFilmes.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly IFilmeService _filmeService;

        public FilmesController(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        /// <summary>
        /// Buscar todos os filmes de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os filmes sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de filmes</response>
        /// <response code="204">Caso não haja filmes</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmeViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var filmes = await _filmeService.Obter(pagina, quantidade);

            if (filmes.Count() == 0)
                return NoContent();

            return Ok(filmes);
        }

        /// <summary>
        /// Buscar um filme pelo seu Id
        /// </summary>
        /// <param name="idFilme">Id do filme buscado</param>
        /// <response code="200">Retorna o filme filtrado</response>
        /// <response code="204">Caso não haja filme com este id</response>   
        [HttpGet("{idFilme:guid}")]
        public async Task<ActionResult<FilmeViewModel>> Obter([FromRoute] Guid idFilme)
        {
            var filme = await _filmeService.Obter(idFilme);

            if (filme == null)
                return NoContent();

            return Ok(filme);
        }

        /// <summary>
        /// Inserir um filme no catálogo
        /// </summary>
        /// <param name=FilmeInputModel">Dados do filme a ser inserido</param>
        /// <response code="200">Caso o filme seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um filme com mesmo nome para a mesma produtora</response>   
        [HttpPost]
        public async Task<ActionResult<FilmeViewModel>> InserirFilme([FromBody] FilmeInputModel filmeInputModel)
        {
            try
            {
                var filme = await _filmeService.Inserir(filmeInputModel);

                return Ok(filme);
            }
            catch (FilmeJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um filme com este nome para esta produtora");
            }
        }

        /// <summary>
        /// Atualizar um filme no catálogo
        /// </summary>
        /// /// <param name="idFilme">Id do filme a ser atualizado</param>
        /// <param name="filmeInputModel">Novos dados para atualizar o filme indicado</param>
        /// <response code="200">Caso o filme seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um filme com este Id</response>   
        [HttpPut("{idFilme:guid}")]
        public async Task<ActionResult> AtualizarFilme([FromRoute] Guid idFilme, [FromBody] FilmeInputModel filmeInputModel)
        {
            try
            {
                await _filmeService.Atualizar(idFilme, filmeInputModel);

                return Ok();
            }
            catch (FilmeNaoCadastradoException ex)
            {
                return NotFound("Não existe este filme");
            }
        }

        /// <summary>
        /// Atualizar o preço de um filme
        /// </summary>
        /// /// <param name="idFilme">Id do filme a ser atualizado</param>
        /// <param name="preco">Novo preço do filme</param>
        /// <response code="200">Caso o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um filme com este Id</response>   
        [HttpPatch("{idFilme:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarFilme([FromRoute] Guid idFIlme, [FromRoute] double preco)
        {
            try
            {
                await _filmeService.Atualizar(idFIlme, preco);

                return Ok();
            }
            catch (FilmeNaoCadastradoException ex)
            {
                return NotFound("Não existe este filme");
            }
        }

        /// <summary>
        /// Excluir um filme
        /// </summary>
        /// /// <param name="idFilme">Id do filme a ser excluído</param>
        /// <response code="200">Caso o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um filme com este Id</response>   
        [HttpDelete("{idFilme:guid}")]
        public async Task<ActionResult> ApagarJFilme([FromRoute] Guid idFilme)
        {
            try
            {
                await _filmeService.Remover(idFilme);

                return Ok();
            }
            catch (FilmeNaoCadastradoException ex)
            {
                return NotFound("Não existe este filme");
            }
        }
    }
}
