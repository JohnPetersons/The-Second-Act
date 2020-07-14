using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class Player1 : Player {
    
    public override void Begin() {
        base.Begin();
        this.SetPlayerNumber(1);
        this.SetDirection(Player.RIGHT);
    }

    public override void Tick() {
        base.Tick();
    }
}
