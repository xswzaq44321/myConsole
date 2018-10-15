namespace myConsole
{
	partial class console
	{
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		/// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 設計工具產生的程式碼

		/// <summary>
		/// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{
			this.commandBox = new System.Windows.Forms.TextBox();
			this.richTextBox_message = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// commandBox
			// 
			this.commandBox.Font = new System.Drawing.Font("DejaVu Sans Mono", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.commandBox.Location = new System.Drawing.Point(50, 456);
			this.commandBox.Name = "commandBox";
			this.commandBox.Size = new System.Drawing.Size(700, 28);
			this.commandBox.TabIndex = 1;
			this.commandBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.commandBox_KeyDown);
			// 
			// richTextBox_message
			// 
			this.richTextBox_message.Font = new System.Drawing.Font("DejaVu Sans Mono", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextBox_message.Location = new System.Drawing.Point(50, 50);
			this.richTextBox_message.Name = "richTextBox_message";
			this.richTextBox_message.ReadOnly = true;
			this.richTextBox_message.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			this.richTextBox_message.Size = new System.Drawing.Size(700, 400);
			this.richTextBox_message.TabIndex = 2;
			this.richTextBox_message.Text = "";
			// 
			// console
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(778, 544);
			this.Controls.Add(this.richTextBox_message);
			this.Controls.Add(this.commandBox);
			this.Name = "console";
			this.Text = "lua console";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox commandBox;
		private System.Windows.Forms.RichTextBox richTextBox_message;
	}
}

