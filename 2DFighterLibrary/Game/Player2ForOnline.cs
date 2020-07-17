using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class Player2ForOnline : Player {
    
    public const string TAG = "player2";

    public override void Begin() {
        base.Begin();
        this.SetListenerId(Player2ForOnline.TAG);
        this.SetGamepadNumber(1);
        this.SetDirection(Player.LEFT);
    }

    public override void Tick() {
        base.Tick();
    }
}
