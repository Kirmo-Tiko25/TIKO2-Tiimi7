using Godot;
using System;

public partial class Orlop : RigidBody2D
{
	private int _health = 6;

	public override void _Ready()
	{
		// declares its presense and rights itself.
		GD.Print(Name + "I'm Hunting here");
		Rotation = 0f;
	}


	// Rotates Orlop upright constantly
	public float UpForce = 10f;
	public override void _PhysicsProcess(double delta)
	{
		//self righting bit
		// upright target
		float upright = 0f;
		float currentRot = Rotation;
		float angleError = Mathf.AngleDifference(currentRot, upright);

		float torque = -angleError * UpForce - AngularVelocity * 2f;
		ApplyTorque(torque);

		// collision bit
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
		GD.Print(Name + " Orlop took damage. Current health " + _health);
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
