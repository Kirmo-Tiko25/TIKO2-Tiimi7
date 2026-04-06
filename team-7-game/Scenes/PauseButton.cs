using Godot;
using System;

public partial class PauseButton : Control
{
	private AudioStreamPlayer _buttonSound;
	public override void _Ready()
	{
		Button pause = GetNode<Button>("PauseButton");
		pause.Pressed += OnPausePressed;

		// Gets the node hitsound
		_buttonSound = GetNode<AudioStreamPlayer>("PauseClick");
	}

	private void OnPausePressed()
	{
		GetTree().Paused = true;
		GD.Print("Paused");
		AddChild(GD.Load<PackedScene>("res://Scenes/pause_ui.tscn").Instantiate());
		_buttonSound.Play();
	}
}
