using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class testClient : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;

    void Start()
    {
        // Connexion au serveur TCP
        client = new TcpClient();
        client.Connect("192.168.1.39", 7777);
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
    }

    void OnDestroy()
    {
        // Fermeture de la connexion
        stream.Close();
        client.Close();
    }
}