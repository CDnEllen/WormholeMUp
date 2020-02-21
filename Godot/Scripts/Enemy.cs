using Godot;
using System;

public class Enemy : PortalableObject
{
    [Export]
    public NodePath BulletSpawnPoint;
    [Export]
    public int ShootInterval;
    int shootTimer = 0;
    public bool Turned;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
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

            // Bullet Spawning
            var bulletScene = (PackedScene)ResourceLoader.Load("res://Scenes/Bullet.tscn");
            Bullet instance = (Bullet)bulletScene.Instance();
            GetNode("/root/Node2D/Bullets").AddChild(instance);
            instance.GlobalPosition = ((Node2D)GetNode(BulletSpawnPoint)).GlobalPosition;
            instance.GlobalRotation = GlobalRotation;
            instance.IsEnemyBullet = !Turned;
        }
    }
}
