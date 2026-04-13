using Godot;
using System;

public partial class KillZone : Area2D
{
	public override void _Ready()
	{
		base._Ready();
		BodyEntered += HandleOverlap;
	}
	//Shrink effect when dissapears to a gravity well
	private void HandleOverlap(Node2D body)
	{
		if (!body.IsInGroup("player"))

			// Only shrink nodes that have a scale property
			if (body is Node2D node)
			{
				// Stop physics if it's a RigidBody2D
				if (node is RigidBody2D rb)
				{
					rb.LinearVelocity = Vector2.Zero;
					//rb.AngularVelocity = 0;
				}

				// Create tween ON THE SHRINKER, but animate the other object
				var tween = CreateTween();
				tween.TweenProperty(node, "scale", Vector2.Zero, 0.4f)
					 .SetEase(Tween.EaseType.In)
					 .SetTrans(Tween.TransitionType.Quad);

				tween.Finished += () => node.QueueFree();
			}
	}

}
