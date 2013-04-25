using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace KSPOBC
{
	
	/**
	 * 
	 * Mark Doyle
	 * Johannes Klug
	 * 
	 * */
	public class OBC : PartModule {
		private ObcGui gui;
			
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
	    }
		

        private void OnGUI() {
            if (HighLogic.LoadedSceneIsFlight) {
                gui.drawGui();
            }
        }
		
	}
	
}
