using System;
using System.Collections.Generic;
using System.Threading;

namespace CoinJar
{
    class Program
    {
        public interface ICoinJar
        {
            void AddCoin(ICoin coin);
            decimal GetTotalAmount();
            void Reset();
        }
        public interface ICoin
        {
            decimal Amount { get; set; }
            decimal Volume { get; set; }
        }
        public class Coin : ICoin
        {
            public decimal Amount { get ; set ; }
            public decimal Volume { get; set; }
        }

        public class CoinJar : ICoinJar
        {
         

            private const decimal TotalVolume = 42;
            private decimal MoneyCounter { get; set; }
            public CoinJar()
            {
                this.RemainingVolume = TotalVolume;
                MoneyCounter = 0;
                Coins = new List<ICoin>();
            }

            private decimal RemainingVolume { get; set; }
            private List<ICoin> Coins { get; set; }
            public void AddCoin(ICoin coin)
            {
                if(RemainingVolume - coin.Volume >= 0)
                {
                    Coins.Add(coin);
                    RemainingVolume -= coin.Volume;
                    MoneyCounter += coin.Amount;
                }
                else
                {
                    Console.WriteLine("No more space to add more coins");
                }

            }

            public decimal GetTotalAmount()
            {  
                return MoneyCounter;
            }
            public override string ToString()
            {
                var total = GetTotalAmount();
                return $"Total Volume = {TotalVolume}\nTotal Volume Remaining = {RemainingVolume}\nTotal Amount = {total}";
            }
            public void Reset()
            {
                Coins = new List<ICoin>();
                RemainingVolume = TotalVolume;
                MoneyCounter = 0;
                Console.WriteLine("Coin Jar Has been Reset!");
                Console.WriteLine(ToString());
            }
            public decimal GetTotalVolumeRemaining()
            {
                return RemainingVolume;
            }
            public ICoin GetCoin(int index)
            {
                if(index > 0 && index < Coins.Count)
                {
                    return Coins[index];
                }
                return new Coin();
            }
        }


        static void Main(string[] args)
        {
            var coinjar = new CoinJar();
            var count = 0;
            while (true)
            {
                if (count == 0) { 
                    Console.WriteLine("Welcome to our coin jar APP, Coin Jar Details:");
                    Console.WriteLine(coinjar.ToString());
                }

                 count = count == 0 ? count + 1 : count ;
                 Thread.Sleep(100);
                 Console.WriteLine("How many coins would you like to add ?");
                 Thread.Sleep(100);
                 var amount = Convert.ToDecimal(Console.ReadLine());

                 Console.WriteLine("What's the volume of the coins you would like to add ?");
                 Thread.Sleep(100);
                 var volume = Convert.ToDecimal(Console.ReadLine());
                 if(volume > 42)
                 {
                     Console.WriteLine("Maximum coin jar volume is 42 Ounces Fluid, please add a volume less than or equal to 42");
                     continue;
                 }
                 var coin = new Coin() { Amount = amount, Volume = volume };
                
                 coinjar.AddCoin(coin);
                 Console.WriteLine(coinjar.ToString());
                if (coinjar.GetTotalVolumeRemaining() == 0)
                 {
                    Console.WriteLine("Coin Jar is full, Would you like to start over y/n ?");
                    var restartresponse = Console.ReadLine().ToLower();
                    if (restartresponse == "y")
                    {
                        coinjar.Reset();
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Thank you for playing our CoinJar App");
                        Thread.Sleep(1000);
                        break;
                    }
                 }
                 
                 Thread.Sleep(100);
                 Console.WriteLine("Would you like add another coin y / n ?");

                 var response = Console.ReadLine().ToLower();
             
                  if(response == "y"){ continue;}
                  else
                  {
                    Console.WriteLine("Would you like to start over y/n ?");
                    response = Console.ReadLine().ToLower();
                    if(response == "y")
                    {
                        coinjar.Reset();

                    }
                    else
                    {
                        Console.WriteLine("Thank you for playing our CoinJar App");
                        Thread.Sleep(1000);
                        break;
                    }

                  }
            }
        
        }
    }
}
