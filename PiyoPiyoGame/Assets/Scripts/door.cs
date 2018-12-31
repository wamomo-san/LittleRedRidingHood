using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class door : MonoBehaviour {

    public int LevelToLoad;

    private gameMaster gm;

	// Use this for initialization
	void Start () {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gm.InputText.text = ("[E] to Enter");
            if (Input.GetKeyDown("e"))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKeyDown("e"))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gm.InputText.text = (" ");
        }
    }

}
