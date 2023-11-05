using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateSky : MonoBehaviour
{
    public Material sky;
    public float increaseValue;
    public float fixedAmount;
    public float previousValue;
    public bool isBack;
    private void Start()
    {
        isBack = false;
        fixedAmount = 1;
        increaseValue = 0;
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            rotateSky();
        }
        else
        {
            if (!PlayerManager.isGameStarted)
            {
                return;
            }

            else
            {
                rotateSky();
            }


        }
        

       



    }

    public void rotateSky()
    {
        if (previousValue >= 230 && !isBack)
        {
            fixedAmount *= -1;
           
           
        }
        
        else
        {
            isBack = false;
            if (previousValue <= 0)
            {
                isBack = true;
                fixedAmount *= -1;
            }

        }

        increaseValue += fixedAmount;
        previousValue = sky.GetFloat("_Rotation");
        previousValue = Mathf.Clamp(previousValue, 0, 230);
        
       
        increaseValue = Mathf.Clamp(increaseValue, 0, 13000);
        sky.SetFloat("_Rotation", Mathf.Lerp(previousValue, increaseValue, Time.deltaTime * 0.01f));
       
    }
}
