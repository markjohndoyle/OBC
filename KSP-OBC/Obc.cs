using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace KSP_OBC {

	/**
	 * Mark Doyle
	 */
	public class OBC : PartModule {
        private static int counter = 0;
		private ObcGui gui;

        private TmGenerator tmGenerator;
			
		/**
		 * Called when the part is started by Unity.
		 */
	    public override void OnStart(StartState state) {
			print("OBC starting...");
            if (state != StartState.Editor) {
                setupGui();
                setupTmGenerator();
            }
	    }

        private void setupGui() {
            gui = new ObcGui();
            gui.hostVessel = vessel;
        }

        private void setupTmGenerator() {
            print("Creating TM generator " + counter++);
            tmGenerator = new TmGenerator();
            tmGenerator.tmVessel = vessel;
            tmGenerator.startTelemetry();
        }

        private void OnGUI() {
            if (HighLogic.LoadedSceneIsFlight) {
                gui.drawGui();
            }
        }

        public override void OnUpdate() {
            if (vessel != null) {
                if (tmGenerator.tmVessel == null) {
                    print("Resetting vessel reference on tm generator");
                    tmGenerator.tmVessel = vessel;
                }
                tmGenerator.verticalSpeed = vessel.verticalSpeed;
            }
        }
	}
	
}
