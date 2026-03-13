using Godot;
using System;

public partial class GameManager : Node2D
{
	public static int Points { get; private set; } = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public static void AddPoint(int amount)
	{
		Points += amount;
		GD.Print("You got a point, now you have: " + Points);
	}
}