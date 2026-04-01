using Godot;
using System;

public partial class PauseUI : Control
{
	[Export] private Control _pauseMenu = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Always;
		Button continueButton = GetNode<Button>("Continue");
		continueButton.Pressed += OnContinuePressed;

		Button quitButton = GetNode<Button>("Quit");
		quitButton.Pressed += OnQuitPressed;

	}
	private void OnContinuePressed()
	{
		GetTree().Paused = false;
		GD.Print("Unpaused");
		QueueFree();
	}
	private void OnQuitPressed()
	{
		GetTree().Paused = false;
		GD.Print("Unpaused");
		GameManager.ResetPoints();
		GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn");
	}


}
