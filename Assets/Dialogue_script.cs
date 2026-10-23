using System.Collections;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;


public class simpleDialog: MonoBehaviour
{
    public GameObject uiCanvas;
    public TextMeshProUGUI maintext;

    private void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag("Player"))
        {
            uiCanvas.SetActive(true);
            StartCoroutine(PlayIntro());

            GetComponent<Collider>().enabled=false;
        }
    }
    IEnumerator PlayIntro()
    {
        maintext.text = "Welcome to the Wrath floor...";
        yield return new WaitForSeconds(3.0f);

        maintext.text= "To proceed....";

        yield return new WaitForSeconds(3.0f);
        maintext.text= "riddle me this?";
    }

}