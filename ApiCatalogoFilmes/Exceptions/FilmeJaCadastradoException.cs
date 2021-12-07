namespace ApiCatalogoFilmes.Exceptions
{
    public class FilmeJaCadastradoException : Exception
    {
        public FilmeJaCadastradoException()
            : base("Este filme já está cadastradp")
        { }
    }
}
