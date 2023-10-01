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
	[Export] PackedScene riftTimerScene = null;

	Node2D background;
	Node2D ring;
	Node2D boxSpawner;
	Node2D player;
	Node riftTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartGameEvent.RegisterListener(OnStartGameEvent);
		CountdownDoneEvent.RegisterListener(OnCountdownDoneEvent);
		BlackoutDoneEvent.RegisterListener(OnBlackoutDoneEvent);
		ShowResultsEvent.RegisterListener(OnShowResultsEvent);
	}

	private void OnStartGameEvent(StartGameEvent sme)
	{
		PlayMusicEvent pme = new();
		pme.music = MusicList.Game;
		pme.FireEvent();
		//Load the scenes for the game, the players and the ring
		ShowHUDEvent shude = new();
		shude.FireEvent();

		background = backgroundScene.Instantiate() as Node2D;
		ring = ringScene.Instantiate() as Node2D;
		boxSpawner = boxSpawnerScene.Instantiate() as Node2D;
		player = playerScene.Instantiate() as Node2D;
		riftTimer = riftTimerScene.Instantiate() as Node;

		background.GlobalPosition = new Vector2(GetViewportRect().Size.X / 2.0f, GetViewportRect().Size.Y / 2.0f);
		ring.GlobalPosition = new Vector2(GetViewportRect().Size.X / 2.0f, GetViewportRect().Size.Y / 2.0f);
		player.GlobalPosition = new Vector2(GetViewportRect().Size.X / 2.0f, GetViewportRect().Size.Y / 2.0f);

		AddChild(ring);
		AddChild(boxSpawner);
		AddChild(player);
		AddChild(background);
		AddChild(riftTimer);

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
	private void OnShowResultsEvent(ShowResultsEvent sre)
	{
		background.CallDeferred("queue_free");
		ring.CallDeferred("queue_free");
		boxSpawner.CallDeferred("queue_free");
		player.CallDeferred("queue_free");
		riftTimer.CallDeferred("queue_free");
	}

}
