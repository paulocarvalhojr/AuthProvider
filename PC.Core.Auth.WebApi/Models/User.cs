namespace PC.Core.Auth.WebApi.Models
{
    /// <summary>
    /// Representa o usuário no login
    /// </summary>
    public class User
    {
        /// <summary>
        /// Chave de acesso do usuário.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Senha do usuário.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Name { get; set; }
    }
}