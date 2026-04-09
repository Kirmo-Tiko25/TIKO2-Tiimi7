using Godot;
using System;

public partial class Asteroid : RigidBody2D
{
	private int _health = 2;
	public override void _Ready()
	{
		// select one of the (1) animation types and plays it (good for later).
		//var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		//string[] asteroidTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
		//animatedSprite2D.Play(asteroidTypes[0]); //currently only plays the one.

		// here is the randomaizer version for later:
		// animatedSprite2D.Play(asteroidTypes[GD.Randi() % asteroidTypes.Length]);

		AngularVelocity = GD.RandRange(-5, 5);
	}
	int _size = 1;
	public override void _PhysicsProcess(double delta)
	{
		var collision = MoveAndCollide(LinearVelocity * (float)delta);

		// Checks if a collision happened.
		if ((collision != null) && collision.GetCollider() is Node collider
		&& (collider.IsInGroup("Hazards") || collider.IsInGroup("player")))
		{
			TakeDamage(1);
		}
	}

	private void TakeDamage(int amount)
	{
		_health -= amount;
		GD.Print(Name + " Asteroid took damage. Current health " + _health);
		if (_health <= 0)
		{
			Removed();
		}
	}

	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		Removed();
	}

	public void Removed()
	{
		QueueFree(); // removes object
	}


}

