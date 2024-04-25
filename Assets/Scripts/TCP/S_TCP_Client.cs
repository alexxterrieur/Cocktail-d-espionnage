using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class S_TCP_Client : MonoBehaviour
{
    private TcpClient _client;
    private NetworkStream _stream;
    private CancellationTokenSource _cts;
    public int _discoveryPort = 8888;
    private Thread _serverlistenerThread;
    private Thread _discoveryThread;
    private Thread _multiDiscoveryThread;
    private Thread _connectionThread;
    private string _hostIP;
    private Dictionary<string, Action> _functionMap = new Dictionary<string, Action>();
    private string _loadSceneName = null;
    [SerializeField] private List<string> _hostsIP;

    private bool _megamindWin = false;
    public bool MegamindWin { get { return _megamindWin; } set { _megamindWin = value; } }
    public static S_TCP_Client _TCP_Instance { get; private set; }

    public List<string> HostsList => _hostsIP;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (_TCP_Instance == null)
        {
            _TCP_Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        _functionMap.Add("MegaMindWin", MegaMindWin);
        _functionMap.Add("ShakerWin", ShakerWin);


        DontDestroyOnLoad(this.gameObject);
        _discoveryThread = new Thread(new ThreadStart(StartDiscovery));
        _discoveryThread.Start();
    }

    private void FixedUpdate()
    {
        if (_loadSceneName != null && SceneManager.GetSceneByName(_loadSceneName) != null)
        {
            SceneManager.LoadScene(_loadSceneName);
            _loadSceneName = null;
        }
    }

    void StartMultiDiscovery()
    {
        UdpClient udpClient = new UdpClient();
        udpClient.EnableBroadcast = true;

        Debug.Log("Recherche de services sur le r�seau local...");

        // Envoyer une demande de d�couverte en diffusion
        byte[] discoverData = Encoding.UTF8.GetBytes("DISCOVER_MY_SERVICE");
        udpClient.Send(discoverData, discoverData.Length, new IPEndPoint(IPAddress.Broadcast, _discoveryPort));

        _hostsIP.Clear();

        while (true)
        {
            // Attendre la r�ponse du serveur de d�couverte
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] responseData = udpClient.Receive(ref serverEndPoint);
            string response = Encoding.UTF8.GetString(responseData);

            // Analyser la r�ponse pour obtenir l'adresse IP de l'h�te
            if (response.StartsWith("HOST_IP:"))
            {
                _hostsIP.Add(response.Substring("HOST_IP:".Length));   
            }
            //else
            //{
            //    Debug.Log("pas d'IP trouver");
            //    Debug.Log(response.Substring("HOST_IP:".Length));
            //}
        }
    }

    void StartDiscovery()
    {
        UdpClient udpClient = new UdpClient();
        udpClient.EnableBroadcast = true;

        Debug.Log("Recherche de services sur le r�seau local...");

        // Envoyer une demande de d�couverte en diffusion
        byte[] discoverData = Encoding.UTF8.GetBytes("DISCOVER_MY_SERVICE");
        udpClient.Send(discoverData, discoverData.Length, new IPEndPoint(IPAddress.Broadcast, _discoveryPort));

        // Attendre la r�ponse du serveur de d�couverte
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
        byte[] responseData = udpClient.Receive(ref serverEndPoint);
        string response = Encoding.UTF8.GetString(responseData);

        // Analyser la r�ponse pour obtenir l'adresse IP de l'h�te
        if (response.StartsWith("HOST_IP:"))
        {
            _hostIP = response.Substring("HOST_IP:".Length);
        }
        else
        {
            Debug.Log("pas d'IP trouver");
            Debug.Log(response.Substring("HOST_IP:".Length));
        }
    }

    public void ConnectToServer(string ip)
    {
        _hostIP = ip;
        _connectionThread = new Thread(new ThreadStart(ConnectToServer));
        _connectionThread.Start();

        if (_discoveryThread != null)
        {
            _discoveryThread.Abort();
        }
        if (_multiDiscoveryThread != null)
        {
            _multiDiscoveryThread.Abort();
        }
    }

    public void SearchServer()
    {
        if (_discoveryThread != null)
        {
            _discoveryThread.Abort();
        }
        _multiDiscoveryThread = new Thread(new ThreadStart(StartMultiDiscovery));
        _multiDiscoveryThread.Start();
    }

    private void ConnectToServer()
    {
        
        Debug.Log("Service trouv� � l'adresse IP : " + _hostIP);

        // Connectez-vous � l'h�te en utilisant l'adresse IP trouv�e
        // Impl�mentez votre code de connexion ici

        // Connexion au serveur TCP
        _client = new TcpClient();
        _client.Connect(_hostIP, _discoveryPort);
        _stream = _client.GetStream();
        
        Debug.Log("Connect� au serveur !");

        // Envoi de donn�es au serveur
        string message = "Bonjour serveur !";
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        _stream.Write(messageBytes, 0, messageBytes.Length);

        // Lecture de la r�ponse du serveur
        byte[] buffer = new byte[1024];
        int bytesRead = _stream.Read(buffer, 0, buffer.Length);
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Debug.Log("R�ponse du serveur : " + dataReceived);

        _serverlistenerThread = new Thread(new ThreadStart(Listener));
        _serverlistenerThread.Start();
    }

    private void Listener()
    {
        Debug.Log("D�but de l'�coute.");
        // Tant que la connexion est ouverte, �couter les messages du serveur
        while (_client.Connected)
        {
            try
            {
                // Lecture des donn�es envoy�es par le serveur
                byte[] buffer = new byte[1024];
                int bytesRead = _stream.Read(buffer, 0, buffer.Length);

                // V�rifier si des donn�es ont �t� lues
                if (bytesRead > 0)
                {
                    // Convertir les donn�es en cha�ne de caract�res et afficher le message
                    string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Debug.Log("Message perso re�u du serveur : " + dataReceived);
                    if(dataReceived.StartsWith("FUNCTION_NAME:") || dataReceived.StartsWith("LOAD_SCENE:"))
                        Interpreter(dataReceived);
                }
            }
            catch (Exception ex)
            {
                // G�rer toute exception survenue pendant la lecture
                Debug.LogError("Erreur lors de la lecture des donn�es du serveur : " + ex.Message);
                break; // Sortir de la boucle si une erreur survient
            }
        }

        // Une fois la connexion ferm�e, afficher un message
        Debug.Log("Connexion au serveur ferm�e. Arr�t de l'�coute.");
    }

    private void Interpreter(string commande)
    {
        string[] parts = commande.Split(':');
        Debug.Log(parts[0]);
        Debug.Log(parts[1]);
        if(parts[0] == "FUNCTION_NAME" && _functionMap.ContainsKey(parts[1]))
        {
            _functionMap[parts[1]].Invoke();
        }
        if (parts[0] == "LOAD_SCENE")
        {
            _loadSceneName = parts[1];
        }
    }

    public void SenderCallFunction(string functionName)
    {
        if (_stream != null && _client != null && _client.Connected)
        {
            string message = "FUNCTION_NAME:";
            byte[] messageBytes = Encoding.UTF8.GetBytes(message+functionName);
            _stream.Write(messageBytes, 0, messageBytes.Length);
            Debug.Log("Message envoy� au client : " + message+functionName);
        }
        else
        {
            Debug.LogWarning("Impossible d'envoyer le message. La connexion client est ferm�e.");
        }
    }

    public void SenderLoadScene(string sceneName)
    {
        if (_stream != null && _client != null && _client.Connected)
        {
            string message = "LOAD_SCENE:";
            byte[] messageBytes = Encoding.UTF8.GetBytes(message + sceneName);
            _stream.Write(messageBytes, 0, messageBytes.Length);
            Debug.Log("Message envoy� au client : " + message + sceneName);
        }
        else
        {
            Debug.LogWarning("Impossible d'envoyer le message. La connexion client est ferm�e.");
        }
    }

    public void LoadMegaMind()
    {
        SenderLoadScene("MegaMind");
    }

    private void MegaMindWin()
    {
        Debug.Log("MegaMind WIN");
        _megamindWin = true;
    }

    public void LoadShaker()
    {
        SenderLoadScene("Shaker");
    }

    private void ShakerWin()
    {
        Debug.Log("Shaker WIN");
    }

    void OnDestroy()
    {
        // Fermeture de la connexion
        if(_stream != null)
        {
            _stream.Close();
        }
        if (_client != null)
        {
            _client.Close();
        }
        if (_discoveryThread != null)
        {
            _discoveryThread.Abort();
        }
        if (_multiDiscoveryThread != null)
        {
            _multiDiscoveryThread.Abort();
        }
        if (_serverlistenerThread != null)
        {
            _serverlistenerThread.Abort();
        }
    }
}