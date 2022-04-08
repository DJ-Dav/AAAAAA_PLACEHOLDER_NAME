using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public float Speed = 300;
    [Export] public float Jump = 300;
    [Export] public float Gravity = -100;

    Vector2 velocity = Vector2.Zero;

    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(float delta)
    {
        float input = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        input = Mathf.Clamp(input, -1f, 1f);

        velocity.x += input * delta;
        velocity.y -= Gravity * delta;

        MoveAndSlide(velocity, Vector2.Up, floorMaxAngle: (float) Math.PI/6);
    }
}

