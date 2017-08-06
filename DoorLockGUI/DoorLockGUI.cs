using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LCDLabel;

namespace DoorLockGUI
{
    public partial class DoorLockKeypad : Form
    {

        private string _attempt = string.Empty;
        private DoorLock Lock;
        private Timer LockoutTimer = new Timer();
        public DoorLockKeypad()
        {
            InitializeComponent();
            Lock = new DoorLock();
            LockoutTimer.Tick += LockoutTimer_Tick;
            LockoutTimer.Interval = Lock.LockOutTime;
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            Button myBtn = sender as Button;

            if (!Display.Text.StartsWith("*"))
            {
                Display.Text = "*";
                _attempt = myBtn.Text;
            }
            else
            {
                Display.Text += "*";
                _attempt += myBtn.Text;
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Display.Text = "";
            _attempt = "";
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (Lock.SubmitCode(_attempt))
            {
                Display.Text = Lock.IsLocked ? "LOCKED" : "UNLOCKED";
            }
            else
            {
                System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                Display.Text = "Invalid";
                if (Lock.IsDisabled)
                {
                    DisabledKeypad();
                    Display.Text = "Disabled";
                }
            }


        }

        private void DisabledKeypad()
        {
            foreach (Control item in this.Controls)
            {
                if (item is Button)
                {
                    item.Enabled = false;
                }
            }
            LockoutTimer.Start();
        }
        private void LockoutTimer_Tick(object sender, EventArgs e)
        {
            EnableKeypad();
            Display.Text = "";
        }
        private void EnableKeypad()
        {
            foreach (Control item in this.Controls)
            {
                if (item is Button)
                {
                    item.Enabled = true;
                }
            }
        }

        private void HotKeyPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btnEnter.PerformClick();
                    break;
                case Keys.D0:
                    btnNum0.PerformClick();
                    break;
                case Keys.D1:
                    btnNum1.PerformClick();
                    break;
                case Keys.D2:
                    btnNum2.PerformClick();
                    break;
                case Keys.D3:
                    btnNum3.PerformClick();
                    break;
                case Keys.D4:
                    btnNum4.PerformClick();
                    break;
                case Keys.D5:
                    btnNum5.PerformClick();
                    break;
                case Keys.D6:
                    btnNum6.PerformClick();
                    break;
                case Keys.D7:
                    btnNum7.PerformClick();
                    break;
                case Keys.D8:
                    btnNum8.PerformClick();
                    break;
                case Keys.D9:
                    btnNum9.PerformClick();
                    break;
                case Keys.NumPad0:
                    btnNum0.PerformClick();
                    break;
                case Keys.NumPad1:
                    btnNum1.PerformClick();
                    break;
                case Keys.NumPad2:
                    btnNum2.PerformClick();
                    break;
                case Keys.NumPad3:
                    btnNum3.PerformClick();
                    break;
                case Keys.NumPad4:
                    btnNum4.PerformClick();
                    break;
                case Keys.NumPad5:
                    btnNum5.PerformClick();
                    break;
                case Keys.NumPad6:
                    btnNum6.PerformClick();
                    break;
                case Keys.NumPad7:
                    btnNum7.PerformClick();
                    break;
                case Keys.NumPad8:
                    btnNum8.PerformClick();
                    break;
                case Keys.NumPad9:
                    btnNum9.PerformClick();
                    break;
                case Keys.Back:
                    btnClear.PerformClick();
                    break;
                default:
                    break;
            }
        }

    }
}
