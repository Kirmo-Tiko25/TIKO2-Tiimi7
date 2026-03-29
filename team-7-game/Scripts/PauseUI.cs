using Godot;
using System;

public partial class PauseUI : Control
{
	[Export] private Control _pauseMenu = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void OpenPause()
	{
		_pauseMenu.Show();
		//GameManager.Instance.SceneTree.Paused = true;
	}
	public void ClosePause()
	{
		//GameManager.Instance.SceneTree.Paused = false;
		_pauseMenu.Hide();
	}
}
