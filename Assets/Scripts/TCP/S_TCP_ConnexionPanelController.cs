using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_TCP_ConnexionPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private List<GameObject> _buttonList = new List<GameObject>();
    [SerializeField] private List<string> _previousIpList = new List<string>();

    public void RefreshServerList()
    {
        S_TCP_Client._TCP_Instance.SearchServer();
    }

    private void InstantiateButtons()
    {
        if(_previousIpList != S_TCP_Client._TCP_Instance.HostsList || S_TCP_Client._TCP_Instance.HostsList.Count != _buttonList.Count)
        {
            foreach (GameObject bt in _buttonList)
            {
                Destroy(bt);
            }
            _buttonList.Clear();

            foreach (string ip in S_TCP_Client._TCP_Instance.HostsList)
            {
                GameObject button = Instantiate(_buttonPrefab);
                button.transform.parent = transform;
                button.GetComponentInChildren<Text>().text = ip;
                _buttonList.Add(button);
                button.GetComponent<Button>().onClick.AddListener(() => S_TCP_Client._TCP_Instance.ConnectToServer(ip));
            }
            _previousIpList = S_TCP_Client._TCP_Instance.HostsList;
        }
        
        
    }

    private void FixedUpdate()
    {
        InstantiateButtons();
    }
}
