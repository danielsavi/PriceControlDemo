using System;

namespace CoreLibrary
{
    public class NotificationEvent
    {
        public string NotificationMessage { get; set; }

        public DateTime NotificationDate { get; private set; }

        //my dirty-fix to execute some logic on the subscriber side, the NotificationEvent could handle a more complex obj, I've simplified a bit for this demo
        public double ThresholdABSValue { get; private set; } 

        public NotificationEvent(DateTime dateTime, double thresholdABSValue, string message = "")
        {
            NotificationDate = dateTime;
            ThresholdABSValue = thresholdABSValue;
            NotificationMessage = message;
        }
    }
}
