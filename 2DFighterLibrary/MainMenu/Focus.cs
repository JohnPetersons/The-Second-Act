using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class Focus : GameEventListenerId
{
    public const string TAG = "focus";

    public override void Begin() {
        base.Begin();
        this.SetListenerId(Focus.TAG);
    }
}
