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
    }
}

