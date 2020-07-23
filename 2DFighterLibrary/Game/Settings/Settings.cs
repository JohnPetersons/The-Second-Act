using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;
using System.IO;

public class Settings: GameStateMachine
{

    public static string TAG = "Settings";

    private static float PRECISION = 0.04f;

    private Dictionary<string, float> settingsList;
    private Dictionary<string, float> defaultSettingsList;

    public override void Begin(){
        base.Begin();

        GameSystem.SetGameData<Settings>(Settings.TAG, this);

        this.settingsList = new Dictionary<string, float>();
        this.defaultSettingsList = new Dictionary<string, float>();

        this.settingsList.Add("gameSpeed", 1.0f);
        this.settingsList.Add("charge", 1.0f);
        this.settingsList.Add("moveForward", 1.0f);
        this.settingsList.Add("moveBackward", 1.0f);
        this.settingsList.Add("quickStepForward", 1.0f);
        this.settingsList.Add("quickStepForwardTimer1", 1.0f);
        this.settingsList.Add("quickStepForwardTimer2", 1.0f);
        this.settingsList.Add("quickStepBackward", 1.0f);
        this.settingsList.Add("quickStepBackwardTimer1", 1.0f);
        this.settingsList.Add("quickStepBackwardTimer2", 1.0f);
        this.settingsList.Add("dashChargeTimer", 1.0f);
        this.settingsList.Add("dash", 1.0f);
        this.settingsList.Add("dashTimer", 1.0f);
        this.settingsList.Add("chargeRecoveryTimer", 1.0f);
        this.settingsList.Add("dashRecoveryTimer", 1.0f);
        this.settingsList.Add("collisionLoss", 1.0f);
        this.settingsList.Add("collisionWin", 1.0f);

        this.defaultSettingsList.Add("gameSpeed", 1.0f);
        this.defaultSettingsList.Add("charge", 15.0f);
        this.defaultSettingsList.Add("moveForward", 5.0f);
        this.defaultSettingsList.Add("moveBackward", -2.5f);
        this.defaultSettingsList.Add("quickStepForward", 30.0f);
        this.defaultSettingsList.Add("quickStepForwardTimer1", 0.2f);
        this.defaultSettingsList.Add("quickStepForwardTimer2", 0.8f);
        this.defaultSettingsList.Add("quickStepBackward", -15.0f);
        this.defaultSettingsList.Add("quickStepBackwardTimer1", 0.2f);
        this.defaultSettingsList.Add("quickStepBackwardTimer2", 0.8f);
        this.defaultSettingsList.Add("dashChargeTimer", 0.75f);
        this.defaultSettingsList.Add("dash", 60.0f);
        this.defaultSettingsList.Add("dashTimer", 0.15f);
        this.defaultSettingsList.Add("chargeRecoveryTimer", 0.5f);
        this.defaultSettingsList.Add("dashRecoveryTimer", 1.0f);
        this.defaultSettingsList.Add("collisionLoss", -10.0f);
        this.defaultSettingsList.Add("collisionWin", -2.5f);
        this.LoadSettings();
    }

    private void LoadSettings() {
        if (File.Exists(@"settings.txt")) {
            foreach (string line in File.ReadLines(@"settings.txt")) {
                this.settingsList[line.Substring(0, line.IndexOf(","))] = float.Parse(line.Substring(line.IndexOf(",") + 1));
            }
        }
    }

    private void SaveSettings() {
        List<string> saveStrings = new List<string>();
        foreach(string str in this.settingsList.Keys) {
            saveStrings.Add(str +"," + this.settingsList[str]);
        }
        File.Delete(@"settings.txt");
        File.WriteAllLines(@"settings.txt", saveStrings);
    }

    public float GetSetting(string str) {
        return this.defaultSettingsList[str] * this.settingsList[str];
    }

    public void SetSetting(string str, float f) {
        if (f < 1 + Settings.PRECISION && f > 1 - Settings.PRECISION) {
            f = 1;
        }
        this.settingsList[str] = f;
    }

    public override void Tick(){
        base.Tick();
    }
}
