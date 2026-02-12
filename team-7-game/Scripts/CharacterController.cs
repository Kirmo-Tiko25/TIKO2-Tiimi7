using Godot;
using System;
public partial class CharacterController : CharacterBody2D
{
	[Export] public float _speed = 50.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void GetInput()
	{
		Vector2 inputDirection = Input.GetVector("move_left", "move_right", "", "");
		Velocity = inputDirection * _speed;
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
	}

	public override void _Input(InputEvent @event)
    {
        GD.Print(@event.AsText());
	}
}
