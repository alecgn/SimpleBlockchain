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

        public void AddData(object data)
        {
            var newBlock = new Block(data, GetLatestBlockHash());
            Blockchain.Add(newBlock);
        }

        public void AddDataRange(params object[] dataRange)
        {
            foreach (var data in dataRange)
            {
                var newBlock = new Block(data, GetLatestBlockHash());
                Blockchain.Add(newBlock);
            }
        }

        public bool CheckChainValidity()
        {
            for (var i = 1; i < Blockchain.Count; i++) // skip genesis block in index 0
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
            return new Block(new { }, string.Empty);
        }

        private string GetLatestBlockHash()
        {
            return Blockchain[^1].Hash;
        }
    }
}
