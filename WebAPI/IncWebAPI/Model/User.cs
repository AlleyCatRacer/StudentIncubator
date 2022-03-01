namespace WebAPI.Model 
{
    public class User 
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Bio { get; set; }
        public int Highscore { get; set; }
        public bool Online { get; set; }
        public bool HasHug { get; set; }

        public override string ToString() 
        {
            return $"{Username} and {Password}";
        }
    }
}