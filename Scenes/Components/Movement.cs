using Godot;
using System;
using EventCallback;

namespace Components
{
	[GlobalClass]
	public partial class Movement : Node
	{
		[Export] CharacterBody2D body2D;
		[Export] public float speed = 500f;
		[Export] public float friction = .97f;
		private Vector2 AccelMod = Vector2.One;
		private Vector2 velocity = Vector2.Zero;
		private Vector2 acceleration = Vector2.Zero;
		float x = 0, y = 0;
		Vector2 target;
		public override void _PhysicsProcess(double delta)
		{
			BounceBackEvent.RegisterListener(OnBounceBackEvent);

			body2D.LookAt(target);
			acceleration = new Vector2(x, y) * (float)delta;
			velocity += acceleration * speed + AccelMod;
			MoveAndBounceOffTheAsteroids(velocity, (float)delta);
			velocity = velocity.Clamp(new Vector2(-900, -900), new Vector2(900, 900));
			velocity *= friction;
			acceleration = Vector2.Zero;
			AccelMod = Vector2.Zero;
		}

		private void MoveAndBounceOffTheAsteroids(Vector2 velocity, float delta)
		{
			KinematicCollision2D collision = body2D.MoveAndCollide(velocity * delta, false);
			if (collision != null)
			{
				GD.Print(GetParent().Name + " just collided with " + ((Node2D)InstanceFromId(collision.GetColliderId())).Name);
				this.velocity = this.velocity.Bounce(collision.GetNormal()) * 0.5f;
				Vector2 reflect = collision.GetRemainder().Bounce(collision.GetNormal());
				body2D.MoveAndCollide(reflect);

				BounceBackEvent bbe = new()
				{
					callerClass = "Movement: MoveAndBounceOffTheAsteroids()",
					ID = collision.GetColliderId(),
					BounceForce = -collision.GetNormal() * 5.0f
				};
				bbe.FireEvent();
			}
		}

		public void ModAccel(Vector2 _accellMod)
		{
			AccelMod = _accellMod;
		}
		public void GetInput(float _x, float _y, Vector2 _target)
		{
			x = _x;
			y = _y;
			target = _target;
		}

		private void OnBounceBackEvent(BounceBackEvent bbe)
		{
			if (bbe.ID != GetParent().GetInstanceId()) return;
			AccelMod += bbe.BounceForce;
		}
	}
}
