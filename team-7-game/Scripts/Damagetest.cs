using Godot;
using System;

public partial class Damagetest : Area2D
{
    // Reference to the HeartPointsUI script
    private HeartsPointsUI heartUI;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;

        heartUI = GetNode<HeartsPointsUI>("/MoveTest/HeartsPointsUI");
    }

    private void OnBodyEntered(Node body)
    {
        if (body is Snorp snorp)
        {
            // Tell the player to take damage
            snorp.TakeDamage(1);

            // Hide a heart when damage occurs
            heartUI.ChangeHeart(snorp.CurrentHealth);
        }
    }
}