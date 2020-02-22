using Godot;
using System;

public class Enemy : PortalableObject
{
    [Export]
    public NodePath BulletSpawnPoint1;
    [Export]
    public NodePath BulletSpawnPoint2;
    [Export]
    public int ShootInterval;
    [Export]
    public Vector2 EnemySize;
    int shootTimer = 0;
    public bool Turned;
    PackedScene bulletScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        bulletScene = (PackedScene)ResourceLoader.Load("res://Scenes/Bullet.tscn");
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        if (shrinking)
            return;

        shootTimer++;
        if (shootTimer >= ShootInterval)
        {
            shootTimer = 0;

            for (int i = 0; i < 3; i++)
            {
                // Bullet Spawning
                Bullet instance = (Bullet)bulletScene.Instance();
                GetNode("/root/Node2D/Bullets").AddChild(instance);

                if (i % 2 == 0)
                {
                    instance.GlobalPosition = ((Node2D)GetNode(BulletSpawnPoint1)).GlobalPosition;
                }
                if (i % 2 == 1)
                {
                    instance.GlobalPosition = ((Node2D)GetNode(BulletSpawnPoint2)).GlobalPosition;
                }

                instance.GlobalRotation = GlobalRotation;
                instance.IsEnemyBullet = !Turned;
                if (Turned)
                    ((Sprite)instance.GetNode("./Sprite")).SelfModulate = instance.FriendlyBulletColor;
            }
        }
    }
}
