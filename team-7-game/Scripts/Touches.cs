using Godot;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

//functionality from registered touches
public partial class Touches : Node2D
{
	//scenes saved to be placed from touches
	[Export] PackedScene touchPointScene;
	[Export] PackedScene originalTouchPointScene;

	private Dictionary<int, TouchData> touchData = new Dictionary<int, TouchData>();

	// register touches and create the scenes matching them
	public override void _Input(InputEvent @event)
	{
		// checks if event is screen touch
		if (@event is InputEventScreenTouch touch)
		{
			// Register touch and show it
			if (touch.IsPressed())
			{
				Node2D newTouchPoint = touchPointScene.Instantiate<Node2D>();
				Node2D newOriginalTouchPoint = originalTouchPointScene.Instantiate<Node2D>();

				AddChild(newTouchPoint);
				AddChild(newOriginalTouchPoint);

				// make the scenes position be the point touched
				newTouchPoint.Position = touch.Position;
				newOriginalTouchPoint.Position = touch.Position;

				// changes the text number on touchpoint to what number touch it is (0-9 in yeti?)
				newTouchPoint.GetNode<RichTextLabel>("TouchNuber").Text = touch.Index.ToString();

				// save the touch points and add them to an index
				TouchData tempTouchData = new TouchData
				{
					touchPoint = newTouchPoint,
					originalTouchPoint = newOriginalTouchPoint
				};

				touchData.Add(touch.Index, tempTouchData);
			}
			else // no longer touching (could TODO change to trigger from release event)
			{
				// free the touchpoints from the index
				touchData[touch.Index].originalTouchPoint.QueueFree();
				touchData[touch.Index].touchPoint.QueueFree();

				touchData.Remove(touch.Index);
			}
		}
		// add the scene follow touch drag event
		else if (@event is InputEventScreenDrag drag)
		{
			TouchData data = touchData[drag.Index];

			if (data == null) return;

			data.touchPoint.Position = drag.Position;
		}
	}


	private class TouchData
	{
		public Node2D touchPoint;
		public Node2D originalTouchPoint;
	}


}