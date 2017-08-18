using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class characterAnimator : MonoBehaviour {

    const float locomotionAnimationSmoothTime = .1f;

    Animator animator;
    Vector3 previous;
    PlayerMachine speed;

	// Use this for initialization
	void Start () {

        animator = GetComponentInChildren<Animator>();
        speed = GetComponent<PlayerMachine>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 velocity = (transform.position - previous) / Time.deltaTime;
        previous = transform.position;
        float speedPercent = velocity.magnitude / speed.WalkSpeed;
        animator.SetFloat("speedPercent",speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
	}
}
