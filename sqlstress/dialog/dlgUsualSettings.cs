using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace sqlstress.dialog
{
    public partial class dlgUsualSettings : Form
    {
        private EnginSettingsHelper helper = EnginSettingsHelper.LoadEnginSettingsHelper();
        public dlgUsualSettings()
        {
            InitializeComponent();
            ShowSettings();
        }

        private void ShowSettings()
        {
            listSettings.Items.Clear();
            foreach (UsualSetting setting in helper)
            {
                ListViewItem it = listSettings.Items.Add(setting.DisplayName, setting.DisplayName, -1);
                it.SubItems.Add(setting.Setting.Type);
                it.SubItems.Add(setting.Setting.ConnectString);
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listSettings.SelectedItems)
            {
                UsualSetting s = helper.GetSettingByName(item.Text);
                helper.Remove(s);
            }
            ShowSettings();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            UsualSetting newsetting = new UsualSetting();

            newsetting.DisplayName = Interaction.InputBox(Resource.NEWCONNNAME, Resource.DIAG_NEWCONN, "NewConnection");
            if (string.IsNullOrWhiteSpace(newsetting.DisplayName)) return;
            if ((newsetting.DisplayName == string.Empty) || (helper.GetSettingByName(newsetting.DisplayName) != null))
            {
                MessageBox.Show(Resource.NEWCONNERRMSG, Resource.MSGERRTITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            newsetting.Setting.ShowWizard();
            if (string.IsNullOrWhiteSpace(newsetting.Setting.ConnectString)) return;

            helper.Add(newsetting);

            ShowSettings();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            helper.Save();
        }

        private void listSettings_DoubleClick(object sender, EventArgs e)
        {
            if (listSettings.SelectedItems.Count == 0) return;
            foreach (ListViewItem item in listSettings.SelectedItems)
            {
                UsualSetting s = helper.GetSettingByName(item.Text);
                s.Setting.ShowWizard();
            }

            ShowSettings();
        }
    }
}
