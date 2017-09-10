using UnityEngine;
using System.Collections;
//We need all this namespaces
using System; 
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSystem : MonoBehaviour {

    public Transform Player; //The transform for our player, add anything else you can take variables to save

    void Awake() 
    {
        //We want to keep our manager in every scene
       DontDestroyOnLoad(this.transform);
       
        //But if there is another game manager in the scene
       GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
       
        //And it's not this game manager, then destroy it
       if(gm != this.gameObject)
       {
           Destroy(gm);
       }
    }

    public void SaveState()
    {
        //We need to create a new binary formatter
        BinaryFormatter bf = new BinaryFormatter();
        //And create a new file to save our data
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerData.dat");
        //we create a new instance of our Player data class
        PlayerData data = new PlayerData();

        //And then pass all the values we want to save in the player data instance...
        data.posx = Player.transform.position.x;
        data.posy = Player.transform.position.y;
        data.posz = Player.transform.position.z;
        data.level = Application.loadedLevel;

        data.rotx = Player.transform.eulerAngles.x;
        data.roty = Player.transform.eulerAngles.y;
        data.rotz = Player.transform.eulerAngles.z;

        //..so that we serialize it and save it
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadState()
    {
        //When we load we first need to check if there is data to load
      if(File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
      {
          //same as above
          BinaryFormatter bf = new BinaryFormatter();
          //but instead we open the file
          FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);
         //and deserialize it, remember to cast it otherwise Unity won't know what type of object this is
          PlayerData data = (PlayerData) bf.Deserialize(file);
          file.Close();

          //Then simply take the values we stored and do changes to our objects
          Application.LoadLevel(data.level);
          Player.transform.position = new Vector3(data.posx, data.posy, data.posz);
          Player.transform.rotation = Quaternion.Euler(data.rotx, data.roty, data.rotz);
      }
    }

}

//We do not want to serialize a class that derives from monobehaviour to avoid weird behaviours
[Serializable]
class PlayerData
{
    public float posx;
    public float posy;
    public float posz;
    
    public float rotx;
    public float roty;
    public float rotz;

    public int weaponType;
    public int level;
}

/*Of course this system is only a single state save,
 *If you want more save states you can simple make a list
 *and on your data path pass the index of the list
 */

