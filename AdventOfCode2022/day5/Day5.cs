using System.Text;

namespace AdventOfCode2022
{
    /*
    
    https://adventofcode.com/2022/day/5

    --- Day 5: Supply Stacks ---

    The expedition can depart as soon as the final supplies have been unloaded from the ships. Supplies are stored in stacks of marked crates, but because the needed supplies are buried under many other crates, the crates need to be rearranged.

    The ship has a giant cargo crane capable of moving crates between stacks. To ensure none of the crates get crushed or fall over, the crane operator will rearrange them in a series of carefully-planned steps. After the crates are rearranged, the desired crates will be at the top of each stack.

    The Elves don't want to interrupt the crane operator during this delicate procedure, but they forgot to ask her which crate will end up where, and they want to be ready to unload them as soon as possible so they can embark.

    They do, however, have a drawing of the starting stacks of crates and the rearrangement procedure (your puzzle input). For example:

        [D]    
    [N] [C]    
    [Z] [M] [P]
     1   2   3 

    move 1 from 2 to 1
    move 3 from 1 to 3
    move 2 from 2 to 1
    move 1 from 1 to 2

    In this example, there are three stacks of crates. Stack 1 contains two crates: crate Z is on the bottom, and crate N is on top. Stack 2 contains three crates; from bottom to top, they are crates M, C, and D. Finally, stack 3 contains a single crate, P.

    Then, the rearrangement procedure is given. In each step of the procedure, a quantity of crates is moved from one stack to a different stack. In the first step of the above rearrangement procedure, one crate is moved from stack 2 to stack 1, resulting in this configuration:

    [D]        
    [N] [C]    
    [Z] [M] [P]
     1   2   3 

    In the second step, three crates are moved from stack 1 to stack 3. Crates are moved one at a time, so the first crate to be moved (D) ends up below the second and third crates:

            [Z]
            [N]
        [C] [D]
        [M] [P]
     1   2   3

    Then, both crates are moved from stack 2 to stack 1. Again, because crates are moved one at a time, crate C ends up below crate M:

            [Z]
            [N]
    [M]     [D]
    [C]     [P]
     1   2   3

    Finally, one crate is moved from stack 1 to stack 2:

            [Z]
            [N]
            [D]
    [C] [M] [P]
     1   2   3

    The Elves just need to know which crate will end up on top of each stack; in this example, the top crates are C in stack 1, M in stack 2, and Z in stack 3, so you should combine these together and give the Elves the message CMZ.

    After the rearrangement procedure completes, what crate ends up on top of each stack?

    */

    public class Day5
    {
        public static async Task Run()
        {
            Console.WriteLine("Day 5 Part 1: " + await GetPart1());
        }

        public static async Task<string> GetPart1()
        {
            string[] input = await File.ReadAllLinesAsync("day5/input");

            const int CRATE_COUNT = 9;
            Stack<string>[] crates = new Stack<string>[CRATE_COUNT];
            for (int i = 0; i < CRATE_COUNT; i++)
                crates[i] = new Stack<string>();

            var pendingCrates = new Stack<string>();
            foreach (string line in input)
            {
                if (line.StartsWith('['))
                    pendingCrates.Push(line);

                if (string.IsNullOrWhiteSpace(line))
                {
                    foreach (var crateLine in pendingCrates)
                    {
                        for (int i = 0; i < CRATE_COUNT; i++)
                        {
                            int charIndex = i * 4 + 1;
                            string crate = crateLine.Substring(charIndex, 1);
                            if (!string.IsNullOrWhiteSpace(crate))
                                crates[i].Push(crate);
                        }
                    }
                }

                if (line.StartsWith("move"))
                {
                    var numbers = line.Split(' ');
                    var quantity = int.Parse(numbers[1].Trim());
                    var from = int.Parse(numbers[3].Trim());
                    var to = int.Parse(numbers[5].Trim());

                    for (int i = 0; i < quantity; i++)
                    {
                        string item = crates[from - 1].Pop();
                        crates[to - 1].Push(item);
                    }
                }

            }

            var solution = new StringBuilder();
            for (int i = 0; i < CRATE_COUNT; i++)
                solution.Append(crates[i].Pop());

            return solution.ToString();
        }
    }
}