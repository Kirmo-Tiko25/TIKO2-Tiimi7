using Godot;
using System;

public partial class Boulder : RigidBody2D
{
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree(); //this 'frees', or deletes, the node at the end of the frame.
	}
}
