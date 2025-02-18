using System.Diagnostics;

Console.WriteLine("\u001b[1;34mPlease enter your notes. Type 'notes' to open the notes file, 'clear' to clear the notes, 'show' to show the notes, or 'exit' to exit the program.\u001b[0m");

var storedNotes = new List<string>();
string userInput;

while (true)
{
    Console.Write("\u001b[1;32m> \u001b[0m");
    userInput = Console.ReadLine() ?? string.Empty;

    if (string.IsNullOrEmpty(userInput))
    {
        Console.WriteLine("\u001b[1;31mInvalid input!\u001b[0m");
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
        Console.WriteLine("\u001b[1;33mNotes cleared successfully!\u001b[0m");
    }
    else if (userInput == "show")
    {
        if (!File.Exists("notes.txt") || new FileInfo("notes.txt").Length == 0)
        {
            Console.WriteLine("\u001b[1;31mNo notes found!\u001b[0m");
        }
        else
        {
            Console.WriteLine("\u001b[1;34mNotes:\u001b[0m");
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
        Console.WriteLine($"\u001b[1;32m\"{userInput}\" note added successfully!\u001b[0m");
    }
}
