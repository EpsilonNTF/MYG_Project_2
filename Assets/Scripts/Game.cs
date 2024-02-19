using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Xml.Schema;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    private Words word = new Words();
    public TMP_Text txt;
    private string reponse;
    private bool win = false;
    public Sprite[] sp;
    private Sprite initial;
    public AudioClip SFXWin, SFXFaux;
    private AudioSource audiosource;
    public GameObject Pendu;
    public GameObject panelend;
    private int i = 0;
    private Score score;
    private string lien = "https://makeyourgame.fun/api/pendu/avoir-un-mot";

    private void FunctionC()
    {
        foreach (char item in word.curWord)
        {
            txt.text += "_";
        }
    }

    //Au demarrage
    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
        StartCoroutine(word.GetRequest(lien, this, false));
        //Genere le nombre de _ en fonction du mots
    }

    public void StartGame()
    {
        word.GetMot();
        //Genere le nombre de _ en fonction du mots
        FunctionC();
        score = GetComponent<Score>();

        initial = Pendu.GetComponent<Image>().sprite;
    }

    public void KeyboardPress(string letter)
    {
        Validation(letter.ToLower());
    }

    //Validation de la lettre pressez
    private void Validation(string letter)
    {
        reponse = "";
        win = false;

        for (int i = 0; i < word.curWord.Length; i++)
        {
            if (txt.text.Substring(i, 1) == "_")
            {
                if (word.curWord.Substring(i, 1) == letter)
                {
                    reponse += letter;
                    win = true;
                }
                else
                {
                    reponse += "_";
                }
            }
            else
            {
                reponse += txt.text.Substring(i, 1);
            }
        }
        txt.text = reponse;
        Verification();
    }
    void Verification()
    {
        if (win)
        {
            audiosource.PlayOneShot(SFXWin);
            if (txt.text == word.curWord)
            {
                panelend.SetActive(true);// Apelle du panel (panelend)
                panelend.GetComponentInChildren<Text>().text = "BRAVO ! le mot était " + word.curWord; //Affichage Win Lose
                score.Point ++;
                StartCoroutine(Restart());//Restart System
            }
        }
        else
        {
            Pendu.GetComponent<Image>().sprite = sp[i];
            i++;
            audiosource.PlayOneShot(SFXFaux);
            if (i == 6)
            {
                panelend.SetActive(true); // Apelle du panel (panelend)
                panelend.GetComponentInChildren<Text>().text = "PERDU ! le mot était " + word.curWord;//Affichage Win Lose
                StartCoroutine(Restart());//Restart System
            }
        }
    }

    //Restart System
    IEnumerator Restart()
    {
        StartCoroutine(word.GetRequest(lien, this, true));
     yield return new WaitForSeconds(5f);

        if(win) //Si je gagne alors je cherche un nouveau /je Regenere le nombre de _
        {
            word.GetMot();
            txt.text = "";

            //Genere le nombre de _ en fonction du mots
            FunctionC();
            panelend.SetActive(false);
            //Sert a reinitialisé le pendu
            Pendu.GetComponent<Image>().sprite = initial;
            BtnInteractableOn();
            i = 0;
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }
    }

    //Reinitialisation des boutton Lettre
     void BtnInteractableOn()
    {
        Button[] btn = GameObject.FindObjectsOfType<Button>();
        
        //Rendre denouveau les button utilisable apres la victoire 
        foreach (var item in btn) 
        {
            item.interactable = true;
        }

    }
}

