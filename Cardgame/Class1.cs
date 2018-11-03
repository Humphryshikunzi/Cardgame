using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Cards
{
    class Program
    {
        static void Main()
        {
            char stringbuildlastchar, stringbuildsecondlastchar, inputfirstchar, inputlastchar, quit, playagain;
            string userpickedcard, startupcard, computervalidcard = null;
            int getrandomindex, cardtostartwithindex;


            List<string> cardsbeforereshuffling = new List<string>()
            {
                "KH","QH","JH","10H","9H","8H","7H","6H","5H","4H","3H","2H","AH",
                "KF","QF","JF","10F","9F","8F","7F","6F","5F","4F","3F","2F","AF",
                "KD","QD","JD","10D","9D","8D","7D","6D","5D","4D","3D","2D","AD",
                "KS","QS","JS","10S","9S","8S","7S","6S","5S","4S","3S","2S","AS"
           };
            List<string> cardsafterreshuffling = new List<string>();
            List<int> indexes = new List<int>();
            List<string> computercards = new List<string>();
            List<string> mycards = new List<string>();
            List<string> cardsthatcanstartgame = new List<string>()
            {
                "10H","9H","7H","6H","5H","4H",
                "10F","9F","7F","6F","5F","4F",
                "10D","9D","7D","6D","5D","4D",
                "10S","9S","7S","6S","5S","4S"
            };


            Random rand = new Random();
            getrandomindex = rand.Next(cardsbeforereshuffling.Count);
            for (int i = 0; i < cardsbeforereshuffling.Count - 1; i++)
            {
                cardsafterreshuffling.Add(cardsbeforereshuffling[getrandomindex]);
                indexes.Add(getrandomindex);
                while (true)
                {
                    getrandomindex = rand.Next(cardsbeforereshuffling.Count);
                    if (indexes.Contains(getrandomindex))
                    {
                        continue;
                    }
                    break;
                }
            }

            cardtostartwithindex = rand.Next(cardsthatcanstartgame.Count);
            startupcard = cardsthatcanstartgame[cardtostartwithindex];
            cardsafterreshuffling.Remove(startupcard);




            for (int i = 0; i < 8; i++)
            {
                computercards.Add(cardsafterreshuffling[i]);
                cardsafterreshuffling.RemoveAt(i);
                Console.Write(computercards[i] + " ");
            }
            Console.WriteLine("> Computer Cards");

            for (int i = 8; i < 16; i++)
            {
                mycards.Add(cardsafterreshuffling[i]);
                cardsafterreshuffling.RemoveAt(i);
            }

            foreach (var card in mycards)
            {
                Console.Write(card + " ");
            }

            Console.WriteLine("> My Cards");


            StringBuilder build = new StringBuilder();
            build.Append(startupcard);
            Console.Write(startupcard);
            bool play = true;
            while (play)
            {
                userpickedcard = Console.ReadLine().ToUpperInvariant();
                stringbuildsecondlastchar = build.ToString()[build.ToString().Length - 2];
                stringbuildlastchar = build.ToString()[build.ToString().Length - 1];
                inputfirstchar = userpickedcard[0];
                inputlastchar = userpickedcard[1];
                if (userpickedcard.Length > 2)
                {
                    stringbuildsecondlastchar = build.ToString()[build.ToString().Length - 3];
                    inputlastchar = userpickedcard[2];
                }

                if (mycards.Contains(userpickedcard) && ((stringbuildsecondlastchar == inputfirstchar) || (stringbuildlastchar == inputlastchar)))
                {
                    build.Append(userpickedcard.ToUpper());
                    mycards.Remove(userpickedcard);

                    if (build.ToString()[build.ToString().Length - 2] == '2')
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            computercards.Add(cardsafterreshuffling[0]);
                            cardsafterreshuffling.RemoveAt(0);
                        }
                    }
                    foreach (var myremaingcard in mycards)
                    {
                        Console.Write(myremaingcard + " ");
                    }
                    Console.WriteLine(build.ToString());
                }
                else
                {
                    mycards.Add(cardsafterreshuffling[0]);
                    cardsafterreshuffling.RemoveAt(0);
                }

                stringbuildsecondlastchar = build.ToString()[build.ToString().Length - 2];
                stringbuildlastchar = build.ToString()[build.ToString().Length - 1];
                foreach (var compcard in computercards)
                {
                    if (compcard.StartsWith(stringbuildsecondlastchar) || compcard[1] == '0' || compcard.EndsWith(stringbuildlastchar))
                    {
                        computervalidcard = compcard;
                        build.Append(computervalidcard);
                        if (build.ToString()[build.ToString().Length - 2] == '2')
                        {
                            for (int i = 1; i < 3; i++)
                            {
                                computercards.Add(cardsafterreshuffling[0]);
                                cardsafterreshuffling.RemoveAt(0);
                            }
                        }
                        computercards.Remove(computervalidcard);

                        break;
                    }
                    else
                    {
                        computervalidcard = null;
                    }
                }

                if (computervalidcard == null)
                {
                    computercards.Add(cardsafterreshuffling[0]);
                    cardsafterreshuffling.RemoveAt(0);
                }

                Console.Clear();

                foreach (var card in cardsafterreshuffling)
                {
                    Console.Write(card + " ");
                }

                Console.WriteLine($"> Deck cards ({cardsafterreshuffling.Count}) ");

                foreach (var remainingcard in computercards)
                {
                    Console.Write(remainingcard + " ");
                }

                Console.WriteLine($"> Computer Cards that are remaining ({computercards.Count})");

                foreach (var myremaingcard in mycards)
                {
                    Console.Write(myremaingcard + " ");
                }
                Console.WriteLine($">My remaing cards ({mycards.Count})");
                Console.WriteLine(build.ToString());
                //probability of winning 

                float winningprobabilty = (float)(mycards.Count / (mycards.Count + computercards.Count)) * 100;
                // Console.SetCursorPosition(10,20);
                // Console.WriteLine($"Your winning probability ={winningprobabilty % ");
                if (mycards == null)
                {
                    Console.WriteLine("Congratulations , you won");
                    Console.WriteLine("Press 'P' to play again and 'Q' to quit game");
                    quit = char.Parse(Console.ReadLine());
                    if (quit == 'Q')
                    {
                        break;
                    }
                    if (quit == 'P')
                    {
                        Main();
                    }

                }

                if (computercards == null)
                {
                    Console.WriteLine("Oops ,you lost ");
                    Console.WriteLine("Press 'P' to play again and 'Q' to quit game");
                    quit = char.Parse(Console.ReadLine());
                    if (quit == 'Q')
                    {
                        break;
                    }
                    if (quit == 'P')
                    {
                        Main();
                    }

                }

            }
        }

        public static void Savedcode()
        {
            /*  if (build.ToString()[build.ToString().Length - 2] == '2')
                       {
                           computercards.Add(cardsafterreshuffling[0]);
                           cardsafterreshuffling.RemoveAt(0);
                           computercards.Add(cardsafterreshuffling[0]);
                           cardsafterreshuffling.RemoveAt(0);

                           if (userpickedcard.Length==2)
                           {
                           build.Remove(build.ToString().Length - 3, build.Length - 1);
                       }
                           if ( userpickedcard.Length>3)
                           {
                           build.Remove(build.ToString().Length - 3, build.Length - 1);
                       }
                       }
                       if (build.ToString()[build.ToString().Length - 2] == '3')
                       {
                           computercards.Add(cardsafterreshuffling[0]);
                           cardsafterreshuffling.RemoveAt(0);
                           computercards.Add(cardsafterreshuffling[0]);
                           cardsafterreshuffling.RemoveAt(0);
                           computercards.Add(cardsafterreshuffling[0]);
                           cardsafterreshuffling.RemoveAt(0);
                       }*/

            /*  if (build.ToString()[build.ToString().Length - 2] == '2')
                                   {
                                       mycards.Add(cardsafterreshuffling[0]);
                                       cardsafterreshuffling.RemoveAt(0);
                                       mycards.Add(cardsafterreshuffling[0]);
                                       cardsafterreshuffling.RemoveAt(0);
                                   }
                                   if (build.ToString()[build.ToString().Length - 2] == '3')
                                   {
                                       mycards.Add(cardsafterreshuffling[0]);
                                       cardsafterreshuffling.RemoveAt(0);
                                       mycards.Add(cardsafterreshuffling[0]);
                                       cardsafterreshuffling.RemoveAt(0);
                                       mycards.Add(cardsafterreshuffling[0]);
                                       cardsafterreshuffling.RemoveAt(0);
                                   }*/

        }
    }
    public enum Namesofcards
    {
        Hearts = 13, Spades = 13, Flowers = 13, Diamonds = 13


    }
}
