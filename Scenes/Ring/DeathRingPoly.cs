using Godot;
using System;
using System.Linq;

[Tool]
public partial class DeathRingPoly : CollisionPolygon2D
{
	// Define the number of vertices for your ring (more vertices create a smoother ring).
	[Export] int numVertices = 90;
	// Set the outer and inner radii of the ring.
	[Export] float outerRadius = 500.0f;
	[Export] float innerRadius = 490.0f;
	[Export] Timer shrinkTimer = null;

	public override void _Ready()
	{
		// Get the main viewport
		((Node2D)GetParent()).Position = new Vector2(GetViewportRect().Size.X / 2.0f, GetViewportRect().Size.Y / 2.0f);
	}

	public override void _Process(double delta)
	{
		// Calculate the angle between each vertex.
		float _angleIncrement = Mathf.Tau / numVertices;

		// Initialize an array to store the ring's vertices.
		Vector2[] _vertices = new Vector2[numVertices * 2];

		// Calculate and set the vertices for the ring.
		for (int i = 0; i < numVertices; i++)
		{
			float _angle = i * _angleIncrement;
			float x = Mathf.Cos(_angle) * outerRadius;
			float y = Mathf.Sin(_angle) * outerRadius;
			_vertices[i] = new Vector2(x, y);
		}

		// Add the inner vertices to create the ring.
		for (int i = 0; i < numVertices; i++)
		{
			float _angle = (numVertices - (i + 1)) * _angleIncrement;
			float x = Mathf.Cos(_angle) * innerRadius;
			float y = Mathf.Sin(_angle) * innerRadius;
			_vertices[numVertices + i] = new Vector2(x, y);
		}
		// Set the ring's vertices.
		Polygon = _vertices;
	}


}