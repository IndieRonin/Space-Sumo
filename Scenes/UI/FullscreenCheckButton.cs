using Godot;
using System;
using EventCallback;
public partial class FullscreenCheckButton : CheckButton
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Toggled += (bool toggled) => OnToggled(toggled);
	}

	private void OnToggled(bool toggled)
	{
		if (toggled)
		{
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.ExclusiveFullscreen);
			RefreshRingEvent rre = new();
			rre.FireEvent();
		}
		else
		{
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
		}
	}

}
