﻿namespace LojaApi.Models
{
    public class Compras
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
