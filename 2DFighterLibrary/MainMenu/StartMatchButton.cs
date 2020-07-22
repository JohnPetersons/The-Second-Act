using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class StartMatchButton : GameEventListenerId
{
    public const string TAG = "startMatchButton";

    public override void Begin() {
        base.Begin();
        this.SetListenerId(StartMatchButton.TAG);
    }
}
