using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;


namespace game
{
    class Program
    {
        static void Main(string[] args)
        {
            if(ChecksCorrectData(args))
                StartGame(args, KeyGenerator.CreatesKey128bit(), MakesRandomMove(args));
        }

        static void StartGame(string[] args, byte[] key, int compMove)
        {
            Console.WriteLine("HMAC:"+ConvertsFromByteToHex(HMACGenerator.CreatesHMAC(key, args[compMove])));
            var playersMove = GetsCorrectMove(CreatesMenu(args));
            if (playersMove != "?" && playersMove != "0")
                PrintsResults(args, compMove, key, int.Parse(playersMove) - 1);
            else if (playersMove == "?") PrintsHelpTable(args);
        }

        static void PrintsHelpTable(string [] moves)
        {
            TableGenerator.PrintsTable(moves);
        }

        static void PrintsResults(string[] moves, int compMove, byte[] key, int playersMove)
        {
            Console.WriteLine($"Your move:{moves[playersMove]}\nComputer move:{moves[compMove]}");
            var result = WinnersDaterminant.DaterminesWinner(moves, playersMove, compMove);
            Console.WriteLine(result=="draw"? result : $"You {result}");
            Console.WriteLine("HMAC key:"+ConvertsFromByteToHex(key));
        }

        static string GetsCorrectMove(Dictionary<string, string> menu )
        {
            string intputStr="";
            while (!menu.ContainsKey(intputStr))
                intputStr =PrintMenuAndGetsMove(menu);
            return intputStr; 
        }

        static string PrintMenuAndGetsMove(Dictionary<string,string> menu)
        {
            PrintsMenu(menu);
            return Console.ReadLine();
        }

        static int MakesRandomMove(string [] moves) 
        {
            var uintBuffer = new byte [4];
            using(var rngCsp = new RNGCryptoServiceProvider())
            rngCsp.GetBytes(uintBuffer);
            return (int)(BitConverter.ToUInt32(uintBuffer, 0)%moves.Length);    
        } 

        static Dictionary<string,string> CreatesMenu(string [] moves)
        {
            var menu=moves.Select((m, i) => (num: (i + 1).ToString(), m)).ToDictionary(v => v.num, v => v.m);
            menu.Add("0", "exit");
            menu.Add("?", "help");
            return menu;
        }

        static string ConvertsFromByteToHex (byte [] bytes)
        {
            return string.Concat(bytes.Select(b => b.ToString("X2")));
        }

        static void PrintsMenu(Dictionary<string,string> menu)
        {
            var str = menu.Aggregate("", (total, next) => $"{total}\n{next.Key}-{next.Value}");
            Console.Write($"Available moves:{str}\nEnter your move:");
        }

        static bool ChecksCorrectData(string [] moves) 
        {
            if (moves.Length%2==0 || moves.Length<3)
                Console.WriteLine("Error:Incorrect data, the number of moves must be odd and >=3.");
            var hasRepetition = moves.GroupBy(x=>x).Where(x=>x.Count()>1).Any();
            if(hasRepetition)
                Console.WriteLine("Error:Incorrect data, moves should not be repeated.");
            if(hasRepetition || moves.Length%2==0 || moves.Length<3)
                Console.WriteLine("Example:dotnet run rock paper scissors\nExample:dotnet run 1 2 3 4 5");
            return !(hasRepetition || moves.Length%2==0 || moves.Length<3);
        }
    }
}
