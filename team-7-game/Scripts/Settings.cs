using Godot;
using System;

public partial class Settings : Control
{
	[Export] private Button pointsVisibilityButton;
	[Export] private Button leaderboardVisibilityButton;
	[Export] private Button tutorialOnButton;

	[Export] private Label	pointsVisibilityLabel;
	[Export] private Label	leaderboardVisibilityLabel;
	[Export] private Label tutorialOnLabel;
	private int _musicBus;
    private int _sfxBus;
	public override void _Ready()
	{
		// Get nodes for naming convenience
		pointsVisibilityButton = GetNode<Button>("SettingsBackground/PointsVisibleButton");
		leaderboardVisibilityButton = GetNode<Button>("SettingsBackground/LeaderboardVisibleButton");

		pointsVisibilityLabel = GetNode<Label>("SettingsBackground/PointsVisibleLabel");
		leaderboardVisibilityLabel = GetNode<Label>("SettingsBackground/LeaderboardVisibleLabel");

		//GetNode<HSlider>("SettingsBackground/Music")

		ButtonUpdate();

		pointsVisibilityButton.Pressed += ChangePointVisibility;
		leaderboardVisibilityButton.Pressed += ChangeLeaderboardVisibility;
		tutorialOnButton.Pressed += ChangeTutorialOn;

		GetNode<Button>("SettingsBackground/ExitButton").Pressed += OnExitButtonPressed;

		//Gets the music and sfx bus nodes.
		_musicBus = AudioServer.GetBusIndex("Music");
        _sfxBus = AudioServer.GetBusIndex("SFX");

		var musicSlider = GetNode<HSlider>("SettingsBackground/Music");
		musicSlider.ValueChanged += OnMusicVolumeChanged;

		var sfxSlider = GetNode<HSlider>("SettingsBackground/SFX");
		sfxSlider.ValueChanged += OnSfxVolumeChanged;
	}

	private void ChangePointVisibility()
	{
		GameManager.PointsVisibilityToggled();
		ButtonUpdate();
	}

	private void ChangeLeaderboardVisibility()
	{
		GameManager.LeaderboardVisibilityToggled();
		ButtonUpdate();
	}

	private void ChangeTutorialOn()
	{
		GameManager.TutorialToggled();
		ButtonUpdate();
	}

	private void ButtonUpdate()
	{
		if (GameManager.PointsVisible)
		{
			pointsVisibilityLabel.Text = "ON";
			pointsVisibilityLabel.AddThemeColorOverride("font_color", Colors.Green);
		}
		else
		{
			pointsVisibilityLabel.Text = "OFF";
			pointsVisibilityLabel.AddThemeColorOverride("font_color", Colors.Red);

		}

		if (GameManager.LeaderboardVisible)
		{
			leaderboardVisibilityLabel.Text = "ON";
			leaderboardVisibilityLabel.AddThemeColorOverride("font_color", Colors.Green);
		}
		else
		{
			leaderboardVisibilityLabel.Text = "OFF";
			leaderboardVisibilityLabel.AddThemeColorOverride("font_color", Colors.Red);
		}

		if (GameManager.TutorialOn)
		{
			tutorialOnLabel.Text = "ON";
			tutorialOnLabel.AddThemeColorOverride("font_color", Colors.Green);
		}
		else
		{
			tutorialOnLabel.Text = "OFF";
			tutorialOnLabel.AddThemeColorOverride("font_color", Colors.Red);
		}
	}

	public void OnMusicVolumeChanged(double value)
    {
        AudioServer.SetBusVolumeDb(_musicBus, LinearToDb((float)value));
    }

    public void OnSfxVolumeChanged(double value)
    {
        AudioServer.SetBusVolumeDb(_sfxBus, LinearToDb((float)value));
    }

  	private float LinearToDb(float linear)
	{
    	return Mathf.LinearToDb(Mathf.Max(linear, 0.0001f));
	}

	private void OnExitButtonPressed()
	{
		QueueFree();
	}

}
