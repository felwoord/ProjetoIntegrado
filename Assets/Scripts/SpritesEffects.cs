using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesEffects : MonoBehaviour {
	public Material mat;
	public float fade = 0;
	public float fadeVelocity = 10;
	private bool actFade = false;
	SpriteRenderer sp;
	Color tmp;




	void Start () {
		sp = GetComponent<SpriteRenderer> ();
		tmp = GetComponent<SpriteRenderer> ().color;
		FadeAndAddEffect ();
	}
	

	void Update () {
		RotateSprite ();


		if(actFade == true){
			fade += Time.deltaTime;
			sp.color = new Color (tmp.r,tmp.g,tmp.b, 1 - (fade/fadeVelocity));
		}

	}

	public void RotateSprite ()
	{
		transform.Rotate (Vector3.forward * -0.5f);
	}


	public void FadeAndAddEffect(){
		mat = Resources.Load ("Materials/ParticleMaterial") as Material;
		ParticleSystemRenderer psr = gameObject.GetComponent<ParticleSystemRenderer> ();
		psr.material = mat;
		if (gameObject.tag == "Black") {
			psr.material.color = Color.black;
		}
		if (gameObject.tag == "Blue") {
			psr.material.color = Color.blue;
		}
		if (gameObject.tag == "Yellow") {
			psr.material.color = Color.yellow;
		}
		if (gameObject.tag == "Green") {
			psr.material.color = Color.green;
		}
		if (gameObject.tag == "Red") {
			psr.material.color = Color.red;
		}
		if (gameObject.tag == "White") {
			psr.material.color = Color.white;
		}

		actFade = true;

	}
		
}
