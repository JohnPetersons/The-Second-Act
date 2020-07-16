using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class CollisionDistanceFixer : GameEventListener
{
    public double previousX, previousX2;
    public override void Begin() {
        base.Begin();
        previousX = previousX2 = this.gameObject.transform.position.x;
    }

    public override void Tick() {
        base.Tick();
        previousX2 = previousX;
        previousX = this.gameObject.transform.position.x;
    }

    public double GetVelocity() {
        if (this.previousX == this.gameObject.transform.position.x) {
            return this.gameObject.transform.position.x - this.previousX2;
        } else {
            return this.gameObject.transform.position.x - this.previousX;
        }
    }

    public override void HandleGameEvent(GameEvent gameEvent) {
        base.Tick();
        if (gameEvent.GetName().Equals("collision")) {
            GameObject go = gameEvent.GetGameData<GameObject>();
            CollisionDistanceFixer fixer = go.GetComponent<CollisionDistanceFixer>();
            if (fixer != null) {
                double total = Math.Abs(fixer.GetVelocity()) + Math.Abs(this.GetVelocity());
                float distance = 1 - Vector3.Distance(this.transform.position, go.transform.position);
                Debug.Log("distance: " + distance);
                new TypedGameEvent<double>(this.GetListenerId(), "fixCollision", -1 * distance * ((this.GetVelocity() / total)));
            }
        } else if (gameEvent.GetName().Equals("fixCollision")) {
            Debug.Log(gameEvent.GetGameData<double>());
            this.transform.Translate(new Vector3((float)gameEvent.GetGameData<double>(), 0, 0));
        }
    }
}
