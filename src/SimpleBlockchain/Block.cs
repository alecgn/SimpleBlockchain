using CryptographyHelpers.Hash;
using Newtonsoft.Json;
using System;
using System.Text;

namespace SimpleBlockchain
{
    public class Block
    {
        public Block(object data, string previousHash)
        {
            Timestamp = DateTime.Now;
            Data = data;
            PreviousHash = previousHash;
            Hash = ComputeHash();
        }

        public DateTime Timestamp { get; private set; }
        public object Data { get; set; } // "set" just allowed here to demonstrate tampering data with
        public string PreviousHash { get; private set; }
        public string Hash { get; private set; }

        public string ComputeHash()
        {
            var blockString = $"{PreviousHash}{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{JsonConvert.SerializeObject(Data)}";
            using var sha256 = new SHA256();
            
            return sha256.ComputeTextHash(blockString).HashString;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{nameof(Timestamp)}: {Timestamp: yyyy-MM-dd HH:mm:ss.fff}");
            sb.AppendLine($"{nameof(Data)}: {JsonConvert.SerializeObject(Data)}");
            sb.AppendLine($"{nameof(Hash)}: {Hash}");
            sb.AppendLine($"{nameof(PreviousHash)}: {PreviousHash}");

            return sb.ToString();
        }
    }
}
