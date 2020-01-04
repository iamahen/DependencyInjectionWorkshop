class NLogAdapter
{
    public NLogAdapter()
    {
        
    }
    public void LogFailedCount(string accountId, HttpClient httpClient)
    {
        //紀錄失敗次數 
        var failedCountResponse =
            httpClient.PostAsJsonAsync("api/failedCounter/GetFailedCount", accountId).Result;

        failedCountResponse.EnsureSuccessStatusCode();

        var failedCount = failedCountResponse.Content.ReadAsAsync<int>().Result;
        var logger = NLog.LogManager.GetCurrentClassLogger();
        logger.Info($"accountId:{accountId} failed times:{failedCount}");
    }
}