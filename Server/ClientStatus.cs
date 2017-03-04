namespace Server
{
    public class ClientStatus
    {
        public bool LightBulbIsOn
        {
            get
            {
                return lightBulbIsOn;
            }
            set
            {
                lightBulbIsOn = value;
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

        private bool lightBulbIsOn;
        private float temperature;
        private float humidity;
    }
}
