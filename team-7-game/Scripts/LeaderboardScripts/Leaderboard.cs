using Godot;
using System;

public partial class Leaderboard : Control
{
	private const string filePath = "user://leaderboard.json";
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// hide unnecessary UI elements until we know if the player has a high score or not
		var NameInput = GetNode<LineEdit>("NameInput");
		NameInput.Visible = false;

		var EnterButton = GetNode<Button>("EnterName");
		EnterButton.Visible = false;

		// checks if the player has a score that is higher than the lowest score on the leaderboard, 
		// if it is then it shows the input field and the button to enter their name
		ScoreCheck();

		EnterButton.Pressed += OnEnterNamePressed;


		LeaderboardFileExistCheck();
	}

	// private void ScoreCheck()
	// {
	// 	if (GameManager.Points > ???)
	// 	{
	// 		GetNode<LineEdit>("NameInput").Visible = true;
	// 		GetNode<Button>("EnterName").Visible = true;
	// 	}
	// }

	private void OnEnterNamePressed()
	{
		var nameInput = GetNode<LineEdit>("NameInput");
		var EnterButton = GetNode<Button>("EnterName");

		if (!string.IsNullOrEmpty(nameInput.Text))
		{
			SaveScore(nameInput.Text, GameManager.Points);

			nameInput.Visible = false;
			EnterButton.Visible = false;
			nameInput.Text = "";
		}
	}

	private void LeaderboardFileExistCheck()
	{
		if (!FileAccess.FileExists(filePath))
		{
			var file = FileAccess.Open(filePath, FileAccess.ModeFlags.Write);
			file.StoreString("[]");
			file.Close();
		}
	}
}
// SaveScore(nameInput.Text, GameManager.Points);