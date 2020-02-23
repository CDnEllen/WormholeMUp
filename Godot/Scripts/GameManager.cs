using Godot;
using System;
using System.Collections.Generic;

public class WaveData
{
    public SpawnData[] Spawns;
    public int Length;
}

public class GameManager : Node
{
    private int score = 0;
    [Export]
    private int NumWaves;
    public static List<WaveData> Waves = new List<WaveData>();

    public static int SCREEN_WIDTH = 1920;
    public static int SCREEN_HEIGHT = 1080;

    public static int currentLevel = 0; // 0 for pre-game/menu, use 1-? for actual levels

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        for (int i = 0; i < NumWaves; i++)
        {
            PackedScene scene = (PackedScene)ResourceLoader.Load("res://Scenes/Wave" + i + ".tscn");
            Node2D root = (Node2D)scene.Instance();
            this.AddChild(root);
            Waves.Add(new WaveData { Spawns = new SpawnData[root.GetChildCount()] });
            int longest = 0;
            for (int idx = 0; idx < root.GetChildCount(); idx++)
            {
                SpawnData data = (SpawnData)root.GetChild(idx);
                if (data.SpawnTime >= longest)
                    longest = data.SpawnTime;
                Waves[i].Spawns[idx] = new SpawnData(data);
            }
            Waves[i].Length = longest;
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        score++;
        ((RichTextLabel)GetNode("./ScoreText")).Text = "SCORE: " + score;
    }
}
