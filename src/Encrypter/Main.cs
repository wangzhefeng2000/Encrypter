﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encrypter
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        #region functions
        private bool CheckPasswordMatch()
        {
            if(tbPassword1.Text == tbPassword2.Text)
            {
                pbPasswordMatch.Image = Properties.Resources.tick;
                return true;
            }
            else
            {
                pbPasswordMatch.Image = Properties.Resources.cross;
                return false;
            }
        }

        private void EncrypterProgressChange(int percent, EncrypterProgressState progressState)
        {
            pbProgress.Value = percent;

            switch (progressState)
            {
                case EncrypterProgressState.compressing:
                    lblProgressState.Text = "Copressing files...";
                    break;
                case EncrypterProgressState.extracting:
                    lblProgressState.Text = "Extracting files...";
                    break;
                case EncrypterProgressState.encrypting:
                    lblProgressState.Text = "Encrypting files...";
                    break;
                case EncrypterProgressState.decrypting:
                    lblProgressState.Text = "Decrypting files...";
                    break;
                case EncrypterProgressState.cleanup:
                    lblProgressState.Text = "Cleaning up files...";
                    break;
                default:
                    lblProgressState.Text = "";
                    break;
            }
            lblProgressState.Refresh();
        }
        #endregion

        #region events
        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void tbPassword1_KeyUp(object sender, KeyEventArgs e)
        {
            CheckPasswordMatch();
        }

        private void tbPassword2_KeyUp(object sender, KeyEventArgs e)
        {
            CheckPasswordMatch();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdOpen = new OpenFileDialog();
            
            if(fdOpen.ShowDialog() == DialogResult.OK)
            {
                tbFileDirPath.Text = fdOpen.FileName;
            }
        }

        private void btnSelectDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdOpen = new FolderBrowserDialog();
            if (fdOpen.ShowDialog() == DialogResult.OK)
            {
                tbFileDirPath.Text = fdOpen.SelectedPath;
            }
        }

        private void cbDeleteOriginalFileDir_CheckedChangedState(object sender, EventArgs e)
        {
            if (cbDeleteOriginalFileDir.CheckState == CheckState.Checked)
            {
                cbShreadFileDir.Enabled = true;
            }
            else
            {
                cbShreadFileDir.Enabled = false;
            }
        }

        private void cbShreadFileDir_CheckedChangedState(object sender, EventArgs e)
        {

        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (CheckPasswordMatch())
            {
                FileEncrypter.Encrypt(tbFileDirPath.Text, tbPassword1.Text, cbDeleteOriginalFileDir.Checked, cbShreadFileDir.Checked, EncrypterProgressChange);
                pbProgress.Value = 0;
                lblProgressState.Text = "";
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (CheckPasswordMatch())
            {
                FileEncrypter.Decrypt(tbFileDirPath.Text, tbPassword1.Text, cbDeleteOriginalFileDir.Checked, cbShreadFileDir.Checked, EncrypterProgressChange);
                pbProgress.Value = 0;
                lblProgressState.Text = "";
            }
        }
        #endregion
    }
}
