using EventCallback;
using Godot;
using System;

public partial class BlackoutFadeInOut : ColorRect
{
	bool fadingIn = false;
	bool fadingOut = false;

	public override void _Ready()
	{
		ShowBlackoutEvent.RegisterListener(OnShowBlackoutEvent);
		//Listen for ContinueBlackout event
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (fadingIn)
		{
			Color = new Color(0, 0, 0, Mathf.Lerp(Color.A, 1, .01f));
			if (Color.A > .95)
			{
				Color = new Color(0, 0, 0, 1);
				fadingIn = false;

				//send WaitforBlackoutEvent  
			}
		}
		if (fadingOut)
		{
			Color = new Color(0, 0, 0, Mathf.Lerp(Color.A, 0, .01f));
			if (Color.A < .05)
			{
				Color = new Color(0, 0, 0, 0);
				fadingOut = false;
				((Control)GetParent()).Visible = false;
			}
		}
	}
	private void OnShowBlackoutEvent(ShowBlackoutEvent sbe)
	{
		fadingIn = true;
	}

	private void OnContinueBlackoutEvent(ContinueBlackoutEvent cbe)
	{
		fadingOut = true;
	}



	Left and error here for myself//Continue her wit hte fade in and out have loding events WaitforBlackoutEvent to hide and load while everything is black
}
