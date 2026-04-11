using Godot;
using System;

public partial class GameManager : Node2D
{
	public static int Points { get; private set; } = 0;
	public static bool PointsVisible { get; set; } = true;
	public static bool LeaderboardVisible { get; set; } = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustReleased("pause"))
		{
			PauseGame();
		}

		if (Input.IsActionJustReleased("quit"))
		{
			if (GetTree().Paused == true)
			{
				GD.Print("Quit");

				// TODO write a log file of console?

				GetTree().Quit();
			}
			else PauseGame();
		}
	}

	public void PauseGame()
	{
		if (GetTree().Paused == false)
		{
			GetTree().Paused = true;
			GD.Print("Paused");
		}
		else
		{
			GetTree().Paused = false;
			GD.Print("Unpaused");
		}
	}

	public static void AddPoint(int amount)
	{
		Points += amount;
		GD.Print("You got a point, now you have: " + Points);
	}

	public static void ResetPoints()
	{
		Points = 0;
	}

	public static void PointsVisibilityToggled()
	{
		PointsVisible = !PointsVisible;
	}

	public static void LeaderboardVisibilityToggled()
	{
		LeaderboardVisible = !LeaderboardVisible;
	}
}