using Godot;
using System;

public enum ShipControlScheme
{
    Unknown,
    LeftStick,
    RightStick,
}

public class PlayerShip : Area2D
{
    [Export]
    private ShipControlScheme controlScheme;
    [Export]
    private float speed;

    private Vector2 inputDirection;
    public Vector2 velocity = new Vector2();

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
        if (inputDirection.Length() > 1)
            inputDirection.Normalized();
        velocity = inputDirection * speed;
        Translate(velocity);
    }

    public void CheckMovement()
    {
        inputDirection = Vector2.Zero;
        if (controlScheme == ShipControlScheme.LeftStick)
        {
            inputDirection.y -= Input.GetActionStrength("a_move_up");
            inputDirection.y += Input.GetActionStrength("a_move_down");
            inputDirection.x += Input.GetActionStrength("a_move_right");
            inputDirection.x -= Input.GetActionStrength("a_move_left");
        }
        else if (controlScheme == ShipControlScheme.RightStick)
        {
            inputDirection.y -= Input.GetActionStrength("b_move_up");
            inputDirection.y += Input.GetActionStrength("b_move_down");
            inputDirection.x += Input.GetActionStrength("b_move_right");
            inputDirection.x -= Input.GetActionStrength("b_move_left");
        }
    }
}
