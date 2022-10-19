using System;
namespace open_imsdk_for_cs.observer
{
    public class OBEventArgs
    {
        
        public String Value { get; set; }

        public OBEventArgs(String value)
        {
            Value = value;
        }

    }
}

