using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Randumberator.Core;
using Randumberator.Core.Abstractions;

namespace Randumerator.UI
{
    public partial class Form1 : Form
    {
        public IPluginPicker PluginPicker { get; set; }
        public IPluginRepository PluginRepository { get; set; }
    
        public Form1()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            if (PluginPicker == null)
                throw new NullReferenceException("You must initialize the 'PluginPicker' before performing this operation.");
            if (PluginRepository == null)
                throw new NullReferenceException("You must initialize the 'Repository' before performing this operation.");

            int value = PluginPicker.PickPlugin(PluginRepository).Randomize(1);
            MessageBox.Show(value.ToString());
        }
    }
}
