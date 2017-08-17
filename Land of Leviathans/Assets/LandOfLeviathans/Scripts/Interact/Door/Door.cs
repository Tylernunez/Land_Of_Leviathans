using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Door")]
public class Door : ScriptableObject {

    new public string name = "New Door";
    public string destination;
    public string Partner;

}
