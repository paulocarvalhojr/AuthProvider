namespace PC.Core.Auth.WebApi.Contract
{
    /// <summary>
    /// Representa o usuário narequisição de login.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Chave de acesso do usuário.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Senha do usuário.
        /// </summary>
        public string Password { get; set; }
    }
}