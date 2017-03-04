namespace Server
{
    public class ClientStatus
    {
        public bool LightIsOn
        {
            get
            {
                return lightIsOn;
            }
            set
            {
                lightIsOn = value;
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

        public float Temperature2
        {
            get
            {
                return temperature2;
            }
            set
            {
                temperature2 = value;
            }
        }

        public float Humidity2
        {
            get
            {
                return humidity2;
            }
            set
            {
                humidity2 = value;
            }
        }
        private bool lightIsOn;
        private float temperature;
        private float humidity;
        private float temperature2;
        private float humidity2;
    }
}
