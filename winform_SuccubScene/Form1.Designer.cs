namespace SuccubScene
{
    partial class SuccubSceneForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.startButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.x1SpeedButton = new System.Windows.Forms.Button();
            this.x1_5SpeedButton = new System.Windows.Forms.Button();
            this.x2SpeedButton = new System.Windows.Forms.Button();
            this.x3SpeedButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.modelsSpeedTextBox = new System.Windows.Forms.TextBox();
            this.addStopPointButton = new System.Windows.Forms.Button();
            this.addStartPointButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lineEditTextBox = new System.Windows.Forms.TextBox();
            this.timerLabel = new System.Windows.Forms.Label();
            this.stopPointPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.stopPointNameLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.stopPointPauseTextBox = new System.Windows.Forms.TextBox();
            this.deleteStopPointButton = new System.Windows.Forms.Button();
            this.groupPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.groupNameLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numberOfModelsTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupDelayTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupPathTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupPeriodTextBox = new System.Windows.Forms.TextBox();
            this.deleteGroupButton = new System.Windows.Forms.Button();
            this.saveGroupButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.linePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lineEditNameLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lineLengthEditTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.stopPointPanel.SuspendLayout();
            this.groupPanel.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.linePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.startButton);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel3);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.modelsSpeedTextBox);
            this.flowLayoutPanel1.Controls.Add(this.addStopPointButton);
            this.flowLayoutPanel1.Controls.Add(this.addStartPointButton);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.lineEditTextBox);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.MinimumSize = new System.Drawing.Size(210, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(210, 268);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(3, 3);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(197, 31);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Старт";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.Controls.Add(this.x1SpeedButton);
            this.flowLayoutPanel3.Controls.Add(this.x1_5SpeedButton);
            this.flowLayoutPanel3.Controls.Add(this.x2SpeedButton);
            this.flowLayoutPanel3.Controls.Add(this.x3SpeedButton);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 40);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(177, 36);
            this.flowLayoutPanel3.TabIndex = 6;
            // 
            // x1SpeedButton
            // 
            this.x1SpeedButton.AutoSize = true;
            this.x1SpeedButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.x1SpeedButton.Location = new System.Drawing.Point(3, 3);
            this.x1SpeedButton.Name = "x1SpeedButton";
            this.x1SpeedButton.Size = new System.Drawing.Size(35, 30);
            this.x1SpeedButton.TabIndex = 0;
            this.x1SpeedButton.Text = "x1";
            this.x1SpeedButton.UseVisualStyleBackColor = false;
            this.x1SpeedButton.Click += new System.EventHandler(this.x1SpeedButton_Click);
            // 
            // x1_5SpeedButton
            // 
            this.x1_5SpeedButton.AutoSize = true;
            this.x1_5SpeedButton.Location = new System.Drawing.Point(44, 3);
            this.x1_5SpeedButton.Name = "x1_5SpeedButton";
            this.x1_5SpeedButton.Size = new System.Drawing.Size(48, 30);
            this.x1_5SpeedButton.TabIndex = 1;
            this.x1_5SpeedButton.Text = "x1.5";
            this.x1_5SpeedButton.UseVisualStyleBackColor = true;
            this.x1_5SpeedButton.Click += new System.EventHandler(this.x1_5SpeedButton_Click);
            // 
            // x2SpeedButton
            // 
            this.x2SpeedButton.AutoSize = true;
            this.x2SpeedButton.Location = new System.Drawing.Point(98, 3);
            this.x2SpeedButton.Name = "x2SpeedButton";
            this.x2SpeedButton.Size = new System.Drawing.Size(35, 30);
            this.x2SpeedButton.TabIndex = 2;
            this.x2SpeedButton.Text = "x2";
            this.x2SpeedButton.UseVisualStyleBackColor = true;
            this.x2SpeedButton.Click += new System.EventHandler(this.x2SpeedButton_Click);
            // 
            // x3SpeedButton
            // 
            this.x3SpeedButton.AutoSize = true;
            this.x3SpeedButton.Location = new System.Drawing.Point(139, 3);
            this.x3SpeedButton.Name = "x3SpeedButton";
            this.x3SpeedButton.Size = new System.Drawing.Size(35, 30);
            this.x3SpeedButton.TabIndex = 3;
            this.x3SpeedButton.Text = "x3";
            this.x3SpeedButton.UseVisualStyleBackColor = true;
            this.x3SpeedButton.Click += new System.EventHandler(this.x3SpeedButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 85);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Скорость моделей(см/c):";
            // 
            // modelsSpeedTextBox
            // 
            this.modelsSpeedTextBox.Location = new System.Drawing.Point(3, 111);
            this.modelsSpeedTextBox.Name = "modelsSpeedTextBox";
            this.modelsSpeedTextBox.Size = new System.Drawing.Size(54, 26);
            this.modelsSpeedTextBox.TabIndex = 3;
            this.modelsSpeedTextBox.Text = "150";
            this.modelsSpeedTextBox.TextChanged += new System.EventHandler(this.modelsSpeedTextBox_TextChanged);
            this.modelsSpeedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // addStopPointButton
            // 
            this.addStopPointButton.Location = new System.Drawing.Point(3, 143);
            this.addStopPointButton.Name = "addStopPointButton";
            this.addStopPointButton.Size = new System.Drawing.Size(197, 31);
            this.addStopPointButton.TabIndex = 2;
            this.addStopPointButton.Text = "Добавить точку";
            this.addStopPointButton.UseVisualStyleBackColor = true;
            this.addStopPointButton.Click += new System.EventHandler(this.addStopPointButton_Click);
            // 
            // addStartPointButton
            // 
            this.addStartPointButton.Location = new System.Drawing.Point(3, 180);
            this.addStartPointButton.Name = "addStartPointButton";
            this.addStartPointButton.Size = new System.Drawing.Size(197, 31);
            this.addStartPointButton.TabIndex = 3;
            this.addStartPointButton.Text = "Добавить выход";
            this.addStartPointButton.UseVisualStyleBackColor = true;
            this.addStartPointButton.Click += new System.EventHandler(this.addStartPointButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Настроить линию:";
            // 
            // lineEditTextBox
            // 
            this.lineEditTextBox.Location = new System.Drawing.Point(3, 237);
            this.lineEditTextBox.Name = "lineEditTextBox";
            this.lineEditTextBox.Size = new System.Drawing.Size(100, 26);
            this.lineEditTextBox.TabIndex = 5;
            this.lineEditTextBox.TextChanged += new System.EventHandler(this.lineEditTextBox_TextChanged);
            this.lineEditTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lineEditTextBox_KeyPress);
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timerLabel.Location = new System.Drawing.Point(523, 9);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(159, 29);
            this.timerLabel.TabIndex = 1;
            this.timerLabel.Text = "Время: 00:00";
            this.timerLabel.Click += new System.EventHandler(this.timerLabel_Click);
            // 
            // stopPointPanel
            // 
            this.stopPointPanel.AutoSize = true;
            this.stopPointPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stopPointPanel.Controls.Add(this.stopPointNameLabel);
            this.stopPointPanel.Controls.Add(this.label5);
            this.stopPointPanel.Controls.Add(this.stopPointPauseTextBox);
            this.stopPointPanel.Controls.Add(this.deleteStopPointButton);
            this.stopPointPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.stopPointPanel.Location = new System.Drawing.Point(3, 357);
            this.stopPointPanel.MinimumSize = new System.Drawing.Size(210, 2);
            this.stopPointPanel.Name = "stopPointPanel";
            this.stopPointPanel.Size = new System.Drawing.Size(210, 117);
            this.stopPointPanel.TabIndex = 2;
            this.stopPointPanel.Visible = false;
            // 
            // stopPointNameLabel
            // 
            this.stopPointNameLabel.Location = new System.Drawing.Point(3, 0);
            this.stopPointNameLabel.Name = "stopPointNameLabel";
            this.stopPointNameLabel.Size = new System.Drawing.Size(188, 20);
            this.stopPointNameLabel.TabIndex = 5;
            this.stopPointNameLabel.Text = "Точка номер 5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 26);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Время паузы(сек):";
            // 
            // stopPointPauseTextBox
            // 
            this.stopPointPauseTextBox.Location = new System.Drawing.Point(3, 49);
            this.stopPointPauseTextBox.Name = "stopPointPauseTextBox";
            this.stopPointPauseTextBox.Size = new System.Drawing.Size(51, 26);
            this.stopPointPauseTextBox.TabIndex = 7;
            this.stopPointPauseTextBox.TextChanged += new System.EventHandler(this.stopPointPauseTextBox_TextChanged);
            this.stopPointPauseTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.stopPointPauseTextBox_KeyPress);
            // 
            // deleteStopPointButton
            // 
            this.deleteStopPointButton.Location = new System.Drawing.Point(3, 81);
            this.deleteStopPointButton.Name = "deleteStopPointButton";
            this.deleteStopPointButton.Size = new System.Drawing.Size(197, 31);
            this.deleteStopPointButton.TabIndex = 8;
            this.deleteStopPointButton.Text = "Удалить точку";
            this.deleteStopPointButton.UseVisualStyleBackColor = true;
            this.deleteStopPointButton.Click += new System.EventHandler(this.DeleteStopPointButton_Click);
            // 
            // groupPanel
            // 
            this.groupPanel.AutoSize = true;
            this.groupPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.groupPanel.Controls.Add(this.groupNameLabel);
            this.groupPanel.Controls.Add(this.label3);
            this.groupPanel.Controls.Add(this.numberOfModelsTextBox);
            this.groupPanel.Controls.Add(this.label4);
            this.groupPanel.Controls.Add(this.groupDelayTextBox);
            this.groupPanel.Controls.Add(this.label7);
            this.groupPanel.Controls.Add(this.groupPathTextBox);
            this.groupPanel.Controls.Add(this.label6);
            this.groupPanel.Controls.Add(this.groupPeriodTextBox);
            this.groupPanel.Controls.Add(this.deleteGroupButton);
            this.groupPanel.Controls.Add(this.saveGroupButton);
            this.groupPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.groupPanel.Location = new System.Drawing.Point(3, 480);
            this.groupPanel.MinimumSize = new System.Drawing.Size(210, 2);
            this.groupPanel.Name = "groupPanel";
            this.groupPanel.Size = new System.Drawing.Size(212, 312);
            this.groupPanel.TabIndex = 3;
            this.groupPanel.Visible = false;
            // 
            // groupNameLabel
            // 
            this.groupNameLabel.AutoSize = true;
            this.groupNameLabel.Location = new System.Drawing.Point(3, 0);
            this.groupNameLabel.Name = "groupNameLabel";
            this.groupNameLabel.Size = new System.Drawing.Size(125, 20);
            this.groupNameLabel.TabIndex = 4;
            this.groupNameLabel.Text = "Группа номер 1";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Кол-во моделей:";
            // 
            // numberOfModelsTextBox
            // 
            this.numberOfModelsTextBox.Location = new System.Drawing.Point(5, 51);
            this.numberOfModelsTextBox.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.numberOfModelsTextBox.Name = "numberOfModelsTextBox";
            this.numberOfModelsTextBox.Size = new System.Drawing.Size(102, 26);
            this.numberOfModelsTextBox.TabIndex = 10;
            this.numberOfModelsTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberOfModelsTextBox_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Задержка от старта:";
            // 
            // groupDelayTextBox
            // 
            this.groupDelayTextBox.Location = new System.Drawing.Point(5, 103);
            this.groupDelayTextBox.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.groupDelayTextBox.Name = "groupDelayTextBox";
            this.groupDelayTextBox.Size = new System.Drawing.Size(100, 26);
            this.groupDelayTextBox.TabIndex = 11;
            this.groupDelayTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.groupDelayTextBox_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "Путь:";
            // 
            // groupPathTextBox
            // 
            this.groupPathTextBox.Location = new System.Drawing.Point(5, 155);
            this.groupPathTextBox.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.groupPathTextBox.Name = "groupPathTextBox";
            this.groupPathTextBox.Size = new System.Drawing.Size(186, 26);
            this.groupPathTextBox.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(204, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "Период выхода моделей:";
            // 
            // groupPeriodTextBox
            // 
            this.groupPeriodTextBox.Location = new System.Drawing.Point(5, 207);
            this.groupPeriodTextBox.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.groupPeriodTextBox.Name = "groupPeriodTextBox";
            this.groupPeriodTextBox.Size = new System.Drawing.Size(100, 26);
            this.groupPeriodTextBox.TabIndex = 13;
            this.groupPeriodTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.groupPeriodTextBox_KeyPress);
            // 
            // deleteGroupButton
            // 
            this.deleteGroupButton.Location = new System.Drawing.Point(3, 239);
            this.deleteGroupButton.Name = "deleteGroupButton";
            this.deleteGroupButton.Size = new System.Drawing.Size(197, 31);
            this.deleteGroupButton.TabIndex = 9;
            this.deleteGroupButton.Text = "Удалить группу";
            this.deleteGroupButton.UseVisualStyleBackColor = true;
            this.deleteGroupButton.Click += new System.EventHandler(this.deleteGroupButton_Click);
            // 
            // saveGroupButton
            // 
            this.saveGroupButton.Location = new System.Drawing.Point(3, 276);
            this.saveGroupButton.Name = "saveGroupButton";
            this.saveGroupButton.Size = new System.Drawing.Size(197, 31);
            this.saveGroupButton.TabIndex = 8;
            this.saveGroupButton.Text = "Сохранить";
            this.saveGroupButton.UseVisualStyleBackColor = true;
            this.saveGroupButton.Click += new System.EventHandler(this.saveGroupButton_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.flowLayoutPanel1);
            this.flowLayoutPanel2.Controls.Add(this.linePanel);
            this.flowLayoutPanel2.Controls.Add(this.stopPointPanel);
            this.flowLayoutPanel2.Controls.Add(this.groupPanel);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(1116, 12);
            this.flowLayoutPanel2.MinimumSize = new System.Drawing.Size(210, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.flowLayoutPanel2.Size = new System.Drawing.Size(218, 827);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // linePanel
            // 
            this.linePanel.AutoSize = true;
            this.linePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linePanel.Controls.Add(this.lineEditNameLabel);
            this.linePanel.Controls.Add(this.label9);
            this.linePanel.Controls.Add(this.lineLengthEditTextBox);
            this.linePanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.linePanel.Location = new System.Drawing.Point(3, 277);
            this.linePanel.MinimumSize = new System.Drawing.Size(210, 2);
            this.linePanel.Name = "linePanel";
            this.linePanel.Size = new System.Drawing.Size(210, 74);
            this.linePanel.TabIndex = 4;
            this.linePanel.Visible = false;
            // 
            // lineEditNameLabel
            // 
            this.lineEditNameLabel.AutoSize = true;
            this.lineEditNameLabel.Location = new System.Drawing.Point(3, 0);
            this.lineEditNameLabel.Name = "lineEditNameLabel";
            this.lineEditNameLabel.Size = new System.Drawing.Size(121, 20);
            this.lineEditNameLabel.TabIndex = 0;
            this.lineEditNameLabel.Text = "Линия номер 8";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(154, 20);
            this.label9.TabIndex = 1;
            this.label9.Text = "Длина линии(в см):";
            // 
            // lineLengthEditTextBox
            // 
            this.lineLengthEditTextBox.Location = new System.Drawing.Point(3, 43);
            this.lineLengthEditTextBox.Name = "lineLengthEditTextBox";
            this.lineLengthEditTextBox.Size = new System.Drawing.Size(100, 26);
            this.lineLengthEditTextBox.TabIndex = 2;
            this.lineLengthEditTextBox.TextChanged += new System.EventHandler(this.lineLengthEditTextBox_TextChanged);
            this.lineLengthEditTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lineEditTextBox_KeyPress);
            // 
            // saveButton
            // 
            this.saveButton.AutoSize = true;
            this.saveButton.Location = new System.Drawing.Point(1, -1);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(129, 30);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Сохранить как";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.AutoSize = true;
            this.loadButton.Location = new System.Drawing.Point(136, -1);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(97, 30);
            this.loadButton.TabIndex = 6;
            this.loadButton.Text = "Загрузить";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // SuccubSceneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 712);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.timerLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SuccubSceneForm";
            this.Text = "SuccubScene";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.stopPointPanel.ResumeLayout(false);
            this.stopPointPanel.PerformLayout();
            this.groupPanel.ResumeLayout(false);
            this.groupPanel.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.linePanel.ResumeLayout(false);
            this.linePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addStopPointButton;
        private System.Windows.Forms.Button addStartPointButton;
        private System.Windows.Forms.TextBox modelsSpeedTextBox;
        private System.Windows.Forms.Label timerLabel;
        private System.Windows.Forms.FlowLayoutPanel stopPointPanel;
        private System.Windows.Forms.Label stopPointNameLabel;
        private System.Windows.Forms.FlowLayoutPanel groupPanel;
        private System.Windows.Forms.Label groupNameLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox stopPointPauseTextBox;
        private System.Windows.Forms.Button deleteStopPointButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox numberOfModelsTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox groupDelayTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox groupPathTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox groupPeriodTextBox;
        private System.Windows.Forms.Button deleteGroupButton;
        private System.Windows.Forms.Button saveGroupButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox lineEditTextBox;
        private System.Windows.Forms.FlowLayoutPanel linePanel;
        private System.Windows.Forms.Label lineEditNameLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox lineLengthEditTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button x1SpeedButton;
        private System.Windows.Forms.Button x1_5SpeedButton;
        private System.Windows.Forms.Button x2SpeedButton;
        private System.Windows.Forms.Button x3SpeedButton;
    }
}

