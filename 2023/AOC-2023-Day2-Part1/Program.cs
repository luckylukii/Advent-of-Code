﻿using Microsoft.Win32.SafeHandles;

internal class Program
{
    private const int MAX_RED = 12;
    private const int MAX_GREEN = 13;
    private const int MAX_BLUE = 14;

    private static string[] testInputs = new string[]{
        "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
        "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
        "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
        "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
        "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
    };
    private static void Main()
    {
        string[] inputs = Reader.ReadFile("D:/C# Projects/AOC/Advent-of-Code/2023/AOC-2023-Day2-Part1/Inputs.txt").ToArray();


        int sumOfIDs = 0;

        for (int i = 0; i < inputs.Length; i++)
        {
            //Remove "Game x: " from string
            int lengthOfStringToRemove = $"Game {i+1}: ".Length;
            inputs[i] = inputs[i].Remove(0, lengthOfStringToRemove);

            string[] reveals = inputs[i].Split(';');

            bool[] evaluations = new bool[reveals.Length];

            for (int j = 0; j < reveals.Length; j++)
            {
                int reds = 0;
                int greens = 0;
                int blues = 0;

                string[] colors = reveals[j].Split(',');
                for (int k = 0; k < colors.Length; k++){

                    colors[k] = colors[k].Trim();

                    int endOfNumber = colors[k].IndexOf(' ');
                    int numToAdd = int.Parse(colors[k][0..endOfNumber].ToString());

                    if (colors[k].Contains("red")) reds += numToAdd;
                    if (colors[k].Contains("green")) greens += numToAdd;
                    if (colors[k].Contains("blue")) blues += numToAdd;
                }

                evaluations[j] = EvaluateReveal(reds, greens, blues);
            }

            bool wholeInputValid = AndBoolArray(evaluations);

            if (wholeInputValid) sumOfIDs += i+1;
        }

        Console.WriteLine(sumOfIDs);
        Console.ReadLine();
    }

    private static bool EvaluateReveal(int red, int green, int blue){
        return red <= MAX_RED && 
               green <= MAX_GREEN && 
               blue <= MAX_BLUE;
    }

    private static bool AndBoolArray(bool[] arr){
        bool output = true;
        foreach (bool condition in arr)
        {
            output &= condition;
        }
        return output;
    }
}