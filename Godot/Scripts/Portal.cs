using Godot;
using System;

public enum PortalType
{
    Unknown,
    Both,
    Input,
    Output
}

public class Portal : Area2D
{
    [Export]
    public PortalType portalType = PortalType.Unknown;
    [Export]
    public NodePath pairedPortal;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() { }
}
