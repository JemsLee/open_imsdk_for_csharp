using System;
using open_imsdk_for_cs_ns;

namespace open_imsdk_for_cs.observer
{
    public class OBPublishSub
    {
        public event EventHandler<OBEventArgs> OnChange = delegate { };

        public void Raise(String message)
        {
            OBEventArgs eventArgs = new OBEventArgs(message);

            List<Exception> exceptions = new List<Exception>();

            foreach (Delegate handler in OnChange.GetInvocationList())
            {
                try
                {
                    handler.DynamicInvoke(this, eventArgs);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

    }
}

