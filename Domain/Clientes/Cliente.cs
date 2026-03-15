namespace Pizzeria.Domain.Clientes
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public int Puntos { get; set; }

        public void AcumularPuntos(int puntos)
        {
            Puntos += puntos;
        }

        public bool EsCumpleanosHoy()
        {
            return FechaNacimiento.Day == DateTime.Now.Day &&
                   FechaNacimiento.Month == DateTime.Now.Month;
        }
    }
}