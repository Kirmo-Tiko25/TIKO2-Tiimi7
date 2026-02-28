using Godot;
using System;

public partial class Spawner : Node
{
	// Don't forget to rebuild the project so the editor knows about the new export variable.

	[Export]
	public PackedScene AsteroidScene { get; set; }

	//TODO score system.
	// private int _score;

	public override void _Ready()
	{
		NewGame();
	}
	public void GameOver()
	{
		// stops object timer
		GetNode<Timer>("ObjectTimer").Stop();
		// stops score timer
		//GetNode<Timer>("ScoreTimer").Stop();
	}

	public void NewGame()
	{
		// TODO score system.
		//_score = 0;

		/* Optional Player start if we want different starting positions.
			var player = GetNode<Player>("Player");
			var startPosition = GetNode<Marker2D>("StartPosition");
			player.Start(startPosition.Position);
		*/

		// start counting time, that triggers other things like spawning.
		GetNode<Timer>("StartTimer").Start();
	}
	/* SCORE TODO
		// Score counter from time lapsed.
		private void OnScoreTimerTimeout()
		{
			_score++;
		}
	*/
	// After (2) seconds from Game Start: starts the other timers.
	private void OnStartTimerTimeout()
	{
		GetNode<Timer>("ObjectTimer").Start();
		//GetNode<Timer>("ScoreTimer").Start();

		// for Logging.
		GD.Print("Start Timer timeout: Started ObjectTimer");
	}

	// After each timer tick spawn a new child object and fling it in random direction.
	private void OnObjectTimerTimeout()
	{
		// for Logging.
		GD.Print("Object timer tick");
		// Create a new instance of the Asteroid scene.
		Asteroid asteroid = AsteroidScene.Instantiate<Asteroid>();

		// Choose a random location on Path2D.
		var SpawnLocation = GetNode<PathFollow2D>("ObjectPath/ObjectSpawnLocation");
		SpawnLocation.ProgressRatio = GD.Randf();

		// Set the Object's direction perpendicular to the path direction.
		float direction = SpawnLocation.Rotation + Mathf.Pi / 2;

		// Set the Object's position to a random location.
		asteroid.Position = SpawnLocation.Position;

		// Add some randomness to the direction.
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		asteroid.Rotation = direction;

		// Choose the velocity.
		var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
		asteroid.LinearVelocity = velocity.Rotated(direction);

		// Spawn the mob by adding it to the Main scene.
		AddChild(asteroid);
	}
}

