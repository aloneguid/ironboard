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
         this.PostReview = new System.Windows.Forms.Button();
         this.panel1 = new System.Windows.Forms.Panel();
         this.WorkItems = new IronBoard.Core.WinForms.WorkItemRangeSelector();
         this.splitter1 = new System.Windows.Forms.Splitter();
         this.panel2 = new System.Windows.Forms.Panel();
         this.Review = new IronBoard.Core.WinForms.ReviewEntityControl();
         this.Status = new System.Windows.Forms.StatusStrip();
         this.SvnUri = new System.Windows.Forms.ToolStripStatusLabel();
         this.CommandLine = new System.Windows.Forms.ToolStripStatusLabel();
         this.Progress = new System.Windows.Forms.ToolStripStatusLabel();
         this.panel1.SuspendLayout();
         this.panel2.SuspendLayout();
         this.Status.SuspendLayout();
         this.SuspendLayout();
         // 
         // PostReview
         // 
         this.PostReview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.PostReview.Enabled = false;
         this.PostReview.Location = new System.Drawing.Point(652, 313);
         this.PostReview.Name = "PostReview";
         this.PostReview.Size = new System.Drawing.Size(95, 23);
         this.PostReview.TabIndex = 9;
         this.PostReview.Text = "post...";
         this.PostReview.UseVisualStyleBackColor = true;
         this.PostReview.Click += new System.EventHandler(this.PostReview_Click);
         // 
         // panel1
         // 
         this.panel1.BackColor = System.Drawing.SystemColors.Control;
         this.panel1.Controls.Add(this.WorkItems);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel1.Location = new System.Drawing.Point(0, 0);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(759, 141);
         this.panel1.TabIndex = 13;
         // 
         // WorkItems
         // 
         this.WorkItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.WorkItems.Location = new System.Drawing.Point(1, 2);
         this.WorkItems.Name = "WorkItems";
         this.WorkItems.Size = new System.Drawing.Size(758, 136);
         this.WorkItems.TabIndex = 0;
         this.WorkItems.WorkItems = ((System.Collections.Generic.IEnumerable<IronBoard.Core.Model.WorkItem>)(resources.GetObject("WorkItems.WorkItems")));
         // 
         // splitter1
         // 
         this.splitter1.BackColor = System.Drawing.SystemColors.ActiveCaption;
         this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
         this.splitter1.Location = new System.Drawing.Point(0, 141);
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
         this.panel2.Controls.Add(this.PostReview);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel2.Location = new System.Drawing.Point(0, 144);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(759, 362);
         this.panel2.TabIndex = 15;
         // 
         // Review
         // 
         this.Review.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.Review.Location = new System.Drawing.Point(-2, 4);
         this.Review.Name = "Review";
         this.Review.Size = new System.Drawing.Size(758, 305);
         this.Review.TabIndex = 14;
         // 
         // Status
         // 
         this.Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SvnUri,
            this.CommandLine,
            this.Progress});
         this.Status.Location = new System.Drawing.Point(0, 340);
         this.Status.Name = "Status";
         this.Status.Size = new System.Drawing.Size(759, 22);
         this.Status.TabIndex = 13;
         this.Status.Text = "statusStrip1";
         // 
         // SvnUri
         // 
         this.SvnUri.Name = "SvnUri";
         this.SvnUri.Size = new System.Drawing.Size(38, 17);
         this.SvnUri.Text = "svn://";
         // 
         // CommandLine
         // 
         this.CommandLine.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
         this.CommandLine.Name = "CommandLine";
         this.CommandLine.Size = new System.Drawing.Size(16, 17);
         this.CommandLine.Text = "...";
         // 
         // Progress
         // 
         this.Progress.Name = "Progress";
         this.Progress.Size = new System.Drawing.Size(16, 17);
         this.Progress.Text = "...";
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
         this.panel2.ResumeLayout(false);
         this.panel2.PerformLayout();
         this.Status.ResumeLayout(false);
         this.Status.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button PostReview;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Splitter splitter1;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.StatusStrip Status;
      private ReviewEntityControl Review;
      private System.Windows.Forms.ToolStripStatusLabel SvnUri;
      private System.Windows.Forms.ToolStripStatusLabel Progress;
      private System.Windows.Forms.ToolStripStatusLabel CommandLine;
      private WorkItemRangeSelector WorkItems;
   }
}