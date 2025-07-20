using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFixedJoystick
{
    bool IsJoystickUsed();
    int joystickFingerId { get; set; }
}
