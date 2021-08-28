using CryptographyHelpers.Hash;
using System;
using System.Text;

namespace SimpleBlockchain
{
    public class Block
    {
        public Block(Transaction transaction, string previousHash)
        {
            Timestamp = DateTime.Now;
            Transaction = transaction;
            PreviousHash = previousHash;
            Hash = ComputeHash();
        }

        public DateTime Timestamp { get; private set; }
        public Transaction Transaction { get; set; } // "set" just allowed here to demonstrate tampering data with
        public string PreviousHash { get; private set; }
        public string Hash { get; private set; }

        public string ComputeHash()
        {
            var blockString = $"{PreviousHash}{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{Transaction.ToJson()}";
            using var sha256 = new SHA256();

            return sha256.ComputeTextHash(blockString).HashString;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{nameof(Timestamp)}: {Timestamp: yyyy-MM-dd HH:mm:ss.fff}");
            sb.AppendLine($"{nameof(Transaction)}: {Transaction.ToJson()}");
            sb.AppendLine($"{nameof(Hash)}: {Hash}");
            sb.AppendLine($"{nameof(PreviousHash)}: {PreviousHash}");

            return sb.ToString();
        }
    }
}
