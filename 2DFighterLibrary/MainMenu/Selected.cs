using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class Selected : GameEventListenerId
{
    public const string TAG = "selected";

    public override void Begin() {
        base.Begin();
        this.SetListenerId(Selected.TAG);
    }
}
