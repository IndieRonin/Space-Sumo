using Godot;
using System;
namespace EventCallback
{
    public class ShowResultsEvent : Event<ShowResultsEvent>
    {
        public bool win = false;
        public bool wentOutOfRift = false;
    }
}