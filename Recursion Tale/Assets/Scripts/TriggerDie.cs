using System.Collections;
using UnityEngine.SceneManagement;

using System.Collections.Generic;
using UnityEngine;
public class TriggerDie : MonoBehaviour {

	Animator anim;
	[SerializeField] GameObject character;

	void Start(){
		anim = character.GetComponent<Animator> ();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		
        //MoveChar(GetComponent<Rigidbody2D>(), new Vector2(1, 0));
		StartCoroutine(DeathAnimation());

    }

	IEnumerator DeathAnimation(){
		anim.SetInteger ("State", 3);
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}
}
