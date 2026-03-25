using Godot;
using System;
using System.Text.RegularExpressions;

public partial class SnorpRespawn : CharacterBody2D
{
	// Creates the speed and direction
	[Export] public float Speed = 300.0f;
	[Export] public float speedIncrease = 50.0f;
	[Export] public float speedDecrease = 0.9f;
	private Vector2 direction;
	[Export] private bool canMove = false;
	[Export] private int maxhealth = 3;
	[Signal] delegate void HealthUIEventHandler(int CurrentHealth);
	public int CurrentHealth;
	private bool immune = false;
	public override async void _Ready()
	{
		// Randomize start movement direction
		int startDirection = GD.RandRange(0, 3);

		switch (startDirection)
		{
			case 0: direction = new Vector2(1, -1).Normalized(); break;
			case 1: direction = new Vector2(-1, -1).Normalized(); break;
			case 2: direction = new Vector2(-1, 1).Normalized(); break;
			case 3: direction = new Vector2(1, 1).Normalized(); break;
		}

		// Create timer so Snorp wont move immediately
		await ToSignal(
			GetTree().CreateTimer(3.0f),
			SceneTreeTimer.SignalName.Timeout
		);

		canMove = true;

		// Gives you maxhealth in the start
		CurrentHealth = maxhealth;

	}

	public override void _PhysicsProcess(double delta)
	{
		if (!canMove) return;

		// Create Velocity and local _velocity
		Vector2 Velocity = direction * Speed;

		// Create Movement and enables colliding instead of MoveAndSlide sliding
		var collision = MoveAndCollide(Velocity * (float)delta);

		// Checks if a collision happened.
		if (collision != null)
		{
			HandleWallCollision(collision);
		}
	}
	public void TakeDamage(int damage)
    {
		if (immune)
		{
			GD.Print("Crashed while Immune");
		}
		else
		{
			GetNode<Timer>("ImmuneTimer").Start();

			// Damage taking system every time this method is used it takes 1 of your HP away
			CurrentHealth -= damage;
			EmitSignal(SignalName.HealthUI, CurrentHealth);
			GD.Print("Player HP: " + CurrentHealth);
			// This checks that if you have 0 HP after taking damage the game ends and you die

			// Decreases speed by speedDecrease amount
			Speed *= speedDecrease;
		}

		if (CurrentHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
    {
		var startPosition = GetNode<Marker2D>("../StartPosition");

		// When this method is called it means you have died and the game ends
        GD.Print("You Died!");
		GD.Print("You got "+ GameManager.Points +" Points!");
        Position = startPosition.Position;
		CurrentHealth = maxhealth;
		Speed = 300.0f;
		GD.Print(startPosition.Position);
    }

	private void HandleWallCollision(KinematicCollision2D collision)
	{
		direction = direction.Bounce(collision.GetNormal()); // Bounces in the relevant direction.
		Speed += speedIncrease;  // also increases Speed everytime by speedIncrease amount

		GameManager.AddPoint(1);
		TakeDamage(1);
	}
}
