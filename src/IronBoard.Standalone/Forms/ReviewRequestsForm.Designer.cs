namespace IronBoard.Standalone.Forms
{
   partial class ReviewRequestsForm
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
         this.reviewRequests1 = new IronBoard.Core.WinForms.ReviewRequests();
         this.SuspendLayout();
         // 
         // reviewRequests1
         // 
         this.reviewRequests1.Location = new System.Drawing.Point(4, 3);
         this.reviewRequests1.Name = "reviewRequests1";
         this.reviewRequests1.Size = new System.Drawing.Size(843, 176);
         this.reviewRequests1.TabIndex = 0;
         // 
         // ReviewRequestsForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(859, 365);
         this.Controls.Add(this.reviewRequests1);
         this.Name = "ReviewRequestsForm";
         this.Text = "ReviewRequestsForm";
         this.ResumeLayout(false);

      }

      #endregion

      private Core.WinForms.ReviewRequests reviewRequests1;
   }
}