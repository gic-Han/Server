using System.Net;
using System.Net.Sockets;

namespace Server;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        start.Click += start_Click;
    }

    private void start_Click(object? sender, EventArgs e)
    {
        start.Click -= start_Click;
        server_state.Text = "Server Starting...";
        Thread listeningThread = new Thread(ServerOpen)
        {
            IsBackground = true
        };
        listeningThread.Start();
    }


    static TcpListener? listener;
    const int Port = 50001;
    int id;
    Queue<int> waitlist = new Queue<int>();

    public void ServerOpen()
    {
        listener = new TcpListener(IPAddress.Any, Port);
        listener.Start();
        server_state.Text = "Server Running";
        id = 0;

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Thread clientThread = new Thread(() => HandleClient(client, id))
            {
                IsBackground = true
            };
            clientThread.Start();
            client_list.Items.Add("client" + id);
            client_state.Text = "client " + id + " entered";
            id++;
        }
    }
    
    private void HandleClient(TcpClient client, int i)
    {
        Client _client = new Client(client, i);

        if(waitlist.Count > 0)
        {
            int tmp = waitlist.Dequeue();
            if(ClientManage.client_list.ContainsKey(tmp))
            {
                _client.roomid = tmp;
                ClientManage.client_list[_client.roomid].roomid = i;
                ClientManage.client_list[_client.roomid].SendMessage(250);
            }
            else
            {
                waitlist.Enqueue(i);
            }
        }
        else
            waitlist.Enqueue(i);

        int recv;

        while ((recv = _client.StreamRead()) > -1)
        {
            if(ClientManage.client_list.ContainsKey(_client.roomid))
            {
                ClientManage.client_list[_client.roomid].SendMessage((byte)recv);
            }

            if(recv == 255)
                break;
        }

        _client.SendMessage(254);
        client_state.Text = "client " + _client.id + " has left  " + i;
        client_list.Items.RemoveAt(client_list.FindString("client" + _client.id));
        ClientManage.client_list.Remove(_client.id);
        _client.ClientClose();
    }
}

public static class ClientManage
{
    public static Dictionary<int, Client> client_list = [];
}

public class Client
{
    public int id, roomid;
    private TcpClient client;
    private NetworkStream stream;

    public Client(TcpClient client, int i)
    {
        id = i;
        roomid = -1;
        this.client = client;
        stream = this.client.GetStream();
        ClientManage.client_list.Add(id, this);
    }

    public int StreamRead()
    {
        return stream.ReadByte();
    }

    public void ClientClose()
    {
        stream.Close();
        client.Close();
    }

    public void SendMessage(byte l)
    {
        stream!.WriteByte(l);
    }
}