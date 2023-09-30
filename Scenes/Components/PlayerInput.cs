using Godot;
using System;

namespace Components
{
	[GlobalClass]
	public partial class PlayerInput : ExternalInput
	{
		[Export] Movement move;
		[Export] Dash dash;
		float x, y;
		Vector2 target;

		public override void _Input(InputEvent @event)
		{
			target = (GetParent() as Node2D).GetGlobalMousePosition();

			if (Input.IsKeyPressed(Key.W))
			{
				y = -1;
			}
			if (Input.IsKeyPressed(Key.S))
			{
				y = 1;
			}
			if (Input.IsKeyPressed(Key.A))
			{
				x = -1;
			}
			if (Input.IsKeyPressed(Key.D))
			{
				x = 1;
			}
			if (Input.IsMouseButtonPressed(MouseButton.Left))
			{
				dash?.GetInput(true);
			}
		}
		public override void _Process(double delta)
		{
			move?.GetInput(x, y, target);
		}
	}
}
