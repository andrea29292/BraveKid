﻿using UnityEngine;
using System.Collections;

public class RockThrow : MonoBehaviour {


		public Transform Target;
		public float firingAngle = 45.0f;
		public float gravity = 9.8f;
		
		public GameObject Projectile;      
		public Transform myTransform;
		
		void Awake()
		{      
		}
		
		void Start()
		{          
			//StartCoroutine(SimulateProjectile());
		}

		void Update(){
			
		}
		
		
		IEnumerator SimulateProjectile()
		{
			// Short delay added before Projectile is thrown
			//yield return new WaitForSeconds(1.5f);
			
			// Move projectile to the position of throwing object + add some offset if needed.
			Projectile.transform.position = myTransform.position + new Vector3(0, 0.0f, 0);
			
			// Calculate distance to target
			float target_Distance = Vector3.Distance(Projectile.transform.position, Target.position);
			
			// Calculate the velocity needed to throw the object to the target at specified angle.
			float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);
			
			// Extract the X  Y componenent of the velocity
			float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
			float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
			
			// Calculate flight time.
			float flightDuration = target_Distance / Vx;
			
			// Rotate projectile to face the target.
			Projectile.transform.rotation = Quaternion.LookRotation(Target.position - Projectile.transform.position);
			
			float elapse_time = 0;
			
			while (elapse_time < flightDuration)
			{
				Projectile.transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
				
				elapse_time += Time.deltaTime;
				
				yield return null;
			}
		}  
	}