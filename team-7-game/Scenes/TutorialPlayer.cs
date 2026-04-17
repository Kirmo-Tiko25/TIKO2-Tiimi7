using Godot;
using System;

public partial class TutorialPlayer : Node2D
{
	[Export] Button ExitButton;
	[Export] Button DontShowAgainButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// pause tree, but keep this node processing so that the buttons work
		GetTree().Paused = true;
		ProcessMode = ProcessModeEnum.Always;

		GD.Print("Tutorial "+ GameManager.TutorialOn);

		GD.Print("TutorialPlayer ready");

		ExitButton.Pressed += OnExitButtonPressed;

		DontShowAgainButton.Pressed += OnDontShowAgainButtonPressed;
	}

	private void OnExitButtonPressed()
	{
		GetTree().Paused = false;

		QueueFree();
	}

	private void OnDontShowAgainButtonPressed()
	{
		GetTree().Paused = false;

		GameManager.TutorialOn = false;

		GetNode<GameManager>("/root/GameManager").SaveSettings();
		QueueFree();
	}
}
