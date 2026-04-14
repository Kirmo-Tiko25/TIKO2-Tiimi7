using Godot;
using System;
using System.Diagnostics.CodeAnalysis;

public partial class Menu : Control
{
    [Export] TextureButton play;
    [Export] TextureButton options;
    [Export] TextureButton quit;
    [Export] TextureButton tutorial;
    [Export] TextureButton finnish;
    [Export] TextureButton english;
    [Export] TextureButton swedish;
    private AudioStreamPlayer _buttonSound;
    public override void _Ready()
    {
        // When button Play pressed it class the method OnPlayPressed and that method opens new scene
        play.Pressed += OnPlayPressed;

        options.Pressed += OnOptionsPressed;

        // When button quit pressed it class the method OnQuitPressed and that method quits the game
        quit.Pressed += OnQuitPressed;

        tutorial.Pressed += OnTutorialPressed;

        finnish.Pressed += OnFinnishPressed;

        english.Pressed += OnEnglishPressed;

        swedish.Pressed += OnSwedishPressed;

        // Gets the node MenuClick
        _buttonSound = GetNode<AudioStreamPlayer>("MenuClick");
    }

    private void OnPlayPressed()
    {
        // When this method is called it opens the games MainScene where the main game is running
        _buttonSound.Play();
        GetTree().ChangeSceneToFile("res://Scenes/MainScene.tscn");

    }

    private void OnOptionsPressed()
    {
        _buttonSound.Play();
        AddChild(GD.Load<PackedScene>("res://Scenes/Settings.tscn").Instantiate());
    }

    private void OnQuitPressed()
    {
        // quits
        _buttonSound.Play();
        GetTree().Quit();
    }

    private void OnTutorialPressed()
    {
        _buttonSound.Play();
        GetTree().CurrentScene.AddChild(GD.Load<PackedScene>("res://Scenes/TutorialPlayer.tscn").Instantiate());
    }

    private void OnFinnishPressed()
    {
        _buttonSound.Play();
        TranslationServer.SetLocale("fi");
    }

    private void OnEnglishPressed()
    {
        _buttonSound.Play();
        TranslationServer.SetLocale("en");
    }

    private void OnSwedishPressed()
    {
        GD.Print("Swedish pressed");
        _buttonSound.Play();
        TranslationServer.SetLocale("sv");
    }
}
