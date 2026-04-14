using Godot;
using System;

public partial class MainScene : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// load the tutorial if it's on in settings
		if (GameManager.TutorialOn)
		{
			var tutorialLayer = new CanvasLayer();
			AddChild(tutorialLayer);
			tutorialLayer.AddChild(GD.Load<PackedScene>("res://Scenes/TutorialPlayer.tscn").Instantiate());
		}
	}
}
