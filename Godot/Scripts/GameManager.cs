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
    // private int score = 0;
    [Export]
    public int NumWaves;
    public static List<WaveData> Waves = new List<WaveData>();

    public static int SCREEN_WIDTH = 1920;
    public static int SCREEN_HEIGHT = 1080;

    public static int currentLevel = 0; // 0 for pre-game/menu, use 1-? for actual levels

    public static int score = 0;
    public static int highScore = 0;
    public static bool scoreLocked = false;


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
        score = 0;
        LoadHighScore();
    }

    public override void _Process(float delta)
    {
        if (scoreLocked)
        {
            GetTree().ReloadCurrentScene();
            SaveIfHighScore();
            scoreLocked = false;
        }
    }

    public void LoadHighScore()
    {
        var scoreFile = new File();
        if (!scoreFile.FileExists("user://highscores.txt"))
        {
            return;
        }
        scoreFile.Open("user://highscores.txt", File.ModeFlags.Read);

        // var data = (Godot.Collections.Dictionary<string, int>)JSON.Parse(scoreFile.GetLine()).Result;
        try
        {
            var data = (Godot.Collections.Dictionary<string, int>)JSON.Parse(scoreFile.GetAsText()).Result;
            highScore = data["high_score"];
        }
        catch
        {
            // highScore = 0;
        }
        scoreFile.Close();
    }

    public void SaveIfHighScore()
    {
        highScore = Mathf.Max(highScore, score);
        Dictionary<string, int> saveDict = new Dictionary<string, int>();
        saveDict.Add("high_score", highScore);

        File scoreFile = new File();
        scoreFile.Open("user://highscores.txt", File.ModeFlags.Write);

        scoreFile.StoreLine(JSON.Print(saveDict));
        scoreFile.Close();
    }
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        // score++;
        ((RichTextLabel)GetNode("./ScoreText")).Text = "SCORE: " + score + " | HIGH SCORE: " + highScore;
    }
}
