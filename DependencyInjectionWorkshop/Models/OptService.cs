class OptService
{
    public OptService()
    {
        
    }
    public string GetCurrentOtp(string accountId, HttpClient httpClient)
    {
        //get otp
        var response = httpClient.PostAsJsonAsync("api/otps", accountId).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"web api error, accountId:{accountId}");
        }

        var currentOtp = response.Content.ReadAsAsync<string>().Result;
        return currentOtp;
    }
}