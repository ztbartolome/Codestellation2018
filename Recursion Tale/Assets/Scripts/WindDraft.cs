using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindDraft : MonoBehaviour {

    [SerializeField] GameObject character;
    public bool inDraft;

    private void OnTriggerStay2D(Collider2D collision)
    {
        inDraft = true;
        Physics2D.gravity= Vector2.zero;
        character.transform.position = new Vector2(character.transform.position.x + .03f, character.transform.position.y + .01f);
    }

    private void Update()
    {
        if (!gameObject.GetComponent<Collider2D>().IsTouching(character.GetComponent<Collider2D>()))
        {
            inDraft = false;
            Physics2D.gravity = new Vector2(0f, -9.8f);
        }
    }
}
