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
         this.MaxRevisions = new System.Windows.Forms.ComboBox();
         this.label1 = new System.Windows.Forms.Label();
         this.HistoryGrid = new System.Windows.Forms.DataGridView();
         this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
         this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Author = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.label2 = new System.Windows.Forms.Label();
         this.Summary = new System.Windows.Forms.TextBox();
         this.label3 = new System.Windows.Forms.Label();
         this.Description = new System.Windows.Forms.TextBox();
         this.label4 = new System.Windows.Forms.Label();
         this.PostReview = new System.Windows.Forms.Button();
         this.Testing = new System.Windows.Forms.ComboBox();
         ((System.ComponentModel.ISupportInitialize)(this.HistoryGrid)).BeginInit();
         this.SuspendLayout();
         // 
         // MaxRevisions
         // 
         this.MaxRevisions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.MaxRevisions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.MaxRevisions.FormattingEnabled = true;
         this.MaxRevisions.Items.AddRange(new object[] {
            "20",
            "50",
            "100",
            "500"});
         this.MaxRevisions.Location = new System.Drawing.Point(88, 7);
         this.MaxRevisions.Name = "MaxRevisions";
         this.MaxRevisions.Size = new System.Drawing.Size(121, 21);
         this.MaxRevisions.TabIndex = 0;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(7, 11);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(73, 13);
         this.label1.TabIndex = 1;
         this.label1.Text = "max revisions:";
         // 
         // HistoryGrid
         // 
         this.HistoryGrid.AllowUserToAddRows = false;
         this.HistoryGrid.AllowUserToDeleteRows = false;
         this.HistoryGrid.AllowUserToResizeColumns = false;
         this.HistoryGrid.AllowUserToResizeRows = false;
         this.HistoryGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.HistoryGrid.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
         this.HistoryGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.HistoryGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
         this.HistoryGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
         this.HistoryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.HistoryGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.Id,
            this.Author,
            this.Date,
            this.Message});
         this.HistoryGrid.Location = new System.Drawing.Point(7, 34);
         this.HistoryGrid.Name = "HistoryGrid";
         this.HistoryGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
         this.HistoryGrid.RowHeadersVisible = false;
         this.HistoryGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.HistoryGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
         this.HistoryGrid.ShowEditingIcon = false;
         this.HistoryGrid.Size = new System.Drawing.Size(573, 150);
         this.HistoryGrid.TabIndex = 2;
         this.HistoryGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.HistoryGrid_CellClick);
         this.HistoryGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.HistoryGrid_CellEndEdit);
         this.HistoryGrid.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.HistoryGrid_CellMouseUp);
         // 
         // Selected
         // 
         this.Selected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
         this.Selected.HeaderText = "";
         this.Selected.Name = "Selected";
         this.Selected.Resizable = System.Windows.Forms.DataGridViewTriState.False;
         this.Selected.Width = 25;
         // 
         // Id
         // 
         this.Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
         this.Id.HeaderText = "#";
         this.Id.Name = "Id";
         this.Id.ReadOnly = true;
         this.Id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
         this.Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
         this.Id.Width = 24;
         // 
         // Author
         // 
         this.Author.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
         this.Author.HeaderText = "author";
         this.Author.Name = "Author";
         this.Author.ReadOnly = true;
         this.Author.Resizable = System.Windows.Forms.DataGridViewTriState.False;
         this.Author.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
         this.Author.Width = 47;
         // 
         // Date
         // 
         this.Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
         this.Date.HeaderText = "date";
         this.Date.Name = "Date";
         this.Date.ReadOnly = true;
         this.Date.Resizable = System.Windows.Forms.DataGridViewTriState.False;
         this.Date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
         this.Date.Width = 38;
         // 
         // Message
         // 
         this.Message.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
         this.Message.HeaderText = "message";
         this.Message.Name = "Message";
         this.Message.ReadOnly = true;
         this.Message.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(7, 193);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(51, 13);
         this.label2.TabIndex = 3;
         this.label2.Text = "summary:";
         // 
         // Summary
         // 
         this.Summary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.Summary.Location = new System.Drawing.Point(64, 189);
         this.Summary.Name = "Summary";
         this.Summary.Size = new System.Drawing.Size(516, 20);
         this.Summary.TabIndex = 4;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(7, 214);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(61, 13);
         this.label3.TabIndex = 5;
         this.label3.Text = "description:";
         // 
         // Description
         // 
         this.Description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.Description.Location = new System.Drawing.Point(7, 230);
         this.Description.Multiline = true;
         this.Description.Name = "Description";
         this.Description.Size = new System.Drawing.Size(573, 190);
         this.Description.TabIndex = 6;
         // 
         // label4
         // 
         this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(7, 430);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(41, 13);
         this.label4.TabIndex = 7;
         this.label4.Text = "testing:";
         // 
         // PostReview
         // 
         this.PostReview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.PostReview.Location = new System.Drawing.Point(483, 454);
         this.PostReview.Name = "PostReview";
         this.PostReview.Size = new System.Drawing.Size(95, 23);
         this.PostReview.TabIndex = 9;
         this.PostReview.Text = "post review >>";
         this.PostReview.UseVisualStyleBackColor = true;
         this.PostReview.Click += new System.EventHandler(this.PostReview_Click);
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
         this.Testing.Location = new System.Drawing.Point(54, 426);
         this.Testing.Name = "Testing";
         this.Testing.Size = new System.Drawing.Size(526, 21);
         this.Testing.TabIndex = 10;
         // 
         // PostCommitReviewForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(585, 482);
         this.Controls.Add(this.Testing);
         this.Controls.Add(this.PostReview);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.Description);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.Summary);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.HistoryGrid);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.MaxRevisions);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
         this.Name = "PostCommitReviewForm";
         this.Text = "Post-Commit Review";
         ((System.ComponentModel.ISupportInitialize)(this.HistoryGrid)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.ComboBox MaxRevisions;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.DataGridView HistoryGrid;
      private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
      private System.Windows.Forms.DataGridViewTextBoxColumn Id;
      private System.Windows.Forms.DataGridViewTextBoxColumn Author;
      private System.Windows.Forms.DataGridViewTextBoxColumn Date;
      private System.Windows.Forms.DataGridViewTextBoxColumn Message;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.TextBox Summary;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.TextBox Description;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Button PostReview;
      private System.Windows.Forms.ComboBox Testing;
   }
}