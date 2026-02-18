using Godot;
using System;

public partial class TestMovementCollide : CharacterBody2D
{
	// Creates the speed and direction
	[Export] public float Speed = 300.0f;
	[Export] public float speedIncrease = 50.0f;
	private Vector2 direction;

	public override void _Ready()
	{
		Random i = new Random();
		int startDirection = i.Next(0, 4);

		switch (startDirection)
		{
			case 0: direction = new Vector2(1, -1).Normalized(); break;
			case 1: direction = new Vector2(-1, -1).Normalized(); break;
			case 2: direction = new Vector2(-1, 1).Normalized(); break;
			case 3: direction = new Vector2(1, 1).Normalized(); break;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		// Create Velocity and local _velocity
		Vector2 _velocity = direction * Speed;
		Velocity = _velocity;

		// Create Movement and enables colliding instead of MoveAndSlide sliding
		var collision = MoveAndCollide(Velocity * (float)delta);

		// if collides Bounces in the relative direction
		if (collision != null)
		{
			direction = direction.Bounce(collision.GetNormal());
			Speed += speedIncrease;
		}
	}
}