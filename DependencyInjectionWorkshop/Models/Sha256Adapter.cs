class Sha256Adapter
{
    public Sha256Adapter()
    {
        
    }
    public string GetHashedPassword(string password)
    {
        //hash
        var crypt = new SHA256Managed();
        var hash = new StringBuilder();
        var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password));
        foreach (var theByte in crypto)
        {
            hash.Append(theByte.ToString("x2"));
        }

        var hashedPassword = hash.ToString();
        return hashedPassword;
    }
}