using Godot;
using System;
using System.Diagnostics.CodeAnalysis;

public partial class Menu : Control
{
    private AudioStreamPlayer _buttonSound;
    public override void _Ready()
    {
        // When button Play pressed it class the method OnPlayPressed and that method opens new scene
        TextureButton play = GetNode<TextureButton>("play");
        play.Pressed += OnPlayPressed;

        TextureButton options = GetNode<TextureButton>("options");
        options.Pressed += OnOptionsPressed;

        // When button quit pressed it class the method OnQuitPressed and that method quits the game
        TextureButton quit = GetNode<TextureButton>("quit");
        quit.Pressed += OnQuitPressed;

        TextureButton finnish = GetNode<TextureButton>("Translation/Finnish");
        finnish.Pressed += OnFinnishPressed;

        TextureButton english = GetNode<TextureButton>("Translation/English");
        english.Pressed += OnEnglishPressed;

        TextureButton swedish = GetNode<TextureButton>("Translation/Swedish");
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
