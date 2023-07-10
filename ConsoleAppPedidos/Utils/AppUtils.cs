namespace ConsoleAppPedidos.Services
{
    /// <summary>
    /// Classe de métodos úteis
    /// </summary>
    public static class AppUtils
    {
        /// <summary>
        /// Carrega a categoria do produto com base no seu ID.
        /// </summary>
        /// <param name="categoriaId">ID da categoria do produto.</param>
        /// <returns>Nome da categoria do produto.</returns>
        public static string CarregarCategoriaProduto(int categoriaId)
        {
            if (categoriaId == 0)
                return "0 - Perecível";
            else
                return "1 - Não perecível";
        }

        /// <summary>
        /// Valida a resposta do usuário, verificando se é uma opção válida.
        /// </summary>
        /// <param name="respostaUsuario">A resposta do usuário a ser validada.</param>
        /// <returns>True se a resposta for "y" ou "n", caso contrário retorna False.</returns>
        public static bool ValidacaorespostaUsuario(string respostaUsuario)
        {
            return respostaUsuario.Equals("s") || respostaUsuario.Equals("n");
        }

        /// <summary>
        /// Exibe uma mensagem informando que a opção fornecida pelo usuário é inválida.
        /// </summary>
        public static void MensagemRespostaInvalidaUsuario()
        {
            Console.WriteLine("Opção inválida. Digite s para SIM ou n para NÃO. Tente novamente.");
        }
    }
}

