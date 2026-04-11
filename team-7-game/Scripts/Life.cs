using Godot;
using System;

public partial class Life : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	private void OnBodyEntered(Node body)
	{
		if (body.IsInGroup("player"))
		{
			var tween = CreateTween();
			tween.TweenProperty(this, "scale", Vector2.Zero, 0.4f)
				 .SetEase(Tween.EaseType.In)
				 .SetTrans(Tween.TransitionType.Quad);

			tween.Finished += () => Removed();
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
