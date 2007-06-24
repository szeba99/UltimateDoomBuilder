
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

#endregion

namespace CodeImp.DoomBuilder.Controls
{
	internal class Action
	{
		#region ================== Constants

		public const string NEWMAP = "newmap";
		public const string OPENMAP = "openmap";
		public const string SAVEMAP = "savemap";
		public const string SAVEMAPAS = "savemapas";
		public const string SHOWOVERVIEW = "showoverview";
		
		#endregion
		
		#region ================== Variables

		// Description
		private string title;
		private string description;

		// Shortcut key
		private int key;

		// Delegate
		private List<ActionDelegate> delegates;
		
		#endregion

		#region ================== Properties

		public string Title { get { return title; } }
		public string Description { get { return description; } }
		public int ShortcutKey { get { return key; } }

		#endregion

		#region ================== Constructor / Disposer

		// Constructor
		public Action(string title, string description)
		{
			// Initialize
			this.title = title;
			this.description = description;
			this.delegates = new List<ActionDelegate>();
		}

		// Destructor
		~Action()
		{
			// Moo.
		}
		
		#endregion

		#region ================== Methods

		// This sets a new key for the action
		public void SetShortcutKey(int key)
		{
			// Make it so.
			this.key = key;
		}
		
		// This binds a delegate to this action
		public void Bind(ActionDelegate method)
		{
			delegates.Add(method);
		}

		// This removes a delegate from this action
		public void Unbind(ActionDelegate method)
		{
			delegates.Remove(method);
		}

		// This raises events for this action
		public void Invoke()
		{
			// Check if this action exists
			foreach(ActionDelegate ad in delegates) ad.Invoke();
		}

		#endregion
	}
}
