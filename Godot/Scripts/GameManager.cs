using Godot;
using System;

public class GameManager : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    public static int SCREEN_WIDTH = 1920;
    public static int SCREEN_HEIGHT = 1080;

    public static int currentLevel = 0; // 0 for pre-game/menu, use 1-? for actual levels

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
