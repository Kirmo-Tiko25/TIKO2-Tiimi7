using Godot;
using System;

public partial class KillZone : Area2D
{
	public override void _Ready()
	{
		base._Ready();
		BodyEntered += HandleOverlap;
	}
	private void HandleOverlap(Node2D body)
	{
		if (body.IsInGroup("Hazards"))
		{
			body.QueueFree();
		}



	}



}
