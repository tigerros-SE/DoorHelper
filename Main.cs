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
	partial class Program {
		public void Main(string argument, UpdateType updateSource) {
			if ((updateSource & (UpdateType.Update10)) != 0) {
				foreach (var pair in LockTimers.ToList()) {
					if (pair.Value.Finished) {
						pair.Key.Enabled = false;
						LockTimers.Remove(pair.Key);
					}
				}

				while (SystemListener.HasPendingMessage) {
					var message = SystemListener.AcceptMessage();
					var split = message.Data.ToString().Split('|');
					var type = split[0];
					var value = split[1];

					if (type == "ALERT") {
						Surface.Writeln($"INFO: ALERT {(value != "1" ? "DE" : "")}ACTIVATED");

						if (value == "1") {
							if (Surface.BackgroundColor == Color.Yellow && Surface.FontColor == Color.Black) {
								Surface.BackgroundColor = CurrentBColor;
								Surface.FontColor = CurrentFColor;
							} else {
								Surface.BackgroundColor = Color.Yellow;
								Surface.FontColor = Color.Black;
							}
						} else {
							Surface.BackgroundColor = CurrentBColor;
							Surface.FontColor = CurrentFColor;
						}
					}
				}
			} else if (CommandLine.TryParse(argument)) {
				foreach (var key in Switches.Keys.ToList()) {
					Switches[key] = false;
				}

				CommandLine.HandleSwitches(Me, Switches);
				CommandLine.HandleArguments(Me, Arguments);
			}
		}
	}
}
