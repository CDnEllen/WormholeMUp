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
    Animation shrinkAnimation;
    Animation growAnimation;
    private bool shrinking = false;
    private Node2D teleportTarget;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // shrinkAnimation = new Animation();
        // shrinkAnimation.AddTrack(0);
        // shrinkAnimation.Length = shrinkTime;

        // shrinkAnimation.TrackSetPath(0, (string)GetPath() + ":Scale"); // may need to do x and y separately
        // shrinkAnimation.TrackInsertKey(0, 0.0f, 1f);
        // shrinkAnimation.TrackInsertKey(0, shrinkTime, 0f);
        // shrinkAnimation.ValueTrackSetUpdateMode(0, Animation.UpdateMode.Continuous); // may need to be set to Discrete or another mode

        // growAnimation = new Animation();
        // growAnimation.AddTrack(0);
        // growAnimation.Length = shrinkTime;

        // growAnimation.TrackSetPath(0, (string)GetPath() + ":Scale"); // may need to do x and y separately
        // growAnimation.TrackInsertKey(0, 0.0f, 0f);
        // growAnimation.TrackInsertKey(0, shrinkTime, 1f);
        // growAnimation.ValueTrackSetUpdateMode(0, Animation.UpdateMode.Continuous); // may need to be set to Discrete or another mode

        // animationPlayer = new AnimationPlayer();
        // AddChild(animationPlayer);
        animationPlayer = (AnimationPlayer)GetNode(animationPlayerNode);
        // animationPlayer.AddAnimation("shrink", shrinkAnimation);
        // animationPlayer.AddAnimation("grow", growAnimation);


    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public override void _PhysicsProcess(float delta)
    {
        if (!shrinking)
        {
            KinematicCollision2D col = MoveAndCollide(velocity, true, true, true);
            Translate(velocity);
            if (col != null)
            {
                bool isInputCapablePortal = CheckPortalType(col);
                if (isInputCapablePortal)
                {
                    float y = GlobalPosition.y;
                    GlobalPosition = Vector2.Zero;
                    ShrinkToNothing();
                    GetParent().RemoveChild(this);
                    (col.Collider as Node2D).AddChild(this);
                    GlobalPosition = new Vector2(GlobalPosition.x, y);
                }
            }
        }

        if (shrinking && Scale.x < 0.26f && Scale.y < 0.26f) // if shrinking is finished
        {
            Node node = GetNode("/root/Node2D/Enemies");
            GetParent().RemoveChild(this);
            node.AddChild(this);

            // teleport to other portal
            GlobalPosition = teleportTarget.GlobalPosition;
            RotationDegrees = 180f;
            velocity.x = -velocity.x;
            velocity.y = -velocity.y;
            //velocity += ((PlayerShip)teleportTarget.GetParent()).velocity;

            // grow
            GrowToNormal();
        }
    }

    private bool CheckPortalType(KinematicCollision2D col)
    {
        if ((col.Collider as Node).Name == "Portal")
        {
            Node portalNode = (Node)col.Collider;
            Portal portal = (Portal)portalNode;
            PortalType portalType = portal.portalType;

            if (portalType == PortalType.Input)
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
        // animationPlayer.CurrentAnimation = "shrink";
        animationPlayer.Play("shrink_to_nothing");
    }

    private void GrowToNormal()
    {
        shrinking = false;
        // animationPlayer.CurrentAnimation = "grow";
        animationPlayer.Play("grow_to_full");
    }

}
