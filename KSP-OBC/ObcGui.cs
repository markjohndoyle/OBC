using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace KSPOBC
{
	
	public class ObcGui {
		private Rect windowPos = new Rect(100, 100, 100, 100);
        private Vessel vessel;
        
        public Vessel hostVessel {
            get { return vessel; }
            set { vessel = value; }
        }
		private String verticalSpeed = "--";
		
		private void createObcWindow(int windowID) {
	    	GUIStyle mySty = new GUIStyle(GUI.skin.window); 
	   		mySty.normal.textColor = mySty.focused.textColor = Color.white;
	  		mySty.hover.textColor = mySty.active.textColor = Color.yellow;
	  		mySty.onNormal.textColor = mySty.onFocused.textColor = mySty.onHover.textColor = mySty.onActive.textColor = Color.green;
	  		mySty.padding = new RectOffset(8, 8, 8, 8);
	
	        GUILayout.BeginVertical();
			GUILayout.Label(vessel.GetName(), GUILayout.ExpandWidth(true));
			GUILayout.Label(verticalSpeed, GUILayout.ExpandWidth(true));
			GUILayout.EndVertical();
	
	        //DragWindow makes the window draggable. The Rect specifies which part of the window it can by dragged by, and is 
	        //clipped to the actual boundary of the window. You can also pass no argument at all and then the window can by
	        //dragged by any part of it. Make sure the DragWindow command is AFTER all your other GUI input stuff, or else
	        //it may "cover up" your controls and make them stop responding to the mouse.
	        GUI.DragWindow(new Rect(0, 0, 10000, 20));
		}


		public void updateValues() {
                verticalSpeed = vessel.verticalSpeed.ToString();
		}
			
		public void drawGui() {
            windowPos = GUILayout.Window(1, windowPos, createObcWindow, "OBC display", GUILayout.MinWidth(100));
            updateValues();
		}
	}
	
	
}

