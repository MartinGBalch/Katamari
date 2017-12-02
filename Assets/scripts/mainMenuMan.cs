using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenuMan : MonoBehaviour
{
    public InputField ipField;
    public InputField portField;

    public Button hostButton;
    public Button clientButton;
    public Button quitGame;
    public Button join;
    public Button host;
    public Button back;

    public GameObject mainMenu;
    public GameObject joinMenu;
    public GameObject hostMenu;

	// Use this for initialization
	void Start ()
    {
        mainMenu.gameObject.SetActive(true);
        hostMenu.gameObject.SetActive(false);
        joinMenu.gameObject.SetActive(false);

        ipField.onEndEdit.AddListener(updateIP);
        portField.onEndEdit.AddListener(updatePort);
        hostButton.onClick.AddListener(HostButton);
        clientButton.onClick.AddListener(ConnectButton);
        quitGame.onClick.AddListener(QuitGame);
        join.onClick.AddListener(ShowJoinMenu);
        host.onClick.AddListener(ShowHostMenu);
        back.onClick.AddListener(Back);
    }
	
	// Update is called once per frame
	void Update () {}

    void updateIP(string newIP)
    {
        NetworkManager.singleton.networkAddress = newIP;
    }

    void updatePort(string port)
    {
        NetworkManager.singleton.networkPort = Int32.Parse(port);
    }

    void HostButton()
    {
        NetworkManager.singleton.StartHost();
    }

    void ConnectButton()
    {
        NetworkManager.singleton.StartClient();
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void ShowHostMenu()
    {
        hostMenu.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }

    void ShowJoinMenu()
    {
        joinMenu.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }

    public void Back()
    {
        mainMenu.gameObject.SetActive(true);
        joinMenu.gameObject.SetActive(false);
        hostMenu.gameObject.SetActive(false);

    }
}
