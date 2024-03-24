namespace MauiApp1;

public partial class MainPage : ContentPage
{
	//int count = 0;

	// calling the Encryptor class
	private Encryptor _encryptor;
	private Decryptor _decryptor;

	private string _key = "{s86#}LetRCon*&$";

	public MainPage()
	{
		InitializeComponent();
		_encryptor = new Encryptor(_key);
		_decryptor = new Decryptor(_key);
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
				var decryptedText = _decryptor.DecryptFile(filePath);
				StatusLabel.Text = $"Decrypted content:\n{decryptedText}";
				DisplayAlert("Success", $"The selected file was successfully decrypted", "OK");
			}
			else 
			{
				var encryptedFilePath = _encryptor.EncryptFile(filePath);
				StatusLabel.Text = $"Encryption Complete\nEncrypted file saved at:\n{encryptedFilePath}";
				await DisplayAlert("Success", $"The selected file was successfully ecrypted", "OK");
			}
		}
	}


}

