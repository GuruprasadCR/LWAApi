namespace LWAApi.Error_handlings
{
    public class CustomExceptions:Exception
    {
       public CustomExceptions(): base()
        {
        }

        public CustomExceptions(string message) : base(message)
        {
        }

        public CustomExceptions(string message, Exception innerexception) : base(message,innerexception)
        {
        }
    }
}
