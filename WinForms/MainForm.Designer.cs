
namespace WinForms
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnlFavoritePlayers = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlAllPlayers = new System.Windows.Forms.FlowLayoutPanel();
            this.cmsPlayers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToFavoriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setPictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsFavorites = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRankGoals = new System.Windows.Forms.Button();
            this.btnRankYellowCards = new System.Windows.Forms.Button();
            this.btnRankVisitors = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.cmsPlayers.SuspendLayout();
            this.cmsFavorites.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFavoritePlayers
            // 
            resources.ApplyResources(this.pnlFavoritePlayers, "pnlFavoritePlayers");
            this.pnlFavoritePlayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFavoritePlayers.Name = "pnlFavoritePlayers";
            // 
            // pnlAllPlayers
            // 
            resources.ApplyResources(this.pnlAllPlayers, "pnlAllPlayers");
            this.pnlAllPlayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAllPlayers.Name = "pnlAllPlayers";
            // 
            // cmsPlayers
            // 
            resources.ApplyResources(this.cmsPlayers, "cmsPlayers");
            this.cmsPlayers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToFavoriteToolStripMenuItem,
            this.clearSelectionToolStripMenuItem,
            this.setPictureToolStripMenuItem});
            this.cmsPlayers.Name = "contextMenuStrip1";
            // 
            // addToFavoriteToolStripMenuItem
            // 
            resources.ApplyResources(this.addToFavoriteToolStripMenuItem, "addToFavoriteToolStripMenuItem");
            this.addToFavoriteToolStripMenuItem.Name = "addToFavoriteToolStripMenuItem";
            this.addToFavoriteToolStripMenuItem.Click += new System.EventHandler(this.addToFavoriteToolStripMenuItem_Click);
            // 
            // clearSelectionToolStripMenuItem
            // 
            resources.ApplyResources(this.clearSelectionToolStripMenuItem, "clearSelectionToolStripMenuItem");
            this.clearSelectionToolStripMenuItem.Name = "clearSelectionToolStripMenuItem";
            this.clearSelectionToolStripMenuItem.Click += new System.EventHandler(this.clearSelection_Click);
            // 
            // setPictureToolStripMenuItem
            // 
            resources.ApplyResources(this.setPictureToolStripMenuItem, "setPictureToolStripMenuItem");
            this.setPictureToolStripMenuItem.Name = "setPictureToolStripMenuItem";
            this.setPictureToolStripMenuItem.Click += new System.EventHandler(this.setPictureToolStripMenuItem_Click);
            // 
            // cmsFavorites
            // 
            resources.ApplyResources(this.cmsFavorites, "cmsFavorites");
            this.cmsFavorites.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.cmsFavorites.Name = "contextMenuStrip1";
            // 
            // toolStripMenuItem2
            // 
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.clearSelectionFavorites_Click);
            // 
            // btnRankGoals
            // 
            resources.ApplyResources(this.btnRankGoals, "btnRankGoals");
            this.btnRankGoals.Name = "btnRankGoals";
            this.btnRankGoals.UseVisualStyleBackColor = true;
            this.btnRankGoals.Click += new System.EventHandler(this.btnRankGoals_Click);
            // 
            // btnRankYellowCards
            // 
            resources.ApplyResources(this.btnRankYellowCards, "btnRankYellowCards");
            this.btnRankYellowCards.Name = "btnRankYellowCards";
            this.btnRankYellowCards.UseVisualStyleBackColor = true;
            this.btnRankYellowCards.Click += new System.EventHandler(this.btnRankYellowCards_Click);
            // 
            // btnRankVisitors
            // 
            resources.ApplyResources(this.btnRankVisitors, "btnRankVisitors");
            this.btnRankVisitors.Name = "btnRankVisitors";
            this.btnRankVisitors.UseVisualStyleBackColor = true;
            this.btnRankVisitors.Click += new System.EventHandler(this.btnRankVisitors_Click);
            // 
            // btnSettings
            // 
            resources.ApplyResources(this.btnSettings, "btnSettings");
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnRankVisitors);
            this.Controls.Add(this.btnRankYellowCards);
            this.Controls.Add(this.btnRankGoals);
            this.Controls.Add(this.pnlAllPlayers);
            this.Controls.Add(this.pnlFavoritePlayers);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.cmsPlayers.ResumeLayout(false);
            this.cmsFavorites.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel pnlFavoritePlayers;
        private System.Windows.Forms.FlowLayoutPanel pnlAllPlayers;
        private System.Windows.Forms.ContextMenuStrip cmsPlayers;
        private System.Windows.Forms.ToolStripMenuItem addToFavoriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSelectionToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsFavorites;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Button btnRankGoals;
        private System.Windows.Forms.Button btnRankYellowCards;
        private System.Windows.Forms.Button btnRankVisitors;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.ToolStripMenuItem setPictureToolStripMenuItem;
    }
}