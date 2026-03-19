using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;

public partial class Leaderboard : Control
{
	// The file path where the leaderboard data will be stored
	private const string filePath = "user://leaderboard.json";

	// A list to hold the leaderboard entries
	private List<LeaderboardEntries> Entry = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Points at leaderboard load: ", GameManager.Points);

		// hide unnecessary UI elements until we know if the player has a high score or not
		GetNode<Control>("IfGoodScore").Visible = false;


		// checks if the player has a score that is higher than the lowest score on the leaderboard,
		// if it is then it shows the input field and the button to enter their name

		LoadLeaderboard();

		ScoreCheck();

		GetNode<Button>("IfGoodScore/EnterName").Pressed += OnEnterNamePressed;

	}

	private void LoadLeaderboard()
	{
		GD.Print("Loading leaderboard...");

		// Check if the leaderboard file exists, if not create an empty leaderboard
		if (!FileAccess.FileExists(filePath))
		{
			GD.Print("No existing leaderboard file found.");
			Entry = new List<LeaderboardEntries>();
			DisplayLeaderboard();
			return;
		}

		GD.Print("Leaderboard exists");

		using var fileRead = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
		string jsonString = fileRead.GetAsText();

		GD.Print("JSON content: ", jsonString);

		// Check if the file is empty or contains only whitespace
		// necessary if empty gives me cancer
		if (string.IsNullOrWhiteSpace(jsonString))
		{
			GD.Print("File is empty.");
			Entry = new List<LeaderboardEntries>();
		}
		else
		{
			// Deserialize the JSON string into a list of leaderboard entries if null make new (string to objects)
			Entry = JsonSerializer.Deserialize<List<LeaderboardEntries>>(jsonString) ?? new();
		}

		GD.Print("Deserialization done");

		SortScores();
		DisplayLeaderboard();
	}

	private void ScoreCheck()
	{
		GD.Print("Running ScoreCheck");

		// If conditions match allow player to enter name
		if (Entry.Count < 5 || GameManager.Points > Entry[Entry.Count - 1].Score)
		{
			GD.Print("Should be showing IfGoodScore now");
			GetNode<Control>("IfGoodScore").Visible = true;
		}
		else
		{
			GD.Print("Not showing IfGoodScore");
		}
	}

	private void OnEnterNamePressed()
	{
		var nameInput = GetNode<LineEdit>("IfGoodScore/NameInput");

		// Force player to enter a name before saving the score
		if (!string.IsNullOrEmpty(nameInput.Text))
		{
			SaveScore(nameInput.Text, GameManager.Points);

			// hide input field and button and reset text
			GetNode<Control>("IfGoodScore").Visible = false;
			nameInput.Text = "";
		}
	}

	private void SaveScore(string name, int score)
	{
		// open file for writing
		using var fileWrite = FileAccess.Open(filePath, FileAccess.ModeFlags.Write);

		// add the new score to the list of entries
		Entry.Add(new LeaderboardEntries { Name = name, Score = score });

		SortScores();

		// serialize the list of entries to JSON and write it to the file (not sure copied from stack overflow boom)
		string jsonString = JsonSerializer.Serialize(Entry);
		fileWrite.StoreString(jsonString);

		DisplayLeaderboard();
	}

	private void DisplayLeaderboard()
	{
		// update the UI labels to show the leaderboard entries
		for (int i = 0; i < 5; i++)
		{
			var nameLabel = GetNode<Label>($"VisibleLeaderboard/GridContainer/{i + 1}_Name");
			var scoreLabel = GetNode<Label>($"VisibleLeaderboard/GridContainer/{i + 1}_Score");

			if (i < Entry.Count)
			{
				nameLabel.Text = Entry[i].Name;
				scoreLabel.Text = Entry[i].Score.ToString();
			}
			else
			{
				nameLabel.Text = "Empty";
				scoreLabel.Text = "0";
			}
		}	
	
	}

	private void SortScores()
	{
		// sort the entries in descending order based on the score
		Entry.Sort((a, b) => b.Score.CompareTo(a.Score));

		// keep only the top 5 entries
		if (Entry.Count > 5)
		{
			Entry = Entry.GetRange(0, 5);
		}
	}
}

// get set class for name and score of leaderboard entries
public class LeaderboardEntries
{
	public string Name { get; set; }
	public int Score { get; set; }
}