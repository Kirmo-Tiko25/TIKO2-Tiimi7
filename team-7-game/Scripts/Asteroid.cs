using Godot;
using System;

public partial class Asteroid : RigidBody2D
{
	[Export] public int MaxHealth = 2;
	[Export] public int DamageTreshold = 100; // min speed to take damage
	[Export] public PackedScene DebrisAScene;
	[Export] public int DebrisCount = 1;
	private float _health;
	private bool _immune = false;
	private AudioStreamPlayer _hitSound;
	public override void _Ready()
	{
		_health = MaxHealth;
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;

		AngularVelocity = GD.RandRange(-5, 5);

		_hitSound = GetNode<AudioStreamPlayer>("HitSound");
	}

	private void OnBodyEntered(Node body)
	{
		if (body is not RigidBody2D other)
			return;
		_hitSound.Play();
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
		if ((collision != null) && collision.GetCollider() is Node collider
		&& collider.IsInGroup("player"))
		{
			if (!_immune)
			{
				TakeDamage(1);
			}
		}
	}
	private void TakeDamage(int amount)
	{
		_health -= amount;
		GD.Print(Name + " Asteroid took damage. Current health " + _health);
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
		Removed();
	}

	public void Removed()
	{
		QueueFree(); // removes object
	}
}

