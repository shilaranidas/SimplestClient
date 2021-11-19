using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameSystemManager : MonoBehaviour
{
    GameObject btnSubmit, txtUserId, txtPwd, chkCreate,btnJoin,lblU,lblP,lblInfo, gameBoard, txtMsg, btnSend, ddlMsg, chatBox, btnSendPrefixMsg, btnJoinObserver, btnReplay;
    //,btnPlay
    public GameObject networkedClient;
    List<string> preFixMsg = new List<string> { "hello","test","bye","call you later"};
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
            else if (go.name == "btnLogin")
            {
                btnSubmit = go;
            }
            else if (go.name == "txtUser")
            {
                txtUserId = go;
            }
            else if (go.name == "txtPwd")
            {
                txtPwd = go;
            }
            else if (go.name == "chkCreate")
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
            else if (go.name == "btnReplay")
            {
                btnReplay = go;
            }
            //else if (go.name == "gameBoard")
            //    gameBoard = go;
            else if (go.name == "txtMsg")
                txtMsg = go;
            else if (go.name == "btnSend")
                btnSend = go;
            else if (go.name == "ddlMsg")
                ddlMsg = go;
            else if (go.name == "chatBox")
                chatBox = go;
            else if (go.name == "btnSendPrefixMsg")
                btnSendPrefixMsg = go;
            else if (go.name == "btnJoinObserver")
                btnJoinObserver = go;




        }
        btnSubmit.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        btnJoin.GetComponent<Button>().onClick.AddListener(JoinButtonPressed);
        btnJoinObserver.GetComponent<Button>().onClick.AddListener(ObserveButtonPressed);
        btnReplay.GetComponent<Button>().onClick.AddListener(ReplayButtonPressed);
        btnSend.GetComponent<Button>().onClick.AddListener(SendButtonPressed);
        btnSendPrefixMsg.GetComponent<Button>().onClick.AddListener(SendPrefButtonPressed);
        chkCreate.GetComponent<Toggle>().onValueChanged.AddListener(CreateToggleChanged);
        
        ChangeState(GameStates.LoginMenu);
       
            ddlMsg.GetComponent<Dropdown>().AddOptions(preFixMsg);
        
       
    }
    public void updateChat(string msg)
    {
        chatBox.GetComponent<Text>().text += msg+"\n";
    }
    public void updateUserName(string name)
    {
        lblInfo.GetComponent<Text>().text = name;
    }
    public void ReplayButtonPressed()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SendPrefButtonPressed()
    {
        string msg = ClientToServerSignifiers .SendPrefixMsg+","+ ddlMsg.GetComponent<Dropdown>().options[ddlMsg.GetComponent<Dropdown>().value].text.ToString();
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
        Debug.Log("send" + msg);
    }
    public void SendButtonPressed()
    {
        string msg = ClientToServerSignifiers .SendMsg+","+ txtMsg.GetComponent<InputField>().text;
        Debug.Log("msg:" + msg);
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
        Debug.Log("send" + msg);
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
        ChangeState(GameStates.TicTacToe);
    }
    public void ObserveButtonPressed()
    {
        string msg = ClientToServerSignifiers.JoinAsObserver + "";
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
        ChangeState(GameStates.Observer);
    }
    public void CreateToggleChanged(bool newValue)
    {

    }
    public void PlayButtonPressed()
    {
        string msg = ClientToServerSignifiers.PlayGame + "";
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
        //load tictactoe
        ChangeState(GameStates.Running);
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
        //btnPlay.SetActive(false);
        //txtMsg, btnSend, ddlMsg, chatBox, btnSendPrefixMsg
        txtMsg.SetActive(false);
        btnSend.SetActive(false);
        ddlMsg.SetActive(false);
        chatBox.SetActive(false);
        btnSendPrefixMsg.SetActive(false);
        btnJoinObserver.SetActive(false);
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
            

            txtMsg.SetActive(true);
            btnSend.SetActive(true);
            ddlMsg.SetActive(true);
            chatBox.SetActive(true);
            btnSendPrefixMsg.SetActive(true);
            btnJoinObserver.SetActive(true);
        }
        else if (newState == GameStates.WaitingInQueue)
        {
        }
        else if (newState == GameStates.WaitingForPlayer)
        {
            lblInfo.GetComponent<Text>().text = "waiting for player";
        }
        else if (newState == GameStates.TicTacToe)
        {
            //btnPlay.SetActive(true);
            txtMsg.SetActive(true);
            btnSend.SetActive(true);
            ddlMsg.SetActive(true);
            chatBox.SetActive(true);
            btnSendPrefixMsg.SetActive(true);
        }
        else if (newState==GameStates.Observer)
        {
            chatBox.SetActive(true);         
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
    public const int Running = 6;
    public const int Observer = 7;
}