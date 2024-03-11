namespace master_api_dotnet_6.IRepository.Hangfire
{
    public interface IHangFireRepo
    {
        public object CreateBackgroundJob();
        public object CreateScheduledJob();
        public object CreateContinuationJob();
        public object CreateRecurring();
    }
}
