using Godot;
using System;
namespace EventCallback
{
    public class PlaySFXEvent : Event<PlaySFXEvent>
    {
        //The actor being bounced
        public SFXList sfx;
    }
}