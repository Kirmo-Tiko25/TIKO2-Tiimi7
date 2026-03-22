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
    }

    // Hide a specific heart by index (0 = Heart1, 1 = Heart2, 2 = Heart3)
    public void HideHeart(int currentHealth)
{
    // Hide the heart at the index equal to current health
    // Example: if health drops from 3 -> 2, hide Heart3 (index 2)
    int heartIndex = currentHealth;
    if (heartIndex >= 0 && heartIndex < hearts.Length)
        hearts[heartIndex].Visible = false;
}
}