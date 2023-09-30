using EventCallback;
using Godot;
using System;

public partial class countdown : Label
{
	[Export] Timer countdownTimer = null;
	bool startedTimer = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		countdownTimer.Timeout += () => OnTimeout();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Visible && !startedTimer) countdownTimer.Start();
		if (countdownTimer.TimeLeft < 1)
		{
			Text = "Fight!";
		}
		else
		{
			Text = Mathf.RoundToInt(countdownTimer.TimeLeft).ToString();
		}

		startedTimer = true;
	}

	private void OnTimeout()
	{
		CountdownDoneEvent cde = new();
		cde.FireEvent();
		Visible = false; //Hide itself as it is done
	}
}
