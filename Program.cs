using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Windows.Input;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;
using IngameScript.TiCommons.Extensions;

namespace IngameScript.DoorHelper {
	partial class Program : MyGridProgram {
		Dictionary<IMyDoor, Timer> LockTimers = new Dictionary<IMyDoor, Timer>();
		IMyBroadcastListener SystemListener;
		IMyTextSurface Surface;
		MyCommandLine CommandLine = new MyCommandLine();
		Dictionary<string, Action> Arguments = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase);
		Dictionary<string, bool> Switches = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase) {
			["open"] = false,
			["close"] = false,
			["lock"] = false
		};

		private Color _currentBColor;
		public Color CurrentBColor {
			get { return _currentBColor; }
			set {
				_currentBColor = value;
				Surface.BackgroundColor = value;
			}
		}

		private Color _currentFColor;
		public Color CurrentFColor {
			get { return _currentFColor; }
			set {
				_currentFColor = value;
				Surface.FontColor = value;
			}
		}


		string Description = @"Arguments:
door - Opens/closes the door according to the switches. Must be followed by ""DoorName""
""DoorName"" - The name of the door
		 
Switches:
open - Opens the door
close - Closes the door
lock - Locks the door after opening/closing. The door is always unlocked before opening/closing";

		public Program() {
			Runtime.UpdateFrequency = UpdateFrequency.Update10;
			SystemListener = IGC.RegisterBroadcastListener("SYSTEM");

			Surface = Me.GetSurface(0);
			Surface.ContentType = ContentType.TEXT_AND_IMAGE;
			Surface.FontSize = 1;
			Surface.Alignment = TextAlignment.LEFT;

			CurrentBColor = Color.Green;
			CurrentFColor = Color.White;
			
			Surface.WriteLine("DOOR HANDLER STATUS: ONLINE", false);
			Arguments["open|->*"] = Open;
			Arguments["close|->*"] = Close;
		}
	}
}
