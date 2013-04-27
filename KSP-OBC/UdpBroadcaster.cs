using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace KSP_OBC {

    /**
     * UDP send link for transmitting raw data out of the game engine.
     */
    class UdpBroadcaster : MonoBehaviour, SendLink {
        private Socket udpSocket;
        private IPEndPoint ipEndpoint;
        private IPAddress broadcast;

        public UdpBroadcaster() {
            udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            broadcast = IPAddress.Parse("127.0.0.1");
            ipEndpoint = new IPEndPoint(broadcast, 12509);
        }

        public void send(byte[] rawOut) {
            print("sending " + rawOut);
            udpSocket.SendTo(rawOut, ipEndpoint);
        }

        void OnDestroy() {
            Debug.Log("Stopping thread and closing UDP socket");
            udpSocket.Close();
        }
    }
}
