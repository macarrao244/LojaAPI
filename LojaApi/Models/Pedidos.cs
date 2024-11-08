namespace LojaApi.Models
{
    public class Pedidos
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataPedido { get; set; }
        public string StatusPedido { get; set; }
        public float ValorTotal { get; set; }
    }
}
