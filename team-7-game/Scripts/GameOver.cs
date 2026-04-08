using Godot;
using System;

public partial class GameOver : Control
{
    private AudioStreamPlayer _buttonSound;
	public override void _Ready()
    {
        var pointsText = GetNode<Label>("PointsText");
        var pointsNum = GetNode<Label>("PointsNum");

		//When buttons retry and menu are pressed they call a method that opens a new scene
        GetNode<TextureButton>("retry").Pressed += OnRetryPressed;
        GetNode<TextureButton>("menu").Pressed += OnMenuPressed;

        // Gets the node GameOverClick
		_buttonSound = GetNode<AudioStreamPlayer>("GameOverClick");

        // if setting off hide points else update them to relevant
        if (!GameManager.PointsVisible)
        {
            pointsText.Visible = false;
            pointsNum.Visible = false;
        }
        else
        {
            pointsNum.Text = GameManager.Points.ToString();
        }
    }

	// When these 2 methods are used they open new scenes
    private void OnRetryPressed()
    {
        _buttonSound.Play();
        GetTree().ChangeSceneToFile("res://Scenes/Mainscene.tscn");
        GameManager.ResetPoints();
    }

    private void OnMenuPressed()
    {
        _buttonSound.Play();
        GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn");
        GameManager.ResetPoints();
    }
}
