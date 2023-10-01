using EventCallback;
using Godot;
using System;

public partial class background : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RefreshBackgroundEvent.RegisterListener(OnRefreshBackgroundEvent);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void OnRefreshBackgroundEvent(RefreshBackgroundEvent rbe)
	{
		Position = new Vector2(GetViewportRect().Size.X / 2.0f, GetViewportRect().Size.Y / 2.0f);
	}
}
