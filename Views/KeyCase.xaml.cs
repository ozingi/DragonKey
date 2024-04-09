namespace DragonKey.Views;
using DragonKey.Models.core;

[QueryProperty(nameof(ItemId), nameof(ItemId))]

public partial class KeyCase : ContentPage
{
    public KeyCase()
	{
		InitializeComponent();


        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

        LoadNote(Path.Combine(appDataPath, randomFileName));
    }
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Note note)
            File.WriteAllText(note.Filename, TextEditor.Text);
      
        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Note note)
        {
            // Delete the file.
            if (File.Exists(note.Filename))
                File.Delete(note.Filename);
        }

        await Shell.Current.GoToAsync("..");
    }
    private void LoadNote(string fileName)
    {
        Note noteModel = new Note();
        noteModel.Filename = fileName;

        if (File.Exists(fileName))
        {
            noteModel.Date = File.GetCreationTime(fileName);
            noteModel.Text = File.ReadAllText(fileName);
        }

        BindingContext = noteModel;
    }
    public string ItemId
    {
        set { LoadNote(value); }
    }
}