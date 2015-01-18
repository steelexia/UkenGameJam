using UnityEngine;
using System.Collections;
   

public  class NPC : AIPath
    {


       public Animation anim;
       public float sleepVelocity = 0.4F;
       public GameObject endOfPathEffect;
       protected Vector3 lastTarget;
        public float animationSpeed = 0.2F;
		
       public override void OnTargetReached()
       {

           if (endOfPathEffect != null && Vector3.Distance(tr.position, lastTarget) > 1)
           {
               GameObject.Instantiate(endOfPathEffect, tr.position, tr.rotation);
               lastTarget = tr.position;
           }
       }
       public override Vector3 GetFeetPosition()
       {
           return tr.position;
       }
     void Start()
       {
           base.Start();
       }

    protected new void Update () {
			
			//Get velocity in world-space
			Vector3 velocity;
			if (canMove) {
			
				//Calculate desired velocity
				Vector3 dir = CalculateVelocity (GetFeetPosition());
				
				//Rotate towards targetDirection (filled in by CalculateVelocity)
				RotateTowards (targetDirection);
				
				dir.y = 0;
				if (dir.sqrMagnitude > sleepVelocity*sleepVelocity) {
					//If the velocity is large enough, move
				} else {
					//Otherwise, just stand still (this ensures gravity is applied)
					dir = Vector3.zero;
				}

				if ( this.rvoController != null ) {
					rvoController.Move ( dir );
					velocity = rvoController.velocity;
				} else 
				if (navController != null) {
	#if FALSE
					navController.SimpleMove (GetFeetPosition(), dir);
	#endif
					velocity = Vector3.zero;
				} else if (controller != null) {
					controller.SimpleMove (dir);
					velocity = controller.velocity;
				} else {
					Debug.LogWarning ("No NavmeshController or CharacterController attached to GameObject");
					velocity = Vector3.zero;
				}
			} else {
				velocity = Vector3.zero;
			}
			
			
			//Animation
			
			//Calculate the velocity relative to this transform's orientation
			Vector3 relVelocity = tr.InverseTransformDirection (velocity);
			relVelocity.y = 0;
			
			if (velocity.sqrMagnitude <= sleepVelocity*sleepVelocity) {
				//Fade out walking animation
				anim.Blend ("human_run",0,0.2F);
			} else {
				//Fade in walking animation
                anim.Blend("human_run", 1, 0.2F);
				
				//Modify animation speed to match velocity
                AnimationState state = anim["human_run"];
				
				float speed = relVelocity.z;
				state.speed = speed*animationSpeed;
			}
		}
	}

