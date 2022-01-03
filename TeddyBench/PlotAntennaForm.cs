﻿#pragma warning disable CA1416 // Validate platform compatibility
using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeddyBench
{
    public partial class PlotAntennaForm : Form
    {
        public PlotAntennaForm(Proxmark3.MeasurementResult result)
        {
            InitializeComponent();

            formsPlot1.Plot.PlotScatter(result.GetFrequencieskHz(), result.GetVoltages());
            formsPlot1.Plot.XLabel("Frequency [kHz]");
            formsPlot1.Plot.YLabel("Amplitude [V]");
            formsPlot1.Plot.Title("LF Antenna Plot (no relevance)");
          //  formsPlot1.Plot.AxisBounds(46.0f, 600.0f, 0, 65.0f);          
            formsPlot1.Plot.Style(Style.Gray2);
            formsPlot1.Render();
            formsPlot1.Show();

            lblV125.Text = result.vLF125.ToString("0.00", CultureInfo.InvariantCulture) + " V";
            lblV134.Text = result.vLF134.ToString("0.00", CultureInfo.InvariantCulture) + " V";
            lblVHF.Text = result.vHF.ToString("0.00", CultureInfo.InvariantCulture) + " V";
            lblOptimalFreq.Text = (result.GetPeakFrequency() / 1000.0f).ToString("0.00", CultureInfo.InvariantCulture) + " kHz";
            lblVopt.Text = result.peakV.ToString("0.00", CultureInfo.InvariantCulture) + " V";

            if (result.vHF > 45)
            {
                lblVHF.BackColor = Color.Red;
                lblVHF.Text += " (critical)";
            }
            else if (result.vHF > 40)
            {
                lblVHF.BackColor = Color.Yellow;
                lblVHF.Text += " (danger)";
            }
            else if (result.vHF > 33)
            {
                lblVHF.BackColor = Color.Green;
                lblVHF.Text += " (wow)";
            }
            else if (result.vHF > 25)
            {
                lblVHF.BackColor = Color.GreenYellow;
                lblVHF.Text += " (good)";
            }
            else if (result.vHF > 20)
            {
                lblVHF.BackColor = Color.Yellow;
                lblVHF.Text += " (weak)";
            }
            else if (result.vHF > 10)
            {
                lblVHF.BackColor = Color.Orange;
                lblVHF.Text += " (meh)";
            }
            else
            {
                lblVHF.BackColor = Color.Red;
                lblVHF.Text += " (bad)";
            }
        }
    }
}
