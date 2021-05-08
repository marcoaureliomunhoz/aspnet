using webapiv1.Interfaces;

namespace webapiv1.Jobs
{
    public class MyDateJob : IJob
    {
        private readonly IMyDateHelper _myDateHelper;

        public MyDateJob(IMyDateHelper myDateHelper)
        {
            _myDateHelper = myDateHelper;
        }

        public void Execute()
        {
            _myDateHelper.GetCurrentDate();
        }
    }
}