using Godot;
using System;

public partial class HeartsPointsUI : Control
{
    private TextureRect[] hearts;
    public override void _Ready()
    {

        // Get all heart nodes
        hearts = new TextureRect[3];
        hearts[0] = GetNode<TextureRect>("Hearts/Heart1");
        hearts[1] = GetNode<TextureRect>("Hearts/Heart2");
        hearts[2] = GetNode<TextureRect>("Hearts/Heart3");

        // Hide points if setting off
        if (!GameManager.PointsVisible)
        {
            GetNode<Label>("PointsLabel").Visible = false;
        }
    }

    public override void _Process(double delta)
    {
        var PointsLabel = GetNode<Label>("PointsLabel");

        // Update points if they are visible
        if (GameManager.PointsVisible)
        {
            PointsLabel.Text = GameManager.Points.ToString();
        }
    }

    // Hide a specific heart by index (0 = Heart1, 1 = Heart2, 2 = Heart3)
    public void HideHeart(int currentHealth)
    {
        // Hide the heart at the index equal to current health
        hearts[currentHealth].Visible = false;
    }
}