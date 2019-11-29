namespace congye_pe
{
    partial class FrmTjdj
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTjdj));
            this.PDoc = new System.Drawing.Printing.PrintDocument();
            this.Pvd = new System.Windows.Forms.PrintPreviewDialog();
            this.Psd = new System.Windows.Forms.PageSetupDialog();
            this.Pd = new System.Windows.Forms.PrintDialog();
            this.dtp_djrq = new System.Windows.Forms.DateTimePicker();
            this.txt_qt = new System.Windows.Forms.TextBox();
            this.txt_pfb = new System.Windows.Forms.TextBox();
            this.txt_fjh = new System.Windows.Forms.TextBox();
            this.txt_sh = new System.Windows.Forms.TextBox();
            this.txt_lj = new System.Windows.Forms.TextBox();
            this.txt_gy = new System.Windows.Forms.TextBox();
            this.cbo_xb = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_lxdh = new System.Windows.Forms.TextBox();
            this.txt_gzdw = new System.Windows.Forms.TextBox();
            this.txt_sfzhm = new System.Windows.Forms.TextBox();
            this.txt_nl = new System.Windows.Forms.TextBox();
            this.txt_xm = new System.Windows.Forms.TextBox();
            this.cbo_hylbdl = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.PDoc2 = new System.Drawing.Printing.PrintDocument();
            this.Pvd2 = new System.Windows.Forms.PrintPreviewDialog();
            this.Psd2 = new System.Windows.Forms.PageSetupDialog();
            this.Pd2 = new System.Windows.Forms.PrintDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // PDoc
            // 
            this.PDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PDoc_PrintPage);
            // 
            // Pvd
            // 
            this.Pvd.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.Pvd.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.Pvd.ClientSize = new System.Drawing.Size(400, 300);
            this.Pvd.Document = this.PDoc;
            this.Pvd.Enabled = true;
            this.Pvd.Icon = ((System.Drawing.Icon)(resources.GetObject("Pvd.Icon")));
            this.Pvd.Name = "Pvd";
            this.Pvd.Visible = false;
            this.Pvd.Load += new System.EventHandler(this.Pvd_Load);
            // 
            // Psd
            // 
            this.Psd.EnableMetric = true;
            // 
            // Pd
            // 
            this.Pd.UseEXDialog = true;
            // 
            // dtp_djrq
            // 
            this.dtp_djrq.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtp_djrq.Location = new System.Drawing.Point(408, 109);
            this.dtp_djrq.Name = "dtp_djrq";
            this.dtp_djrq.Size = new System.Drawing.Size(154, 29);
            this.dtp_djrq.TabIndex = 101;
            // 
            // txt_qt
            // 
            this.txt_qt.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_qt.Location = new System.Drawing.Point(832, 510);
            this.txt_qt.Name = "txt_qt";
            this.txt_qt.Size = new System.Drawing.Size(89, 29);
            this.txt_qt.TabIndex = 117;
            this.txt_qt.Text = "/";
            // 
            // txt_pfb
            // 
            this.txt_pfb.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_pfb.Location = new System.Drawing.Point(704, 510);
            this.txt_pfb.Name = "txt_pfb";
            this.txt_pfb.Size = new System.Drawing.Size(89, 29);
            this.txt_pfb.TabIndex = 116;
            this.txt_pfb.Text = "/";
            // 
            // txt_fjh
            // 
            this.txt_fjh.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_fjh.Location = new System.Drawing.Point(573, 510);
            this.txt_fjh.Name = "txt_fjh";
            this.txt_fjh.Size = new System.Drawing.Size(89, 29);
            this.txt_fjh.TabIndex = 115;
            this.txt_fjh.Text = "/";
            // 
            // txt_sh
            // 
            this.txt_sh.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_sh.Location = new System.Drawing.Point(473, 510);
            this.txt_sh.Name = "txt_sh";
            this.txt_sh.Size = new System.Drawing.Size(89, 29);
            this.txt_sh.TabIndex = 114;
            this.txt_sh.Text = "/";
            // 
            // txt_lj
            // 
            this.txt_lj.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_lj.Location = new System.Drawing.Point(355, 510);
            this.txt_lj.Name = "txt_lj";
            this.txt_lj.Size = new System.Drawing.Size(89, 29);
            this.txt_lj.TabIndex = 113;
            this.txt_lj.Text = "/";
            // 
            // txt_gy
            // 
            this.txt_gy.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_gy.Location = new System.Drawing.Point(232, 510);
            this.txt_gy.Name = "txt_gy";
            this.txt_gy.Size = new System.Drawing.Size(89, 29);
            this.txt_gy.TabIndex = 112;
            this.txt_gy.Text = "/";
            // 
            // cbo_xb
            // 
            this.cbo_xb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_xb.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbo_xb.FormattingEnabled = true;
            this.cbo_xb.Items.AddRange(new object[] {
            "男",
            "女"});
            this.cbo_xb.Location = new System.Drawing.Point(573, 170);
            this.cbo_xb.Name = "cbo_xb";
            this.cbo_xb.Size = new System.Drawing.Size(79, 27);
            this.cbo_xb.TabIndex = 104;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(177, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 19);
            this.label1.TabIndex = 49;
            // 
            // txt_lxdh
            // 
            this.txt_lxdh.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_lxdh.Location = new System.Drawing.Point(801, 230);
            this.txt_lxdh.Name = "txt_lxdh";
            this.txt_lxdh.Size = new System.Drawing.Size(137, 29);
            this.txt_lxdh.TabIndex = 108;
            this.txt_lxdh.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_lxdh_KeyPress);
            // 
            // txt_gzdw
            // 
            this.txt_gzdw.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_gzdw.Location = new System.Drawing.Point(382, 276);
            this.txt_gzdw.Name = "txt_gzdw";
            this.txt_gzdw.Size = new System.Drawing.Size(270, 29);
            this.txt_gzdw.TabIndex = 109;
            // 
            // txt_sfzhm
            // 
            this.txt_sfzhm.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_sfzhm.Location = new System.Drawing.Point(382, 230);
            this.txt_sfzhm.Name = "txt_sfzhm";
            this.txt_sfzhm.Size = new System.Drawing.Size(270, 29);
            this.txt_sfzhm.TabIndex = 107;
            this.txt_sfzhm.TabIndexChanged += new System.EventHandler(this.txt_sfzhm_TabIndexChanged);
            this.txt_sfzhm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_sfzhm_KeyPress);
            // 
            // txt_nl
            // 
            this.txt_nl.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_nl.Location = new System.Drawing.Point(801, 169);
            this.txt_nl.Name = "txt_nl";
            this.txt_nl.Size = new System.Drawing.Size(44, 29);
            this.txt_nl.TabIndex = 105;
            this.txt_nl.TextChanged += new System.EventHandler(this.txt_nl_TextChanged);
            this.txt_nl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_nl_KeyPress);
            // 
            // txt_xm
            // 
            this.txt_xm.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_xm.Location = new System.Drawing.Point(324, 169);
            this.txt_xm.Name = "txt_xm";
            this.txt_xm.Size = new System.Drawing.Size(120, 29);
            this.txt_xm.TabIndex = 103;
            // 
            // cbo_hylbdl
            // 
            this.cbo_hylbdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_hylbdl.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbo_hylbdl.FormattingEnabled = true;
            this.cbo_hylbdl.Items.AddRange(new object[] {
            "食品安全",
            "公共卫生",
            "劳动卫生"});
            this.cbo_hylbdl.Location = new System.Drawing.Point(801, 278);
            this.cbo_hylbdl.Name = "cbo_hylbdl";
            this.cbo_hylbdl.Size = new System.Drawing.Size(137, 27);
            this.cbo_hylbdl.TabIndex = 110;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(96, 151);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(158, 199);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 57;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox2.Location = new System.Drawing.Point(94, 57);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(273, 46);
            this.pictureBox2.TabIndex = 96;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox3.Location = new System.Drawing.Point(144, 366);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(58, 33);
            this.pictureBox3.TabIndex = 96;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.button8_Click_1);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox4.Location = new System.Drawing.Point(298, 601);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(174, 45);
            this.pictureBox4.TabIndex = 97;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.button5_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox6.Location = new System.Drawing.Point(505, 601);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(91, 45);
            this.pictureBox6.TabIndex = 97;
            this.pictureBox6.TabStop = false;
            this.pictureBox6.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox7.Location = new System.Drawing.Point(623, 601);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(91, 45);
            this.pictureBox7.TabIndex = 97;
            this.pictureBox7.TabStop = false;
            this.pictureBox7.Click += new System.EventHandler(this.button4_Click);
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox8.Location = new System.Drawing.Point(741, 601);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(91, 45);
            this.pictureBox8.TabIndex = 97;
            this.pictureBox8.TabStop = false;
            this.pictureBox8.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(797, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 19);
            this.label2.TabIndex = 49;
            // 
            // pictureBox9
            // 
            this.pictureBox9.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox9.Location = new System.Drawing.Point(857, 601);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(105, 45);
            this.pictureBox9.TabIndex = 119;
            this.pictureBox9.TabStop = false;
            this.pictureBox9.Click += new System.EventHandler(this.pictureBox9_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(355, 354);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 45);
            this.button2.TabIndex = 121;
            this.button2.Text = "登记信息修改";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_4);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox5.Location = new System.Drawing.Point(101, 601);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(174, 45);
            this.pictureBox5.TabIndex = 122;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new System.EventHandler(this.pictureBox5_Click);
            // 
            // PDoc2
            // 
            this.PDoc2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PDoc2_PrintPage);
            // 
            // Pvd2
            // 
            this.Pvd2.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.Pvd2.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.Pvd2.ClientSize = new System.Drawing.Size(400, 300);
            this.Pvd2.Document = this.PDoc2;
            this.Pvd2.Enabled = true;
            this.Pvd2.Icon = ((System.Drawing.Icon)(resources.GetObject("Pvd2.Icon")));
            this.Pvd2.Name = "Pvd";
            this.Pvd2.Visible = false;
            // 
            // Psd2
            // 
            this.Psd2.EnableMetric = true;
            // 
            // Pd2
            // 
            this.Pd2.UseEXDialog = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FrmTjdj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::congye_pe.Properties.Resources.体检登记10;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1076, 702);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox9);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.cbo_hylbdl);
            this.Controls.Add(this.dtp_djrq);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txt_qt);
            this.Controls.Add(this.txt_pfb);
            this.Controls.Add(this.txt_fjh);
            this.Controls.Add(this.txt_sh);
            this.Controls.Add(this.txt_lj);
            this.Controls.Add(this.txt_gy);
            this.Controls.Add(this.cbo_xb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_lxdh);
            this.Controls.Add(this.txt_gzdw);
            this.Controls.Add(this.txt_sfzhm);
            this.Controls.Add(this.txt_nl);
            this.Controls.Add(this.txt_xm);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTjdj";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "体检登记";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Drawing.Printing.PrintDocument PDoc;
        private System.Windows.Forms.PrintPreviewDialog Pvd;
        private System.Windows.Forms.PageSetupDialog Psd;
        private System.Windows.Forms.PrintDialog Pd;
        private System.Windows.Forms.DateTimePicker dtp_djrq;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txt_qt;
        private System.Windows.Forms.TextBox txt_pfb;
        private System.Windows.Forms.TextBox txt_fjh;
        private System.Windows.Forms.TextBox txt_sh;
        private System.Windows.Forms.TextBox txt_lj;
        private System.Windows.Forms.TextBox txt_gy;
        private System.Windows.Forms.ComboBox cbo_xb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_lxdh;
        private System.Windows.Forms.TextBox txt_gzdw;
        private System.Windows.Forms.TextBox txt_sfzhm;
        private System.Windows.Forms.TextBox txt_nl;
        private System.Windows.Forms.TextBox txt_xm;
        private System.Windows.Forms.ComboBox cbo_hylbdl;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Drawing.Printing.PrintDocument PDoc2;
        private System.Windows.Forms.PrintPreviewDialog Pvd2;
        private System.Windows.Forms.PageSetupDialog Psd2;
        private System.Windows.Forms.PrintDialog Pd2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

