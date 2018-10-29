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
using MetroFramework.Forms;

namespace myConsole
{
	public partial class console : Form
	{
		public console()
		{
			InitializeComponent();
			script.Options.DebugPrint = s => richTextBox_message.AppendText(s + "\r\n");
			richTextBox_message.AppendText("> ");
			this.ActiveControl = textbox_command;

			sub.Show();

			timer1.Tick += (s, e) => { sub.label1.Text = obj.val.ToString(); };
			timer1.Start();

			//script.Globals["changeLocation"] = (Action<int, int>)((x, y) => richTextBox_message.Location = new Point(x, y));
			//script.Globals["createForm"] = (Action)(() => { sub = new Form2(); sub.Show(); });
			script.Globals["newLabel"] = (Action<string, int, int>)((string msg, int x, int y) => sub.foo(msg, x, y));

			UserData.RegisterType<MyClass>();
			DynValue luaObj = UserData.Create(obj);
			script.Globals.Set("obj", luaObj);
			script.DoFile(@"res\test.lua");
		}

		Script script = new Script();
		List<string> prevCommands = new List<string>();
		int prevCommandsIter = 0;
		Form2 sub = new Form2();
		MyClass obj = new MyClass();

		private void commandBox_KeyDown(object sender, KeyEventArgs e)
		{
			TextBox me = (TextBox)sender;
			if (e.KeyCode == Keys.Enter)
			{
				submitCommand(me);
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

		private void button_submit_Click(object sender, EventArgs e)
		{
			submitCommand(textbox_command);
		}

		private void submitCommand(TextBox me)
		{
			if (me.Text != "")
			{
				prevCommands.Add(me.Text);
				prevCommandsIter = prevCommands.Count;
			}
			richTextBox_message.AppendText(me.Text + "\r\n");

			if (me.Text == "clear") // magic words
			{
				richTextBox_message.Text = "";
			}
			else // execute lua
			{
				try
				{
					script.DoString(me.Text);
				}
				catch (Exception err)
				{
					printMessage("Error: " + err.Message + "\r\n\r\n", Color.Red);
				}
				//viewBox.AppendText(res.Number + "\r\n");
			}
			richTextBox_message.AppendText("> ");
			me.Text = "";
		}

		public void printMessage(string message)
		{
			richTextBox_message.AppendText("\r\n" + message);
		}
		public void printMessage(string message, Color color)
		{
			Color temp = richTextBox_message.ForeColor;
			richTextBox_message.SelectionColor = color;
			richTextBox_message.AppendText("\r\n" + message);
			richTextBox_message.SelectionColor = temp;
			richTextBox_message.ScrollToCaret();
		}
	}

	class MyClass
	{
		public MyClass()
		{
			val = 100;
		}

		public int val;

		public double CalcHypotenuse(double a, double b)
		{
			return Math.Sqrt(a * a + b * b);
		}

		public static int foo()
		{
			return 10;
		}
	}
}
