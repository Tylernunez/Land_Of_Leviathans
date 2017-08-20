﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class TimerNode : BaseInputNode {

    private float enabledSeconds;
    private float disabledSeconds;

    private float statusTimer = 0;

    private bool enableWait = true;

    private bool currentResult = false;

    public TimerNode()
    {
        windowTitle = "Timer Node";
    }

    public override void DrawWindow()
    {
        base.DrawWindow();

        float.TryParse(EditorGUILayout.TextField("Seconds to Enable: ", enabledSeconds.ToString()), out enabledSeconds);
        float.TryParse(EditorGUILayout.TextField("Seconds to Disable: ", disabledSeconds.ToString()), out disabledSeconds);

        string status = "Seconds to enable : " + (enabledSeconds - statusTimer);

        if (!enableWait)
        {
            status = "Seconds to disable : " + (disabledSeconds - statusTimer);

        }
        EditorGUILayout.LabelField(status);
    }
    public override void Tick(float deltaTime)
    {
        if (enableWait)
        {
            if (statusTimer < enabledSeconds)
            {
                statusTimer += deltaTime;
            }
            else
            {
                statusTimer = 0;
                enableWait = false;
                currentResult = true;
            }
        }else
        {
            if(statusTimer < disabledSeconds)
            {
                statusTimer += deltaTime;
            }else
            {
                statusTimer = 0;
                enableWait = true;
                currentResult = false;
            }
        }

        nodeResult = currentResult.ToString().ToLower();
    }
}
