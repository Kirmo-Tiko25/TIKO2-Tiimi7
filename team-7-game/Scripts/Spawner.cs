using Godot;
using System;

public partial class Spawner : Node
{
	[Export] private int Difficulty = 1;
	[Export]
	public PackedScene SmallAsteroidScene { get; set; }
	[Export]
	public PackedScene BigAsteroidScene { get; set; }
	[Export]
	public PackedScene CometScene { get; set; }

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
		GetNode<Timer>("DistracTimer").Start();

		// for Logging.
		GD.Print("Start Timer timeout: Started ObjectTimer and DistracTimer");
	}

	int objectsSpawned = 0;
	int timerTick = 0;
	int dangerLevel = 1;

	// After each timer tick spawn a new child object and fling it in random direction.
	private void OnObjectTimerTimeout()
	{
		timerTick++;
		if ((timerTick - dangerLevel) >= objectsSpawned)
		{
			if (GD.RandRange(1, (Difficulty - dangerLevel + objectsSpawned)) > 5)
			{
				SpawnAsteroidBig();
				dangerLevel++;
			}
			else
			{
				SpawnAsteroidSmall();
			}
			objectsSpawned++;

			// for Logging.
			GD.Print("Objects spawned: " + objectsSpawned);
		}
	}

	private void SpawnAsteroidSmall()
	{
		// Create a new instance of the Asteroid scene.
		Asteroid asteroidS = SmallAsteroidScene.Instantiate<Asteroid>();

		// Choose a random location on Path2D.
		var SpawnLocation = GetNode<PathFollow2D>("ObjectPath/ObjectSpawnLocation");
		SpawnLocation.ProgressRatio = GD.Randf();

		// Set the Object's direction perpendicular to the path direction.
		float direction = SpawnLocation.Rotation + Mathf.Pi / 2;

		// Set the Object's position to a random location.
		asteroidS.Position = SpawnLocation.Position;

		// Add some randomness to the direction.
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		asteroidS.Rotation = direction;

		// Choose the velocity.
		var velocity = new Vector2((float)GD.RandRange(200.0, 400.0), 0);
		asteroidS.LinearVelocity = velocity.Rotated(direction);

		// Spawn the mob by adding it to the Main scene.
		AddChild(asteroidS);
	}
	private void SpawnAsteroidBig()
	{
		// Create a new instance of the Asteroid scene.
		Asteroid asteroidB = BigAsteroidScene.Instantiate<Asteroid>();

		// Choose a random location on Path2D.
		var SpawnLocation = GetNode<PathFollow2D>("ObjectPath/ObjectSpawnLocation");
		SpawnLocation.ProgressRatio = GD.Randf();

		// Set the Object's direction perpendicular to the path direction.
		float direction = SpawnLocation.Rotation + Mathf.Pi / 2;

		// Set the Object's position to a random location.
		asteroidB.Position = SpawnLocation.Position;

		// Add some randomness to the direction.
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		asteroidB.Rotation = direction;

		// Choose the velocity.
		var velocity = new Vector2((float)GD.RandRange(50.0, 250.0), 0);
		asteroidB.LinearVelocity = velocity.Rotated(direction);

		// Spawn the mob by adding it to the Main scene.
		AddChild(asteroidB);
	}

	private void OnDistracTimerTimeout()
	{
		// Create a new instance of the comet scene.
		Comet comet = CometScene.Instantiate<Comet>();

		// Choose a random location on Path2D.
		var SpawnLocation = GetNode<PathFollow2D>("LeftPath/LeftSpawnLocation");
		SpawnLocation.ProgressRatio = GD.Randf();

		// Set the Object's direction perpendicular to the path direction.
		float direction = SpawnLocation.Rotation + Mathf.Pi / 2;

		// Set the Object's position to a random location.
		comet.Position = SpawnLocation.Position;

		// Add some randomness to the direction.
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		comet.Rotation = direction;

		// Choose the velocity.
		var velocity = new Vector2((float)GD.RandRange(100.0, 200.0), 0);
		comet.LinearVelocity = velocity.Rotated(direction);

		// Spawn the mob by adding it to the Main scene.
		AddChild(comet);
	}
}

