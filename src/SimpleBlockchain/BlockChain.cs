using System.Collections.Generic;
using System.Text;

namespace SimpleBlockchain
{
    public class BlockChain
    {
        public List<Block> Blockchain { get; private set; }

        public BlockChain()
        {
            Blockchain = new List<Block>
            {
                StartGenesisBlock()
            };
        }

        public void AddData(Transaction transaction)
        {
            var newBlock = new Block(transaction, GetLatestBlockHash());
            Blockchain.Add(newBlock);
        }

        public void AddDataRange(params Transaction[] transactions)
        {
            foreach (var transaction in transactions)
            {
                var newBlock = new Block(transaction, GetLatestBlockHash());
                Blockchain.Add(newBlock);
            }
        }

        public bool CheckChainValidity()
        {
            for (var i = 1; i < Blockchain.Count; i++) // skip genesis block at index 0
            {
                var currentBlock = Blockchain[i];
                var previousBlock = Blockchain[i - 1];

                if (currentBlock.Hash != currentBlock.ComputeHash())
                {
                    return false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }

            }

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var i = 1; i < Blockchain.Count; i++)
            {
                sb.AppendLine();
                sb.AppendLine(Blockchain[i].ToString());
            }

            return sb.ToString();
        }

        private static Block StartGenesisBlock()
        {
            return new Block(new Transaction(string.Empty, string.Empty, decimal.Zero), string.Empty);
        }

        private string GetLatestBlockHash()
        {
            return Blockchain[^1].Hash;
        }
    }
}
