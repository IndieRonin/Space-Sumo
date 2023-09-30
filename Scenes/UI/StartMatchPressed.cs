using EventCallback;
using Godot;
using System;

public partial class StartMatchPressed : Button
{
	[Export] Control menu;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ButtonUp += () => OnButtonUp();
	}

	private void OnButtonUp()
	{
		ShowBlackoutEvent sbe = new();
		sbe.FireEvent();
		StartMatchEvent sme = new();
		sme.FireEvent();
		menu.Visible = false;
		Disabled = true;
	}
}
