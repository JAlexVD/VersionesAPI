namespace APIVersionControl.DTO
{
     public class User
     {
        public string? id { get; set; }
        public string? title { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? picture { get; set; }

    }

    //Clase para gestionar las respuestas
    //Cuando haga peticiones esta me devuekve cierto contenido
    public class UsersResponseData
    {
        public User[]? data {get; set;} 
        public int total { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }
}
