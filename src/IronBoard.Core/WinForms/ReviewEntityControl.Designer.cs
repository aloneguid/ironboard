namespace IronBoard.Core.WinForms
{
   partial class ReviewEntityControl
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
         this.IsDraft = new System.Windows.Forms.CheckBox();
         this.Summary = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.Testing = new System.Windows.Forms.ComboBox();
         this.Description = new System.Windows.Forms.TextBox();
         this.label4 = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // IsDraft
         // 
         this.IsDraft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.IsDraft.AutoSize = true;
         this.IsDraft.Checked = true;
         this.IsDraft.CheckState = System.Windows.Forms.CheckState.Checked;
         this.IsDraft.Enabled = false;
         this.IsDraft.Location = new System.Drawing.Point(6, 263);
         this.IsDraft.Name = "IsDraft";
         this.IsDraft.Size = new System.Drawing.Size(47, 17);
         this.IsDraft.TabIndex = 18;
         this.IsDraft.Text = "draft";
         this.IsDraft.UseVisualStyleBackColor = true;
         // 
         // Summary
         // 
         this.Summary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.Summary.Location = new System.Drawing.Point(61, 4);
         this.Summary.Name = "Summary";
         this.Summary.Size = new System.Drawing.Size(410, 20);
         this.Summary.TabIndex = 13;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(4, 7);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(51, 13);
         this.label2.TabIndex = 12;
         this.label2.Text = "summary:";
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(4, 30);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(61, 13);
         this.label3.TabIndex = 14;
         this.label3.Text = "description:";
         // 
         // Testing
         // 
         this.Testing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.Testing.FormattingEnabled = true;
         this.Testing.Items.AddRange(new object[] {
            "unit tests (see ",
            "integration tests (see",
            "Manual testing. To reproduce ..."});
         this.Testing.Location = new System.Drawing.Point(56, 242);
         this.Testing.Name = "Testing";
         this.Testing.Size = new System.Drawing.Size(415, 21);
         this.Testing.TabIndex = 17;
         // 
         // Description
         // 
         this.Description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.Description.Location = new System.Drawing.Point(4, 48);
         this.Description.Multiline = true;
         this.Description.Name = "Description";
         this.Description.Size = new System.Drawing.Size(467, 192);
         this.Description.TabIndex = 15;
         // 
         // label4
         // 
         this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(4, 245);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(41, 13);
         this.label4.TabIndex = 16;
         this.label4.Text = "testing:";
         // 
         // ReviewEntityControl
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.IsDraft);
         this.Controls.Add(this.Summary);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.Testing);
         this.Controls.Add(this.Description);
         this.Controls.Add(this.label4);
         this.Name = "ReviewEntityControl";
         this.Size = new System.Drawing.Size(474, 283);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.CheckBox IsDraft;
      private System.Windows.Forms.TextBox Summary;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.ComboBox Testing;
      private System.Windows.Forms.TextBox Description;
      private System.Windows.Forms.Label label4;
   }
}
