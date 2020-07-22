using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class ControlsButton : GameEventListenerId
{
    public const string TAG = "controlsButton";

    public override void Begin() {
        base.Begin();
        this.SetListenerId(ControlsButton.TAG);
    }
}