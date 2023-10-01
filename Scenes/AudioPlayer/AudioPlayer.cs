using EventCallback;
using Godot;
using System;
using System.IO;

public enum MusicList
{
	Menu,
	Game
};
public enum SFXList
{
	Dash,
	Hit,
	ButtonClick,
	RiftOpen,
	explosion
};
public partial class AudioPlayer : Node
{
	[Export] AudioStreamPlayer SFXPlayer = null;
	[Export] AudioStreamPlayer MusicPlayer = null;

	[Export] AudioStream gameMusic = null;
	[Export] AudioStream menuMusic = null;
	[Export] AudioStream dash = null;
	[Export] AudioStream hit = null;
	[Export] AudioStream buttonClick = null;
	[Export] AudioStream riftOpen = null;
	[Export] AudioStream explosion = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayMusicEvent.RegisterListener(OnPlayMusicEvent);
		PlaySFXEvent.RegisterListener(OnPlaySFXEvent);
	}

	private void OnPlayMusicEvent(PlayMusicEvent pme)
	{
		if (pme.music == MusicList.Menu)
		{
			MusicPlayer.Stream = menuMusic;
		}
		if (pme.music == MusicList.Game)
		{
			MusicPlayer.Stream = gameMusic;
		}
		MusicPlayer.Play();
	}

	private void OnPlaySFXEvent(PlaySFXEvent psfxe)
	{
		switch (psfxe.sfx)
		{
			case SFXList.Dash:
				SFXPlayer.Stream = dash;
				break;
			case SFXList.ButtonClick:
				SFXPlayer.Stream = buttonClick;
				break;
			case SFXList.Hit:
				SFXPlayer.Stream = hit;
				break;
			case SFXList.RiftOpen:
				SFXPlayer.Stream = riftOpen;
				break;
			case SFXList.explosion:
				SFXPlayer.Stream = explosion;
				break;
		}
		SFXPlayer.Play();
	}
}
