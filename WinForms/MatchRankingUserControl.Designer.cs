
namespace WinForms
{
    partial class MatchRankingUserControl
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
            this.lbLocation = new System.Windows.Forms.Label();
            this.lbAttendance = new System.Windows.Forms.Label();
            this.lbHomeTeam = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbAwayTeam = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbLocation
            // 
            this.lbLocation.AutoSize = true;
            this.lbLocation.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLocation.Location = new System.Drawing.Point(3, 3);
            this.lbLocation.Margin = new System.Windows.Forms.Padding(3);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(36, 18);
            this.lbLocation.TabIndex = 0;
            this.lbLocation.Text = "Text";
            // 
            // lbAttendance
            // 
            this.lbAttendance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAttendance.AutoSize = true;
            this.lbAttendance.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAttendance.Location = new System.Drawing.Point(387, 3);
            this.lbAttendance.Margin = new System.Windows.Forms.Padding(3);
            this.lbAttendance.Name = "lbAttendance";
            this.lbAttendance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbAttendance.Size = new System.Drawing.Size(36, 18);
            this.lbAttendance.TabIndex = 1;
            this.lbAttendance.Text = "Text";
            this.lbAttendance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbHomeTeam
            // 
            this.lbHomeTeam.AutoSize = true;
            this.lbHomeTeam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHomeTeam.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHomeTeam.Location = new System.Drawing.Point(3, 3);
            this.lbHomeTeam.Margin = new System.Windows.Forms.Padding(3);
            this.lbHomeTeam.Name = "lbHomeTeam";
            this.lbHomeTeam.Size = new System.Drawing.Size(141, 29);
            this.lbHomeTeam.TabIndex = 2;
            this.lbHomeTeam.Text = "Text";
            this.lbHomeTeam.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(209, 3);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 18);
            this.label5.TabIndex = 3;
            this.label5.Text = "vs";
            // 
            // lbAwayTeam
            // 
            this.lbAwayTeam.AutoSize = true;
            this.lbAwayTeam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAwayTeam.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAwayTeam.Location = new System.Drawing.Point(297, 3);
            this.lbAwayTeam.Margin = new System.Windows.Forms.Padding(3);
            this.lbAwayTeam.Name = "lbAwayTeam";
            this.lbAwayTeam.Size = new System.Drawing.Size(143, 29);
            this.lbAwayTeam.TabIndex = 4;
            this.lbAwayTeam.Text = "Text";
            this.lbAwayTeam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.lbHomeTeam, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbAwayTeam, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(-1, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(443, 35);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // MatchRankingUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lbAttendance);
            this.Controls.Add(this.lbLocation);
            this.Name = "MatchRankingUserControl";
            this.Size = new System.Drawing.Size(441, 61);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbLocation;
        private System.Windows.Forms.Label lbAttendance;
        private System.Windows.Forms.Label lbHomeTeam;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbAwayTeam;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
