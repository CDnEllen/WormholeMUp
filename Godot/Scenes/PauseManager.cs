using Godot;
using System;

public class PauseManager : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private bool gamePaused;

    // Called when the node enters the scene tree for the first time.
    // public override void _Ready()
    // {

    // }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("pause"))
        {
            if (gamePaused)
            {
                gamePaused = false;
                GetTree().Paused = false;
            }
            else
            {
                gamePaused = true;
                GetTree().Paused = true;
            }
        }
    }
}
