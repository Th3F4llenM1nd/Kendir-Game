using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Botoes : MonoBehaviour
{
    public GameObject PanelLogin;
    public GameObject PanelSair;
    public GameObject PanelRegistar;
    public GameObject PanelInformacaoSuplementar;
    public GameObject PanelEsqueciPassword;
    public GameObject PanelTermosCondicoes;


    //Botões do Login Main
    public void ButtonEntrar()
    {
        Debug.Log("AIIIIII!!");
        SceneManager.LoadScene(0); //numero da cena que vai conter a UI 3D
    }

    public void ButtonRegisto()
    {
        if (PanelLogin != null)
        {
            bool isActive = PanelLogin.activeSelf;

            PanelLogin.SetActive(!isActive);
        }

        if (PanelRegistar != null)
        {
            bool isActive = PanelRegistar.activeSelf;

            PanelRegistar.SetActive(!isActive);
        }
    }

    public void ButtonSairLM()
    {
        if (PanelLogin != null)
        {
            bool isActive = PanelLogin.activeSelf;

            PanelLogin.SetActive(!isActive);
        }

        if (PanelSair != null)
        {
            bool isActive = PanelSair.activeSelf;

            PanelSair.SetActive(!isActive);
        }
    }
    //FIM

    //Botões Main Sair
    public void ButtonSim()
    {
        Application.Quit();
    }

    public void ButtonNao()
    {
        if (PanelLogin != null)
        {
            bool isActive = PanelLogin.activeSelf;

            PanelLogin.SetActive(!isActive);
        }

        if (PanelSair != null)
        {
            bool isActive = PanelSair.activeSelf;

            PanelSair.SetActive(!isActive);
        }
    }
    //FIM

    //Botões Main Nova Password
    public void ButtonNovaPassword()
    {
        if (PanelLogin != null)
        {
            bool isActive = PanelLogin.activeSelf;

            PanelLogin.SetActive(!isActive);
        }

        if (PanelEsqueciPassword != null)
        {
            bool isActive = PanelEsqueciPassword.activeSelf;

            PanelEsqueciPassword.SetActive(!isActive);
        }
    }

    public void ButtonVoltarNP()
    {
        if(PanelLogin != null)
        {
            bool isActive = PanelLogin.activeSelf;

            PanelLogin.SetActive(!isActive);
        }

        if (PanelEsqueciPassword != null)
        {
            bool isActive = PanelEsqueciPassword.activeSelf;

            PanelEsqueciPassword.SetActive(!isActive);
        }
    }

    public void ButtonSairNP()
    {
        if (PanelEsqueciPassword != null)
        {
            bool isActive = PanelEsqueciPassword.activeSelf;

            PanelEsqueciPassword.SetActive(!isActive);
        }

        if (PanelSair != null)
        {
            bool isActive = PanelSair.activeSelf;

            PanelSair.SetActive(!isActive);
        }
    }
    //FIM

    //Botões Main Informação Suplementar
    public void ButtonConcluir()
    {
        if (PanelLogin != null)
        {
            bool isActive = PanelLogin.activeSelf;

            PanelLogin.SetActive(!isActive);
        }

        if (PanelInformacaoSuplementar != null)
        {
            bool isActive = PanelInformacaoSuplementar.activeSelf;

            PanelInformacaoSuplementar.SetActive(!isActive);
        }
    }

    public void ButtonVoltarIS()
    {
        if (PanelRegistar != null)
        {
            bool isActive = PanelRegistar.activeSelf;

            PanelRegistar.SetActive(!isActive);
        }

        if (PanelInformacaoSuplementar != null)
        {
            bool isActive = PanelInformacaoSuplementar.activeSelf;

            PanelInformacaoSuplementar.SetActive(!isActive);
        }
    }

    public void ButtonSairIS()
    {
        if (PanelInformacaoSuplementar != null)
        {
            bool isActive = PanelInformacaoSuplementar.activeSelf;

            PanelInformacaoSuplementar.SetActive(!isActive);
        }

        if (PanelSair != null)
        {
            bool isActive = PanelSair.activeSelf;

            PanelSair.SetActive(!isActive);
        }
    }
    //FIM

    //Botões Main Registar
    public void ButtonContinuar()
    {
        if (PanelRegistar != null)
        {
            bool isActive = PanelRegistar.activeSelf;

            PanelRegistar.SetActive(!isActive);
        }

        if (PanelInformacaoSuplementar != null)
        {
            bool isActive = PanelInformacaoSuplementar.activeSelf;

            PanelInformacaoSuplementar.SetActive(!isActive);
        }
    }

    public void ButtonVoltarR()
    {
        if (PanelLogin != null)
        {
            bool isActive = PanelLogin.activeSelf;

            PanelLogin.SetActive(!isActive);
        }

        if (PanelRegistar != null)
        {
            bool isActive = PanelRegistar.activeSelf;

            PanelRegistar.SetActive(!isActive);
        }
    }
    public void ButtonSairMR()
    {
        if (PanelRegistar != null)
        {
            bool isActive = PanelRegistar.activeSelf;

            PanelRegistar.SetActive(!isActive);
        }

        if (PanelSair != null)
        {
            bool isActive = PanelSair.activeSelf;

            PanelSair.SetActive(!isActive);
        }
    }
    //FIM

    //Botão do Main Termos e Condições
    public void ButtonVoltarTC()
    {
        if (PanelLogin != null)
        {
            bool isActive = PanelLogin.activeSelf;

            PanelLogin.SetActive(!isActive);
        }

        if (PanelTermosCondicoes != null)
        {
            bool isActive = PanelTermosCondicoes.activeSelf;

            PanelTermosCondicoes.SetActive(!isActive);
        }
    }
    public void ButtonSairTC()
    {
        if (PanelTermosCondicoes != null)
        {
            bool isActive = PanelTermosCondicoes.activeSelf;

            PanelTermosCondicoes.SetActive(!isActive);
        }

        if (PanelSair != null)
        {
            bool isActive = PanelSair.activeSelf;

            PanelSair.SetActive(!isActive);
        }
    }
    //FIM
}
