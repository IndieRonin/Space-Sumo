using Godot;
using System;
using EventCallback;

public partial class RiftTimer : Node
{
	[Export] Timer riftTimer = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetRiftTimeEvent.RegisterListener(OnGetRiftTimeEvent);
		StartRiftTimerEvent.RegisterListener(OnStartRiftTimerEvent);

		riftTimer.Timeout += () => OnRiftTimerTimeout();
	}

	private void OnGetRiftTimeEvent(GetRiftTimeEvent grte)
	{
		grte.timeLeft = (float)riftTimer.TimeLeft;
		grte.WaitTime = (float)riftTimer.WaitTime;
	}
	private void OnStartRiftTimerEvent(StartRiftTimerEvent srte)
	{
		PlaySFXEvent psfxe = new();
		psfxe.sfx = SFXList.RiftOpen;
		psfxe.FireEvent();

		riftTimer.Start();
	}
	private void OnRiftTimerTimeout()
	{
		RiftTimerDoneEvent rtde = new();
		rtde.FireEvent();
	}

	public override void _ExitTree()
	{
		GetRiftTimeEvent.UnregisterListener(OnGetRiftTimeEvent);
		StartRiftTimerEvent.UnregisterListener(OnStartRiftTimerEvent);
	}
}

