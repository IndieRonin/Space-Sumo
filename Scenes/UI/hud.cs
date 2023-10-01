using Godot;
using System;
using EventCallback;
public partial class hud : Control
{
	[Export] ProgressBar dashBar = null;
	Timer dashtimer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartDashBarEvent.RegisterListener(OnStartDashBarEvent);
		SetProcess(false);
	}

	public override void _Process(double delta)
	{
		dashBar.Value = dashtimer.TimeLeft;
		if (dashtimer.IsStopped())
		{
			SetProcess(false);
		}
	}
	private void OnStartDashBarEvent(StartDashBarEvent sdbe)
	{
		dashtimer = sdbe.DashTimer; // Set the timer to the local timer
		dashBar.MaxValue = dashtimer.WaitTime; //Set the max time
		dashBar.Value = dashtimer.WaitTime; //Set the max time
		SetProcess(true);
	}

	public override void _ExitTree()
	{
		StartDashBarEvent.UnregisterListener(OnStartDashBarEvent);

	}
}
