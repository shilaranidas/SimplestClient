using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameSystemManager : MonoBehaviour
{
    GameObject btnSubmit, txtUserId, txtPwd, chkCreate,btnJoin,lblU,lblP,btnPlay,lblInfo;
   
    public GameObject networkedClient;
    //static GameObject instance;
    // Start is called before the first frame update
    void Start()
    {
        //instance = this.gameObject;
        GameObject[] allobjects = FindObjectsOfType<GameObject>();
        foreach (GameObject go in allobjects)
        {
            if (go.name == "btnJoin")
            {
                btnJoin = go;
            }
            else if (go.name == "lblInfo")
            {
                lblInfo = go;
            }
            else if (go.name=="btnLogin")
            {
                btnSubmit = go;
            }
            else if (go.name=="txtUser")
            {
                txtUserId = go;
            }
            else if(go.name=="txtPwd")
            {
                txtPwd = go;
            }
            else if(go.name=="chkCreate")
            {
                chkCreate = go;
            }
            else if (go.name == "lblUser")
            {
                lblU = go;
            }
            else if (go.name == "lblPwd")
            {
                lblP = go;
            }
            else if (go.name == "btnPlay")
            {
                btnPlay = go;
            }


        }
        btnSubmit.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        btnJoin.GetComponent<Button>().onClick.AddListener(JoinButtonPressed);
        btnPlay.GetComponent<Button>().onClick.AddListener(PlayButtonPressed);
        chkCreate.GetComponent<Toggle>().onValueChanged.AddListener(CreateToggleChanged);
        ChangeState(GameStates.LoginMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SubmitButtonPressed()
    {
        Debug.Log("button");
        string p = txtPwd.GetComponent<InputField>().text;
        string n = txtUserId.GetComponent<InputField>().text;
        string msg;
        if (chkCreate.GetComponent<Toggle>().isOn)
            msg = ClientToServerSignifiers.CreateAccount + "," + n + "," + p;
        else
            msg = ClientToServerSignifiers.Login + "," + n + "," + p;
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
    }
    public void JoinButtonPressed()
    {
        string msg = ClientToServerSignifiers.JoinGammeRoomQueue+"";
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
        ChangeState(GameStates.WaitingInQueue);
    }
    public void CreateToggleChanged(bool newValue)
    {

    }
    public void PlayButtonPressed()
    {
        string msg = ClientToServerSignifiers.PlayGame + "";
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
        ChangeState(GameStates.TicTacToe);
    }
    public void ChangeState(int newState)
    {
        btnJoin.SetActive(false);
        btnSubmit.SetActive(false);
        chkCreate.SetActive(false);
        txtPwd.SetActive(false);
        txtUserId.SetActive(false);
        lblU.SetActive(false);
        lblP.SetActive(false);
        btnPlay.SetActive(false);
        if (newState == GameStates.LoginMenu)
        {
            btnSubmit.SetActive(true);
            chkCreate.SetActive(true);
            txtPwd.SetActive(true);
            txtUserId.SetActive(true);
            lblU.SetActive(true);
            lblP.SetActive(true);
        }
        else if (newState == GameStates.MainMenu)
        {
            btnJoin.SetActive(true);

        }
        else if (newState == GameStates.WaitingInQueue)
        {
        }
        else if (newState == GameStates.WaitingForPlayer)
        {
        }
        else if (newState == GameStates.TicTacToe)
        {
            btnPlay.SetActive(true);
        }
    }

}

static public class GameStates
{
    public const int LoginMenu = 1;
    public const int MainMenu = 2;
    public const int WaitingInQueue = 3;
    public const int TicTacToe = 4;
    public const int WaitingForPlayer = 5;
}