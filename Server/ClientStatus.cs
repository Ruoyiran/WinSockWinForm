namespace Server
{
    public class ClientStatus
    {
        public bool BulbState
        {
            get
            {
                return bulbState;
            }
            set
            {
                bulbState = value;
            }
        }

        public float Temperature
        {
            get
            {
                return temperature;
            }
            set
            {
                temperature = value;
            }
        }

        public float Humidity
        {
            get
            {
                return humidity;
            }
            set
            {
                humidity = value;
            }
        }

        private bool bulbState;
        private float temperature;
        private float humidity;
    }
}
