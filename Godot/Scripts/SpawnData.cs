using Godot;
using System;

public class SpawnData : Node2D
{
    [Export]
    public int SpawnTime = 0;
    [Export]
    public int EnemyType = 0;
    public new Vector2 Position;

    public SpawnData() { }

    public SpawnData(SpawnData data)
    {
        SpawnTime = data.SpawnTime;
        EnemyType = data.EnemyType;
        Position = data.GlobalPosition;
    }

    public void Print()
    {
        GD.Print("SpawnData: {SpawnTime: " + SpawnTime + ", EnemyType: " + EnemyType + ", SpawnPos: " + GlobalPosition + "}");
    }
}
