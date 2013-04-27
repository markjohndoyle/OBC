using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace KSP_OBC  {

    class TmGenerator {

        private Thread tmThread;
        private bool broadcasting = false;
        private Vessel vessel;
        private TelemetryFactory tmFactory = new BasicIdTelemetryFactory();
        private SendLink sendLink = new UdpBroadcaster();
        private double vspeed = 0;

        public Vessel tmVessel {
            get {return vessel;}
            set {vessel = value;}
        }

        public double verticalSpeed {
            set { vspeed = value; }
        }

        public void startTelemetry() {
            Debug.Log("Starting telemetry");
            broadcasting = true;
            tmThread = new Thread(new ThreadStart(sendTm));
            tmThread.Start();
        }

        private void stopSendingTelemetry() {
            broadcasting = false;
        }

        private void sendTm() {
            while (broadcasting) {
                if (HighLogic.LoadedSceneIsFlight) {
                    Debug.Log("Sending TM packet to sendlink");
                    byte[] buffer = BitConverter.GetBytes(vspeed);
                    sendLink.send(tmFactory.createTelemetry(BasicIdTelemetryFactory.VSPEED, buffer));
                }
                Debug.Log("Sleeping broadcast thread");
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
            Debug.Log("Broadcast thread is ending; exiting now");
        }

    } // end class

} // end namespace
