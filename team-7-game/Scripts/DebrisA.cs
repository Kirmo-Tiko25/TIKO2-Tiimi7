using Godot;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;

public partial class DebrisA : RigidBody2D
{
	[Export] public float StartScale = 0.1f;
	[Export] public float EndScale = 2.8f;
	[Export] public float GrowingTime = 6f;
	public override void _Ready()
	{
		// Start from nothing
		Scale = new Vector2(StartScale, StartScale);
		// grow to full size
		var grow = CreateTween();
		grow.TweenProperty(this, "scale", new Vector2(EndScale, EndScale), GrowingTime)
		.SetEase(Tween.EaseType.Out)
		.SetTrans(Tween.TransitionType.Back);
		// TODO change opacity to 0%

		var fades = CreateTween();
		fades.TweenProperty(this, "modulate:a", 0, GrowingTime);
		fades.Finished += () => QueueFree();

	}

	public void Launch(Vector2 direction, float force)
	{
		ApplyImpulse(direction * force);
		AngularVelocity = GD.RandRange(-1, 1);
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
