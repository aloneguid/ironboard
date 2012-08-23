namespace IronBoard.Core.WinForms
{
   partial class RBAuthForm
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RBAuthForm));
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.Login = new System.Windows.Forms.TextBox();
         this.Password = new System.Windows.Forms.TextBox();
         this.Authenticate = new System.Windows.Forms.Button();
         this.Cancel = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(39, 8);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(32, 13);
         this.label1.TabIndex = 0;
         this.label1.Text = "login:";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(16, 30);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(55, 13);
         this.label2.TabIndex = 1;
         this.label2.Text = "password:";
         // 
         // Login
         // 
         this.Login.Location = new System.Drawing.Point(72, 4);
         this.Login.Name = "Login";
         this.Login.Size = new System.Drawing.Size(274, 20);
         this.Login.TabIndex = 2;
         this.Login.TextChanged += new System.EventHandler(this.Login_TextChanged);
         this.Login.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextKeyUp);
         // 
         // Password
         // 
         this.Password.Location = new System.Drawing.Point(72, 28);
         this.Password.Name = "Password";
         this.Password.PasswordChar = '*';
         this.Password.Size = new System.Drawing.Size(274, 20);
         this.Password.TabIndex = 3;
         this.Password.UseSystemPasswordChar = true;
         this.Password.TextChanged += new System.EventHandler(this.Password_TextChanged);
         this.Password.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextKeyUp);
         // 
         // Authenticate
         // 
         this.Authenticate.Enabled = false;
         this.Authenticate.Location = new System.Drawing.Point(193, 53);
         this.Authenticate.Name = "Authenticate";
         this.Authenticate.Size = new System.Drawing.Size(75, 23);
         this.Authenticate.TabIndex = 4;
         this.Authenticate.Text = "&Login";
         this.Authenticate.UseVisualStyleBackColor = true;
         this.Authenticate.Click += new System.EventHandler(this.Authenticate_Click);
         // 
         // Cancel
         // 
         this.Cancel.Location = new System.Drawing.Point(271, 53);
         this.Cancel.Name = "Cancel";
         this.Cancel.Size = new System.Drawing.Size(75, 23);
         this.Cancel.TabIndex = 5;
         this.Cancel.Text = "&Cancel";
         this.Cancel.UseVisualStyleBackColor = true;
         this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
         // 
         // RBAuthForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(352, 82);
         this.Controls.Add(this.Cancel);
         this.Controls.Add(this.Authenticate);
         this.Controls.Add(this.Password);
         this.Controls.Add(this.Login);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Name = "RBAuthForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "ReviewBoard Authentication";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.TextBox Login;
      private System.Windows.Forms.TextBox Password;
      private System.Windows.Forms.Button Authenticate;
      private System.Windows.Forms.Button Cancel;
   }
}