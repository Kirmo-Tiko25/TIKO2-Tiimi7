using Godot;
using System;

public partial class Satellite : RigidBody2D
{
	[Export] public int MaxHealth = 2;
	[Export] public int DamageTreshold = 100; // min speed to take damage
	[Export] public PackedScene DebrisAScene;
	[Export] public int DebrisCount = 1;
	private float _health;
	private bool _immune = false;
	public override void _Ready()
	{
		_health = MaxHealth;
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}
	private void OnBodyEntered(Node body)
	{
		if (body is not RigidBody2D other)
			return;

		// Check relative velocity
		Vector2 relativeVelocity = LinearVelocity - other.LinearVelocity;
		float impactSpeed = relativeVelocity.Length();
		GD.Print("Impact speed of collision: " + impactSpeed);
		// if over threshold then apply damage
		if (impactSpeed > DamageTreshold && !_immune)
		{
			_immune = true;
			if (impactSpeed - DamageTreshold < 10)
				TakeDamage(1);
			else
			{
				TakeDamage(2);
			}
		}

	}
	private void OnBodyExited(Node body)
	{
		// allow damage again
		_immune = false;
	}
	public override void _PhysicsProcess(double delta)
	{
		var collision = MoveAndCollide(LinearVelocity * (float)delta);

		// Checks if a collision happened.
		if (collision != null && collision.GetCollider() is Node collider
		&& (collider.IsInGroup("Hazards") || collider.IsInGroup("player")))
		{
			if (!_immune)
			{
				TakeDamage(1);
			}
		}
	}

	private void TakeDamage(int amount)
	{
		GD.Print("Satellite took damage. Current health " + _health);
		_health -= amount;

		if (_health <= 0)
		{
			SpawnDebris();
			Removed();
		}
	}
	private void SpawnDebris()
	{
		for (int i = 0; i < DebrisCount; i++)
		{
			DebrisA debris = DebrisAScene.Instantiate<DebrisA>();
			GetParent().AddChild(debris);

			debris.Position = GlobalPosition;

			Vector2 dir = Vector2.Right.Rotated(GD.Randf() * Mathf.Tau);
			float force = GD.RandRange(80, 200);

			debris.Launch(dir, force);
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
