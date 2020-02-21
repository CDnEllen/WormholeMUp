using Godot;
using System;

public class Bullet : PortalableObject
{
    public bool IsEnemyBullet;

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
        base._PhysicsProcess(delta);

        foreach(Area2D area in GetOverlappingAreas())
        {
            if (area is PlayerShip)
                area.Free();
            else if (area is Enemy)
            {
                if (((Enemy)area).Turned && IsEnemyBullet ||
                    !((Enemy)area).Turned && !IsEnemyBullet)
                {
                    area.Free();
                }
            }
        }
    }
}
