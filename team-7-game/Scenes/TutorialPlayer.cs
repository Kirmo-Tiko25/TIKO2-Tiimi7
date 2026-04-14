using Godot;
using System;

public partial class TutorialPlayer : Node2D
{
	[Export] Button ExitButton;
	[Export] Button DontShowAgainButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("TutorialPlayer ready");

		ExitButton.Pressed += OnExitButtonPressed;

		DontShowAgainButton.Pressed += OnDontShowAgainButtonPressed;
	}

	private void OnExitButtonPressed()
	{
		QueueFree();
	}

	private void OnDontShowAgainButtonPressed()
	{
		GameManager.TutorialToggled();
		QueueFree();
	}
}
