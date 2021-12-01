using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaDemo01.Actors.Demo;
using AkkaDemo01.Actors.Game;
using AkkaDemo01.Actors.Game.Messages;
using AkkaDemo01.Actors.Game.Model;
using AkkaDemo01.Unit1;
using AkkaDemo01.Unit1.DoThis;

namespace AkkaDemo01
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var input = "aaabzzzaafff";
            var output = GetCompressedString(input);
            Console.WriteLine(output);
            // DoThisProgram.Main();
        }//

        #region algoritm

        static string GetCompressedString(string raw)
        {
            string result = "";
            foreach (var c in raw.ToCharArray())
            {
                var digitChar = result[result.Length - 1];
                if (char.IsDigit(digitChar))
                {
                    int digit = Convert.ToInt16(c);
                    if (c == result[result.Length - 2])
                    {
                        digit++;
                        result.Remove(result.Length - 1);
                        result += digit;
                    }
                }
                else
                {
                    result += $"{c}1";
                }
            }
            return "";
        }


        #endregion

    }
}
