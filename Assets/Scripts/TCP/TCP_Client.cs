using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.Collections.Generic;

public class TCP_Client : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    private CancellationTokenSource cts;
    public int discoveryPort = 8888;
    private Thread serverlistenerThread;
    private Thread discoveryThread;
    private Thread connectionThread;
    private string hostIP;
    private Dictionary<string, Action> functionMap = new Dictionary<string, Action>();



    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        discoveryThread = new Thread(new ThreadStart(StartDiscovery));
        discoveryThread.Start();
    }

    void StartDiscovery()
    {
        UdpClient udpClient = new UdpClient();
        udpClient.EnableBroadcast = true;

        Debug.Log("Recherche de services sur le r�seau local...");

        // Envoyer une demande de d�couverte en diffusion
        byte[] discoverData = Encoding.UTF8.GetBytes("DISCOVER_MY_SERVICE");
        udpClient.Send(discoverData, discoverData.Length, new IPEndPoint(IPAddress.Broadcast, discoveryPort));

        // Attendre la r�ponse du serveur de d�couverte
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
        byte[] responseData = udpClient.Receive(ref serverEndPoint);
        string response = Encoding.UTF8.GetString(responseData);

        // Analyser la r�ponse pour obtenir l'adresse IP de l'h�te
        if (response.StartsWith("HOST_IP:"))
        {
            hostIP = response.Substring("HOST_IP:".Length);
            connectionThread = new Thread(new ThreadStart(ConnectToServer));
            connectionThread.Start();
        }
        else
        {
            Debug.Log("pas d'IP trouver");
            Debug.Log(response.Substring("HOST_IP:".Length));
        }
    }

    private void ConnectToServer()
    {
        
        Debug.Log("Service trouv� � l'adresse IP : " + hostIP);

        // Connectez-vous � l'h�te en utilisant l'adresse IP trouv�e
        // Impl�mentez votre code de connexion ici

        // Connexion au serveur TCP
        client = new TcpClient();
        client.Connect(hostIP, discoveryPort);
        stream = client.GetStream();
        
        Debug.Log("Connect� au serveur !");

        // Envoi de donn�es au serveur
        string message = "Bonjour serveur !";
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        stream.Write(messageBytes, 0, messageBytes.Length);

        // Lecture de la r�ponse du serveur
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Debug.Log("R�ponse du serveur : " + dataReceived);

        serverlistenerThread = new Thread(new ThreadStart(Listener));
        serverlistenerThread.Start();
    }

    private void Listener()
    {
        Debug.Log("D�but de l'�coute.");
        // Tant que la connexion est ouverte, �couter les messages du serveur
        while (client.Connected)
        {
            try
            {
                // Lecture des donn�es envoy�es par le serveur
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                // V�rifier si des donn�es ont �t� lues
                if (bytesRead > 0)
                {
                    // Convertir les donn�es en cha�ne de caract�res et afficher le message
                    string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Debug.Log("Message perso re�u du serveur : " + dataReceived);
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

    public void Interpreter(string commande)
    {
        string[] parts = commande.Split(':');
        if(parts[0] == "FUNCTION_NAME" && functionMap.ContainsKey(parts[1]))
        {
            functionMap[parts[1]].Invoke();
        }
    }

    public void SenderCallFunction(string functionName)
    {
        if (stream != null && client != null && client.Connected)
        {
            string message = "FUNCTION_NAME:";
            byte[] messageBytes = Encoding.UTF8.GetBytes(message+functionName);
            stream.Write(messageBytes, 0, messageBytes.Length);
            Debug.Log("Message envoy� au client : " + message+functionName);
        }
        else
        {
            Debug.LogWarning("Impossible d'envoyer le message. La connexion client est ferm�e.");
        }
    }

    void OnDestroy()
    {
        // Fermeture de la connexion
        stream.Close();
        client.Close();
    }
}