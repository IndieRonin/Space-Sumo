using EventCallback;
using Godot;
using System;

public partial class BoxSpawner : Node2D
{
	[Export] PackedScene badBox1Scene = new();
	[Export] PackedScene badBox2Scene = new();
	[Export] PackedScene badBox3Scene = new();

	[Export] public float Radius = 1100.0f; // Initial radius value, you can adjust this in the editor.
	private Random random = new();
	private RandomNumberGenerator rng = new();

	int numOfBoxes = 0;
	int boxToSpawn = 0;
	float spawnTimer = 0;
	bool getWattime = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GetRiftTimeEvent grte = new();
		grte.FireEvent();
		if (!getWattime)
		{
			spawnTimer = grte.WaitTime - 5;
			getWattime = true;
		}
		if (grte.timeLeft < 10) return;

		if (grte.timeLeft < spawnTimer)
		{
			SpawnBoxes();
			spawnTimer = grte.timeLeft - 5;
		}
	}

	private void SpawnBoxes()
	{
		numOfBoxes = rng.RandiRange(1, 4);

		for (int i = 0; i < numOfBoxes; i++)
		{
			boxToSpawn = rng.RandiRange(1, 3);

			PackedScene boxToSpawnScene = null;
			if (boxToSpawn == 1) boxToSpawnScene = badBox1Scene;
			if (boxToSpawn == 2) boxToSpawnScene = badBox2Scene;
			if (boxToSpawn == 3) boxToSpawnScene = badBox3Scene;

			float angle = (float)random.NextDouble() * Mathf.Pi * 2;

			// Calculate a random position within the circle based on the angle and radius.
			Vector2 circleSpawnPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Radius;
			Vector2 SpawnPosition = new Vector2(GetViewportRect().Size.X / 2.0f, GetViewportRect().Size.Y / 2.0f);
			Node2D badBox = new();
			badBox = (Node2D)boxToSpawnScene.Instantiate();
			badBox.GlobalPosition = SpawnPosition + circleSpawnPosition;
			AddChild(badBox);
		}

	}
}
