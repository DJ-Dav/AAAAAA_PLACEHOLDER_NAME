using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public float Speed = 100;
    [Export] public float Jump = 150;
    [Export] public float Gravity = 200;
    [Export] public NodePath levelPath;

    Vector2 velocity = Vector2.Zero;

    public override void _Ready()
    {

    }

    public override void _PhysicsProcess(float delta)
    {
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            velocity.y = -Jump;
        }
        velocity.y += Gravity * delta;
        getInput();
        velocity = MoveAndSlide(velocity, Vector2.Up, true);

        AssignAnimation();
    }

    private void AssignAnimation()
    {
        string anim = "idle";

        if (!IsOnFloor())
        {
            anim = "jump";
            GetChild(0).GetChild<AnimatedSprite>(0).Animation = anim;
            GetChild(0).GetChild<AnimatedSprite>(0).Frame = velocity.y > 0 ? 1 : -1;

        }
        else if (velocity.x != 0)
        {
            anim = "walk";
        }
        if (GetChild(0).GetChild<AnimatedSprite>(0).Animation != anim)
        {
            GetChild(0).GetChild<AnimatedSprite>(0).Play(anim);
        }
    }

    private void getInput()
    {
        int move_direction = (Input.IsActionPressed("move_left") ? -1 : 0) + (Input.IsActionPressed("move_right") ? 1 : 0);
        velocity.x = Mathf.Lerp(velocity.x, Speed * move_direction, getHWeight());

        if (move_direction != 0)
        {


#pragma warning disable CS0618  // Sprite.scale cannot be modified from within this class,
                                // SetScale is the only way to avoid red squiggles
            GetChild<Node2D>(0).SetScale(new Vector2(x: move_direction, 1));


#pragma warning restore CS0618
        }
    }

    private float getHWeight()
    {
        return IsOnFloor() ? 0.2f : 0.1f;
    }
}


