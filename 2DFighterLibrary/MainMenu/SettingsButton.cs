using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class SettingsButton : GameEventListenerId
{
    public const string TAG = "settingsButton";

    public override void Begin() {
        base.Begin();
        this.SetListenerId(SettingsButton.TAG);
    }
}
