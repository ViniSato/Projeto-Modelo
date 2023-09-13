namespace ProjetoModelo.Domain.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) 
        {
            
        }

        public IEnumerable<string> Erros { get; set; }
    }
}
