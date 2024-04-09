using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DragonKey.Models.core;
internal class Home
{
    public ObservableCollection<AddKey> SecretKeys { get; set; } = new ObservableCollection<AddKey>();

    public Home() =>
        LoadNotes();
    public void LoadNotes()
    {
        SecretKeys.Clear();

        // Get the folder where the notes are stored.
        string appDataPath = FileSystem.AppDataDirectory;

        // Use Linq extensions to load the *.notes.txt files.
        IEnumerable<AddKey> secretKeys = Directory

                                    // Select the file names from the directory
                                    .EnumerateFiles(appDataPath, "*")

                                    // Each file name is used to create a new Note
                                    .Select(filename => new AddKey()
                                    {
                                        Filename = Path.GetFileName(filename),
                                        SecretKey = File.ReadAllText(filename),
                                        FilenamePath = Path.GetFullPath(filename),
                                        Date = File.GetCreationTime(filename),

                                    })

                                    // With the final collection of notes, order them by date
                                    .OrderBy(secretKey => secretKey.Date);

        // Add each note into the ObservableCollection
        foreach (AddKey secretKey in secretKeys)
            SecretKeys.Add(secretKey);
    }
}