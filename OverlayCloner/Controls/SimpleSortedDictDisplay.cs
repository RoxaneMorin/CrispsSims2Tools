/*
 * Overlay Package Cloner : a tool to quickly create new overlay packages for the Sims 2.
 *
 * Created by Roxane Morin, "Crisps" in the Sims 2 community.
 * https://github.com/RoxaneMorin
 * https://crispsandkerosene.tumblr.com/
 *
 * This code uses libraries by William Howard.
 * https://github.com/whoward69
 * https://www.picknmixmods.com/
 */


using System;
using System.ComponentModel;


namespace CrispsOverlayCloner
{
    //[System.ComponentModel.ComplexBindingProperties("DataSource")]
    public partial class SimpleSortedDictDisplay<TKey, TValue> : UserControl
    {
        // VARIABLES
        protected SortedDictionary<TKey, TValue>? dataSource;


        // PROPERTIES
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Data")]
        public SortedDictionary<TKey, TValue>? DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
                BuildContents();
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        public override string Text
        {
            get { return containerSimpleDictDisplay.Text; }
            set { containerSimpleDictDisplay.Text = value; }
        }



        // INIT
        public SimpleSortedDictDisplay()
        {
            InitializeComponent();
        }



        // METHODS
        protected virtual void BuildContents()
        {
            flowPanelSimpleDictDisplay.Controls.Clear();
            if (dataSource == null) return;

            foreach (var kvp in dataSource)
            {
                var labelKey = new Label
                {
                    Text = string.Format("{0}:", BuildKeyString(kvp.Key)),

                    Size = new Size(80, 15),

                    Margin = new Padding(0),
                    Padding = new Padding(0)
                };

                var labelValue = new Label
                {
                    Text = BuildValueString(kvp.Value),
                    RightToLeft = RightToLeft.Yes,

                    Size = new Size(30, 15),
                    
                    Margin = new Padding(0),
                    Padding = new Padding(0)
                };

                var panelKVPair = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,

                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowOnly,
                    Dock = DockStyle.Fill,

                    Margin = new Padding(4, 2, 0, 2),
                    Padding = new Padding(0, 0, 0, 0)

                    //BackColor = Color.Red
                };

                panelKVPair.Controls.Add(labelKey);
                panelKVPair.Controls.Add(labelValue);
                flowPanelSimpleDictDisplay.Controls.Add(panelKVPair);
            }
        }

        protected virtual string BuildKeyString(TKey key)
        {
            return (key == null) ? "[null key]" : key.ToString();
        }

        protected virtual string BuildValueString(TValue value)
        {
            return (value == null) ? "[null value]" : value.ToString();
        }
    }
}
