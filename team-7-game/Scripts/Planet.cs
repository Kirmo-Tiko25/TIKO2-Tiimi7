using Godot;
using System;

public partial class Planet : Node2D
{
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		Spawner.satellitePassed = false;
		Spawner.noDistract = true;
		Removed();
	}

	public void Removed()
	{
		QueueFree(); //this 'frees', or deletes, the node at the end of the frame.
	}
}
