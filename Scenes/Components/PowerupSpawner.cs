using Godot;
using System;

namespace Components
{
	[GlobalClass]
	public partial class PowerupSpawner : Node
	{
		[Export]
		public float Radius = 100.0f; // Initial radius value, you can adjust this in the editor.

		[Export]
		public PackedScene ObjectToSpawn;

		private Random random = new Random();

		// Call this function to spawn an object within the circle.
		// public void SpawnObjectInCircle()
		// {
		// 	if (ObjectToSpawn == null)
		// 	{
		// 		GD.PrintErr("ObjectToSpawn is not assigned in the inspector.");
		// 		return;
		// 	}

		// 	// Calculate a random angle within the circle.
		// 	float angle = (float)random.NextDouble() * Mathf.Pi * 2;

		// 	// Calculate a random position within the circle based on the angle and radius.
		// 	Vector2 spawnPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Radius;

		// 	// Spawn the object at the calculated position.
		// 	Node2D spawnedObject = (Node2D)ObjectToSpawn.Instance();
		// 	spawnedObject.GlobalPosition = GlobalPosition + spawnPosition;
		// 	AddChild(spawnedObject);
		// }
	}
}

