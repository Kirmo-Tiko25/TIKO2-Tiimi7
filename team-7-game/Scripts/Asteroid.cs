using Godot;
using System;

public partial class Asteroid : RigidBody2D
{
	private int _health = 3;

	//private AudioStreamPlayer2D _hitSound;
	public override void _Ready()
	{
		// select one of the (1) animation types and plays it (good for later).
		//var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		//string[] asteroidTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
		//animatedSprite2D.Play(asteroidTypes[0]); //currently only plays the one.

		// here is the randomaizer version for later:
		// animatedSprite2D.Play(asteroidTypes[GD.Randi() % asteroidTypes.Length]);

		AngularVelocity = GD.RandRange(-5, 5);

		//_hitSound = GetNode<AudioStreamPlayer2D>("HitSound");
	}

	public override void _PhysicsProcess(double delta)
	{
		var collision = MoveAndCollide(LinearVelocity * (float)delta);

		// Checks if a collision happened.
		if (collision != null && collision.GetCollider() is Node collider && collider.IsInGroup("Hazards"))
		{
			TakeDamage(1);

			// TODO Cant have same script for 2 objects or it will errors
			//_hitSound.Play();
		}
	}

	private void TakeDamage(int amount)
	{
		GD.Print("Asteroid took damage. Current healht " + _health);
		_health -= amount;

		if (_health <= 0)
		{
			OnVisibleOnScreenNotifier2DScreenExited();
		}
	}

	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		// TODO send points to score before deleting the object
		// SendPoints();
		Removed();
	}



	public void Removed()
	{
		QueueFree(); //this 'frees', or deletes, the node at the end of the frame.
	}
}
