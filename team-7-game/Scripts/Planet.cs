using Godot;
using System;

public partial class Planet : RigidBody2D
{
	[Export] public int MaxHealth = 10;
	[Export] public int DamageTreshold = 10; // min speed to take damage
	[Export] public PackedScene LifeScene;
	[Export] public PackedScene DebrisAScene;
	[Export] public int DebrisCount = 1;
	private float _health;
	private bool _immune = false;
	public override void _Ready()
	{
		_health = MaxHealth;
		var area = GetNode<Area2D>("Surface");
		area.BodyEntered += OnAreaBodyEntered;
		area.BodyExited += OnAreaBodyExited;
	}

	private void OnAreaBodyEntered(Node body)
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
	private void OnAreaBodyExited(Node body)
	{
		// allow damage again
		_immune = false;
	}
	private void TakeDamage(int amount)
	{
		_health -= amount;
		GD.Print("Planet took damage. Current health " + _health);

		if (_health <= 0)
		{
			SpawnDebris();
			SpawnLife();
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
	private void SpawnLife()
	{
		{
			Life heart = LifeScene.Instantiate<Life>();

			heart.Position = GlobalPosition;
			GetParent().AddChild(heart);
		}
	}
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		Removed();
	}

	public void Removed()
	{
		Spawner.satellitePassed = false;
		Spawner.noDistract = true;
		QueueFree(); //this 'frees', or deletes, the node at the end of the frame.
	}
}
