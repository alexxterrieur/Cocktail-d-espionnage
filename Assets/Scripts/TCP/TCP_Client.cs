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

        Debug.Log("Recherche de services sur le réseau local...");

        // Envoyer une demande de découverte en diffusion
        byte[] discoverData = Encoding.UTF8.GetBytes("DISCOVER_MY_SERVICE");
        udpClient.Send(discoverData, discoverData.Length, new IPEndPoint(IPAddress.Broadcast, discoveryPort));

        // Attendre la réponse du serveur de découverte
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
        byte[] responseData = udpClient.Receive(ref serverEndPoint);
        string response = Encoding.UTF8.GetString(responseData);

        // Analyser la réponse pour obtenir l'adresse IP de l'hôte
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
        
        Debug.Log("Service trouvé à l'adresse IP : " + hostIP);

        // Connectez-vous à l'hôte en utilisant l'adresse IP trouvée
        // Implémentez votre code de connexion ici

        // Connexion au serveur TCP
        client = new TcpClient();
        client.Connect(hostIP, discoveryPort);
        stream = client.GetStream();
        
        Debug.Log("Connecté au serveur !");

        // Envoi de données au serveur
        string message = "Bonjour serveur !";
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        stream.Write(messageBytes, 0, messageBytes.Length);

        // Lecture de la réponse du serveur
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Debug.Log("Réponse du serveur : " + dataReceived);

        serverlistenerThread = new Thread(new ThreadStart(Listener));
        serverlistenerThread.Start();
    }

    private void Listener()
    {
        Debug.Log("Début de l'écoute.");
        // Tant que la connexion est ouverte, écouter les messages du serveur
        while (client.Connected)
        {
            try
            {
                // Lecture des données envoyées par le serveur
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                // Vérifier si des données ont été lues
                if (bytesRead > 0)
                {
                    // Convertir les données en chaîne de caractères et afficher le message
                    string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Debug.Log("Message perso reçu du serveur : " + dataReceived);
                }
            }
            catch (Exception ex)
            {
                // Gérer toute exception survenue pendant la lecture
                Debug.LogError("Erreur lors de la lecture des données du serveur : " + ex.Message);
                break; // Sortir de la boucle si une erreur survient
            }
        }

        // Une fois la connexion fermée, afficher un message
        Debug.Log("Connexion au serveur fermée. Arrêt de l'écoute.");
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
            Debug.Log("Message envoyé au client : " + message+functionName);
        }
        else
        {
            Debug.LogWarning("Impossible d'envoyer le message. La connexion client est fermée.");
        }
    }

    void OnDestroy()
    {
        // Fermeture de la connexion
        stream.Close();
        client.Close();
    }
}