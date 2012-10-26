namespace IronBoard.Vsix.Windows
{
   partial class ToolWindowHost
   {
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.Tabs = new System.Windows.Forms.TabControl();
         this.TabWorkHistory = new System.Windows.Forms.TabPage();
         this.TabTickets = new System.Windows.Forms.TabPage();
         this.Tabs.SuspendLayout();
         this.SuspendLayout();
         // 
         // Tabs
         // 
         this.Tabs.Alignment = System.Windows.Forms.TabAlignment.Bottom;
         this.Tabs.Controls.Add(this.TabWorkHistory);
         this.Tabs.Controls.Add(this.TabTickets);
         this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
         this.Tabs.HotTrack = true;
         this.Tabs.Location = new System.Drawing.Point(0, 0);
         this.Tabs.Multiline = true;
         this.Tabs.Name = "Tabs";
         this.Tabs.SelectedIndex = 0;
         this.Tabs.Size = new System.Drawing.Size(857, 272);
         this.Tabs.TabIndex = 0;
         // 
         // TabWorkHistory
         // 
         this.TabWorkHistory.Location = new System.Drawing.Point(4, 4);
         this.TabWorkHistory.Name = "TabWorkHistory";
         this.TabWorkHistory.Padding = new System.Windows.Forms.Padding(3);
         this.TabWorkHistory.Size = new System.Drawing.Size(849, 246);
         this.TabWorkHistory.TabIndex = 0;
         this.TabWorkHistory.Text = "work log";
         this.TabWorkHistory.UseVisualStyleBackColor = true;
         // 
         // TabTickets
         // 
         this.TabTickets.Location = new System.Drawing.Point(4, 4);
         this.TabTickets.Name = "TabTickets";
         this.TabTickets.Padding = new System.Windows.Forms.Padding(3);
         this.TabTickets.Size = new System.Drawing.Size(849, 246);
         this.TabTickets.TabIndex = 1;
         this.TabTickets.Text = "review requests";
         this.TabTickets.UseVisualStyleBackColor = true;
         // 
         // ToolWindowHost
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.Tabs);
         this.Name = "ToolWindowHost";
         this.Size = new System.Drawing.Size(857, 272);
         this.Tabs.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.TabControl Tabs;
      private System.Windows.Forms.TabPage TabWorkHistory;
      private System.Windows.Forms.TabPage TabTickets;
   }
}
