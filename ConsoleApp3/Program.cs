namespace ns
{
    public class Record
    {
        public int tryNum;

        public string difficulty;

        public int hintsUsed;

        public int attemptsUsed;

        public string result;

    }

    public class tClass
    {
        public static void Main()
        {
            List<Record> records = new List<Record>();
            int ctr = 0;
            int n = 0;
            int hints = 0;
            int attemptsLeft = 0;
            int level = 0;
            var random = new Random();
            while(true)
            {
                bool success = false;
                Console.WriteLine("Choose difficulty:\n1 - easy\n2 - medium\n3 - hard\n\nq - exit");
                var input = Console.ReadLine();
                if (input == "1")
                {
                    hints = 1;
                    n = random.Next(0, 10);
                    attemptsLeft = 2;
                    level = 1;
                    ctr++;
                }
                else if (input == "2")
                {
                    hints = 2;
                    n = random.Next(0, 100);
                    attemptsLeft = 5;
                    level = 2;
                    ctr++;
                }
                else if (input == "3")
                {
                    hints = 4;
                    n = random.Next(0, 1000);
                    attemptsLeft = 10;
                    level = 3;
                    ctr++;
                }
                else if (input == "q")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("GG");
                    continue;
                }
                while (attemptsLeft > 0)
                {
                    Console.WriteLine($"attempts left: {attemptsLeft}");
                    Console.WriteLine("Enter number or \"hint\"");
                    input = Console.ReadLine();
                    if (input != "hint" && !int.TryParse(input, out int parsed))
                    {
                        Console.WriteLine("Wrong input");
                        continue;
                    }
                    else if (int.TryParse(input, out int parsedValue))
                    {
                        if(parsedValue < 0)
                        {
                            Console.WriteLine("Value must be non-negative");
                            continue;
                        }
                        if (n != parsedValue)
                        {
                            attemptsLeft--;
                            Console.WriteLine("Wrong suggestion");
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Congrats!");
                            records.Add(new Record
                            {
                                tryNum = ctr,
                                result = "success",
                                attemptsUsed = level == 1 ? 2 - attemptsLeft + 1 : level == 2 ? 5 - attemptsLeft + 1 : 10 - attemptsLeft + 1,
                                hintsUsed = level == 1 ? 1 - hints : level == 2 ? 2 - hints : 4 - hints,
                                difficulty = level == 1 ? "easy" : level == 2 ? "medium" : "hard"
                            });
                            success = true;
                            break;
                        }
                    }
                    else
                    {
                        do
                        {
                            Console.WriteLine("Choose your hint:\n1 - IsSimple\n2 - IsDevidedPer3\n3 - ReduceRange");
                            input = Console.ReadLine();
                            if (input == "1")
                            {
                                if (hints > 0)
                                {
                                    IsSimple(n);
                                    hints--;
                                }
                                else
                                {
                                    Console.WriteLine("Out of hints");
                                }
                            }
                            else if (input == "2")
                            {
                                if (hints > 0)
                                {
                                    IsDevidedPer3(n);
                                    hints--;
                                }
                                else
                                {
                                    Console.WriteLine("Out of hints");
                                }
                            }
                            else if (input == "3")
                            {
                                if (hints > 0)
                                {
                                    ReduceRange(n, level, random);
                                    hints--;
                                }
                                else
                                {
                                    Console.WriteLine("Out of hints");
                                }
                            }
                        }
                        while (input != "1" && input != "2" && input != "3");
                    }
                }
                if(!success)
                {
                    records.Add(new Record { tryNum = ctr, result = "failure", 
                    attemptsUsed = level == 1 ? 2 - attemptsLeft : level == 2 ? 5 - attemptsLeft : 10 - attemptsLeft,
                    hintsUsed = level == 1 ? 1 - hints : level == 2 ? 2 - hints : 4 - hints,
                    difficulty = level == 1 ? "easy" : level == 2 ? "medium" : "hard" });
                }
                
                Console.WriteLine("GG");
                Console.WriteLine();
            }

            do
            {
                Console.WriteLine("Show records? y/n");
                var input = Console.ReadLine();
                if(input == "y")
                {
                    foreach(var r in records.OrderBy(o => o.attemptsUsed).OrderByDescending(od => od.result))
                    {
                        Console.WriteLine($"Try: {r.tryNum} Result: {r.result} Difficulty: {r.difficulty} Attempts used: {r.attemptsUsed} Hints used: {r.hintsUsed}");
                    }
                    break;
                }
                else if(input == "n")
                {
                    break;
                }
            }
            while (true);
            return;

        }

        public static void IsSimple(int n)
        {
            if(n == 0)
            {
                Console.WriteLine("Not simple");
                return;
            }
            if(n == 1 || n == 2)
            {
                Console.WriteLine("Simple");
                return;
            }
            var range = new List<int>();
            for(int i = 2; i < n; i++)
            {
                range.Add(i);
            }
            if(!range.Any(a => n % a == 0))
            {
                Console.WriteLine("Simple");
            }
            else
            {
                Console.WriteLine("Not simple");
            }
        }

        public static void IsDevidedPer3(int n)
        {
            if(n == 0)
            {
                Console.WriteLine("Not divided by 3");
                return;
            }
            if(n % 3 == 0)
            {
                Console.WriteLine("Divided by 3");
            }
            else
            {
                Console.WriteLine("Not divided by 3");
            }
        }

        public static void ReduceRange(int n, int lvl, Random random)
        {
            var max = lvl == 1 ? 10 : lvl == 2 ? 100 : 1000;
            var newBorder = random.Next(1, max / 2);
            Console.WriteLine(newBorder);
            if(1 <= n && n < newBorder)
            {
                Console.WriteLine($"N is between 1 and {max - newBorder}");
            }
            else
            {
                Console.WriteLine($"N is between {newBorder} and {max}");
            }
        }

    }
}