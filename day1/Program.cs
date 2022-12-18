

using System;
using System.Text.RegularExpressions;

string ReadFile(string input = "input.txt")
{
    
    var enviroment = System.Environment.CurrentDirectory;
    string? projectDirectory = Directory.GetParent(enviroment)?.Parent?.FullName;
    string text = null;
    try
    {
        text = System.IO.File.ReadAllText($"{projectDirectory}/../{input}");
    }
    catch (FileNotFoundException ex)
    {
        // when running from cli dotnet run
        text = System.IO.File.ReadAllText($"./{input}");
    }
    return text;
}


IEnumerable<int>[] SplitElves(string text)
{
    // 1 - Split empty lines, so we can know calories for each elf
    Regex rx = new Regex(@"^$[\r\n]+", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
    var elves = rx.Split(text);

    // 2 - Map lines for each elves, cast to number, to array
    var elvesCalories = elves
        .Select(str => str.Split("\n"))
        .Select(lines => lines.ToArray()
            .Where(item => item != "")
            .Select(line => int.Parse(line)))
        .ToArray();
        
    return elvesCalories;
}



// Read from input txt calories lines 
var text = ReadFile("input.txt");

// Split for each empty lines is an elves then map the correct calories for each elf and cast to number
var elvesCalories = SplitElves(text);

// then sum foreach elves and find the max value
var max = 0;
foreach (var calories in elvesCalories)
{
    var sum = calories.Sum();
    if (sum > max)
    {
        max = sum;
    }
}
Console.WriteLine($"Max calories for all elves: {max}");

// 2 - find the top 3 elves carring max calories, then sum them

// then sum foreach elves and find the max value
var maxElvesCalories = new List<int>();
foreach (var calories in elvesCalories)
{
    var sum = calories.Sum();
    maxElvesCalories.Add(sum);
}
maxElvesCalories.Sort();
maxElvesCalories.Reverse();
Console.WriteLine($"Top 3 elves total calories: {maxElvesCalories.Take(3).Sum()}");

