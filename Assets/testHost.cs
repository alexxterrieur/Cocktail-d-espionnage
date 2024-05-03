using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class testHost : MonoBehaviour
{
    private TcpListener listener;
    private TcpClient client;
    private NetworkStream stream;
    private Thread serverThread;

    void Start()
    {
        serverThread = new Thread(new ThreadStart(StartServer));
        serverThread.Start();
    }

    void StartServer()
    {
        // Cr�ation du serveur TCP
        listener = new TcpListener(IPAddress.Any, 7777);
        listener.Start();

        Debug.Log("Serveur d�marr�. En attente de connexion...");

        // Attente de connexion du client
        client = listener.AcceptTcpClient();
        stream = client.GetStream();

        Debug.Log("Client connect� !");

        // Lancement d'une boucle pour �couter les messages du client
        while (true)
        {
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Debug.Log("Donn�es re�ues du client : " + dataReceived);

            // Envoyer une r�ponse au client
            string message = "Message re�u par le serveur bigup!";
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            stream.Write(messageBytes, 0, messageBytes.Length);
        }
    }

    void OnDestroy()
    {
        // Fermeture de la connexion
        stream.Close();
        client.Close();
        listener.Stop();
        serverThread.Abort();
    }
}