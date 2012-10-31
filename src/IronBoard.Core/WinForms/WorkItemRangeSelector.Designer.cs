namespace IronBoard.Core.WinForms
{
   partial class WorkItemRangeSelector
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
         this.AuthorFilter = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.RevisionsWarning = new System.Windows.Forms.Label();
         this.CommandLine = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.Revisions = new System.Windows.Forms.CheckedListBox();
         this.MaxRevisions = new System.Windows.Forms.ComboBox();
         this.Refresh = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // AuthorFilter
         // 
         this.AuthorFilter.Location = new System.Drawing.Point(325, 2);
         this.AuthorFilter.Name = "AuthorFilter";
         this.AuthorFilter.Size = new System.Drawing.Size(100, 20);
         this.AuthorFilter.TabIndex = 24;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(264, 5);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(62, 13);
         this.label2.TabIndex = 23;
         this.label2.Text = "author filter:";
         // 
         // RevisionsWarning
         // 
         this.RevisionsWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.RevisionsWarning.AutoSize = true;
         this.RevisionsWarning.ForeColor = System.Drawing.Color.Red;
         this.RevisionsWarning.Location = new System.Drawing.Point(514, 5);
         this.RevisionsWarning.Name = "RevisionsWarning";
         this.RevisionsWarning.Size = new System.Drawing.Size(176, 13);
         this.RevisionsWarning.TabIndex = 22;
         this.RevisionsWarning.Text = "* some revisions will not be included";
         this.RevisionsWarning.Visible = false;
         // 
         // CommandLine
         // 
         this.CommandLine.AutoSize = true;
         this.CommandLine.Location = new System.Drawing.Point(431, 5);
         this.CommandLine.Name = "CommandLine";
         this.CommandLine.Size = new System.Drawing.Size(30, 13);
         this.CommandLine.TabIndex = 21;
         this.CommandLine.Text = "rM:N";
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(2, 5);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(73, 13);
         this.label1.TabIndex = 18;
         this.label1.Text = "max revisions:";
         // 
         // Revisions
         // 
         this.Revisions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.Revisions.CheckOnClick = true;
         this.Revisions.FormattingEnabled = true;
         this.Revisions.IntegralHeight = false;
         this.Revisions.Location = new System.Drawing.Point(0, 25);
         this.Revisions.Name = "Revisions";
         this.Revisions.Size = new System.Drawing.Size(688, 149);
         this.Revisions.TabIndex = 20;
         this.Revisions.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.Revisions_ItemCheck);
         this.Revisions.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Revisions_MouseUp);
         // 
         // MaxRevisions
         // 
         this.MaxRevisions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.MaxRevisions.FormattingEnabled = true;
         this.MaxRevisions.Items.AddRange(new object[] {
            "10",
            "20",
            "50",
            "100",
            "500"});
         this.MaxRevisions.Location = new System.Drawing.Point(76, 1);
         this.MaxRevisions.Name = "MaxRevisions";
         this.MaxRevisions.Size = new System.Drawing.Size(121, 21);
         this.MaxRevisions.TabIndex = 17;
         // 
         // RefreshView
         // 
         this.Refresh.Location = new System.Drawing.Point(201, 0);
         this.Refresh.Name = "Refresh";
         this.Refresh.Size = new System.Drawing.Size(56, 23);
         this.Refresh.TabIndex = 19;
         this.Refresh.Text = "refresh";
         this.Refresh.UseVisualStyleBackColor = true;
         this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
         // 
         // WorkItemRangeSelector
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.AuthorFilter);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.RevisionsWarning);
         this.Controls.Add(this.CommandLine);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.Revisions);
         this.Controls.Add(this.MaxRevisions);
         this.Controls.Add(this.Refresh);
         this.Name = "WorkItemRangeSelector";
         this.Size = new System.Drawing.Size(691, 177);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox AuthorFilter;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label RevisionsWarning;
      private System.Windows.Forms.Label CommandLine;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.CheckedListBox Revisions;
      private System.Windows.Forms.ComboBox MaxRevisions;
      private System.Windows.Forms.Button Refresh;
   }
}
