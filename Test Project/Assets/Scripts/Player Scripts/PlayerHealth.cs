﻿using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : Health {
	public Text healthText;
	
	
	protected override void Start () {
		base.Start();
		healthText.text =  health.ToString()+"/"+ maxHealth.ToString();
	}

	protected override void Die(){
		Debug.Log("you died");
		gameManager.EndGame();
	}

	public override void TakeDamage(float damage){
		base.TakeDamage(damage);
		healthText.text =  health.ToString()+"/"+ maxHealth.ToString();	
	}

	public override void MaxHealthUp(){
		base.MaxHealthUp();
		healthText.text =  health.ToString()+"/"+ maxHealth.ToString();
	}

	public override void HealthUp(){
		base.HealthUp();
		healthText.text =  health.ToString()+"/"+ maxHealth.ToString();
	}

}
