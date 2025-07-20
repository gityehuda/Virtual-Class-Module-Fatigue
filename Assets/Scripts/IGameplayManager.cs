using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameplayManager
{
    void ActivateWindow(WindowList window);
    void DeActivateWindow();
    bool CheckSomeWindowActive();
}
