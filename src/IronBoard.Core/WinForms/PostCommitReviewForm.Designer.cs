namespace IronBoard.Core.WinForms
{
   partial class PostCommitReviewForm
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PostCommitReviewForm));
         this.MaxRevisions = new System.Windows.Forms.ComboBox();
         this.label1 = new System.Windows.Forms.Label();
         this.PostReview = new System.Windows.Forms.Button();
         this.Refresh = new System.Windows.Forms.Button();
         this.Revisions = new System.Windows.Forms.CheckedListBox();
         this.panel1 = new System.Windows.Forms.Panel();
         this.CommandLine = new System.Windows.Forms.Label();
         this.splitter1 = new System.Windows.Forms.Splitter();
         this.panel2 = new System.Windows.Forms.Panel();
         this.Status = new System.Windows.Forms.StatusStrip();
         this.SaveDiff = new System.Windows.Forms.Button();
         this.SvnUri = new System.Windows.Forms.ToolStripStatusLabel();
         this.Progress = new System.Windows.Forms.ToolStripStatusLabel();
         this.Review = new IronBoard.Core.WinForms.ReviewEntityControl();
         this.panel1.SuspendLayout();
         this.panel2.SuspendLayout();
         this.Status.SuspendLayout();
         this.SuspendLayout();
         // 
         // MaxRevisions
         // 
         this.MaxRevisions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.MaxRevisions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.MaxRevisions.FormattingEnabled = true;
         this.MaxRevisions.Items.AddRange(new object[] {
            "10",
            "20",
            "50",
            "100",
            "500"});
         this.MaxRevisions.Location = new System.Drawing.Point(77, 2);
         this.MaxRevisions.Name = "MaxRevisions";
         this.MaxRevisions.Size = new System.Drawing.Size(121, 21);
         this.MaxRevisions.TabIndex = 0;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(3, 6);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(73, 13);
         this.label1.TabIndex = 1;
         this.label1.Text = "max revisions:";
         // 
         // PostReview
         // 
         this.PostReview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.PostReview.Enabled = false;
         this.PostReview.Location = new System.Drawing.Point(652, 310);
         this.PostReview.Name = "PostReview";
         this.PostReview.Size = new System.Drawing.Size(95, 23);
         this.PostReview.TabIndex = 9;
         this.PostReview.Text = "post...";
         this.PostReview.UseVisualStyleBackColor = true;
         this.PostReview.Click += new System.EventHandler(this.PostReview_Click);
         // 
         // Refresh
         // 
         this.Refresh.Location = new System.Drawing.Point(202, 1);
         this.Refresh.Name = "Refresh";
         this.Refresh.Size = new System.Drawing.Size(56, 23);
         this.Refresh.TabIndex = 11;
         this.Refresh.Text = "refresh";
         this.Refresh.UseVisualStyleBackColor = true;
         this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
         // 
         // Revisions
         // 
         this.Revisions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.Revisions.CheckOnClick = true;
         this.Revisions.FormattingEnabled = true;
         this.Revisions.IntegralHeight = false;
         this.Revisions.Location = new System.Drawing.Point(1, 26);
         this.Revisions.Name = "Revisions";
         this.Revisions.Size = new System.Drawing.Size(751, 109);
         this.Revisions.TabIndex = 12;
         this.Revisions.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.Revisions_ItemCheck);
         this.Revisions.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Revisions_MouseUp);
         // 
         // panel1
         // 
         this.panel1.BackColor = System.Drawing.SystemColors.Control;
         this.panel1.Controls.Add(this.CommandLine);
         this.panel1.Controls.Add(this.label1);
         this.panel1.Controls.Add(this.Revisions);
         this.panel1.Controls.Add(this.MaxRevisions);
         this.panel1.Controls.Add(this.Refresh);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel1.Location = new System.Drawing.Point(0, 0);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(759, 144);
         this.panel1.TabIndex = 13;
         // 
         // CommandLine
         // 
         this.CommandLine.AutoSize = true;
         this.CommandLine.Location = new System.Drawing.Point(265, 6);
         this.CommandLine.Name = "CommandLine";
         this.CommandLine.Size = new System.Drawing.Size(30, 13);
         this.CommandLine.TabIndex = 13;
         this.CommandLine.Text = "rM:N";
         // 
         // splitter1
         // 
         this.splitter1.BackColor = System.Drawing.SystemColors.ActiveCaption;
         this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
         this.splitter1.Location = new System.Drawing.Point(0, 144);
         this.splitter1.Name = "splitter1";
         this.splitter1.Size = new System.Drawing.Size(759, 3);
         this.splitter1.TabIndex = 14;
         this.splitter1.TabStop = false;
         // 
         // panel2
         // 
         this.panel2.BackColor = System.Drawing.SystemColors.Control;
         this.panel2.Controls.Add(this.Review);
         this.panel2.Controls.Add(this.Status);
         this.panel2.Controls.Add(this.SaveDiff);
         this.panel2.Controls.Add(this.PostReview);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel2.Location = new System.Drawing.Point(0, 147);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(759, 359);
         this.panel2.TabIndex = 15;
         // 
         // Status
         // 
         this.Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SvnUri,
            this.Progress});
         this.Status.Location = new System.Drawing.Point(0, 337);
         this.Status.Name = "Status";
         this.Status.Size = new System.Drawing.Size(759, 22);
         this.Status.TabIndex = 13;
         this.Status.Text = "statusStrip1";
         // 
         // SaveDiff
         // 
         this.SaveDiff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.SaveDiff.Enabled = false;
         this.SaveDiff.Location = new System.Drawing.Point(571, 310);
         this.SaveDiff.Name = "SaveDiff";
         this.SaveDiff.Size = new System.Drawing.Size(75, 23);
         this.SaveDiff.TabIndex = 12;
         this.SaveDiff.Text = "save diff";
         this.SaveDiff.UseVisualStyleBackColor = true;
         this.SaveDiff.Click += new System.EventHandler(this.SaveDiff_Click);
         // 
         // SvnUri
         // 
         this.SvnUri.Name = "SvnUri";
         this.SvnUri.Size = new System.Drawing.Size(36, 17);
         this.SvnUri.Text = "svn://";
         // 
         // Progress
         // 
         this.Progress.Name = "Progress";
         this.Progress.Size = new System.Drawing.Size(19, 17);
         this.Progress.Text = "...";
         // 
         // Review
         // 
         this.Review.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.Review.Location = new System.Drawing.Point(-2, 4);
         this.Review.Name = "Review";
         this.Review.Size = new System.Drawing.Size(758, 302);
         this.Review.TabIndex = 14;
         // 
         // PostCommitReviewForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(759, 506);
         this.Controls.Add(this.panel2);
         this.Controls.Add(this.splitter1);
         this.Controls.Add(this.panel1);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Name = "PostCommitReviewForm";
         this.Text = "Post-Commit Review";
         this.panel1.ResumeLayout(false);
         this.panel1.PerformLayout();
         this.panel2.ResumeLayout(false);
         this.panel2.PerformLayout();
         this.Status.ResumeLayout(false);
         this.Status.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.ComboBox MaxRevisions;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button PostReview;
      private System.Windows.Forms.Button Refresh;
      private System.Windows.Forms.CheckedListBox Revisions;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Splitter splitter1;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.Label CommandLine;
      private System.Windows.Forms.Button SaveDiff;
      private System.Windows.Forms.StatusStrip Status;
      private ReviewEntityControl Review;
      private System.Windows.Forms.ToolStripStatusLabel SvnUri;
      private System.Windows.Forms.ToolStripStatusLabel Progress;
   }
}