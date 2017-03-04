namespace Client
{
    public class CommandConstant
    {
        public static readonly char COMMAND_DELIMITER = ':';
        public static readonly string COMMAND_SERVER_COMMAND_PREFIX = "ServerCommand:";
        public static readonly string COMMAND_CLIENT_COMMAND_PREFIX = "ClientCommand:";
        public static readonly string COMMAND_LIGHT_STATE = "LightState";
        public static readonly string COMMAND_TEMPERATURE = "Temperature";
        public static readonly string COMMAND_HUMIDITY = "Humidity";
    }

    public abstract class CommandBase
    {
        public delegate void CommandHandler(params object[] args);
        public event CommandHandler OnCommandCallbackEvent;

        protected string commandName = "Unknown";

        public CommandBase(string commandName, CommandHandler commandHander)
        {
            this.commandName = commandName;
            OnCommandCallbackEvent += commandHander;
        }

        public virtual void ExecuteCommand(params object[] args)
        {
            if (OnCommandCallbackEvent != null)
                OnCommandCallbackEvent(args);
        }
    }

    public class LightStateCommand : CommandBase
    {
        public LightStateCommand(string commandName, CommandHandler commandHander)
            : base(commandName, commandHander)
        {
        }
    }

    public class TemperatureCommand : CommandBase
    {
        public TemperatureCommand(string commandName, CommandHandler commandHander)
            : base(commandName, commandHander)
        {
        }
    }

    public class HumidityCommand : CommandBase
    {
        public HumidityCommand(string commandName, CommandHandler commandHander)
            : base(commandName, commandHander)
        {
        }
    }
}
