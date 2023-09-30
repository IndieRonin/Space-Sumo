using Godot;
using System;

public partial class Exit : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ButtonUp += () => OnButtonUp();

	}

	private void OnButtonUp()
	{
		GetTree().Quit();
	}
}
