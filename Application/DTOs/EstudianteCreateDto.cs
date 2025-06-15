namespace Application.DTOs
{
    public class EstudianteCreateDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public List<int> MateriaIds { get; set; } = new();
    }
}
