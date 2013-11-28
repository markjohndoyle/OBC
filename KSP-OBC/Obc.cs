using System;//
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace KSP_OBC {

	///
	/// <remarks Mark Doyle />
	///
	public class OBC : PartModule {
        private static int counter = 0;
		private ObcGui gui;

        private TmGenerator tmGenerator;
			
		///
		/// Called when the part is started by Unity.
		///
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
            tmGenerator.vessel = vessel;
            tmGenerator.startTelemetry();
        }

        private void OnGUI() {
            if (HighLogic.LoadedSceneIsFlight) {
                gui.drawGui();
            }
        }

        public override void OnUpdate() {
            if (vessel != null) {
                CelestialBody cbody = vessel.mainBody;
                tmGenerator.verticalSpeed = vessel.verticalSpeed;
                tmGenerator.acceleration = vessel.acceleration;
                tmGenerator.altitude = vessel.GetHeightFromSurface();
                tmGenerator.angularMomentum = vessel.angularMomentum;
                tmGenerator.angularVelocity = vessel.angularVelocity;
                tmGenerator.atmDensity = vessel.atmDensity;
                tmGenerator.currentStage = vessel.currentStage;
                tmGenerator.latitude = cbody.GetLatitude(vessel.findWorldCenterOfMass());
                tmGenerator.longitude = cbody.GetLongitude(vessel.findWorldCenterOfMass());
            }
        }

	}
	
}
