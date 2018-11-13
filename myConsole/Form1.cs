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
using System.IO;

namespace myConsole
{
	public partial class console : Form
	{
		public console()
		{
			InitializeComponent();

			sub.Show();

			loadScript();
			loadMods();
			timer1.Start();

			this.ActiveControl = textbox_command;
			script.Options.DebugPrint = s => printMessage(s);
			richTextBox_message.AppendText("> ");
		}

		Script script = new Script();
		List<string> prevCommands = new List<string>();
		int prevCommandsIter = 0;
		Form2 sub = new Form2();

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
				richTextBox_message.Text = "> ";
			}
			else // execute lua
			{
				try
				{
					script.DoString(me.Text);
				}
				catch (Exception err)
				{
					printMessage("Error: " + err.Message, Color.Red);
				}
				//viewBox.AppendText(res.Number + "\r\n");
			}
			richTextBox_message.AppendText("\r\n> ");
			me.Text = "";
		}

		public void printMessage(string message)
		{
			richTextBox_message.AppendText(message + "\r\n");
			richTextBox_message.ScrollToCaret();
		}
		public void printMessage(string message, Color color)
		{
			Color temp = richTextBox_message.SelectionColor;
			richTextBox_message.SelectionColor = color;
			richTextBox_message.AppendText(message + "\r\n");
			richTextBox_message.SelectionColor = temp;
			richTextBox_message.ScrollToCaret();
		}

		private void loadScript()
		{
			//script.Globals["changeLocation"] = (Action<int, int>)((x, y) => richTextBox_message.Location = new Point(x, y));
			//script.Globals["createForm"] = (Action)(() => { sub = new Form2(); sub.Show(); });
			script.Globals["newMyClass"] = (Func<string, int, int, Object>)((string msg, int x, int y) =>
			{
				MyClass bar = new MyClass();
				bar.setText(msg);
				bar.setPos(x, y);
				bar.Parent = sub;
				bar.Show();
				return bar;
			});

			UserData.RegisterType<MyClass>();
			string filepath = @"res";
			DirectoryInfo d = new DirectoryInfo(filepath);

			foreach (var file in d.GetFiles("*.lua"))
			{
				try
				{
					script.DoFile(file.FullName);
				}
				catch(InterpreterException luaExcept)
				{
					printMessage("LUA ERROR: " + luaExcept.Message, Color.Red);
				}
				catch (Exception exception)
				{
					printMessage("C# ERROR: " + exception, Color.Red);
				}
			}

			try
			{
				Table defines = script.Globals["defines"] as Table;
				Table events = defines["events"] as Table;

				timer1.Tick += (s, e) =>
				{
					foreach (var func in (events["on_tick"] as Table).Values)
					{
						script.Call(func);
					}
				};
			}
			catch (InterpreterException luaExcept)
			{
				printMessage("LUA ERROR: " + luaExcept.Message, Color.Red);
			}
			catch (Exception exception)
			{
				printMessage("C# ERROR: " + exception, Color.Red);
			}
		}

		private void loadMods()
		{
			UserData.RegisterType<MyClass>();
			string filepath = @"mods";
			DirectoryInfo d = new DirectoryInfo(filepath);

			foreach (var mods in d.GetDirectories())
			{
				try
				{
					script.DoFile(mods.GetFiles("control.lua")[0].FullName);
				}
				catch (InterpreterException luaExcept)
				{
					printMessage(string.Format("LUA ERROR: " + "Error! Fail to load mod: \"{0}\" \r\n{1}", mods.Name, luaExcept.Message), Color.Red);
				}
			}
		}
	}

	class MyClass : Label
	{
		public MyClass()
		{
			this.Font = new Font("DejaVu Sans Mono", 12, FontStyle.Bold);
			this.AutoSize = true;
		}
		public void setPos(int x, int y)
		{
			this.Location = new Point(x, y);
		}
		public void setText(string msg)
		{
			this.Text = msg;
		}
	}
}
