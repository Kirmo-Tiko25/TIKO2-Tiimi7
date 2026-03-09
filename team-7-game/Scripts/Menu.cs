using Godot;
using System;
using System.Diagnostics.CodeAnalysis;

public partial class Menu : Control
{
    public override void _Ready()
    {
        // When button nappi pressed it class the method OnPlayPressed and that method opens new scene
        Button nappi = GetNode<Button>("Nappi");
        nappi.Pressed += OnPlayPressed;



        // When button nappi pressed it class the method OnPlayPressed and that method opens new scene
        Button quit = GetNode<Button>("Quit");
        quit.Pressed += OnQuitPressed;
    }

    private void OnPlayPressed()
    {
        // When this method is called it opens the games MainScene where the main game is running
        GetTree().ChangeSceneToFile("res://Scenes/MainScene.tscn");
    }

    private void OnQuitPressed()
    {
        // quits
        GetTree().Quit();
    }
}
