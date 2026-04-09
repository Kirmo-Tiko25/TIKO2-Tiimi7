using Godot;
using System;

public partial class Planet : CharacterBody2D
{
	private int _health = 100;
	public override void _Ready()
	{
		var area = GetNode<Area2D>("Surface");
		area.BodyEntered += OnAreaBodyEntered;
	}

	private void OnAreaBodyEntered(Node body)
	{
		GD.Print("Planet surface was hit by: " + body.Name);
		TakeDamage(1);
	}
	private void TakeDamage(int amount)
	{
		_health -= amount;
		GD.Print("Planet took damage. Current health " + _health);

		if (_health <= 0)
		{
			Removed();
		}
	}

	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		Spawner.satellitePassed = false;
		Spawner.noDistract = true;
		Removed();
	}

	public void Removed()
	{
		QueueFree(); //this 'frees', or deletes, the node at the end of the frame.
	}
}
