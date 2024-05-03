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
        // Création du serveur TCP
        listener = new TcpListener(IPAddress.Any, 7777);
        listener.Start();

        Debug.Log("Serveur démarré. En attente de connexion...");

        // Attente de connexion du client
        client = listener.AcceptTcpClient();
        stream = client.GetStream();

        Debug.Log("Client connecté !");

        // Lancement d'une boucle pour écouter les messages du client
        while (true)
        {
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Debug.Log("Données reçues du client : " + dataReceived);

            // Envoyer une réponse au client
            string message = "Message reçu par le serveur bigup!";
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