using UnityEngine;
using System.Collections;

public class IKHandling : MonoBehaviour {

    Animator anim;

    public float ikWeight = 1;

	//Variables for the hand IK target examples
    public Transform leftIKTarget;
    public Transform rightIKTarget;

    public Transform hintLeft;
    public Transform hintRight;

	//Variables for the Feet IK
    Vector3 lFpos;
    Vector3 rFpos;

    Quaternion lFrot;
    Quaternion rFrot;

    float lFWeight;
    float rFWeight;

    Transform leftFoot;
    Transform rightFoot;

    public float offsetY;

	
	//Variables for the Look IK example
    public float lookIKweight;
    public float bodyWeight;
    public float headWeight;
    public float eyesWeight;
    public float clampWeight;

    public Transform lookPos;

	//Variables for the pointer example
    Vector3 rightHandPos;
    float rightHandWeight;
    public Transform pointer;

	void Start () 
    {
		//Setup the references
        anim = GetComponent<Animator>();

		//Since we know our animator has a humanoid rig then we can take the transforms from there
        leftFoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
        rightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);

		//Initialize a rotation so that we don't get 0NAN errors
        lFrot = leftFoot.rotation;
        rFrot = rightFoot.rotation;
	}
	
	// Update is called once per frame
	void Update () 
    {
		//Example for the look IK, we take a point over a ray and place the look target there
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 15);

        lookPos.position = ray.GetPoint(15);

		
		//Example for the pointer IK, we do a raycast from a pointer and if it finds a wall it places the right hand IK target there
        RaycastHit rightHand;

        if (Physics.Raycast(pointer.position, pointer.right, out rightHand, 1))
        {
            Debug.Log(rightHand.collider.name);

            rightIKTarget.position = rightHand.point;
            rightIKTarget.rotation = Quaternion.FromToRotation(transform.up, rightHand.normal) * transform.rotation;
            anim.SetBool("Raise",true);
        }
        else
        {
            anim.SetBool("Raise", false);
        }


		/*Example for the Feet IK, we do 2 raycasts from our feet transforms and we place their IK targets to the appropriate place
		Keep in mind that there's a good chance we would hit our own collider, so either use a layer mask on the raycasts
		or put your own collider on Ignore Raycast, however depending on the game this might not be ideal
		If you want to get more out of it, do a RaycastAll and then eliminate your own collider and use an IComparer to sort the hits out*/
        RaycastHit leftHit;
        RaycastHit rightHit;

        Vector3 lpos = leftFoot.TransformPoint(Vector3.zero);
        Vector3 rpos = rightFoot.TransformPoint(Vector3.zero);

        if(Physics.Raycast(lpos, -Vector3.up, out leftHit, 1))
        {
            lFpos = leftHit.point;
            lFrot = Quaternion.FromToRotation(transform.up, leftHit.normal) * transform.rotation;
        }

        if (Physics.Raycast(rpos, -Vector3.up, out rightHit, 1))
        {
            rFpos = rightHit.point;
            rFrot = Quaternion.FromToRotation(transform.up, rightHit.normal) * transform.rotation;
        }

	}

	//Everything that has to do with the build in IK system has to go in here
    void OnAnimatorIK()
    {
		//Our look IK weight 
        anim.SetLookAtWeight(lookIKweight, bodyWeight, headWeight, eyesWeight, clampWeight);
        anim.SetLookAtPosition(lookPos.position);

		//The weight for the feet is calculated based on the curves from inside the animator
        lFWeight = anim.GetFloat("LeftFoot");
        rFWeight = anim.GetFloat("RightFoot");

		//we set the position weight
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, lFWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, rFWeight);

		//we set the position + an offset for the Y
        anim.SetIKPosition(AvatarIKGoal.LeftFoot, lFpos + new Vector3(0,offsetY,0));
        anim.SetIKPosition(AvatarIKGoal.RightFoot, rFpos + new Vector3(0, offsetY, 0));

		//we set the rotation weight
        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, lFWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, rFWeight);

		//and the rotation
        anim.SetIKRotation(AvatarIKGoal.LeftFoot, lFrot);
        anim.SetIKRotation(AvatarIKGoal.RightFoot, rFrot);


        //Hand IK, similar to foot IK with slight changes
        float rhWeight = anim.GetFloat("RightHand");

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rhWeight);

        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftIKTarget.position);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightIKTarget.position + new Vector3(-.3f,0,0));

        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, ikWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rhWeight);

        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftIKTarget.rotation);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightIKTarget.rotation);

		
		//The Hint IK works pretty much the same
       /* anim.SetIKHintPositionWeight (AvatarIKHint.LeftElbow, ikWeight);
        anim.SetIKHintPositionWeight(AvatarIKHint.RightKnee, ikWeight);

        anim.SetIKHintPosition(AvatarIKHint.LeftElbow, hintLeft.position);
        anim.SetIKHintPosition(AvatarIKHint.LeftKnee, hintRight.position);*/

    }
	
	/*
	Something to keep in mind, even though you might have set up correct a feet IK system,
	you have to take into account that your controller is also playing nice with it.
	For example, in our controller we use physics to move around, so that means we have to be able to collide with objects
	This might make the IK for the feet to stretch out in order to reach their goal, this might not look too pretty.
*/
}
