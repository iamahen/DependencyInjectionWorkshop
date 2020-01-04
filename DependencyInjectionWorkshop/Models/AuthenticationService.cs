
public class AuthenticationService
    {
        private ProfileDao _profileDao;
        private Sha256Adapter _sha256Adapter;
        private OptService _optService;
        private FailedCounters _failedCounters;
        private NLogAdapter _nLogAdapter;
        private SlackAdapter _slackAdapter;

        public AuthenticationService()
        {
            _profileDao = new ProfileDao();
            _sha256Adapter = new Sha256Adapter();
            _optService = new OptService();
            _failedCounters = new FailedCounters();
            _nLogAdapter = new NLogAdapter();
            _slackAdapter = new SlackAdapter();
        }

        public bool Verify(string accountId, string password, string otp)
        {
            var httpClient = new HttpClient() {BaseAddress = new Uri("http://joey.com/")};

            //check account locked
            var isLocked = _failedCounters.GetAccountIsLocked(accountId, httpClient);
            if (isLocked)
            {
                throw new FailedTooManyTimesException() {AccountId = accountId};
            }

            var passwordFromDb = _profileDao.GetPasswordFromDb(accountId);

            var hashedPassword = _sha256Adapter.GetHashedPassword(password);

            var currentOtp = _optService.GetCurrentOtp(accountId, httpClient);

            //compare
            if (passwordFromDb == hashedPassword && currentOtp == otp)
            {
                _failedCounters.ResetFailedCount(accountId, httpClient);

                return true;
            }
            else
            {
                //失敗
                _failedCounters.AddFailedCount(accountId, httpClient);

                _nLogAdapter.LogFailedCount(accountId, httpClient);

                _slackAdapter.Notify(accountId);

                return false;
            }
        }
    }