using System.Windows.Forms;

namespace CrispsOverlayCloner
{
    partial class SimpleSortedDictDisplay<TKey, TValue> : UserControl
    //partial class SimpleDictionaryDisplay : UserControl
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
        protected void InitializeComponent()
        {
            containerSimpleDictDisplay = new GroupBox();
            flowPanelSimpleDictDisplay = new FlowLayoutPanel();

            containerSimpleDictDisplay.SuspendLayout();
            flowPanelSimpleDictDisplay.SuspendLayout();
            SuspendLayout();
            // 
            // containerSimpleDictDisplay
            // 
            containerSimpleDictDisplay.AutoSize = true;
            containerSimpleDictDisplay.Controls.Add(flowPanelSimpleDictDisplay);
            containerSimpleDictDisplay.Dock = DockStyle.Fill;
            containerSimpleDictDisplay.Margin = new Padding(0);
            containerSimpleDictDisplay.Name = "containerSimpleDictDisplay";
            containerSimpleDictDisplay.Padding = new Padding(0);
            containerSimpleDictDisplay.TabIndex = 0;
            containerSimpleDictDisplay.TabStop = false;
            containerSimpleDictDisplay.Text = "Title";
            // 
            // flowPanelSimpleDictDisplay
            // 
            flowPanelSimpleDictDisplay.AutoSize = true;
            flowPanelSimpleDictDisplay.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowPanelSimpleDictDisplay.BackColor = Color.Transparent;
            flowPanelSimpleDictDisplay.Dock = DockStyle.Fill;
            flowPanelSimpleDictDisplay.FlowDirection = FlowDirection.TopDown;
            flowPanelSimpleDictDisplay.Margin = new Padding(0);
            flowPanelSimpleDictDisplay.Name = "flowPanelSimpleDictDisplay";
            flowPanelSimpleDictDisplay.Padding = new Padding(6);
            flowPanelSimpleDictDisplay.TabIndex = 0;
            // 
            // SimpleDictionaryDisplay
            // 
            AutoScaleDimensions = new SizeF(5F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(containerSimpleDictDisplay);
            Margin = new Padding(0);
            Name = "SimpleDictionaryDisplay";
            Padding = new Padding(0);
            containerSimpleDictDisplay.ResumeLayout(false);
            containerSimpleDictDisplay.PerformLayout();
            flowPanelSimpleDictDisplay.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        protected System.Windows.Forms.GroupBox containerSimpleDictDisplay;
        protected System.Windows.Forms.FlowLayoutPanel flowPanelSimpleDictDisplay;
    }
}
