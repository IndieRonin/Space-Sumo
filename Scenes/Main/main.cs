using EventCallback;
using Godot;
using System;

public partial class main : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartMatchEvent.RegisterListener(OnStartMatchEvent);
	}

	private void OnStartMatchEvent(StartMatchEvent sme)
	{
		//Load the scenes for the game, the players and the ring
	}
}
