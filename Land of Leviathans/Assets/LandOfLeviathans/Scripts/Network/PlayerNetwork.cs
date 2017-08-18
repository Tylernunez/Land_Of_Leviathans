using UnityEngine;

public class PlayerNetwork : MonoBehaviour {

    public static PlayerNetwork instance;

    public string PlayerName { get; private set; }
    private void Awake()
    {
        instance = this;

        PlayerName = "Jin" + Random.Range(1000, 9999);
    }
}
