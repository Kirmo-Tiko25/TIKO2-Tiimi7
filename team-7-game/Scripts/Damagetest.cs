using Godot;
using System;

public partial class Damagetest : Area2D
{
	private void OnBodyEntered(Node body)
    {
		// Every time when player body snorp and Area2D body collides
		// this method is called and it check if its true and then it calls TakeDamage method from snorp
        if (body is Snorp snorp)
        {
            snorp.TakeDamage(1);
        }
    }
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}
}
