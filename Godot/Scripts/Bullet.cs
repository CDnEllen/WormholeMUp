using Godot;
using System;

public class Bullet : PortalableObject
{
    public bool IsEnemyBullet;
    [Export]
    public Color FriendlyBulletColor;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() { }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        try
        {
            foreach (Area2D area in GetOverlappingAreas())
            {
                if (area is PlayerShip && IsEnemyBullet)
                {
                    GameManager.scoreLocked = true;
                    try
                    {
                        GetTree().Root.FindNode("PlayerA", true, false).Free();
                    }
                    catch
                    {

                    }
                    try
                    {
                        GetTree().Root.FindNode("PlayerB", true, false).Free();
                    }
                    catch
                    {

                    }



                    area.Free();
                }

                else if (area is Enemy)
                {
                    if (((Enemy)area).Turned && IsEnemyBullet ||
                        !((Enemy)area).Turned && !IsEnemyBullet)
                    {
                        if (!GameManager.scoreLocked)
                            GameManager.score += 5;
                        area.Free();
                    }
                }
                else if (area is OutOfBounds)
                {
                    this.Free();
                    return;
                }
            }
        }
        catch
        {

        }

    }
}
