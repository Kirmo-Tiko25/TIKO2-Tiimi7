using Godot;
using System;
using System.Diagnostics.CodeAnalysis;

public partial class Menu : Control
{
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
    }

    private void OnPlayPressed()
    {
        // When this method is called it opens the games MainScene where the main game is running
        GetTree().ChangeSceneToFile("res://Scenes/MainScene.tscn");
    }

    private void OnOptionsPressed()
    {
        AddChild(GD.Load<PackedScene>("res://Scenes/Settings.tscn").Instantiate());
    }

    private void OnQuitPressed()
    {
        // quits
        GetTree().Quit();
    }

    private void OnFinnishPressed()
    {
        TranslationServer.SetLocale("fi");
    }

    private void OnEnglishPressed()
    {
        TranslationServer.SetLocale("en");
    }
}
