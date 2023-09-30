using EventCallback;
using Godot;
using System;

namespace Components
{
	[GlobalClass]
	public partial class ListenforEscKey : Node
	{
		public override void _UnhandledKeyInput(InputEvent @event)
		{
			if (@event is InputEventKey inputEventKey)
			{
				if (inputEventKey.Keycode == Key.Escape)
				{
					ShowMenuEvent sme = new();
					sme.FireEvent();
				}
			}
		}
	}
}