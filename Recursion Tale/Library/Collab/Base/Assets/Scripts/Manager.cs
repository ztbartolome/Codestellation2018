using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Manager : MonoBehaviour {
    Queue<string> actions;

    // This image is the object that holds the png
    [SerializeField] Image image;

    [SerializeField] Button walk;
    [SerializeField] Button start;

    //character sprite
    [SerializeField] GameObject character;
	Animator anim; //THIS IS THE ANIMATOR THING DO NOT DELETE

    // This is the music that plays during the game
    [SerializeField] AudioSource codingMusic;
    [SerializeField] AudioSource playMusic;
    [SerializeField] AudioSource jumpMusic;

    private bool dequeuing = false;
    private bool executing = false;
    private string action = "";
    private float originalXPos;

    // Use this for initialization
    void Start () {
        actions = new Queue<string>();
		anim = character.GetComponent<Animator> ();
		anim.SetInteger ("State", 0);
	}

    // Update is called once per frame
    void Update()
    {
        if (dequeuing && actions.Count!=0)
        {
            if (!executing)
            {
                action = actions.Peek();
                originalXPos = character.transform.position.x;
                executing = true;
            } else {
                if (action == "walk")
                {
                    WalkForwards();
                }
                else if (action == "jump")
                {
                    anim.SetInteger("State", 2);
                    character.GetComponent<Rigidbody2D>().AddForce(transform.up * 200);
                }
            }
        } else
        {
            dequeuing = false;
        }
    }

    public void clickJump() {
        actions.Enqueue("jump");
    }

    public void clickWalk()
    {
        print("Walk queued.");
        actions.Enqueue("walk");
    }

    public void startDequeue(){
        print("Dequeuing...");
        dequeuing = true;
    }

    private void WalkForwards()
    {
        print(character.transform.position.x + ", " + (originalXPos + 2f));
        if (character.transform.position.x < (originalXPos + 2f))
        {
            character.transform.position = new Vector2(character.transform.position.x + .13f, character.transform.position.y);
        }
        else
        {
            executing = false;
            actions.Dequeue();
            character.transform.position = new Vector2(originalXPos + 2f, character.transform.position.y);
        }
    }

    public bool isOnGround() {
        //check Layer type of object colliding with character
        return character.GetComponent<CircleCollider2D>().IsTouchingLayers();
    }
}