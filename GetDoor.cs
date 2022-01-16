using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
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
		public IMyDoor GetDoor() {
			var doorName = CommandLine.Argument(1);
			var door = GridTerminalSystem.GetBlockWithName(doorName) as IMyDoor;

			if (door == null) {
				CurrentBColor = Color.Orange;
				CurrentFColor = Color.Black;
				Me.WriteLineAndData($"WARNING: DOOR \"{doorName}\" DOES NOT EXIST");
				return null;
			} else {
				return door;
			}
		}
	}
}
