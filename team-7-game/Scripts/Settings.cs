using Godot;
using System;

public partial class Settings : Control
{
	[Export] private Button pointsVisibilityButton;
	[Export] private Button leaderboardVisibilityButton;
	[Export] private Label	pointsVisibilityLabel;
	[Export] private Label	leaderboardVisibilityLabel;
	public override void _Ready()
	{
		// Get nodes for naming convenience
		pointsVisibilityButton = GetNode<Button>("SettingsBackground/PointsVisibleButton");
		leaderboardVisibilityButton = GetNode<Button>("SettingsBackground/LeaderboardVisibleButton");

		pointsVisibilityLabel = GetNode<Label>("SettingsBackground/PointsVisibleLabel");
		leaderboardVisibilityLabel = GetNode<Label>("SettingsBackground/LeaderboardVisibleLabel");


		ButtonUpdate();

		pointsVisibilityButton.Pressed += ChangePointVisibility;
		leaderboardVisibilityButton.Pressed += ChangeLeaderboardVisibility;

		GetNode<Button>("SettingsBackground/ExitButton").Pressed += OnExitButtonPressed;
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
	}

	private void OnExitButtonPressed()
	{
		QueueFree();
	}
}
