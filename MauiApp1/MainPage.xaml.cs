namespace MauiApp1;

public partial class MainPage : ContentPage
{
	//int count = 0;

	// calling the Encryptor class
	private Encryptor _encryptor;

	private string _key = "{s86#}LetRCon*&$";

	public MainPage()
	{
		InitializeComponent();
		_encryptor = new Encryptor(_key);
	}

	private async void SelectFileButton_Clicked(object sender, EventArgs e)
	{
		var file = await FilePicker.PickAsync();
		if (file != null)
		{
			var filePath = file.FullPath;

			// checking if the file is encrypted or not
			if (_encryptor.IsEncrypted(filePath))
			{
				// var decryptedText = DecryptFile(filePath);
				//DisplayAlert("Success", $"The selected file was successfully decrypted", "OK");
				// StatusLabel.Text = $"Decrypted content:\n{decryptedText}";
				Console.WriteLine("Still in progress!");
			}
			else 
			{
				var encryptedFilePath = _encryptor.EncryptFile(filePath);
				StatusLabel.Text = $"Encryption Complete\nEncrypted file saved at:\n{encryptedFilePath}";
				await DisplayAlert("Success", $"The selected file was successfully ecrypted", "OK");
			}
		}
	}

	// private void OnCounterClicked(object sender, EventArgs e)
	// {
	// 	count++;

	// 	if (count == 1)
	// 		CounterBtn.Text = $"Clicked {count} time";
	// 	else
	// 		CounterBtn.Text = $"Clicked {count} times";

	// 	SemanticScreenReader.Announce(CounterBtn.Text);
	// }




}

