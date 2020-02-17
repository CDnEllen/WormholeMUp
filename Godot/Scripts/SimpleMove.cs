using Godot;
using System;

public class SimpleMove : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    private Vector2 velocity = new Vector2(-1000, 0);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public override void _PhysicsProcess(float delta)
    {
        // velocity = MoveAndSlide(velocity);
        KinematicCollision2D c = MoveAndCollide(velocity);
        /*
        if (c != null)
        {
            Godot.Node col = (Node)c.Collider;
            if (col != null)
            {                
                if (col.GetParent() != null)
                {
                    GD.Print(col.GetParent().Name);
                }
            }            
        }*/
        
        
    }
}
