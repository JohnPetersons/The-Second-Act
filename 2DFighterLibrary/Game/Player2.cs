using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class Player2 : Player {
    
    public const string TAG = "player2";

    public override void Begin() {
        base.Begin();
        this.SetListenerId(Player2.TAG);
        this.SetGamepadNumber(2);
        this.SetDirection(Player.LEFT);
    }

    public override void Tick() {
        base.Tick();
    }
}
