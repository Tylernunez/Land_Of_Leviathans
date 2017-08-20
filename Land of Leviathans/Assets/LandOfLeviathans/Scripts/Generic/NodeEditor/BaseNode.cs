using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class BaseNode : ScriptableObject{

    public Rect windowRect;

    public bool hasInputs = false;

    public string windowTitle = "";

    public virtual void DrawWindow()
    {
        windowTitle = EditorGUILayout.TextField("Title", windowTitle);

    }
    //Draws connections windows
    public abstract void DrawCurves();
    //called when window is clicked during input
    public virtual void SetInput(BaseInputNode input, Vector2 clickPos)
    {

    }
    public virtual void NodeDeleted(BaseNode node)
    {

    }
    public virtual BaseInputNode ClickedOnInput(Vector2 pos)
    {
        return null;
    }
    public abstract void Tick(float deltaTime);

}
