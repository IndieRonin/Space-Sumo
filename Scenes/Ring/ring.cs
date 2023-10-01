using EventCallback;
using Godot;
using System;
using System.Net.Mail;

public partial class ring : Node2D
{
	[Export] int numVertices = 120;
	// Set the outer and inner radii of the ring.
	[Export] float innerRadius = 520.0f;
	[Export] float outerRadius = 700.0f;
	[Export] CollisionPolygon2D colPoly = null;
	[Export] Area2D deathArea = null;
	[Export] Area2D insideArea = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		deathArea.BodyEntered += (Node2D body) => OnBodyEntered(body);
		AdjustCollisionPoly();
		SetRingPosition();
		RefreshRingEvent.RegisterListener(OnRefreshRingEvent);
		StartShrinkEvent.RegisterListener(OnStartShrinkEvent);
		BoxGetTargetEvent.RegisterListener(OnBoxGetTargetEvent);
		RiftTimerDoneEvent.RegisterListener(OnRiftTimerDoneEvent);
		SetProcess(false);
	}
	public override void _Process(double delta)
	{
		Scale = Scale.Slerp(new Vector2(.5f, .5f), 0.0038f * (float)delta);

		if (Scale < new Vector2(0.52f, 0.52f))
		{
			Scale = new Vector2(.5f, .5f);
			SetProcess(false);
		}
	}

	public void SetRingPosition()
	{
		// Get the main viewport
		Position = new Vector2(GetViewportRect().Size.X / 2.0f, GetViewportRect().Size.Y / 2.0f);
	}

	private void OnRefreshRingEvent(RefreshRingEvent rre)
	{
		SetRingPosition();
	}

	private void OnStartShrinkEvent(StartShrinkEvent sse)
	{
		SetProcess(true);
	}

	public void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			PlaySFXEvent psfxe = new();
			psfxe.sfx = SFXList.explosion;
			psfxe.FireEvent();
			ShowResultsEvent sre = new();
			sre.win = false;
			sre.wentOutOfRift = true;
			sre.FireEvent();
		}
	}

	private void OnBoxGetTargetEvent(BoxGetTargetEvent bgte)
	{
		bgte.TargetID = GetInstanceId();
	}

	private void OnRiftTimerDoneEvent(RiftTimerDoneEvent rtde)
	{
		bool boxInRift = false;
		Godot.Collections.Array<Node2D> bodiesCheck = insideArea.GetOverlappingBodies();
		foreach (Node2D node in bodiesCheck)
		{
			if (node.IsInGroup("Box")) boxInRift = true;//Send lose signal
		}

		if (boxInRift)
		{
			ShowResultsEvent sre = new();
			sre.win = false;
			sre.FireEvent();
		}
		else
		{
			ShowResultsEvent sre = new();
			sre.win = true;
			sre.FireEvent();
		}
	}
	public void AdjustCollisionPoly()
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
		colPoly.Polygon = _vertices;
	}

}
