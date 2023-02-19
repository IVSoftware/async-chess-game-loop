namespace async_chess_game_loop
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._opponentClock = new async_chess_game_loop.ChessClock();
            this._playerClock = new async_chess_game_loop.ChessClock();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel.ColumnCount = 8;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.Location = new System.Drawing.Point(121, 23);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 8;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(500, 500);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // _opponentClock
            // 
            this._opponentClock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._opponentClock.Location = new System.Drawing.Point(20, 23);
            this._opponentClock.Name = "_opponentClock";
            this._opponentClock.Size = new System.Drawing.Size(88, 38);
            this._opponentClock.TabIndex = 1;
            this._opponentClock.Text = "0:00:00";
            this._opponentClock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _playerClock
            // 
            this._playerClock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._playerClock.Location = new System.Drawing.Point(20, 485);
            this._playerClock.Name = "_playerClock";
            this._playerClock.Size = new System.Drawing.Size(88, 38);
            this._playerClock.TabIndex = 1;
            this._playerClock.Text = "0:00:00";
            this._playerClock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // richTextBox
            // 
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.BackColor = System.Drawing.Color.White;
            this.richTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.richTextBox.Location = new System.Drawing.Point(639, 23);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(170, 497);
            this.richTextBox.TabIndex = 2;
            this.richTextBox.TabStop = false;
            this.richTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 544);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this._playerClock);
            this.Controls.Add(this._opponentClock);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.Text = "Chess";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private ChessClock _opponentClock;
        private ChessClock _playerClock;
        private System.Windows.Forms.RichTextBox richTextBox;
    }
}
