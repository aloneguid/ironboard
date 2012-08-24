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
         this.Description = new System.Windows.Forms.TextBox();
         this.label4 = new System.Windows.Forms.Label();
         this.Testing = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.label5 = new System.Windows.Forms.Label();
         this.Groups = new IronBoard.Core.WinForms.PeopleEntitySelector();
         this.Users = new IronBoard.Core.WinForms.PeopleEntitySelector();
         this.SuspendLayout();
         // 
         // IsDraft
         // 
         this.IsDraft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.IsDraft.AutoSize = true;
         this.IsDraft.Checked = true;
         this.IsDraft.CheckState = System.Windows.Forms.CheckState.Checked;
         this.IsDraft.Enabled = false;
         this.IsDraft.Location = new System.Drawing.Point(6, 354);
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
         this.Summary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.Summary.Location = new System.Drawing.Point(61, 5);
         this.Summary.Name = "Summary";
         this.Summary.Size = new System.Drawing.Size(586, 20);
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
         // Description
         // 
         this.Description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.Description.Location = new System.Drawing.Point(4, 48);
         this.Description.Multiline = true;
         this.Description.Name = "Description";
         this.Description.Size = new System.Drawing.Size(643, 185);
         this.Description.TabIndex = 15;
         // 
         // label4
         // 
         this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(4, 286);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(41, 13);
         this.label4.TabIndex = 16;
         this.label4.Text = "testing:";
         // 
         // Testing
         // 
         this.Testing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.Testing.Location = new System.Drawing.Point(4, 302);
         this.Testing.Multiline = true;
         this.Testing.Name = "Testing";
         this.Testing.Size = new System.Drawing.Size(643, 52);
         this.Testing.TabIndex = 19;
         // 
         // label1
         // 
         this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(5, 239);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(35, 13);
         this.label1.TabIndex = 21;
         this.label1.Text = "users:";
         // 
         // label5
         // 
         this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(4, 262);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(42, 13);
         this.label5.TabIndex = 22;
         this.label5.Text = "groups:";
         // 
         // Groups
         // 
         this.Groups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.Groups.Location = new System.Drawing.Point(48, 256);
         this.Groups.Name = "Groups";
         this.Groups.Size = new System.Drawing.Size(371, 28);
         this.Groups.TabIndex = 23;
         // 
         // Users
         // 
         this.Users.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.Users.Location = new System.Drawing.Point(48, 233);
         this.Users.Name = "Users";
         this.Users.Size = new System.Drawing.Size(371, 28);
         this.Users.TabIndex = 20;
         // 
         // ReviewEntityControl
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.Groups);
         this.Controls.Add(this.label5);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.Users);
         this.Controls.Add(this.Testing);
         this.Controls.Add(this.IsDraft);
         this.Controls.Add(this.Summary);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.Description);
         this.Controls.Add(this.label4);
         this.Name = "ReviewEntityControl";
         this.Size = new System.Drawing.Size(650, 374);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.CheckBox IsDraft;
      private System.Windows.Forms.TextBox Summary;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.TextBox Description;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.TextBox Testing;
      private PeopleEntitySelector Users;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label5;
      private PeopleEntitySelector Groups;
   }
}
