using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class ExitGameButton : GameEventListenerId
{
    public const string TAG = "exitGameButton";

    public override void Begin() {
        base.Begin();
        this.SetListenerId(ExitGameButton.TAG);
    }
}