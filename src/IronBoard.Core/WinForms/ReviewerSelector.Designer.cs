namespace IronBoard.Core.WinForms
{
   partial class ReviewerSelector
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
         this.components = new System.ComponentModel.Container();
         this.SelectionText = new System.Windows.Forms.TextBox();
         this.Button = new System.Windows.Forms.Button();
         this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
         this.SuspendLayout();
         // 
         // SelectionText
         // 
         this.SelectionText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.SelectionText.Location = new System.Drawing.Point(3, 3);
         this.SelectionText.Name = "SelectionText";
         this.SelectionText.ReadOnly = true;
         this.SelectionText.Size = new System.Drawing.Size(271, 20);
         this.SelectionText.TabIndex = 0;
         // 
         // Button
         // 
         this.Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.Button.Location = new System.Drawing.Point(278, 3);
         this.Button.Name = "Button";
         this.Button.Size = new System.Drawing.Size(30, 20);
         this.Button.TabIndex = 1;
         this.Button.Text = "...";
         this.Button.UseVisualStyleBackColor = true;
         this.Button.Click += new System.EventHandler(this.Button_Click);
         // 
         // ToolTip
         // 
         this.ToolTip.AutoPopDelay = 5000;
         this.ToolTip.InitialDelay = 50;
         this.ToolTip.ReshowDelay = 100;
         this.ToolTip.ShowAlways = true;
         // 
         // PeopleEntitySelector
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.Button);
         this.Controls.Add(this.SelectionText);
         this.Name = "PeopleEntitySelector";
         this.Size = new System.Drawing.Size(314, 28);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox SelectionText;
      private System.Windows.Forms.Button Button;
      private System.Windows.Forms.ToolTip ToolTip;
   }
}
