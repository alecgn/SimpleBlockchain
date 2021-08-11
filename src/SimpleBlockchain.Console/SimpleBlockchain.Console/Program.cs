﻿using Newtonsoft.Json;
using System;

namespace SimpleBlockchain.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var chain = new BlockChain();

            var transaction1 = new Transaction(from: "John", to: "Paul", value: 543.21M);
            System.Console.WriteLine($"Adding {nameof(Transaction)} {JsonConvert.SerializeObject(transaction1)} to the blockchain...");
            chain.AddData(transaction1);

            var transaction2 = new Transaction(from: "Mary", to: "Joana", value: 123.45M);
            System.Console.WriteLine($"Adding {nameof(Transaction)} {JsonConvert.SerializeObject(transaction2)} to the blockchain...");
            chain.AddData(transaction2);

            var transaction3 = new Transaction(from: "Joseph", to: "Mark", value: 987.65M);
            System.Console.WriteLine($"Adding {nameof(Transaction)} {JsonConvert.SerializeObject(transaction3)} to the blockchain...");
            chain.AddData(transaction3);

            System.Console.WriteLine();
            System.Console.WriteLine("Transactions currently in the blockchain:");
            System.Console.WriteLine(chain.ToString());

            System.Console.WriteLine("Verifing integrity:");
            System.Console.WriteLine("Blockchain has a valid integrity: " + chain.CheckChainValidity());

            System.Console.WriteLine();
            var randomIndex = new Random().Next(1, chain.Blockchain.Count); // skip genesis block in index 0
            var randomTransaction = (Transaction)chain.Blockchain[randomIndex].Data;
            System.Console.WriteLine($"Picked a random transaction to tamper data with: {JsonConvert.SerializeObject(randomTransaction)}");
            
            var tamperedWithValue = 1_000_000M;
            randomTransaction.Value = tamperedWithValue;
            System.Console.WriteLine();
            System.Console.WriteLine($"Altered value of transaction to {tamperedWithValue:C}: {JsonConvert.SerializeObject(randomTransaction)}");

            System.Console.WriteLine();
            System.Console.WriteLine("Verifing integrity after the tamper with:");
            System.Console.WriteLine("Blockchain has a valid integrity: " + chain.CheckChainValidity());
        }
    }
}