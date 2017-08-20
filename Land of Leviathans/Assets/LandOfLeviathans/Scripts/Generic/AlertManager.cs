using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertManager : MonoBehaviour {
    public Animator animator;

    public void CloseAlert()
    {
        animator.SetBool("isOpen", false);
    }
    public void OpenAlert()
    {
        animator.SetBool("isOpen", true);
    }


}
