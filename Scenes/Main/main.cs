using EventCallback;
using Godot;
using System;
using System.Threading;

public partial class main : Node2D
{
	[Export] PackedScene backgroundScene = null;
	[Export] PackedScene ringScene = null;
	[Export] PackedScene boxSpawnerScene = null;
	[Export] PackedScene playerScene = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartGameEvent.RegisterListener(OnStartGameEvent);
		CountdownDoneEvent.RegisterListener(OnCountdownDoneEvent);
		BlackoutDoneEvent.RegisterListener(OnBlackoutDoneEvent);
	}

	private void OnStartGameEvent(StartGameEvent sme)
	{
		//Load the scenes for the game, the players and the ring
		ShowHUDEvent shude = new();
		shude.FireEvent();

		Node2D background = backgroundScene.Instantiate() as Node2D;
		Node2D ring = ringScene.Instantiate() as Node2D;
		Node2D boxSpawner = boxSpawnerScene.Instantiate() as Node2D;
		Node2D player = playerScene.Instantiate() as Node2D;

		ring.GlobalPosition = new Vector2(GetViewportRect().Size.X / 2.0f, GetViewportRect().Size.Y / 2.0f);
		boxSpawner.GlobalPosition = new Vector2(GetViewportRect().Size.X / 2.0f, GetViewportRect().Size.Y / 2.0f);
		player.GlobalPosition = new Vector2(GetViewportRect().Size.X / 2.0f, GetViewportRect().Size.Y / 2.0f);

		AddChild(ring);
		AddChild(boxSpawner);
		AddChild(player);
		AddChild(background);
		//Have to move the shildren like this coz I implimented the main scene in a stupid way and don't have time to fix it properly
		MoveChild(player, 0);
		MoveChild(ring, 0);
		MoveChild(boxSpawner, 0);
		MoveChild(background, 0);

		GetTree().Paused = true;
	}
	private void OnBlackoutDoneEvent(BlackoutDoneEvent bde)
	{
		ShowCountdownEvent sce = new();
		sce.FireEvent();
	}
	private void OnCountdownDoneEvent(CountdownDoneEvent cde)
	{
		GetTree().Paused = false;
		StartShrinkEvent sse = new();
		sse.FireEvent();
	}
}
