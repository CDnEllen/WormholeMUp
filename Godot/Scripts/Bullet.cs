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

        KinematicCollision2D col = MoveAndCollide(Vector2.Zero, true, true, true);
        if (col != null)
        {
            Node2D collidingNode = ((Node2D)col.Collider);

            if (collidingNode is PlayerShip)
                collidingNode.Free();
            else if (collidingNode is Enemy)
            {
                if (((Enemy)collidingNode).Turned && IsEnemyBullet ||
                    !((Enemy)collidingNode).Turned && !IsEnemyBullet)
                {
                    collidingNode.Free();
                }
            }
        }
    }
}
