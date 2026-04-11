using Godot;
using System;
using System.Text.RegularExpressions;

public partial class Snorp : CharacterBody2D
{
	// Creates the speed and direction
	[Export] public float Speed = 300.0f;
	[Export] public float speedIncrease = 50.0f;
	[Export] public float speedDecrease = 0.5f;
	[Export] public float maxSpeed = 600f;
	private Vector2 direction;
	[Export] private bool canMove = false;
	[Export] private int maxhealth = 3;
	[Signal] delegate void HealthUIEventHandler(int CurrentHealth);
	public int CurrentHealth;
	private bool immune = false;
	private AudioStreamPlayer _hitSound;
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
		// Gets the node hitsound
		_hitSound = GetNode<AudioStreamPlayer>("HitSound");

	}

	public override void _PhysicsProcess(double delta)
	{
		if (!canMove) return;

		GetNode<Label>("SpeedLabel").Text = $"S: {Speed:F0}";

		// Create Velocity and local _velocity
		Vector2 Velocity = direction * Speed;

		// downgrade speed to maxspeed
		if (Speed > maxSpeed)
		{
			GetNode<Label>("SpeedLabel").AddThemeColorOverride("font_color",
			new Color(1, 0, 0, 1));
		}

		// Create Movement and enables colliding instead of MoveAndSlide sliding
		var collision = MoveAndCollide(Velocity * (float)delta);

		// Checks if a collision happened.
		if (collision != null)
		{
			// Checks if the collider is a hazard and if it is calls the relevant method.
			if (collision.GetCollider() is Node collider && collider.IsInGroup("Hazards"))
			{
				HandleHazardCollision(collision);
			}
			else
			{
				HandleWallCollision(collision);
			}
		}
		// TODO shaking system to make it appear unctrolloed.
		// Rotate(0.1f);
	}
	public void TakeDamage(int damage)
	{
		if (immune)
		{
			GD.Print("Crashed while Immune");
		}
		else
		{
			// 1 sec immunity
			immune = true;
			// switch to damage animation
			GetNode<AnimatedSprite2D>("SnorpUfo").Play("damage");
			// start immune timer
			GetNode<Timer>("ImmuneTimer").Start();

			// Damage taking system every time this method is used it takes 1 of your HP away
			CurrentHealth -= damage;
			EmitSignal(SignalName.HealthUI, CurrentHealth);
			GD.Print("Player HP: " + CurrentHealth);
			// This checks that if you have 0 HP after taking damage the game ends and you die

			// Decreases speed by speedDecrease amount
			Speed *= speedDecrease;
			//When the player takes damage it plays the hit sound
			_hitSound.Play();
		}

		if (CurrentHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		// When this method is called it means you have died and the game ends
		GD.Print("You Died!");
		GD.Print("You got " + GameManager.Points + " Points!");
		QueueFree();
		GetTree().ChangeSceneToFile("res://Scenes/GameOver.tscn");
	}

	private void HandleWallCollision(KinematicCollision2D collision)
	{
		direction = direction.Bounce(collision.GetNormal()); // Bounces in the relevant direction.
		Speed += speedIncrease;  // also increases Speed everytime by speedIncrease amount

		GameManager.AddPoint(1);
	}

	private void HandleHazardCollision(KinematicCollision2D collision)
	{
		// Pushes the player slightly away from the hazard to prevent sticking
		GlobalPosition += collision.GetNormal() * 1f;

		TakeDamage(1);

		Vector2 bounce = direction.Bounce(collision.GetNormal());

		// Checks the direction of the bounce and changes the movement direction accordingly
		if (bounce.X > 0 && bounce.Y > 0) direction = new Vector2(1, 1).Normalized();
		else if (bounce.X > 0 && bounce.Y < 0) direction = new Vector2(1, -1).Normalized();
		else if (bounce.X < 0 && bounce.Y > 0) direction = new Vector2(-1, 1).Normalized();
		else if (bounce.X < 0 && bounce.Y < 0) direction = new Vector2(-1, -1).Normalized();

		GD.Print("You crashed into a hazard!");
	}
	private void OnImmuneTimerTimeout()
	{
		immune = false;
		GD.Print("Immunity ended");
		GetNode<AnimatedSprite2D>("SnorpUfo").Play("default");
	}
}