namespace OpenInput
{
    using System;
    using System.Windows.Forms;

    partial class OutputForm
    {
        const int MaxHistory = 100;

        private GroupBox mouseGroupBox, keyboardGroupBox;
        private Label mouseNamesLabel, mouseStateLabel, keyboardNamesLabel;
        private ListBox keyboardHistory;

        private void InitializeComponent()
        {
            this.Width = 500;
            this.Height = 370;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.mouseGroupBox = new GroupBox();
            this.mouseNamesLabel = new Label();
            this.mouseStateLabel = new Label();

            this.keyboardGroupBox = new GroupBox();
            this.keyboardHistory = new ListBox();
            this.keyboardNamesLabel = new Label();

            this.mouseGroupBox.SuspendLayout();
            this.SuspendLayout();

            //
            //
            //

            this.mouseGroupBox.Controls.Add(this.mouseNamesLabel);
            this.mouseGroupBox.Controls.Add(this.mouseStateLabel);
            this.mouseGroupBox.Location = new System.Drawing.Point(12, 12);
            this.mouseGroupBox.Size = new System.Drawing.Size(460, 100);
            this.mouseGroupBox.Text = "Mouse";
            this.mouseGroupBox.TabStop = false;

            this.mouseNamesLabel.AutoSize = true;
            this.mouseNamesLabel.Location = new System.Drawing.Point(7, 18);
            this.mouseNamesLabel.Text = "Name(s):";
            this.mouseNamesLabel.TabStop = false;

            this.mouseStateLabel.AutoSize = true;
            this.mouseStateLabel.Location = new System.Drawing.Point(7, 36);
            this.mouseStateLabel.Text = "Position: {X}, {Y}";
            this.mouseStateLabel.TabStop = false;

            //
            //
            //

            this.keyboardGroupBox.Controls.Add(this.keyboardNamesLabel);
            this.keyboardGroupBox.Controls.Add(this.keyboardHistory);
            this.keyboardGroupBox.Location = new System.Drawing.Point(12, 118);
            this.keyboardGroupBox.Size = new System.Drawing.Size(460, 200);
            this.keyboardGroupBox.Text = "Keyboard";
            this.keyboardGroupBox.TabStop = false;

            this.keyboardHistory.FormattingEnabled = true;
            this.keyboardHistory.Location = new System.Drawing.Point(10, 47);
            this.keyboardHistory.Size = new System.Drawing.Size(444, 147);
            this.keyboardHistory.BorderStyle = BorderStyle.None;
            this.keyboardHistory.BackColor = this.BackColor;
            this.keyboardHistory.TabStop = false;

            this.keyboardNamesLabel.AutoSize = true;
            this.keyboardNamesLabel.Location = new System.Drawing.Point(7, 18);
            this.keyboardNamesLabel.Text = "Name(s):";
            this.keyboardNamesLabel.TabStop = false;

            //
            //
            //

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 331);

            this.Controls.Add(this.keyboardGroupBox);
            this.Controls.Add(this.mouseGroupBox);

            this.mouseGroupBox.ResumeLayout(false);
            this.mouseGroupBox.PerformLayout();
            this.ResumeLayout(false);
        }

        private void AddKeyboardHistory(string text)
        {
            for (int i = 0; i < (keyboardHistory.Items.Count - MaxHistory); i++)
                keyboardHistory.Items.RemoveAt(0);

            var timestamp = DateTime.Now.ToString("[HH:mm:ss]");
            keyboardHistory.Items.Add(timestamp + ": " + text);

            int visibleItems = keyboardHistory.ClientSize.Height / keyboardHistory.ItemHeight;
            keyboardHistory.TopIndex = Math.Max(keyboardHistory.Items.Count - visibleItems + 1, 0);
        }
    }
}
