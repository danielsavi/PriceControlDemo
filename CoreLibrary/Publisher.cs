using System;

namespace CoreLibrary
{
    public enum DirectionType
    {
        Steady = 0,
        Up = 1,
        Down = -1,
    }

    public class Publisher
    {
        public string AssetName { get; private set; }
        public double Value { get; private set; }
        private double ThresholdValue { get; set; }
        private double ThresholdABSValue { get; set; }

        private DirectionType Direction { get; set; }

        //** Declare an Event...
        // declare a delegate with any name
        public delegate void Notify(Publisher p, NotificationEvent e);
        
        // declare a variable of the delegate with event keyword
        public event Notify OnSellPublish;
        public event Notify OnBuyPublish;


        public Publisher(string assetName, double value)
        {
            if (string.IsNullOrEmpty(assetName)) throw new ArgumentException("Should not be empty or null", "assetName");
            if (value <= 0) throw new ArgumentException("Should not be equal or less than zero", "value");

            AssetName = assetName;
            Value = value;
            ThresholdValue = 0;
            Direction = DirectionType.Steady;
        }

        public void UpdateValue(double newValue)
        {
            if (newValue <= 0) throw new ArgumentException("Should not be equal or less than zero", "value");

            //TODO: We can extract those lines to a specific Call and write some additional unit tests
            ThresholdValue = ((newValue - Value) / Math.Abs(Value)) * 100;
            ThresholdABSValue = Math.Abs(Math.Round(ThresholdValue, 2, MidpointRounding.AwayFromZero));
            Value = newValue;

            SetDirectionAndPublish();
        }

        private void SetDirectionAndPublish()
        {
            if (ThresholdValue >= 0)
            {
                Direction = DirectionType.Up;
                PublishSellEvent();
            }
            else
            {
                Direction = DirectionType.Down;
                PublishBuyEvent();
            }
        }

        //publish function publishes a Notification Event
        private void PublishBuyEvent()
        {
            if (OnBuyPublish != null)
            {
                NotificationEvent notificationObj = new NotificationEvent(DateTime.Now, ThresholdABSValue, "New Buy Trigger from");
                OnBuyPublish(this, notificationObj);
            }
        }

        private void PublishSellEvent()
        {
            if (OnSellPublish != null)
            {
                NotificationEvent notificationObj = new NotificationEvent(DateTime.Now, ThresholdABSValue, "New Sell Trigger from");
                OnSellPublish(this, notificationObj);
            }
        }

    }
}
