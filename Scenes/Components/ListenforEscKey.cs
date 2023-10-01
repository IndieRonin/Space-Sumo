using EventCallback;
using Godot;
using System;

namespace Components
{
	[GlobalClass]
	public partial class ListenforEscKey : Node
	{
		bool pressed = false; //Bool to prevent multiple key presses going to to event system
		public override void _UnhandledKeyInput(InputEvent @event)
		{
			if (@event is InputEventKey inputEventKey)
			{
				if (inputEventKey.Pressed)
				{
					if (inputEventKey.Keycode == Key.Escape)
					{
						if (pressed) return;
						ShowMenuEvent sme = new();
						sme.FireEvent();
						pressed = true;
					}
				}
				else
				{
					pressed = false;
				}
			}
		}
	}
}