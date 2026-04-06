using Godot;
using System;

public partial class Comet : RigidBody2D
{
	public override void _Ready()
	{
		// select one of the (1) animation types and plays it (good for later).
		//var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		//string[] asteroidTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
		//animatedSprite2D.Play(asteroidTypes[0]); //currently only plays the one.

		// here is the randomaizer version for later:
		// animatedSprite2D.Play(asteroidTypes[GD.Randi() % asteroidTypes.Length]);
	}

	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		Spawner.noDistract = true;

		QueueFree(); //this 'frees', or deletes, the node at the end of the frame.
	}


}
