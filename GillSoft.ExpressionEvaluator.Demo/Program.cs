using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GillSoft.ExpressionEvaluator.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var lines = File.ReadAllLines("input.txt");
                foreach (var line in lines)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    if (line.StartsWith("#"))
                    {
                        continue;
                    }
                    try
                    {
                        var expr = new Expression();

                        expr.HandleFunction += (sender, a) =>
                        {
                            if (a.Name == "lower")
                            {
                                a.Result = ("" + a.Params[0].Value).ToLower();
                            }
                        };

                        expr.HandleVariable += (sender, a) =>
                        {
                            if ("r" == a.Name)
                            {
                                a.Value = 5;
                            }
                            switch (a.Name)
                            {
                                case "a": { a.Value = 1; break; }
                                case "b": { a.Value = 2; break; }
                                case "c": { a.Value = 3; break; }
                                case "klm": { a.Value = 15; break; }
                                case "abc.def": { a.Value = "Some value with Caps"; break; }
                                case "#abc.def#": { a.Value = "Dublin, Ireland"; break; }
                                //case "z": { a.Value = 415; break; }
                                default:
                                    break;
                            }
                        };

                        var result = expr.Evaluate(line);

                        Console.WriteLine(line + " = " + result);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception thrown: " + line + ":" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Write("Press RETURN to close...");
            Console.ReadLine();
        }
    }
}
