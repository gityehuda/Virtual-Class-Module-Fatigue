using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CustomClass;

public interface IStartUpManager
{
    void SetLoginManualPageActive(bool active);
    void SetLoginManualButtonActive(bool active);
    string GetInputEmail();
    void LoginManualWaiting();
    void LoginManualFail();
    void SetTextInScreen(string text);
    void LoginManualSuccess(ResponseLoginAAD dataUser);
    void SetLoginPageActive(bool active);
    void LoginMicrosoftWaiting();
    void LoginMicrosoftFail(string text);
    void LoginMicrosoftSuccess(ResponseLoginAAD dataUsr);
    void LoginMicrosoftByPass();
}
