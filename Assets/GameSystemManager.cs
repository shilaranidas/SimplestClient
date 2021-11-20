using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameSystemManager : MonoBehaviour
{
    GameObject btnSubmit, txtUserId, txtPwd, chkCreate,btnJoin,lblU,lblP,lblInfo, gameBoard, txtMsg, btnSend, ddlMsg, chatBox, pnlChat, btnSendPrefixMsg, btnJoinObserver, btnReplay,ddlPlayer;
    //,btnPlay
    GameObject txtCMsg, btnCSend,LoginSys,MsgSend,PMsgSend, C2C, JoinSys;
    public GameObject networkedClient;
    string currentPlayerName = "";
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
            else if (go.name == "pnlChat")
                pnlChat = go;
            else if (go.name == "btnSendPrefixMsg")
                btnSendPrefixMsg = go;
            else if (go.name == "btnJoinObserver")
                btnJoinObserver = go;
            else if (go.name == "ddlPlayer")
            {
                ddlPlayer = go;
            }
            else if (go.name == "PMsgSend")
            {
                PMsgSend = go;
            }
            else if (go.name == "JoinSys")
            {
                JoinSys = go;
            }
            else if (go.name == "C2C")
            {
                C2C = go;
            }
            else if (go.name == "MsgSend")
            {
                MsgSend = go;
            }
            else if (go.name == "LoginSys")
            {
                LoginSys = go;
            }
            else if (go.name == "btnCSend")
            {
                btnCSend = go;
            }
            else if (go.name == "txtCMsg")
            {
                txtCMsg = go;
            }
            


        }
        btnSubmit.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        btnJoin.GetComponent<Button>().onClick.AddListener(JoinButtonPressed);
        btnJoinObserver.GetComponent<Button>().onClick.AddListener(ObserveButtonPressed);
        btnReplay.GetComponent<Button>().onClick.AddListener(ReplayButtonPressed);
        btnSend.GetComponent<Button>().onClick.AddListener(SendButtonPressed);
        btnSendPrefixMsg.GetComponent<Button>().onClick.AddListener(SendPrefButtonPressed);
        btnCSend.GetComponent<Button>().onClick.AddListener(SendClientButtonPressed);
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
        currentPlayerName = name;
        lblInfo.GetComponent<Text>().text ="Logged in user: "+ name;
    }
    public void LoadPlayer(List<string> list)
    {
        ddlPlayer.GetComponent<Dropdown>().ClearOptions();
        ddlPlayer.GetComponent<Dropdown>().AddOptions(list);
    }
    public void ReplayButtonPressed()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SendClientButtonPressed()
    {
        string msg = ClientToServerSignifiers.SendClientMsg + "," + ddlPlayer.GetComponent<Dropdown>().options[ddlPlayer.GetComponent<Dropdown>().value].text.ToString()+","+ txtCMsg.GetComponent<InputField>().text+","+currentPlayerName;
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
        Debug.Log("client send " + msg);
    }
    public void SendPrefButtonPressed()
    {
        string msg = ClientToServerSignifiers .SendPrefixMsg+","+ ddlMsg.GetComponent<Dropdown>().options[ddlMsg.GetComponent<Dropdown>().value].text.ToString() + "," + currentPlayerName;
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
        Debug.Log("sendpre " + msg);
    }
    public void SendButtonPressed()
    {
        string msg = ClientToServerSignifiers .SendMsg+","+ txtMsg.GetComponent<InputField>().text + "," + currentPlayerName;
        Debug.Log("msg:" + msg);
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
        Debug.Log("send " + msg);
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
        string msg = ClientToServerSignifiers.JoinGammeRoomQueue+","+currentPlayerName;
        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
      //  ChangeState(GameStates.TicTacToe);
    }
    public void ObserveButtonPressed()
    {
        string msg = ClientToServerSignifiers.JoinAsObserver + ","+currentPlayerName;
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
        //LoginSys.SetActive(false);
        //JoinSys.SetActive(false);
        btnJoin.SetActive(false);
        btnJoinObserver.SetActive(false);
        btnSubmit.SetActive(false);
        chkCreate.SetActive(false);
        txtPwd.SetActive(false);
        txtUserId.SetActive(false);
        lblU.SetActive(false);
        lblP.SetActive(false);
        //btnPlay.SetActive(false);
        //txtMsg, btnSend, ddlMsg, chatBox, btnSendPrefixMsg
        //MsgSend.SetActive(false);
        txtMsg.SetActive(false);
        btnSend.SetActive(false);
        //PMsgSend.SetActive(false);
        ddlMsg.SetActive(false);
        btnSendPrefixMsg.SetActive(false);
        //C2C.SetActive(false);
        ddlPlayer.SetActive(false);
        btnCSend.SetActive(false);
        txtCMsg.SetActive(false);
        chatBox.SetActive(false);
        pnlChat.SetActive(false);
        
       
        if (newState == GameStates.LoginMenu)
        {
            // LoginSys.SetActive(true);
            btnSubmit.SetActive(true);
            chkCreate.SetActive(true);
            txtPwd.SetActive(true);
            txtUserId.SetActive(true);
            lblU.SetActive(true);
            lblP.SetActive(true);
        }
        else if (newState == GameStates.MainMenu)
        {
            //JoinSys.SetActive(true);
            btnJoin.SetActive(true);
            btnJoinObserver.SetActive(true);
            //MsgSend.SetActive(true);
            txtMsg.SetActive(true);
            btnSend.SetActive(true);
            //PMsgSend.SetActive(true);
            ddlMsg.SetActive(true);
            btnSendPrefixMsg.SetActive(true);
            //C2C.SetActive(true);
            ddlPlayer.SetActive(true);
            btnCSend.SetActive(true);
            txtCMsg.SetActive(true);
            chatBox.SetActive(true);
            pnlChat.SetActive(true);
            
           
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
            //MsgSend.SetActive(true);
             txtMsg.SetActive(true);
            btnSend.SetActive(true);
            //PMsgSend.SetActive(true);
            btnSendPrefixMsg.SetActive(true);
            ddlMsg.SetActive(true);
            //C2C.SetActive(true);
            ddlPlayer.SetActive(true);
            btnCSend.SetActive(true);
            txtCMsg.SetActive(true);
            chatBox.SetActive(true);
            pnlChat.SetActive(true);

            
        }
        else if (newState==GameStates.Observer)
        {
            chatBox.SetActive(true);
            pnlChat.SetActive(true);
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