namespace SystemTrading
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.MenuStripDefault = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this._openApi = new AxKHOpenAPILib.AxKHOpenAPI();
            this.registerNRTInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDefault.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._openApi)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuStripDefault
            // 
            this.MenuStripDefault.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.MenuStripDefault.Location = new System.Drawing.Point(0, 0);
            this.MenuStripDefault.Name = "MenuStripDefault";
            this.MenuStripDefault.Size = new System.Drawing.Size(800, 24);
            this.MenuStripDefault.TabIndex = 1;
            this.MenuStripDefault.Text = "MainMenuStrip";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.registerNRTInfoToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loginToolStripMenuItem.Text = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // _openApi
            // 
            this._openApi.Enabled = true;
            this._openApi.Location = new System.Drawing.Point(688, 335);
            this._openApi.Name = "_openApi";
            this._openApi.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("_openApi.OcxState")));
            this._openApi.Size = new System.Drawing.Size(100, 50);
            this._openApi.TabIndex = 0;
            this._openApi.TabStop = false;
            this._openApi.Visible = false;
            // 
            // registerNRTInfoToolStripMenuItem
            // 
            this.registerNRTInfoToolStripMenuItem.Name = "registerNRTInfoToolStripMenuItem";
            this.registerNRTInfoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.registerNRTInfoToolStripMenuItem.Text = "Register NRT info";
            this.registerNRTInfoToolStripMenuItem.Click += new System.EventHandler(this.registerNRTInfoToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._openApi);
            this.Controls.Add(this.MenuStripDefault);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MenuStripDefault;
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.MenuStripDefault.ResumeLayout(false);
            this.MenuStripDefault.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._openApi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStripDefault;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private AxKHOpenAPILib.AxKHOpenAPI _openApi;
        private System.Windows.Forms.ToolStripMenuItem registerNRTInfoToolStripMenuItem;
    }
}

