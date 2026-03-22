using Godot;
using System;

public partial class Damagetest : Area2D
{
    // Reference to the HeartPointsUI script
    private HeartPointsUI heartUI;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;

        // Assuming HeartPointsUI is somewhere in the scene tree
        heartUI = GetNode<HeartPointsUI>("/MoveTest/HeartsPointsUI");
        // Adjust the path above to where your HeartPointsUI actually is in the scene
    }

    private void OnBodyEntered(Node body)
    {
        if (body is Snorp snorp)
        {
            // Tell the player to take damage
            snorp.TakeDamage(1);

            // Hide a heart when damage occurs
            heartUI.HideHeart(snorp.CurrentHealth); // assuming Health decreases by 1
        }
    }
}