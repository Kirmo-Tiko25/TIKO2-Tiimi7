using Godot;
using System;

public partial class BlackHole : RigidBody2D
{
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		Spawner.noDistract = true;

		QueueFree(); //this 'frees', or deletes, the node at the end of the frame.
	}
}
