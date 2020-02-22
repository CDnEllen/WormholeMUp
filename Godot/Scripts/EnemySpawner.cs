using Godot;
using System;

public class EnemySpawner : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    PackedScene[] enemyScenes;
    public int enemyTypeCount = 0; // number of enemy types

    private int gameTimer = 0;
    static private Random random = new Random();
    private Vector2 defaultEnemyAimTarget = new Vector2(GameManager.SCREEN_WIDTH * 0.25f, GameManager.SCREEN_HEIGHT * 0.5f);

    [Export]
    private float spawnSafetyMargin = Mathf.Max(GameManager.SCREEN_HEIGHT, GameManager.SCREEN_WIDTH) * 0.1f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        enemyScenes = new PackedScene[enemyTypeCount];
        for (int i = 0; i < enemyTypeCount; i++)
        {
            enemyScenes[i] = (PackedScene)ResourceLoader.Load("res://Scenes/Enemy" + i + ".tscn");
        }

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public override void _PhysicsProcess(float delta)
    {
        int spawn = ShouldSpawnEnemy();
        if (spawn > -1)
        {
            SpawnEnemy(0, SpawnPosition.CenterRight);
        }
    }

    private int ShouldSpawnEnemy()
    {
        // add logic for which ehenmy should spawn and when; use -1 for when enemies should not spawn

        return -1;
    }

    private void SpawnEnemy(int enemyType, SpawnPosition position, float rotation = 9001f) // random magic number (yes, I know that's bad) to indicate the rotation should be taken from the SpawnPosition options
    {
        // spawn enemy as node
        Enemy instance = (Enemy)enemyScenes[enemyType].Instance();
        GetNode("/root/Node2D/Enemies").AddChild(instance);

        // position enemy
        Vector2 enemyOffset = new Vector2(instance.EnemySize.x * 0.5f, instance.EnemySize.y * 0.5f);
        Vector2 actualPos;

        switch (position)
        {
            case SpawnPosition.Center:
                actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.5f, GameManager.SCREEN_HEIGHT * 0.5f);
                break;
            case SpawnPosition.TopRight:
                actualPos = new Vector2(GameManager.SCREEN_WIDTH + spawnSafetyMargin + enemyOffset.x, 0 - spawnSafetyMargin - enemyOffset.y);
                break;
            case SpawnPosition.CenterRight:
                actualPos = new Vector2(GameManager.SCREEN_WIDTH + spawnSafetyMargin + enemyOffset.x, GameManager.SCREEN_HEIGHT * 0.5f);
                break;
            case SpawnPosition.BottomRight:
                actualPos = new Vector2(GameManager.SCREEN_WIDTH + spawnSafetyMargin + enemyOffset.x, GameManager.SCREEN_HEIGHT - spawnSafetyMargin - enemyOffset.y);
                break;
            case SpawnPosition.BottomCenterRight:
                actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.75f, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                break;
            case SpawnPosition.BottomCenterMiddle:
                actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.5f, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                break;
            case SpawnPosition.BottomCenterLeft:
                actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.25f, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                break;
            case SpawnPosition.BottomLeft:
                actualPos = new Vector2(0 - spawnSafetyMargin - enemyOffset.x, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                break;
            case SpawnPosition.CenterLeft:
                actualPos = new Vector2(0 - spawnSafetyMargin - enemyOffset.x, GameManager.SCREEN_HEIGHT * 0.5f);
                break;
            case SpawnPosition.TopLeft:
                actualPos = new Vector2(0 - spawnSafetyMargin - enemyOffset.x, 0 + spawnSafetyMargin + enemyOffset.y + enemyOffset.y);
                break;
            case SpawnPosition.TopCenterLeft:
                actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.25f, 0 - spawnSafetyMargin - enemyOffset.y - enemyOffset.y);
                break;
            case SpawnPosition.TopCenterMiddle:
                actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.5f, 0 - spawnSafetyMargin - enemyOffset.y - enemyOffset.y);
                break;
            case SpawnPosition.TopCenterRight:
                actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.75f, 0 - spawnSafetyMargin - enemyOffset.y - enemyOffset.y);
                break;
            case SpawnPosition.AnyRight:
                switch (random.Next(3))
                {
                    case 1:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH + spawnSafetyMargin + enemyOffset.x, 0 - spawnSafetyMargin - enemyOffset.y);
                        break;
                    case 2:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH + spawnSafetyMargin + enemyOffset.x, GameManager.SCREEN_HEIGHT * 0.5f);
                        break;
                    case 0:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH + spawnSafetyMargin + enemyOffset.x, GameManager.SCREEN_HEIGHT - spawnSafetyMargin - enemyOffset.y);
                        break;
                    default:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.5f, GameManager.SCREEN_HEIGHT * 0.5f);
                        break;
                }
                break;
            case SpawnPosition.AnyBottom:
                switch (random.Next(5))
                {
                    case 1:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH + spawnSafetyMargin + enemyOffset.x, GameManager.SCREEN_HEIGHT - spawnSafetyMargin - enemyOffset.y);
                        break;
                    case 2:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.75f, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                        break;
                    case 3:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.5f, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                        break;
                    case 4:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.25f, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                        break;
                    case 0:
                        actualPos = new Vector2(0 - spawnSafetyMargin - enemyOffset.x, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                        break;
                    default:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.5f, GameManager.SCREEN_HEIGHT * 0.5f);
                        break;
                }
                break;
            case SpawnPosition.AnyLeft:
                switch (random.Next(3))
                {
                    case 1:
                        actualPos = new Vector2(0 - spawnSafetyMargin - enemyOffset.x, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                        break;
                    case 2:
                        actualPos = new Vector2(0 - spawnSafetyMargin - enemyOffset.x, GameManager.SCREEN_HEIGHT * 0.5f);
                        break;
                    case 0:
                        actualPos = new Vector2(0 - spawnSafetyMargin - enemyOffset.x, 0 + spawnSafetyMargin + enemyOffset.y + enemyOffset.y);
                        break;
                    default:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.5f, GameManager.SCREEN_HEIGHT * 0.5f);
                        break;
                }
                break;
            case SpawnPosition.AnyTop:
                switch (random.Next(5))
                {
                    case 1:
                        actualPos = new Vector2(0 - spawnSafetyMargin - enemyOffset.x, 0 + spawnSafetyMargin + enemyOffset.y + enemyOffset.y);
                        break;
                    case 2:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.25f, 0 - spawnSafetyMargin - enemyOffset.y - enemyOffset.y);
                        break;
                    case 3:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.5f, 0 - spawnSafetyMargin - enemyOffset.y - enemyOffset.y);
                        break;
                    case 4:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.75f, 0 - spawnSafetyMargin - enemyOffset.y - enemyOffset.y);
                        break;
                    case 0:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH + spawnSafetyMargin + enemyOffset.x, 0 - spawnSafetyMargin - enemyOffset.y);
                        break;
                    default:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.5f, GameManager.SCREEN_HEIGHT * 0.5f);
                        break;
                }
                break;
            case SpawnPosition.AnyLeftOrRight:
                switch (random.Next(6))
                {
                    case 1:
                        actualPos = new Vector2(0 - spawnSafetyMargin - enemyOffset.x, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                        break;
                    case 2:
                        actualPos = new Vector2(0 - spawnSafetyMargin - enemyOffset.x, GameManager.SCREEN_HEIGHT * 0.5f);
                        break;
                    case 3:
                        actualPos = new Vector2(0 - spawnSafetyMargin - enemyOffset.x, 0 + spawnSafetyMargin + enemyOffset.y + enemyOffset.y);
                        break;
                    case 4:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH + spawnSafetyMargin + enemyOffset.x, 0 - spawnSafetyMargin - enemyOffset.y);
                        break;
                    case 5:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH + spawnSafetyMargin + enemyOffset.x, GameManager.SCREEN_HEIGHT * 0.5f);
                        break;
                    case 0:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH + spawnSafetyMargin + enemyOffset.x, GameManager.SCREEN_HEIGHT - spawnSafetyMargin - enemyOffset.y);
                        break;
                    default:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.5f, GameManager.SCREEN_HEIGHT * 0.5f);
                        break;
                }
                break;
            case SpawnPosition.AnyTopOrBottom:
                switch (random.Next(10))
                {
                    case 1:
                        actualPos = new Vector2(0 - spawnSafetyMargin - enemyOffset.x, 0 + spawnSafetyMargin + enemyOffset.y + enemyOffset.y);
                        break;
                    case 2:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.25f, 0 - spawnSafetyMargin - enemyOffset.y - enemyOffset.y);
                        break;
                    case 3:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.5f, 0 - spawnSafetyMargin - enemyOffset.y - enemyOffset.y);
                        break;
                    case 4:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.75f, 0 - spawnSafetyMargin - enemyOffset.y - enemyOffset.y);
                        break;
                    case 5:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH + spawnSafetyMargin + enemyOffset.x, 0 - spawnSafetyMargin - enemyOffset.y);
                        break;
                        break;
                    case 6:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH + spawnSafetyMargin + enemyOffset.x, GameManager.SCREEN_HEIGHT - spawnSafetyMargin - enemyOffset.y);
                        break;
                    case 7:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.75f, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                        break;
                    case 8:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.5f, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                        break;
                    case 9:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.25f, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                        break;
                    case 0:
                        actualPos = new Vector2(0 - spawnSafetyMargin - enemyOffset.x, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                        break;
                    default:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.5f, GameManager.SCREEN_HEIGHT * 0.5f);
                        break;
                }
                break;
            case SpawnPosition.AnyButCenter:
                switch (random.Next(12))
                {
                    case 1:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH + spawnSafetyMargin + enemyOffset.x, 0 - spawnSafetyMargin - enemyOffset.y);
                        break;
                    case 2:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH + spawnSafetyMargin + enemyOffset.x, GameManager.SCREEN_HEIGHT * 0.5f);
                        break;
                    case 3:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH + spawnSafetyMargin + enemyOffset.x, GameManager.SCREEN_HEIGHT - spawnSafetyMargin - enemyOffset.y);
                        break;
                    case 4:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.75f, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                        break;
                    case 5:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.5f, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                        break;
                    case 6:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.25f, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                        break;
                    case 7:
                        actualPos = new Vector2(0 - spawnSafetyMargin - enemyOffset.x, GameManager.SCREEN_HEIGHT + spawnSafetyMargin + enemyOffset.y);
                        break;
                    case 8:
                        actualPos = new Vector2(0 - spawnSafetyMargin - enemyOffset.x, GameManager.SCREEN_HEIGHT * 0.5f);
                        break;
                    case 9:
                        actualPos = new Vector2(0 - spawnSafetyMargin - enemyOffset.x, 0 + spawnSafetyMargin + enemyOffset.y + enemyOffset.y);
                        break;
                    case 10:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.25f, 0 - spawnSafetyMargin - enemyOffset.y - enemyOffset.y);
                        break;
                    case 11:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.5f, 0 - spawnSafetyMargin - enemyOffset.y - enemyOffset.y);
                        break;
                    case 0:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.75f, 0 - spawnSafetyMargin - enemyOffset.y - enemyOffset.y);
                        break;
                    default:
                        actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.5f, GameManager.SCREEN_HEIGHT * 0.5f);
                        break;
                }
                break;
            default:
                actualPos = new Vector2(GameManager.SCREEN_WIDTH * 0.5f, GameManager.SCREEN_HEIGHT * 0.5f);
                break;
        }

        instance.GlobalPosition = actualPos;

        // instance.GlobalPosition = ((Node2D)GetNode(BulletSpawnPoint1)).GlobalPosition;

        // rotate enemy - by default, this faces the default enemy aim target, about a quarter of the way from the left of the screen and in the middile - roughly where the player mostly is
        if (rotation > 9000f)
            instance.GlobalRotationDegrees = Mathf.Atan2(defaultEnemyAimTarget.y - instance.GlobalPosition.y, defaultEnemyAimTarget.x - instance.GlobalPosition.x);
        else
            instance.GlobalRotationDegrees = rotation;

        // add to gamemanager for tracking if destroyed for score?
    }

}

public enum SpawnPosition
{
    Center, // center should only be used for special cases, with warnings
    TopRight,
    CenterRight,
    BottomRight,
    BottomCenterRight,
    BottomCenterMiddle,
    BottomCenterLeft,
    BottomLeft,
    CenterLeft,
    TopLeft,
    TopCenterLeft,
    TopCenterMiddle,
    TopCenterRight,
    AnyRight,
    AnyBottom,
    AnyLeft,
    AnyTop,
    AnyLeftOrRight,
    AnyTopOrBottom,
    AnyButCenter
}
