namespace LojaApi.Models
{
    public class Pedidos
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.Now;
        public string StatusPedido { get; set; } = "Em Andamemto";
        public float ValorTotal { get; set; }
    }
}
