
#region ================== Copyright (c) 2007 Pascal vd Heiden

/*
 * Copyright (c) 2007 Pascal vd Heiden, www.codeimp.com
 * This program is released under GNU General Public License
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 */

#endregion

#region ================== Namespaces

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using CodeImp.DoomBuilder.Properties;
using System.IO;
using CodeImp.DoomBuilder.IO;
using System.Collections;

#endregion

namespace CodeImp.DoomBuilder.Controls
{
	internal class ActionManager : IDisposable
	{
		#region ================== Constants

		#endregion

		#region ================== Variables

		// Actions
		private Dictionary<string, Action> actions;
		
		// Disposing
		private bool isdisposed = false;

		#endregion

		#region ================== Properties

		public Action this[string action] { get { return actions[action]; } }
		public bool IsDisposed { get { return isdisposed; } }

		#endregion

		#region ================== Constructor / Disposer

		// Constructor
		public ActionManager()
		{
			// Initialize
			actions = new Dictionary<string, Action>();

			// Load all actions
			LoadActions();
			
			// We have no destructor
			GC.SuppressFinalize(this);
		}

		// Diposer
		public void Dispose()
		{
			// Not already disposed?
			if(!isdisposed)
			{
				// Clean up
				
				// Done
				isdisposed = true;
			}
		}

		#endregion

		#region ================== Actions

		// This loads all actions
		private void LoadActions()
		{
			Stream actionsdata;
			StreamReader actionsreader;
			Configuration cfg;
			string name, title, desc;

			// Get a stream from the resource
			actionsdata = General.ThisAssembly.GetManifestResourceStream("CodeImp.DoomBuilder.Resources.Actions.cfg");
			actionsreader = new StreamReader(actionsdata, Encoding.ASCII);
			
			// Load configuration from stream
			cfg = new Configuration();
			cfg.InputConfiguration(actionsreader.ReadToEnd());

			// Done with the resource
			actionsreader.Dispose();
			actionsdata.Dispose();

			// Go for all objects in the configuration
			foreach(DictionaryEntry a in cfg.Root)
			{
				// Get action properties
				name = a.Key.ToString();
				title = cfg.ReadSetting(name + ".title", "[" + name + "]");
				desc = cfg.ReadSetting(name + ".description", "");
				
				// Create an action
				actions.Add(name, new Action(title, desc));
			}
		}

		// This checks if a given action exists
		public bool Exists(string action)
		{
			return actions.ContainsKey(action);
		}

		#endregion

		#region ================== Shortcut Keys

		// Removes all shortcut keys
		public void RemoveShortcutKeys()
		{
			// Clear all keys
			foreach(KeyValuePair<string, Action> a in actions)
				a.Value.SetShortcutKey(0);
		}
		
		// This will call the associated action for a keypress
		public void InvokeByKey(int key)
		{
			// Go for all actions
			foreach(KeyValuePair<string, Action> a in actions)
			{
				// This action is associated with this key?
				if(a.Value.ShortcutKey == key)
				{
					// Invoke action
					a.Value.Invoke();
				}
			}
		}
		
		#endregion
	}
}
