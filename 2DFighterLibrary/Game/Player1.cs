using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class Player1 : Player {
    
    public const string TAG = "player1";

    public override void Begin() {
        base.Begin();
        this.SetListenerId(Player1.TAG);
        this.SetGamepadNumber(1);
        this.SetDirection(Player.RIGHT);
    }

    public override void Tick() {
        base.Tick();
    }
}
