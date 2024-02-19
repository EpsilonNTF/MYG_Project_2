using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class Words
{

    public string curWord;
    private BanqueMot mots;


    public IEnumerator GetRequest(string uri, Game GameScript,bool Restart)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    mots = JsonUtility.FromJson<BanqueMot>(webRequest.downloadHandler.text);
                 
                    Debug.Log("Fin de lancement");
                    if (Restart == false) 
                    { 
                    GameScript.StartGame();
                    }
            break;
            }
        }
    }

    public void GetMot()
    {
        curWord = mots.motChoisi;
    }
}

[System.Serializable]
public class BanqueMot
{
    public string motChoisi;

    public static BanqueMot CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<BanqueMot>(jsonString);
    }
}
