using Newtonsoft.Json;

namespace SimpleBlockchain
{
    public class Transaction
    {
        public Transaction(string from, string to, decimal value)
        {
            From = from;
            To = to;
            Value = value;
        }

        public string From { get; private set; }
        public string To { get; private set; }
        public decimal Value { get; set; } // "set" just allowed here to demonstrate tampering with data

        public string ToJson() =>
            JsonConvert.SerializeObject(this);
    }
}
