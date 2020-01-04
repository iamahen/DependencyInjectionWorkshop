class ProfileDao
{
    public ProfileDao()
    {
        
    }
    public string GetPasswordFromDb(string accountId)
    {
        //get password
        string passwordFromDb;
        using (var connection = new SqlConnection("my connection string"))
        {
            passwordFromDb = connection.Query<string>("spGetUserPassword", new {Id = accountId},
                commandType: CommandType.StoredProcedure).SingleOrDefault();
        }

        return passwordFromDb;
    }
}