using Godot;
using System;

public partial class Boulder : RigidBody2D
{
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		// TODO send points to score before deleting the object
		// SendPoints();
		QueueFree(); //this 'frees', or deletes, the node at the end of the frame.
	}
}
