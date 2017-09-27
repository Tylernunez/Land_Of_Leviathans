using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStateEnterBool : StateMachineBehaviour {

    public string boolName;
    public bool status;
    public bool resetOnExit;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        animator.SetBool(boolName, status);

	}
    

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (resetOnExit)
            animator.SetBool(boolName, !status);

	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
