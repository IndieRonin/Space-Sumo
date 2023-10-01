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
		ContinueBlackoutEvent.RegisterListener(OnContinueBlackoutEvent);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (fadingIn)
		{
			FadeIn();
		}
		if (fadingOut)
		{
			FadeOut();
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

	private void FadeIn()
	{
		Color = new Color(.05f, .05f, .05f, Mathf.Lerp(Color.A, 1.0f, .01f));
		if (Color.A > .95f)
		{
			Color = new Color(.05f, .05f, .05f, 1.0f);
			fadingIn = false;

			//Once th eblacout is complete we call this function to contiue the loading of things
			WaitforBlackoutEvent wfbe = new();
			wfbe.FireEvent();
		}
	}
	private void FadeOut()
	{
		Color = new Color(.05f, .05f, .05f, Mathf.Lerp(Color.A, 0.0f, .01f));
		if (Color.A < .05f)
		{
			BlackoutDoneEvent bde = new();//Send a message that the blackout is done
			bde.FireEvent();

			Color = new Color(.05f, .05f, .05f, 0.0f);
			fadingOut = false;
			((Control)GetParent()).Visible = false;
		}
	}
}
