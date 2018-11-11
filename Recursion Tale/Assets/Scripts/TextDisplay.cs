using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour {
    public Queue<Queue<string>> dialogues;
    public Queue<string> currentDialogue;
    public Text display;
    public Scene currentScene;
    [SerializeField] string fileName;


    // Use this for initialization
    void Start () {
        dialogues = new Queue<Queue<string>>();
        display = GetComponent<Text>();      
        readDialogues();
        currentDialogue = dialogues.Peek();
        currentScene = SceneManager.GetActiveScene();
        display.text = currentDialogue.Dequeue() + "\n\nPress Space to Continue";
    }
	
	// Update is called once per frame
	void Update () {
		if(currentDialogue.Count == 0) {
            display.text = "";
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            display.text = currentDialogue.Dequeue() + "\n\nPress Space to Continue";
        }
        //Move on to next dialogue block if scene has changed
        if(SceneManager.GetActiveScene() != currentScene){
            currentDialogue = dialogues.Dequeue();
            display.text = currentDialogue.Dequeue() + "\n\nPress Space to Continue";
        }
        
	}

    void readDialogues() {
        string line;
        Queue<string> current = new Queue<string>();

        // Read each line in the file and add it to dialogues queue
        System.IO.StreamReader file = new System.IO.StreamReader(@"Assets/" + fileName);

        while ((line = file.ReadLine()) != null) {
            if (line.Equals("")) {
                dialogues.Enqueue(current);
                current = new Queue<string>();
            } else {
                current.Enqueue(line);
            }  
        }
        dialogues.Enqueue(current);

        file.Close();
        // Suspend the screen.  
        System.Console.ReadLine();
    }
}
