using System.Diagnostics;

Console.WriteLine("Please enter your notes. Type 'notes' to open the notes file, 'clear' to clear the notes, 'show' to show the notes, or 'exit' to exit the program."); 

var storedNotes = new List<string>();
string userInput;

while (true)
{
	userInput = Console.ReadLine() ?? string.Empty;

	if (string.IsNullOrEmpty(userInput))
	{
		Console.WriteLine("Invalid input!");
		continue;
	}

	if (userInput == "exit")
	{
		break;
	}
	else if (userInput == "notes")
	{
		Process.Start(new ProcessStartInfo("notes.txt") 
		{ UseShellExecute = true });
	}
	else if (userInput == "clear")
	{
		storedNotes.Clear();
		File.WriteAllText("notes.txt", string.Empty);
		Console.WriteLine("Notes cleared successfully!");
	}
	else if (userInput == "show")
{
    if (!File.Exists("notes.txt") || new FileInfo("notes.txt").Length == 0)
    {
        Console.WriteLine("No notes found!");
    }
    else
    {
        Console.WriteLine("Notes:");
        string[] notesFromFile = File.ReadAllLines("notes.txt");
        foreach (var note in notesFromFile)
        {
            Console.WriteLine(note);
        }
    }
}
	else
	{
		storedNotes.Add(userInput);
		File.AppendAllText("notes.txt", userInput + Environment.NewLine);
		Console.WriteLine($"\"{userInput}\" note added successfully!");
	}
}
