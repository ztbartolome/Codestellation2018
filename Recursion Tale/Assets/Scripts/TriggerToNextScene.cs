using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerToNextScene : MonoBehaviour {
    AudioSource source;
    [SerializeField] AudioClip victory;

    private void Start()
    {
        source = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //MoveChar(GetComponent<Rigidbody2D>(), new Vector2(1, 0));

        source.PlayOneShot(victory);
        if (!(SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCount))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }

    }

    void MoveChar(Rigidbody2D character, Vector2 move) {

        Vector2 currentPos = character.transform.position;
        float t = 0f;
        while (t < .5) {
            t += Time.deltaTime;
            //transform.position = Vector2.Lerp(currentPos, move, t);
            character.MovePosition(Vector2.Lerp(currentPos, move, t));

        }
    }
}
