using System.Diagnostics;
internal class Program
{
	private static void Main(string[] args)
	{
		Console.WriteLine(
			"\u001b[1;34mPlease enter your notes. Type 'notes' to open the notes " +
			"file, 'clear' to clear the notes, 'show' to show the notes, or " +
			"'exit' to exit the program.\u001b[0m");

		var StoredNotes = new List<string>();

		while (true)
		{
			string userInput = Console.ReadLine() ?? string.Empty;
			Action operation = userInput switch
			{
				"exit" => () => Environment.Exit(0),
				"notes" => () => Process.Start(
					new ProcessStartInfo("notes.txt") { UseShellExecute = true }),
				"clear" => () =>
				{
					StoredNotes.Clear();
					File.WriteAllText("notes.txt", string.Empty);
					Console.WriteLine("\u001b[1;33mNotes cleared successfully!\u001b[0m");
				}

				,
				"show" => () =>
				{
					if (!File.Exists("notes.txt") ||
						new FileInfo("notes.txt").Length == 0)
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
				,
				_ => () =>
				{
					StoredNotes.Add(userInput);
					File.AppendAllText("notes.txt", userInput + Environment.NewLine);
					Console.WriteLine("\u001b[1;33mNote added successfully!\u001b[0m");
				}
			};

			operation();
		}
	}
}
