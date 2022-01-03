﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace TeddyBench
{
    public partial class AskUIDForm : Form
    {
        internal string Uid;
        private Proxmark3 Proxmark3;

        public AskUIDForm(Proxmark3 pm3)
        {
            Proxmark3 = pm3;
            InitializeComponent();
            txtUid.Select();
            txtUid.Select(6, 10);
            txtUid_TextChanged(null, null);

            if (Proxmark3 != null)
            {
                Proxmark3.UidFound += Proxmark3_UidFound;
            }
        }

        private void Proxmark3_UidFound(object sender, string e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => Proxmark3_UidFound(sender, e)));
                return;
            }

            if (e != null && txtUid.Text != e)
            {
                txtUid.Text = e;
                txtUid.Select();
                txtUid.Select(6, 10);
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    SystemSounds.Beep.Play();
            }
        }

        protected override void OnShown(EventArgs e)
        {
            txtUid.Text = Uid;
            txtUid.Select();
            txtUid.Select(6, 10);
            base.OnShown(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (Proxmark3 != null)
            {
                Proxmark3.UidFound -= Proxmark3_UidFound;
            }
            Uid = txtUid.Text.ToUpper();

            base.OnClosing(e);
        }

        private void txtUid_TextChanged(object sender, EventArgs e)
        {
            if (!txtUid.Text.StartsWith("E00403"))
            {
                txtUid.Text = "E00403";
                txtUid.Select(6, 1);
            }

            if (txtUid.Text.Length != 16 || !txtUid.Text.All("0123456789abcdefABCDEF".Contains))
            {
                txtUid.BackColor = Color.PaleVioletRed;
                btnOk.Enabled = false;
            }
            else
            {
                txtUid.BackColor = Color.LightGreen;
                btnOk.Enabled = true;
            }
        }

        private void txtUid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk.PerformClick();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

        }

        private void Cancel_Click(object sender, EventArgs e)
        {

        }
    }
}
