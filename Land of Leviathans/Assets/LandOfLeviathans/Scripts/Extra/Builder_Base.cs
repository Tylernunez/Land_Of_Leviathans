using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
public class Builder_Base : MonoBehaviour {

    public string buildingName = "Builder_Base";
    public GameObject parentGO; //the GO all instances will be added bellow
    public bool deleteLast; //deletes the last part placed

    //Directions we want to build, add more if you have to
    public bool buildNorth;
    public bool buildSouth;
    public bool buildWest;
    public bool buildEast;

    public bool rotate90;//rotates the part

    public GameObject activeWall; //the active part we are doing changes to
    
    public bool changePrefab;//changes the prefab
    public int curPrefab = 0;

    public List<GameObject> PrefabsToInstantiate = new List<GameObject>(); //Our list of prefabs we can instantiate
    public List<GameObject> previouslyBuilt = new List<GameObject>(); //what we have built till now

    public bool mirrorX;//if we want to mirror this
    bool hasCreatedMirroredObjects; //if we have mirrored our objects
    public bool updateMirrorOffset; 
    public float mirrorOffset = 2;
    public List<GameObject> MirroredObjects = new List<GameObject>(); //our list of mirrored objects  

	void Update () 
    {
        if(deleteLast)
        {
            if (previouslyBuilt.Contains(activeWall))
            {
                previouslyBuilt.Remove(activeWall);
                DestroyImmediate(activeWall);
                activeWall = previouslyBuilt[previouslyBuilt.Count - 1];
            }

            deleteLast = false;
        }


        BuildLogic();
        ChangePrefab();


        if(rotate90)
        {
            if(activeWall)
            {
                Vector3 rot = activeWall.transform.eulerAngles;
                rot.y += 90;
                activeWall.transform.rotation = Quaternion.Euler(rot);
            }
            else
            {
                Vector3 rot = this.transform.eulerAngles;
                rot.y += 90;
                this.transform.rotation = Quaternion.Euler(rot);
            }

            rotate90 = false;
        }

        MirrorLogic();
	}

    void BuildLogic()
    {
        if (buildNorth)
        {
            CallPrefabLogic(-Vector3.forward);
            buildNorth = false;
        }

        if (buildSouth)
        {
            CallPrefabLogic(Vector3.forward);
            buildSouth = false;
        }

        if (buildWest)
        {
            CallPrefabLogic(-Vector3.right);
            buildWest = false;
        }

        if (buildEast)
        {
            CallPrefabLogic(Vector3.right);
            buildEast = false;
        }
    }

    void ChangePrefab()
    {
        if (changePrefab)
        {
            if (PrefabsToInstantiate.Count - 1 > curPrefab)
            {
                curPrefab++;
            }
            else
            {
                curPrefab = 0;
            }

            Vector3 pos = activeWall.transform.position;
            if (activeWall != this.gameObject)
            {
                previouslyBuilt.Remove(activeWall);
                DestroyImmediate(activeWall);
            }
            else
            {
                Debug.Log("Can't destroy starter gameobject! Do it manually!");
                changePrefab = false;
                return;
            }

            InstantiatePrefab(pos, curPrefab);
            changePrefab = false;
        }
    }

    void MirrorLogic()
    {
        if (mirrorX)
        {
            if (previouslyBuilt.Count > 1)
            {
                if (!hasCreatedMirroredObjects)
                {
                    MirrorObjs();
                    hasCreatedMirroredObjects = true;
                }
            }
        }
        else
        {
            if (MirroredObjects.Count > 0)
            {
                if (hasCreatedMirroredObjects)
                {
                    for (int i = 0; i < MirroredObjects.Count; i++)
                    {
                        DestroyImmediate(MirroredObjects[i]);
                    }
                    MirroredObjects.Clear();

                    hasCreatedMirroredObjects = false;
                }
            }
        }

        if (updateMirrorOffset)
        {
            if (hasCreatedMirroredObjects)
            {
                UpdateMirrorOffset();
            }

            updateMirrorOffset = false;
        }
    }

    void MirrorObjs()
    {
        for(int i = 1; i < previouslyBuilt.Count; i++ )
        {
            GameObject mirrorGO = Instantiate(previouslyBuilt[i], transform.position, Quaternion.identity) as GameObject;

            mirrorGO.transform.parent = parentGO.transform;

            Vector3 pos = previouslyBuilt[i].transform.localPosition;
            pos.x = -pos.x;
            mirrorGO.transform.localPosition = pos;

            Quaternion rot = previouslyBuilt[i].transform.localRotation;
            mirrorGO.transform.localRotation = rot * Quaternion.Euler(0, 180,0);
           
            MirroredObjects.Add(mirrorGO);
            
        }

        GameObject masterMirrorGO = Instantiate(previouslyBuilt[0], transform.position, Quaternion.identity) as GameObject;
        masterMirrorGO.transform.parent = parentGO.transform;      
        Vector3 masterPos = previouslyBuilt[0].transform.localPosition;
        masterPos.x = -masterPos.x;
        masterMirrorGO.transform.localPosition = masterPos;

        Quaternion masterRot = previouslyBuilt[0].transform.localRotation;
        masterMirrorGO.transform.localRotation = masterRot * Quaternion.Euler(0,180,0);
        
        MirroredObjects.Add(masterMirrorGO);
 
        if(masterMirrorGO.GetComponent<Builder_Base>())
        {
            DestroyImmediate(masterMirrorGO.GetComponent<Builder_Base>());
        }

        UpdateMirrorOffset();
    }

    void UpdateMirrorOffset()
    {
        for (int i = 0; i < MirroredObjects.Count; i++)
        {
            Vector3 ps = MirroredObjects[i].transform.localPosition;
            ps.x += mirrorOffset;

            MirroredObjects[i].transform.localPosition = ps;
        }
    }

    void CallPrefabLogic(Vector3 direction)
    {
        Vector3 origin = Vector3.zero;

        if (activeWall)
        { origin = activeWall.transform.position; }
        else
        {
            activeWall = this.gameObject;
            origin = activeWall.transform.position;
        }

        origin += direction * 2;

        if (!parentGO)
        {
            InstantiateParentGO();
            activeWall.transform.parent = parentGO.transform;
            previouslyBuilt.Add(activeWall);
            activeWall.gameObject.name = activeWall.name + "_MASTER";
        }

        InstantiatePrefab(origin, curPrefab);
    }

    void InstantiatePrefab(Vector3 pos, int pref)
    {
        if(!parentGO)
        {
            InstantiateParentGO();
        }

        Quaternion rotation = Quaternion.identity;
        
        if(previouslyBuilt.Count > 1)
        {
            rotation = previouslyBuilt[previouslyBuilt.Count - 1].transform.rotation;
        }

        if (PrefabsToInstantiate.Count > 0)
            activeWall = Instantiate(PrefabsToInstantiate[pref], pos, rotation) as GameObject;
        else
            Debug.Log("No Prefabs on the list!");

        activeWall.transform.parent = parentGO.transform;
        previouslyBuilt.Add(activeWall);
    }

    void InstantiateParentGO()
    {
        GameObject go = new GameObject();
        parentGO = go;
        go.name = buildingName;
        go.transform.position = this.transform.position;
    }
}
