

using System;
using System.Text.RegularExpressions;

string input = "input.txt";
var enviroment = System.Environment.CurrentDirectory;
string ?projectDirectory = Directory.GetParent(enviroment)?.Parent?.FullName;
string text = null;
try {
    text = System.IO.File.ReadAllText($"{projectDirectory}/../{input}");
}catch(FileNotFoundException ex) {
    // when running from cli dotnet run
    text = System.IO.File.ReadAllText($"./{input}");
}


// Split for each empty lines is an elves
Regex rx = new Regex(@"^$[\r\n]+", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
var elves = rx.Split(text);
//Console.WriteLine(elves.Count());

// map lines for each elves, cast to number, to array
var elvesCalories = elves
    .Select(str => str.Split("\n"))
    .Select(lines => lines.ToArray()
        .Where(item => item != "")
        .Select(line => int.Parse(line)))
    .ToArray();

// then sum foreach elves
var max = 0;
foreach (var calories in elvesCalories)
{
    var sum = calories.Sum();
    if(sum > max)
    {
        max = sum;
    }
}
Console.WriteLine($"Max calories for all elves: {max}");