using Godot;
using System;

public partial class HeartsPointsUI : Control
{
    [Export] public int MaxHearts = 5;
    private TextureRect[] hearts;
    public override void _Ready()
    {

        // Get all heart nodes
        hearts = new TextureRect[MaxHearts];
        hearts[0] = GetNode<TextureRect>("Hearts/Heart1");
        hearts[1] = GetNode<TextureRect>("Hearts/Heart2");
        hearts[2] = GetNode<TextureRect>("Hearts/Heart3");
        hearts[3] = GetNode<TextureRect>("Hearts/Heart4");
        hearts[4] = GetNode<TextureRect>("Hearts/Heart5");
        ChangeHeart(0);
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
    public void ChangeHeart(int currentHealth)
    {
        // Change to show them equal to current health
        for (int i = 0; i < MaxHearts; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].Visible = true;
            }
            else
            {
                hearts[i].Visible = false;
            }

        }
    }
}