namespace UnivMVC.Domain.Auth
{
    public class TfaRequest
    {
        public string Usuario { get; set; } = "";
        public string Codigo { get; set; } = "";
        public string Secreto { get; set; } = "";
    }
}
