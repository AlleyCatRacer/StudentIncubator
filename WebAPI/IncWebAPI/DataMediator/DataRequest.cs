using System;

namespace WebAPI.DataMediator 
{
    public class DataRequest 
    {
        public string Object { get; }    
        public string Type { get; }
        public string Body { get; }

        public DataRequest(string obj, string type, string body)
        {
            Object = obj;
            Type = type;
            Body = body;
        }
    }
}