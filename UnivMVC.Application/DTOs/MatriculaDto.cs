namespace UnivMVC.Application.DTOs
{
    public class MatriculaDto
    {
        public int estudiante_id { get; set; }
        public int semestre_id { get; set; }
        public int categoria_id { get; set; }        

        public List<Horario> horarios { get; set; } = new List<Horario>();
    }    
}
