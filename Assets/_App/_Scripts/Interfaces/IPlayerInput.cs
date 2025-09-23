using UnityEngine;

public interface IPlayerInput
{
    public Vector2 Move { get; set;  }
    public bool SprintHeld { get; }
    public bool ConsumeJump();
}
