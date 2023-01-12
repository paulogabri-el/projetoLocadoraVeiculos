namespace ProjetoLocadoraDeVeiculos.Models.ViewModels
{
    public class TemporadaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal PercentualAcrescerDiaria { get; set; }
        public decimal PercentualAcrescerMultaFixa { get; set; }
        public decimal PercentualAcrescerMultaDiaria { get; set; }
    }
}
