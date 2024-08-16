using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class BarFadeEffect : MonoBehaviour
{
    private const float MAX_FADE_TIMER = 1f;
    private float fadeTimer;
    private Image redBarImage;
    private Image whiteBarImage;
    private Color whiteBarColor;
    private ENV_Mana envManaScript;

    private float cRed;
    private float mRed;
    private float redBar;

    private bool isFading;
    // Start is called before the first frame update
    private void Awake()
    {
        redBarImage = transform.Find("RedBar").GetComponent<Image>();
        whiteBarImage = transform.Find("WhiteBar").GetComponent <Image>();
        whiteBarColor = whiteBarImage.color;
        whiteBarColor.a = 1f;
        whiteBarImage.color = whiteBarColor;
        
        
    }

    private void Start()
    {
        
        envManaScript = FindObjectOfType<ENV_Mana>();
        redBarImage.fillAmount = envManaScript.currentRed / envManaScript.maxRed;
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    IEnumerator FinishFading(int fadeTime)
    {
        yield return new WaitForSeconds(fadeTime);
        Debug.Log($"COROUTINE! PREVRED: {envManaScript.previousRed} / MAXRED: {envManaScript.maxRed} and CURRENTRED: {envManaScript.currentRed}");
        whiteBarImage.fillAmount = (float)envManaScript.currentRed / (float)envManaScript.maxRed;
        isFading = false;
    }
    IEnumerator StartFading(int fadeTime)
    {
        redBarImage.fillAmount = (float)envManaScript.currentRed / (float)envManaScript.maxRed;
        fadeTimer = fadeTime;
        float fadeAmount = fadeTimer/255f;
        Debug.Log($"BEGIN COROUTINE CurrentRED: {envManaScript.currentRed} / MAXRED: {envManaScript.maxRed} FILLAMOUNT: {redBarImage.fillAmount} MATH: {envManaScript.currentRed / envManaScript.maxRed}");
        whiteBarColor.a = 255;
        

        while (fadeTimer > 0)
        {
                
            whiteBarColor.a -= fadeAmount * Time.deltaTime;
            whiteBarImage.color = whiteBarColor;
            fadeTimer -= Time.deltaTime;
            yield return new WaitForSeconds(.1f);
        }

        
        
    }

    public void MatchWhiteBarToRed(int fadeTime)
    {
        
        if (isFading == false)
        {
            isFading = true;
            whiteBarImage.fillAmount = envManaScript.previousRed / envManaScript.maxRed;
            Debug.Log($"PREVRED: {envManaScript.previousRed} / MAXRED: {envManaScript.maxRed} and CURRENTRED: {envManaScript.currentRed}");
            StartCoroutine(StartFading(fadeTime));
            StartCoroutine(FinishFading(fadeTime));
        }
    }


    public void FadingWhiteRedBar()
    {
        
        if (whiteBarColor.a <= 0)
        {
            //BackgroundBar is invisible
            whiteBarImage.fillAmount = redBarImage.fillAmount;
          
        }
        whiteBarImage.fillAmount = redBarImage.fillAmount;
        whiteBarColor.a = 1;
        whiteBarImage.color = whiteBarColor;
        

        //LoseRed();
    }

    private void GetReds()
    {
        cRed = envManaScript.currentRed;
        mRed = envManaScript.maxRed;
        redBar = cRed / mRed;
    }
}
