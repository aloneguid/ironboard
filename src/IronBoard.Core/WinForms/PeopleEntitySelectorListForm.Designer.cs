namespace IronBoard.Core.WinForms
{
   partial class PeopleEntitySelectorListForm
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
         this.ElementsList = new System.Windows.Forms.CheckedListBox();
         this.OkButton = new System.Windows.Forms.Button();
         this.CancelButton = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // ElementsList
         // 
         this.ElementsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.ElementsList.CheckOnClick = true;
         this.ElementsList.FormattingEnabled = true;
         this.ElementsList.Location = new System.Drawing.Point(2, 2);
         this.ElementsList.Name = "ElementsList";
         this.ElementsList.Size = new System.Drawing.Size(246, 319);
         this.ElementsList.TabIndex = 0;
         // 
         // OkButton
         // 
         this.OkButton.Location = new System.Drawing.Point(95, 323);
         this.OkButton.Name = "OkButton";
         this.OkButton.Size = new System.Drawing.Size(75, 23);
         this.OkButton.TabIndex = 1;
         this.OkButton.Text = "&Ok";
         this.OkButton.UseVisualStyleBackColor = true;
         this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
         // 
         // CancelButton
         // 
         this.CancelButton.Location = new System.Drawing.Point(174, 323);
         this.CancelButton.Name = "CancelButton";
         this.CancelButton.Size = new System.Drawing.Size(75, 23);
         this.CancelButton.TabIndex = 2;
         this.CancelButton.Text = "&Cancel";
         this.CancelButton.UseVisualStyleBackColor = true;
         this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
         // 
         // PeopleEntitySelectorListForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(252, 348);
         this.Controls.Add(this.CancelButton);
         this.Controls.Add(this.OkButton);
         this.Controls.Add(this.ElementsList);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
         this.Name = "PeopleEntitySelectorListForm";
         this.ShowInTaskbar = false;
         this.Text = "select";
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.CheckedListBox ElementsList;
      private System.Windows.Forms.Button OkButton;
      private System.Windows.Forms.Button CancelButton;
   }
}