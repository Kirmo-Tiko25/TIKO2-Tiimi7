using Godot;
using System;

public partial class PauseButton : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Button pause = GetNode<Button>("PauseButton");
		pause.Pressed += OnPausePressed;
	}

	private void OnPausePressed()
	{
		GetTree().Paused = true;
		GD.Print("Paused");
		AddChild(GD.Load<PackedScene>("res://Scenes/pause_ui.tscn").Instantiate());
	}
}
