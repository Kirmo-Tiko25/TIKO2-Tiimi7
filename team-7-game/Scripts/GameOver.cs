using Godot;
using System;

public partial class GameOver : Control
{
	public override void _Ready()
    {
		//When buttons retry and menu are pressed they call a method that opens a new scene
        GetNode<TextureButton>("retry").Pressed += OnRetryPressed;
        GetNode<TextureButton>("menu").Pressed += OnMenuPressed;
    }

	// When these 2 methods are used they open new scenes
    private void OnRetryPressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Mainscene.tscn");
        GameManager.ResetPoints();
    }

    private void OnMenuPressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn");
        GameManager.ResetPoints();
    }
}
