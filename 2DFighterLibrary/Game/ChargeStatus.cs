using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class ChargeStatus : GameEventListener
{
    private int turnNumber;
    private int myTurnNumber;
    private bool active;
    public override void Begin() {
        base.Begin();
        this.myTurnNumber = 0;
        this.turnNumber = 0;
        this.active = false;
    }

    public override void Tick() {
        base.Tick();
        this.turnNumber++;
    }

    public void SetActive() {
        this.active = true;
        this.myTurnNumber = this.turnNumber;
    }

    public void SetInactive() {
        this.active = false;
    }

    public bool IsActive() {
        return this.active;
    }

    public int GetTurnNumber() {
        return this.myTurnNumber;
    }
}
