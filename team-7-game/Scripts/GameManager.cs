using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class GameManager : Node2D
{
	public static int Points { get; private set; } = 0;
	public static bool PointsVisible { get; set; }
	public static bool LeaderboardVisible { get; set; }
	public static bool TutorialOn { get; set; }
	public float MusicVolume { get; private set; }
	public float SfxVolume { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LoadSettings();
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

	// Resets points and other things for a new game
	public static void ResetPoints()
	{
		Points = 0;
		Spawner.satellitePassed = false;
		Spawner.noDistract = true;
		Spawner.orlopSpawned = false;
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), 0.4f);

	}

	public void LoadSettings()
	{
		var config = new ConfigFile();
		// Load data from a file.
		Error err = config.Load("user://config.cfg");

		// If the file didn't load, ignore it.
		if (err != Error.Ok)
		{
			// Filenot found, use defaults
			MusicVolume = 0.5f;
			SfxVolume = 0.5f;
			PointsVisible = true;
			LeaderboardVisible = true;
			TutorialOn = true;
			SaveSettings();
			return;
		}

		MusicVolume = (float)config.GetValue("audio", "music_volume", 0.5);
		SfxVolume = (float)config.GetValue("audio", "sfx_volume", 0.5);
		PointsVisible = (bool)config.GetValue("visible", "points", true);
		LeaderboardVisible = (bool)config.GetValue("visible", "leader", true);
		TutorialOn = (bool)config.GetValue("tutorial", "tutorial", true);
	}

	public void SaveSettings()
	{
		var config = new ConfigFile();

		config.SetValue("audio", "music_volume", MusicVolume);
		config.SetValue("audio", "sfx_volume", SfxVolume);
		config.SetValue("visible", "points", PointsVisible);
		config.SetValue("visible", "leader", LeaderboardVisible);
		config.SetValue("tutorial", "tutorial", TutorialOn);

		config.Save("user://config.cfg");
	}

	public void SetMusicVolume(float value) => MusicVolume = value;
	public void SetSfxVolume(float value) => SfxVolume = value;
	public void SetPoints(bool value) => PointsVisible = value;
	public void SetLeader(bool value) => LeaderboardVisible = value;
	public void SetTutorial(bool value) => TutorialOn = value;

	public static void PointsVisibilityToggled()
	{
		PointsVisible = !PointsVisible;
	}

	public static void LeaderboardVisibilityToggled()
	{
		LeaderboardVisible = !LeaderboardVisible;
	}

	public static void TutorialToggled()
	{
		TutorialOn = !TutorialOn;
		GD.Print("Tutorial: "+TutorialOn);
	}
	// Saves settings when closing game
	public override void _Notification(int what)
	{
		if (what == NotificationWMCloseRequest)
		{
			SaveSettings();
		}
	}
}