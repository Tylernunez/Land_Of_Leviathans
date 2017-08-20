using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableCoop : Interactable {

    public bool isMultiplayer = false;
    public Text interactPortalText;
    public AlertManager alertManager;
    public void InteractPortal()
    {
        if(isMultiplayer == true){
            isMultiplayer = false;
            interactPortalText.text = "Open the Portal?";
        }
        else{
            isMultiplayer = true;
            interactPortalText.text = "Close the Portal?";
        }
        
    }

    public override void Interact()
    {
        base.Interact();
        alertManager.OpenAlert();
    }
    

}
