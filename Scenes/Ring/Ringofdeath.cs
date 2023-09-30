using Godot;
using System;

public partial class Ringofdeath : Area2D
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += (Node2D body) => OnBodyEntered(body);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnBodyEntered(Node2D body)
	{
		GD.Print("Death zone entered");
	}
}
