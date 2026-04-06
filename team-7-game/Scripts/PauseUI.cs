using Godot;
using System;

public partial class PauseUI : Control
{
	private AudioStreamPlayer _buttonSound;
	[Export] private Control _pauseMenu = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Always;
		TextureButton continueButton = GetNode<TextureButton>("Continue");
		continueButton.Pressed += OnContinuePressed;

		TextureButton quitButton = GetNode<TextureButton>("Quit");
		quitButton.Pressed += OnQuitPressed;

		// Gets the node PauseClick
		_buttonSound = GetNode<AudioStreamPlayer>("PauseClick");

	}
	private void OnContinuePressed()
	{
		_buttonSound.Play();
		GetTree().Paused = false;
		GD.Print("Unpaused");
		QueueFree();
	}
	private void OnQuitPressed()
	{
		_buttonSound.Play();
		GetTree().Paused = false;
		GD.Print("Unpaused");
		GameManager.ResetPoints();
		GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn");
	}


}
