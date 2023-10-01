using Godot;
using System;

public partial class BoxSpawner : Node2D
{
	[Export] PackedScene goodBoxScene;
	[Export] PackedScene badBoxScene;
	[Export] PackedScene okBoxScene;
	[Export] public float Radius = 300.0f; // Initial radius value, you can adjust this in the editor.
	private Random random = new();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for (int i = 0; i < 50; i++)
		{
			SpawnObjectInCircle();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	//Call this function to spawn an object within the circle.
	public void SpawnObjectInCircle()
	{
		// Calculate a random angle within the circle.
		float angle = (float)random.NextDouble() * Mathf.Pi * 2;

		// Calculate a random position within the circle based on the angle and radius.
		Vector2 spawnPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Radius;

		Node2D badBox = (Node2D)badBoxScene.Instantiate();
		badBox.GlobalPosition = GlobalPosition + spawnPosition;
		AddChild(badBox);
	}
}
