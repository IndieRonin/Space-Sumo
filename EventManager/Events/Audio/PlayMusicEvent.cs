using Godot;
using System;
namespace EventCallback
{
    public class PlayMusicEvent : Event<PlayMusicEvent>
    {
        //The actor being bounced
        public MusicList music;
    }
}