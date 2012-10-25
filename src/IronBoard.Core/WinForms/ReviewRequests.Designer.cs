namespace IronBoard.Core.WinForms
{
   partial class ReviewRequests
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
         this.Requests = new System.Windows.Forms.DataGridView();
         this.RequestsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.updateWithNewCommitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.ReloadReviews = new System.Windows.Forms.Button();
         this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Submitter = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.LastUpdated = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Groups = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.People = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Summary = new System.Windows.Forms.DataGridViewTextBoxColumn();
         ((System.ComponentModel.ISupportInitialize)(this.Requests)).BeginInit();
         this.RequestsMenu.SuspendLayout();
         this.SuspendLayout();
         // 
         // Requests
         // 
         this.Requests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.Requests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.Requests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.Submitter,
            this.LastUpdated,
            this.Status,
            this.Groups,
            this.People,
            this.Summary});
         this.Requests.ContextMenuStrip = this.RequestsMenu;
         this.Requests.Location = new System.Drawing.Point(3, 30);
         this.Requests.MultiSelect = false;
         this.Requests.Name = "Requests";
         this.Requests.ReadOnly = true;
         this.Requests.RowHeadersVisible = false;
         this.Requests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
         this.Requests.Size = new System.Drawing.Size(952, 211);
         this.Requests.TabIndex = 0;
         // 
         // RequestsMenu
         // 
         this.RequestsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateWithNewCommitToolStripMenuItem});
         this.RequestsMenu.Name = "RequestsMenu";
         this.RequestsMenu.Size = new System.Drawing.Size(202, 26);
         // 
         // updateWithNewCommitToolStripMenuItem
         // 
         this.updateWithNewCommitToolStripMenuItem.Name = "updateWithNewCommitToolStripMenuItem";
         this.updateWithNewCommitToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
         this.updateWithNewCommitToolStripMenuItem.Text = "update with new commit";
         // 
         // ReloadReviews
         // 
         this.ReloadReviews.Location = new System.Drawing.Point(4, 4);
         this.ReloadReviews.Name = "ReloadReviews";
         this.ReloadReviews.Size = new System.Drawing.Size(75, 23);
         this.ReloadReviews.TabIndex = 1;
         this.ReloadReviews.Text = "&refresh";
         this.ReloadReviews.UseVisualStyleBackColor = true;
         this.ReloadReviews.Click += new System.EventHandler(this.ReloadReviews_Click);
         // 
         // id
         // 
         this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
         this.id.HeaderText = "#";
         this.id.Name = "id";
         this.id.ReadOnly = true;
         this.id.Width = 39;
         // 
         // Submitter
         // 
         this.Submitter.HeaderText = "submitter";
         this.Submitter.Name = "Submitter";
         this.Submitter.ReadOnly = true;
         // 
         // LastUpdated
         // 
         this.LastUpdated.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
         this.LastUpdated.HeaderText = "updated";
         this.LastUpdated.Name = "LastUpdated";
         this.LastUpdated.ReadOnly = true;
         this.LastUpdated.Width = 71;
         // 
         // Status
         // 
         this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
         this.Status.HeaderText = "status";
         this.Status.Name = "Status";
         this.Status.ReadOnly = true;
         this.Status.Width = 60;
         // 
         // Groups
         // 
         this.Groups.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
         this.Groups.HeaderText = "groups";
         this.Groups.Name = "Groups";
         this.Groups.ReadOnly = true;
         this.Groups.Width = 64;
         // 
         // People
         // 
         this.People.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
         this.People.HeaderText = "people";
         this.People.Name = "People";
         this.People.ReadOnly = true;
         this.People.Width = 64;
         // 
         // Summary
         // 
         this.Summary.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
         this.Summary.HeaderText = "summary";
         this.Summary.Name = "Summary";
         this.Summary.ReadOnly = true;
         // 
         // ReviewRequests
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.ReloadReviews);
         this.Controls.Add(this.Requests);
         this.Name = "ReviewRequests";
         this.Size = new System.Drawing.Size(958, 244);
         ((System.ComponentModel.ISupportInitialize)(this.Requests)).EndInit();
         this.RequestsMenu.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.DataGridView Requests;
      private System.Windows.Forms.ContextMenuStrip RequestsMenu;
      private System.Windows.Forms.ToolStripMenuItem updateWithNewCommitToolStripMenuItem;
      private System.Windows.Forms.Button ReloadReviews;
      private System.Windows.Forms.DataGridViewTextBoxColumn id;
      private System.Windows.Forms.DataGridViewTextBoxColumn Submitter;
      private System.Windows.Forms.DataGridViewTextBoxColumn LastUpdated;
      private System.Windows.Forms.DataGridViewTextBoxColumn Status;
      private System.Windows.Forms.DataGridViewTextBoxColumn Groups;
      private System.Windows.Forms.DataGridViewTextBoxColumn People;
      private System.Windows.Forms.DataGridViewTextBoxColumn Summary;
   }
}
