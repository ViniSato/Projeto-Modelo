namespace ProjetoModelo.Domain.Models.ViewModels
{
    public class ClienteViewModel
    {
        public int? Id { get; set; }
        public string? CPF { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Endereco { get; set; }
        public string? Telefone { get; set; }
        public string? Profissao { get; set; }
        public string? Comentarios { get; set; }
        public DateTime? DataCadastro { get; set; }

        public ClienteViewModel(Cliente cliente)
        {
            Id = cliente.Id;
            CPF = cliente.CPF;
            Nome = cliente.Nome;
            Email = cliente.Email;
            DataNascimento = cliente.DataNascimento;
            Endereco = cliente.Endereco;
            Telefone = cliente.Telefone;
            Profissao = cliente.Profissao;
            Comentarios = cliente.Comentarios;
            DataCadastro = cliente.DataCadastro;
        }
    }   
}
