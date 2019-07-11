using System;

namespace XmasChecker
{
    public class XmasChecker
    {
        protected DateTime today = new DateTime();

        public bool IsTodayXmas()
        {
            return today.Month == 12 && today.Day == 25;
        }
    }
}