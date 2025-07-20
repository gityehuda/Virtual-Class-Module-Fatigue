using UnityEngine;

public interface IPlayerManager
{
    GameObject character { get; set; }
    int idChair { get; set; }
    Animator animator { get; set; }
    bool isNotWalking { get; set; }
    CustomClass.CharacterGesture characterGesture { get; set; }
    void StandUp();
}
