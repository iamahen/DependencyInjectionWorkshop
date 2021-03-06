class FailedCounters
{
    public FailedCounters()
    {
        
    }
    public void ResetFailedCount(string accountId, HttpClient httpClient)
    {
        var resetResponse = httpClient.PostAsJsonAsync("api/failedCounter/Reset", accountId).Result;
        resetResponse.EnsureSuccessStatusCode();
    }

    public void AddFailedCount(string accountId, HttpClient httpClient)
    {
        var addFailedCountResponse = httpClient.PostAsJsonAsync("api/failedCounter/Add", accountId).Result;
        addFailedCountResponse.EnsureSuccessStatusCode();
    }

    public bool GetAccountIsLocked(string accountId, HttpClient httpClient)
    {
        var isLockedResponse = httpClient.PostAsJsonAsync("api/failedCounter/IsLocked", accountId).Result;

        isLockedResponse.EnsureSuccessStatusCode();
        var isLocked = isLockedResponse.Content.ReadAsAsync<bool>().Result;
        return isLocked;
    }
}