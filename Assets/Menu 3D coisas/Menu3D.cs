using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu3D : MonoBehaviour
{
    public int GameSelected;

    public GameObject img1;  //artefactos
    public GameObject img2;  //aldeia
    public GameObject img3;  //engenhoca
    public GameObject img4;  //planificações
    public GameObject img5;  //cactos
    public GameObject img6;  //runas
    public GameObject img7;  //gincana


    // Start is called before the first frame update
    void Start()
    {
        GameSelected = 7;
        img1.SetActive(false);
        img2.SetActive(false);
        img3.SetActive(false);
        img4.SetActive(false);
        img5.SetActive(false);
        img6.SetActive(false);
        img7.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameSelected < 1)
        {
            GameSelected = 1;
        } else  if(GameSelected > 7)
        {
            GameSelected = 7;
        }


        if(GameSelected == 1)
        {
            img1.SetActive(true);
        }
        else { img1.SetActive(false); }

        if (GameSelected == 2)
        {
            img2.SetActive(true);
        }
        else { img2.SetActive(false); }

        if (GameSelected == 3)
        {
            img3.SetActive(true);
        }
        else { img3.SetActive(false); }

        if (GameSelected == 4)
        {
            img4.SetActive(true);
        }
        else { img4.SetActive(false); }

        if (GameSelected == 5)
        {
            img5.SetActive(true);
        }
        else { img5.SetActive(false); }

        if (GameSelected == 6)
        {
            img6.SetActive(true);
        }
        else { img6.SetActive(false); }

        if (GameSelected == 7)
        {
            img7.SetActive(true);
        }
        else { img7.SetActive(false); }

    }


    public void addGameSelection()
    {
        if(GameSelected < 7)
        {
            GameSelected += 1;
        }
        //GameSelected += 1;
        Debug.Log("numero: " + GameSelected);

        //GameSelected += 1;
        //Debug.Log("numero: " + GameSelected);


    }

    public void ReduceGameSelection()
    {
        if(GameSelected > 1)
        {
            GameSelected -= 1;
        }
        //GameSelected -= 1;
        Debug.Log("numero: " + GameSelected);

        //GameSelected -= 1;
        //Debug.Log("numero: " + GameSelected);

    }



    public void botaoOpcoes()
    {
        Debug.Log("opções");
    }

    public void botaoSairJogo()
    {
        Application.Quit();
        Debug.Log("Sair do jogo");
    }

    public void BotaoJogar()
    {
        if(GameSelected == 1)
        {
            Debug.Log("passar pa jogo 1");
        }
        if (GameSelected == 2)
        {
            Debug.Log("passar pa jogo 2");
        }
        if (GameSelected == 3)
        {
            Debug.Log("passar pa jogo 3");
        }
        if (GameSelected == 4)
        {
            Debug.Log("passar pa jogo 4");
        }
        if (GameSelected == 5)
        {
            Debug.Log("passar pa jogo 5");
        }
        if (GameSelected == 6)
        {
            Debug.Log("passar pa jogo 6");
        }
        if (GameSelected == 7)
        {
            SceneManager.LoadScene("Default");
            Debug.Log("passar pa jogo 7");
        }
    }
}
