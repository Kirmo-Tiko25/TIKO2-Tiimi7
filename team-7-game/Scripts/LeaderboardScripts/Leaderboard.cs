using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;

public partial class Leaderboard : Control
{
	private const string filePath = "user://leaderboard.json";

	private List<LeaderboardEntry> Entry = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// readability improvement
		var NameInput = GetNode<LineEdit>("NameInput");
		var EnterButton = GetNode<Button>("EnterName");

		// hide unnecessary UI elements until we know if the player has a high score or not
		GetNode<Control>("IfGoodScore").Visible = false;


		// checks if the player has a score that is higher than the lowest score on the leaderboard,
		// if it is then it shows the input field and the button to enter their name

		LoadLeaderboard();

		ScoreCheck();

		EnterButton.Pressed += OnEnterNamePressed;

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
			// SaveScore(nameInput.Text, GameManager.Points);

			nameInput.Visible = false;
			EnterButton.Visible = false;
			nameInput.Text = "";
		}
	}

	private void ScoreCheck()
	{
		bool qualifies = LeaderboardEntries < 5;
		LoadLeaderboard();

	}

	private void LoadLeaderboard()
	{
		var fileWrite = FileAccess.Open(filePath, FileAccess.ModeFlags.Write);
		var fileRead = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);

		if (!FileAccess.FileExists(filePath))
		{
			fileWrite.StoreString("[]");
			fileWrite.Close();
		}

	}
}
// SaveScore(nameInput.Text, GameManager.Points);