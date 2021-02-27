using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class ButtonFunction : MonoBehaviour
{
    public  Animator button;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ClickOn();
        }
    }

    public void ClickOn()
    {
        button.SetBool ("ClickButton",true);
        StartCoroutine("EndClick");
    }

    IEnumerator EndClick()
    {
        yield return new WaitForSeconds(1);
        button.SetBool("ClickButton", false);
    }

    
}