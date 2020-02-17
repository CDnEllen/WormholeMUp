using Godot;
using System;

public enum PlayerShipID
{
    A,
    B
}
public class PlayerShip : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    private PlayerShipID id = PlayerShipID.A;
    [Export]
    private float speed = 1000;

    private Vector2 inputDirection;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AnimationPlayer anim = GetNode<AnimationPlayer>("AnimationPlayer");
        anim.Play("bounce_scale");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        CheckMovement();
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 velocity = inputDirection.Normalized() * speed;
        velocity = MoveAndSlide(velocity);


    }

    public void CheckMovement()
    {
        inputDirection = Vector2.Zero;
        if (id == PlayerShipID.A)
        {
            if (Input.IsActionPressed("a_move_up"))
            {
                inputDirection.y -= 1;
            }
            else if (Input.IsActionPressed("a_move_down"))
            {
                inputDirection.y += 1;
            }
            if (Input.IsActionPressed("a_move_left"))
            {
                inputDirection.x -= 1;
            }
            else if (Input.IsActionPressed("a_move_right"))
            {
                inputDirection.x += 1;
            }
        }
        else if (id == PlayerShipID.B)
        {
            if (Input.IsActionPressed("b_move_up"))
            {
                inputDirection.y -= 1;
            }
            else if (Input.IsActionPressed("b_move_down"))
            {
                inputDirection.y += 1;
            }
            if (Input.IsActionPressed("b_move_left"))
            {
                inputDirection.x -= 1;
            }
            else if (Input.IsActionPressed("b_move_right"))
            {
                inputDirection.x += 1;
            }
        }
    }
}
