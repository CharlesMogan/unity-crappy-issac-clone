﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilDestroyBullet : Bullet {

 public override void OnTriggerEnter(Collider other){
	 	if(other.gameObject.tag != "Friendly Bullet" && other.gameObject.tag != "Unfriendly Bullet" && other.gameObject.tag != "Enemy"){
    		Destroy(this.gameObject);
    	}
       	if(other.gameObject.tag == "Player" || other.gameObject.tag == "Wall"){
       		Health health = other.GetComponent<Health>();
       		health.TakeDamage(projectileDamage);
       	}
    }
}
