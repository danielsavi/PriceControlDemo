using System;

namespace CoreLibrary
{
    public class Subscriber
    {
        public enum TriggerType
        {
            Both = 0,
            Sell = 1,
            Buy = -1,
        }

        public string SubscriberName { get; private set; }
        public TriggerType Trigger { get; private set; }
        public double Threshold { get; private set; }


        public Subscriber(string subscriberName, TriggerType trigger, double threshold = 0)
        {
            if (string.IsNullOrEmpty(subscriberName)) throw new ArgumentException("Should not be empty or null", "subscriberName");
            if (threshold < 0) throw new ArgumentException("Should not be less than zero", "threshold");

            SubscriberName = subscriberName;
            Trigger = trigger;
            Threshold = threshold;
        }

        // This function subscribe to the event if it is raised by the Publisher
        public void Subscribe(Publisher p)
        {
            //https://www.tutorialsteacher.com/csharp/csharp-event
            // register OnNotificationReceived with publisher event
            //
            switch (Trigger)
            {
                case TriggerType.Both:
                    p.OnBuyPublish += OnNotificationReceived;  // multicast delegate
                    p.OnSellPublish += OnNotificationReceived;  // multicast delegate
                    break;
                case TriggerType.Sell:
                    p.OnSellPublish += OnNotificationReceived;  // multicast delegate
                    break;
                case TriggerType.Buy:
                    p.OnBuyPublish += OnNotificationReceived;  // multicast delegate
                    break;
                default:
                    break;
            }
        }

        // This function unsubscribe from the event if it is raised by the Publisher
        public void Unsubscribe(Publisher p)
        {
            // unregister OnNotificationReceived from publisher
            switch (Trigger)
            {
                case TriggerType.Both:
                    p.OnBuyPublish -= OnNotificationReceived;  // multicast delegate
                    p.OnSellPublish -= OnNotificationReceived;  // multicast delegate
                    break;
                case TriggerType.Sell:
                    p.OnSellPublish -= OnNotificationReceived;  // multicast delegate
                    break;
                case TriggerType.Buy:
                    p.OnBuyPublish -= OnNotificationReceived;  // multicast delegate
                    break;
                default:
                    break;
            }
        }

        // It get executed when the event published by the Publisher
        protected virtual void OnNotificationReceived(Publisher p, NotificationEvent e)
        {
            /* To keep it simple, I've decided to branch the event callback consumption on the subscriber side
             * 
             * Not the most friendly / elegant solution, but keep it in mind that this is a demo project, so K.I.S.S. =)
             * I'm pretty sure that Azure Service Bus + Topics offers more advanced options, such as:
             *      FIFO
             *      Batching / Sessions
             *      Transactions
             *      Dead lettering
             *      Temporal control
             *      Routing and filtering
             *      Duplicate detection
             *      At least once delivery
             *      https://www.serverless360.com/blog/azure-service-bus-topics-vs-event-grid
            */
            if (Threshold == 0) // Threshold == 0 equals to undefined value, i.e. ignore the Threshold and process
            {
                Console.WriteLine($"Hey { SubscriberName} , { e.NotificationMessage} - { p.AssetName} at { e.NotificationDate} Threshold: {e.ThresholdABSValue}");
            }
            else if (e.ThresholdABSValue.CompareTo(Threshold) > 0) //take Threshold into consideration before processing...
            {
                Console.WriteLine($"*Threshold* Hey { SubscriberName} , { e.NotificationMessage} - { p.AssetName} at { e.NotificationDate} Threshold: {e.ThresholdABSValue}");
            }
        }

        // We can also implement different callbacks if we need to...uncomment to fit your needs
        //protected virtual void OnBuyNotificationReceived(Publisher p, NotificationEvent e)
        //{
        //    Console.WriteLine("Hey " + SubscriberName + ", " + e.NotificationMessage + " - " + p.AssetName + " at " + e.NotificationDate);
        //}

        //protected virtual void OnSellNotificationReceived(Publisher p, NotificationEvent e)
        //{
        //    Console.WriteLine("Hey " + SubscriberName + ", " + e.NotificationMessage + " - " + p.AssetName + " at " + e.NotificationDate);
        //}
    }
}
