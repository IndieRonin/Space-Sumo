using Godot;
using System;
[Tool]
public partial class RingPoly : Polygon2D
{
	// Define the number of vertices for your ring (more vertices create a smoother ring).
	[Export] int numVertices = 30;
	// Set the outer and inner radii of the ring.
	[Export] float outerRadius = 500.0f;
	[Export] float innerRadius = 490.0f;

	public override void _Ready()
	{

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
			float _angle = i * _angleIncrement;
			float x = Mathf.Cos(_angle) * innerRadius;
			float y = Mathf.Sin(_angle) * innerRadius;
			_vertices[numVertices + i] = new Vector2(x, y);
		}

		// Set the ring's vertices.
		Polygon = _vertices;
	}
}