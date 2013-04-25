using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading;

namespace KSP_OBC
{

	/**
	 * 
	 * Mark Doyle
	 * Johannes Klug
	 * 
	 * */
	public class OBC : PartModule {
		private ObcGui gui;

        private SendLink sendLink;
        private Thread tmThread;
        private bool broadcasting = false;
			
		/**
		 *
		 * Called when the part is started by Unity.
		 * 
		 * */
	    public override void OnStart(StartState state) {
			print("OBC starting...");
            base.OnStart(state);
            gui = new ObcGui();
            gui.hostVessel = vessel;

            sendLink = new UdpBroadcaster();

            startTelemetry();
	    }

        public override void OnFixedUpdate() {
            base.OnFixedUpdate();
        }

        private void OnGUI() {
            if (HighLogic.LoadedSceneIsFlight) {
                gui.drawGui();
            }
        }


        private void startTelemetry() {
            tmThread = new Thread(new ThreadStart(sendTm));
            broadcasting = true;
            tmThread.Start();
        }

        private void stopSendingTelemetry() {
            broadcasting = false;
        }

        private void sendTm() {
            while (broadcasting) {
                byte[] buffer;
                buffer = BitConverter.GetBytes(vessel.verticalSpeed);
                sendLink.send(buffer);
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }
		
	}
	
}
