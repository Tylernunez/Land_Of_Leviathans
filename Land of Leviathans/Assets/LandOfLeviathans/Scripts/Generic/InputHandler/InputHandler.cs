using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    float vertical;
    float horizontal;
    bool b_input;
    bool a_input;
    bool x_input;
    bool y_input;

    bool rb_input;
    float rt_input;
    bool lb_input;
    float lt_input;

    StateManager states;
    CameraManager camManager;

    float delta;

	// Use this for initialization
	void Start () {
        states = GetComponent<StateManager>();
        states.Init();

        camManager = CameraManager.singleton;
        camManager.Init(this.transform);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        delta = Time.fixedDeltaTime;
        GetInput();
        UpdateStates();
        states.FixedTick(delta);
        camManager.Tick(delta);

    }

    private void Update()
    {
        delta = Time.deltaTime;
        states.Tick(delta);
        
    }

    void GetInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        b_input = Input.GetButton("b_input");
    }

    void UpdateStates()
    {
        states.vertical = vertical;
        states.horizontal = horizontal;

        Vector3 v = vertical * camManager.transform.forward;
        Vector3 h = horizontal * camManager.transform.right;
        states.moveDir = (v + h).normalized;
        float m = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        states.moveAmount = Mathf.Clamp01(m);

        if(b_input)
        {
            states.run = (states.moveAmount > 0);
        }
        else
        {
            states.run = false;
        }
    }
}
