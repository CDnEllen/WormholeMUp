using Godot;
using System;

public enum PortalType
{
    Unknown,
    Both,
    Input,
    Output
}

public class Portal : KinematicBody2D
{
    [Export]
    public PortalType portalType = PortalType.Unknown;
    [Export]
    public NodePath pairedPortal;

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
