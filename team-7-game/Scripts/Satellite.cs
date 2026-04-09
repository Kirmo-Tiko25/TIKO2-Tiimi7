using Godot;
using System;

public partial class Satellite : RigidBody2D
{
	private int _health = 3;
	public override void _PhysicsProcess(double delta)
	{
		var collision = MoveAndCollide(LinearVelocity * (float)delta);

		// Checks if a collision happened.
		if (collision != null && collision.GetCollider() is Node collider
		&& (collider.IsInGroup("Hazards") || collider.IsInGroup("player")))
		{
			TakeDamage(1);
		}
	}

	private void TakeDamage(int amount)
	{
		GD.Print("Satellite took damage. Current health " + _health);
		_health -= amount;

		if (_health <= 0)
		{
			Removed();
		}
	}
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		Spawner.satellitePassed = true;
		Spawner.noDistract = true;
		Removed();
	}

	public void Removed()
	{
		QueueFree(); //this 'frees', or deletes, the node at the end of the frame.
	}
}
