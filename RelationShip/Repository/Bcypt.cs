namespace RelationShip.Repository
{
    public class Bcypt
    {
        public string HashPassword(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var passwordHash =BCrypt.Net.BCrypt.HashPassword(password, salt);
            return passwordHash;
        }
    }
}
