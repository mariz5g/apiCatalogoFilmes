namespace ApiCatalogoFilmes.Exceptions
{
    public class FilmeNaoCadastradoException : Exception
    {
        public FilmeNaoCadastradoException()
            : base("Este filme não está cadastrado")
        { }
    }
}
