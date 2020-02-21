using Godot;
using System;

public class PortalableObject : KinematicBody2D
{
    [Export]
    protected Vector2 velocity = new Vector2();
    [Export]
    protected float shrinkTime = 50000.5f; // also growTime
    [Export]
    NodePath animationPlayerNode;
    AnimationPlayer animationPlayer;
    public bool shrinking;
    private Node2D teleportTarget;
    private NodePath originalPath;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();

        animationPlayer = (AnimationPlayer)GetNode(animationPlayerNode);
    }

    public override void _PhysicsProcess(float delta)
    {
        if (!shrinking)
        {
            KinematicCollision2D col = MoveAndCollide(velocity, true, true, true);
            Translate(velocity.Rotated(this.Rotation));
            if (col != null)
            {
                if ((this is Enemy && !((Enemy)this).Turned) ||
                    (this is Bullet && ((Bullet)this).IsEnemyBullet))
                {
                    bool isInputCapablePortal = CheckPortalType(col);
                    if (isInputCapablePortal)
                    {
                        float y = GlobalPosition.y;
                        GlobalPosition = Vector2.Zero;
                        ShrinkToNothing();
                        originalPath = GetParent().GetPath();
                        GetParent().RemoveChild(this);
                        ((Node2D)col.Collider).AddChild(this);
                        GlobalPosition = new Vector2(GlobalPosition.x, y);
                    }
                }
            }
        }

        if ((shrinking && Scale.x < 0.26f && Scale.y < 0.26f) ||
            (shrinking && animationPlayer == null)) // if shrinking is finished
        {
            Node node = GetNode(originalPath);
            GetParent().RemoveChild(this);
            node.AddChild(this);

            // teleport to other portal
            if (this is Enemy)
                ((Enemy)this).Turned = true;
            if (this is Bullet)
                ((Bullet)this).IsEnemyBullet = false;
            GlobalPosition = teleportTarget.GlobalPosition + new Vector2(0.0f, Position.y);
            Rotate((float)Math.PI);

            GrowToNormal();
        }
    }

    private bool CheckPortalType(KinematicCollision2D col)
    {
        if ((col.Collider as Node).Name == "Portal")
        {
            Portal portal = (Portal)col.Collider;

            if (portal.portalType == PortalType.Input)
            {
                teleportTarget = (Node2D)GetNode(portal.pairedPortal);
                return true; // this means the portal can be entered
            }
        }
        return false; // this means the portal cant be entered
    }

    private void ShrinkToNothing()
    {
        shrinking = true;
        animationPlayer?.Play("shrink_to_nothing");
    }

    private void GrowToNormal()
    {
        shrinking = false;
        animationPlayer?.Play("grow_to_full");
    }

}
