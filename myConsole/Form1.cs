using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MoonSharp.Interpreter;

namespace myConsole
{
	public partial class console : Form
	{
		public console()
		{
			InitializeComponent();
			ActiveControl = commandBox;
			script.Options.DebugPrint = s => richTextBox_message.AppendText(s + "\r\n");
			richTextBox_message.AppendText("> ");

			//script.Globals["changeLocation"] = (Action<int, int>)((x, y) => richTextBox_message.Location = new Point(x, y));
			//script.Globals["createForm"] = (Action)(() => { sub = new Form2(); sub.Show(); });

			sub = new Form2();
			sub.Show();

			script.Globals["newLabel"] = (Action<string>)((string msg) => sub.foo(msg));
		}

		Script script = new Script();
		List<string> prevCommands = new List<string>();
		int prevCommandsIter = 0;
		Form2 sub;

		private void commandBox_KeyDown(object sender, KeyEventArgs e)
		{
			TextBox me = (TextBox)sender;
			if (e.KeyCode == Keys.Enter)
			{
				if (me.Text != "")
				{
					prevCommands.Add(me.Text);
					prevCommandsIter = prevCommands.Count;
				}
				richTextBox_message.AppendText(me.Text + "\r\n");
				DynValue res = new DynValue();
				try
				{
					res = script.DoString(me.Text);
				}
				catch (Exception err)
				{
					richTextBox_message.SelectionColor = Color.Red;
					richTextBox_message.AppendText("\r\nError: " + err.Message + "\r\n\r\n");
					richTextBox_message.SelectionColor = Color.Black;
				}
				//viewBox.AppendText(res.Number + "\r\n");
				richTextBox_message.AppendText("> ");
				richTextBox_message.ScrollToCaret();
				me.Text = "";
			}
			else if (e.KeyCode == Keys.Up)
			{
				if (prevCommands.Count == 0)
					return;
				me.Text = prevCommands[prevCommandsIter == 0 ? 0 : --prevCommandsIter];
				me.SelectionStart = me.TextLength;
			}
			else if (e.KeyCode == Keys.Down)
			{
				if (prevCommands.Count == 0)
					return;
				me.Text = prevCommandsIter >= prevCommands.Count - 1 ? "" : prevCommands[++prevCommandsIter];
				me.SelectionStart = me.TextLength;
			}
		}
	}
}
