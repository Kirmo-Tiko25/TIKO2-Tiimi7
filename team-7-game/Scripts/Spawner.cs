using Godot;
using System;
using System.Reflection;

public partial class Spawner : Node
{
	[Export] private int Difficulty = 3;
	[Export]
	public PackedScene SmallAsteroidScene { get; set; }
	[Export]
	public PackedScene BigAsteroidScene { get; set; }
	[Export]
	public PackedScene OrlopScene { get; set; }
	[Export]
	public PackedScene CometScene { get; set; }
	[Export]
	public PackedScene BoulderScene { get; set; }
	[Export]
	public PackedScene BHScene { get; set; }
	[Export]
	public PackedScene SatelliteScene { get; set; }
	[Export]
	public PackedScene PlanetScene { get; set; }
	public static bool noDistract { get; set; } = true;
	public static bool satellitePassed { get; set; } = false;
	public override void _Ready()
	{
		NewGame();

	}

	public void GameOver()
	{
		// stops object timer
		GetNode<Timer>("ObjectTimer").Stop();

	}

	public void NewGame()
	{

		/* Optional Player start if we want different starting positions.
			var player = GetNode<Player>("Player");
			var startPosition = GetNode<Marker2D>("StartPosition");
			player.Start(startPosition.Position);
		*/

		// start counting time, that triggers other things like spawning.
		GetNode<Timer>("StartTimer").Start();
	}

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
	int cometSpawned = 0;

	// After each timer tick spawn a new child object and fling it in random direction.
	private void OnObjectTimerTimeout()
	{

		// used for quick testing
		timerTick++;
		if ((timerTick - dangerLevel) >= objectsSpawned)
		{
			if (GD.RandRange(1, (Difficulty - dangerLevel + objectsSpawned)) > 5)
			{
				var foe = GD.RandRange(0, 2);

				if (foe == 0)
				{
					SpawnAsteroidBig();
				}
				else if (foe == 1)
				{
					SpawnBoulder();
				}
				else if (foe == 2)
				{
					SpawnOrlop();
				}

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

	public void SpawnOrlop()
	{
		// Create a new instance of the test scene.
		Orlop orlop = OrlopScene.Instantiate<Orlop>();

		// Choose a random location on Path2D.
		var SpawnLocation = GetNode<PathFollow2D>("ObjectPath/ObjectSpawnLocation");
		SpawnLocation.ProgressRatio = GD.Randf();

		// Set the Object's direction perpendicular to the path direction.
		float direction = SpawnLocation.Rotation + Mathf.Pi / 2;

		// Set the Object's position to a random location.
		orlop.Position = SpawnLocation.Position;

		// Add some randomness to the direction.
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		orlop.Rotation = direction;

		// Choose the velocity.
		var velocity = new Vector2((float)GD.RandRange(100.0, 200.0), 0);
		orlop.LinearVelocity = velocity.Rotated(direction);

		// Spawn the mob by adding it to the Main scene.
		AddChild(orlop);
	}

	private void SpawnBoulder()
	{
		// Create a new instance of the test scene.
		Asteroid boulder = BoulderScene.Instantiate<Asteroid>();

		// Choose a random location on Path2D.
		var SpawnLocation = GetNode<PathFollow2D>("ObjectPath/ObjectSpawnLocation");
		SpawnLocation.ProgressRatio = GD.Randf();

		// Set the Object's direction perpendicular to the path direction.
		float direction = SpawnLocation.Rotation + Mathf.Pi / 2;

		// Set the Object's position to a random location.
		boulder.Position = SpawnLocation.Position;

		// Add some randomness to the direction.
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		boulder.Rotation = direction;

		// Choose the velocity.
		var velocity = new Vector2((float)GD.RandRange(200.0, 400.0), 0);
		boulder.LinearVelocity = velocity.Rotated(direction);

		// Spawn the mob by adding it to the Main scene.
		AddChild(boulder);
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

		if (noDistract)
		{
			if (satellitePassed)
			{
				SpawnPlanet();
			}
			else if (cometSpawned == 0)
			{
				SpawnComet();
				cometSpawned++;
			}
			else if (cometSpawned > 0)
			{
				int i = GD.RandRange(0, 2);
				GD.Print("Spawn Number: " + i);
				if (i == 0)
				{
					SpawnBlackHole();
				}
				else if (i == 1)
				{
					SpawnSatellite();
				}
				else if (i == 2)
				{
					cometSpawned--;
				}
			}

		}
	}

	public void SpawnPlanet()
	{
		// Create a new instance of the planet scene.
		Planet planet = PlanetScene.Instantiate<Planet>();

		// Choose a random location on Path2D.
		var SpawnLocation = GetNode<PathFollow2D>("LeftPath/LeftSpawnLocation");
		SpawnLocation.ProgressRatio = GD.Randf();

		// Set the Object's position to a random location.
		planet.Position = SpawnLocation.Position;

		// Choose the velocity.
		planet.LinearVelocity = new Vector2((float)GD.RandRange(100.0, 200.0), 0);

		// Spawn it by adding it to the Main scene.
		AddChild(planet);
		noDistract = false;
	}

	private void SpawnSatellite()
	{
		// Create a new instance of the satellite scene.
		Satellite sat = SatelliteScene.Instantiate<Satellite>();

		// Choose a random location on Path2D.
		var SpawnLocation = GetNode<PathFollow2D>("LeftPath/LeftSpawnLocation");
		SpawnLocation.ProgressRatio = GD.Randf();

		// Set the Object's position to a random location.
		sat.Position = SpawnLocation.Position;

		// Choose the velocity.
		sat.LinearVelocity = new Vector2((float)GD.RandRange(100.0, 400.0), 0);

		// Spawn it by adding it to the Main scene.
		AddChild(sat);
		noDistract = false;
	}

	public void SpawnBlackHole()
	{
		// Create a new instance of the Black Hole scene.
		BlackHole succ = BHScene.Instantiate<BlackHole>();

		// Choose a random location on Path2D.
		var SpawnLocation = GetNode<PathFollow2D>("LeftPath/LeftSpawnLocation");
		SpawnLocation.ProgressRatio = GD.Randf();

		// Set the Object's position to a random location.
		succ.Position = SpawnLocation.Position;

		// Choose the velocity.
		succ.LinearVelocity = new Vector2((float)GD.RandRange(100.0, 400.0), 0);

		// Spawn it by adding it to the Main scene.
		AddChild(succ);
		noDistract = false;
	}

	private void SpawnComet()
	{
		// Create a new instance of the comet scene.
		Comet comet = CometScene.Instantiate<Comet>();

		// Choose a random location on Path2D.
		var SpawnLocation = GetNode<PathFollow2D>("LeftPath/LeftSpawnLocation");
		SpawnLocation.ProgressRatio = GD.Randf();

		// Set the Object's position to a random location.
		comet.Position = SpawnLocation.Position;

		// Choose the velocity.
		comet.LinearVelocity = new Vector2((float)GD.RandRange(300.0, 400.0), 0);

		// Spawn it by adding it to the Main scene.
		AddChild(comet);
		noDistract = false;
	}
}

