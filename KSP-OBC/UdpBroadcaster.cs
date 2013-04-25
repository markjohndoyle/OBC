using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace KSP_OBC {

    /**
     * UDP send link for transmitting raw data out of the game engine.
     */
    class UdpBroadcaster : MonoBehaviour, SendLink {

        private bool broadcasting = false;
        private Socket udpSocket;

        UdpBroadcaster() {
            udpSocket = new Socket(SocketType.Dgram, ProtocolType.Udp);
        }

        public void send(byte[] rawOut) {
            udpSocket.Send(rawOut);
        }

        void OnDestroy() {
            Debug.Log("Stopping thread and closing UDP socket");
            broadcasting = false;
            udpSocket.Close();
        }
    }
}
