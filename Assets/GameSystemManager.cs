using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameSystemManager : MonoBehaviour
{
    GameObject btnSubmit, txtUserId, txtPwd, chkCreate;
   
    public GameObject networkedClient;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allobjects = FindObjectsOfType<GameObject>();
        foreach (GameObject go in allobjects)
        {
            if(go.name=="btnLogin")
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
            
            
        }
        btnSubmit.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        chkCreate.GetComponent<Toggle>().onValueChanged.AddListener(CreateToggleChanged);

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
    public void CreateToggleChanged(bool newValue)
    {

    }

}
