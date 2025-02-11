using Byboy.SignPlugin.DbUtils;
using System.ComponentModel;

namespace Byboy.SignPlugin
{
    public partial class FormMain : Form
    {
        Plugin plugin = null;
        ClusterConfig config = null;
        public FormMain(Plugin plugin)
        {
            this.plugin = plugin;
            InitializeComponent();
        }

        private void FormMain_Load(object sender,EventArgs e)
        {
            try {
                chkOnlyAdminEdit.Checked = plugin.gConfig.OnlyAdminEdit;

                List<string> ExternalIds = new List<string>();
                DbUtil.configs.ForEach(t => {
                    ExternalIds.Add(t.GroupUsername);
                    var c = plugin.GetClusterConfig(t.GroupUsername);
                    lvCluster.Items.Add(new ListViewItem(new string[] { t.GroupUsername.ToString(),t.GroupNickName,t.MemberCount.ToString(),t.Creator,c != null && c.ConfigObj.Status ? "开" : "关" }));
                }
                );
            } catch (Exception ex) {
                plugin.OnLog($"加载配置失败{ex}");
            }
        }

        string SortCol = "Id";
        string Sort = "DESC";
        /// <summary>
        /// 从1开始的页数
        /// </summary>
        /// <param name="p"></param>
        private void Show(int p)
        {
            p--;
            if (cmbPage.Items.Count == 0) {
                long count = DbUtil.GetSignCount();
                label19.Text = "记录数：" + count.ToString();
                if (count == 0)
                    cmbPage.Items.Add("1");
                else {
                    int page = (int)Math.Ceiling(count / 50.0);
                    for (int i = 1;i <= page;i++) {
                        cmbPage.Items.Add(i);
                    }
                }
                cmbPage.SelectedIndex = 0;
            }
            List<ClusterSign> css = DbUtil.GetSignCountPath(p * 50);

            lvSign.Items.Clear();
            css.ForEach(cs => {
                lvSign.Items.Add(new ListViewItem(new string[] { cs.Id.ToString(),cs.GroupUsername.ToString(),cs.Username.ToString(),cs.Nickname,cs.SignCount.ToString(),cs.MonthSignCount.ToString(),cs.Continue.ToString(),cs.LastSignTime.ToString(),cs.Extcredits.ToString(),cs.AddTime.ToString(),cs.LastSentTime.ToString(),cs.SentCount.ToString(),cs.MonthSentCount.ToString() }));
            }
            );
        }

        private void cmbPage_SelectedIndexChanged(object sender,EventArgs e)
        {
            Show(cmbPage.SelectedIndex + 1);
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    double add = 1 + (double)numAdd.Value / 100;
        //    for (int i = 0; i < 30; i++)
        //    {
        //        sb.AppendFormat("连续签到：{0}天，奖励加成：{1:#.##}\r\n"
        //            , i + 1, (int)numMin.Value * Math.Pow(add, i));
        //    }

        //    MessageBox.Show(sb.ToString());
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    List<uint> tmp = new List<uint>();
        //    foreach (ListViewItem item in lvCluster.CheckedItems)
        //    {
        //        uint ExternalId = uint.Parse(item.SubItems[0].Text);
        //        if (!tmp.Contains(ExternalId))
        //            tmp.Add(ExternalId);
        //    }

        //    plugin.config.ExternalIds = tmp;
        //    plugin.config.Save();
        //}

        private void lvSign_ColumnClick(object sender,ColumnClickEventArgs e)
        {
            if (this.lvSign.Columns[e.Column].Tag == null)
                this.lvSign.Columns[e.Column].Tag = true;
            bool flag = (bool)this.lvSign.Columns[e.Column].Tag;
            if (flag) {
                Sort = "ASC";
                this.lvSign.Columns[e.Column].Tag = false;
            } else {
                Sort = "DESC";
                this.lvSign.Columns[e.Column].Tag = true;
            }
            //cs.Id., cs.ExternalId., cs.QQ., cs.Nick, cs.SignCount., cs.MonthSignCount., cs.Continue., cs.LastSignTime., cs.Extcredits., cs.AddTime., cs.LastSentTime., cs.SentCount., cs.MonthSentCount.
            SortCol = new string[] { "Id","ExternalId","QQ","Nick","SignCount","MonthSignCount","Continue","LastSignTime","Extcredits","AddTime","LastSentTime","SentCount","MonthSentCount" }[e.Column];
            switch (e.Column) {
                case 0:
                case 1:
                case 2:
                case 4:
                case 5:
                case 6:
                case 8:
                case 11:
                case 12:
                    this.lvSign.ListViewItemSorter = new ListViewSort(e.Column,this.lvSign.Columns[e.Column].Tag,SortType.Value);
                    break;
                case 7:
                case 10:
                    this.lvSign.ListViewItemSorter = new ListViewSort(e.Column,this.lvSign.Columns[e.Column].Tag,SortType.DateTime);
                    break;
                default:
                    this.lvSign.ListViewItemSorter = new ListViewSort(e.Column,this.lvSign.Columns[e.Column].Tag,SortType.String);
                    break;
            }
            //this.lvSign.Sort();//对列表进行自定义排序  
            Show(cmbPage.SelectedIndex + 1);
        }

        private void 开ToolStripMenuItem_Click(object sender,EventArgs e)
        {
            for (int i = 0;i < lvCluster.SelectedItems.Count;i++) {
                var item = lvCluster.SelectedItems[i];
                string ExternalId = item.SubItems[0].Text;
                var c = plugin.GetClusterConfig(ExternalId);
                if (c == null) {
                    var co = new ConfigObj() { Status = true };
                    c = new ClusterConfig() { GroupUsername = ExternalId,ConfigObj = co };
                } else if (!c.ConfigObj.Status) {
                    c.ConfigObj.Status = true;
                }
                DbUtil.SaveClusterConfig(c);
                item.SubItems[4].Text = "开";
            }
        }

        private void 关ToolStripMenuItem_Click(object sender,EventArgs e)
        {
            for (int i = 0;i < lvCluster.SelectedItems.Count;i++) {
                var item = lvCluster.SelectedItems[i];
                var c = plugin.GetClusterConfig(item.SubItems[0].Text);
                if (c != null && c.ConfigObj.Status) {
                    c.ConfigObj.Status = false;
                    DbUtil.SaveClusterConfig(c);
                }
                item.SubItems[4].Text = "关";
            }
        }

        private void button1_Click(object sender,EventArgs e)
        {
            List<long> eids = new List<long>();
            var cs = new List<ClusterSign>();

            if (cs.Count > 0) {

            } else
                MessageBox.Show("没有找到可以清理的数据");
        }

        private void tabControl1_SelectedIndexChanged(object sender,EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
                Show(1);
        }

        private void 设置ToolStripMenuItem_Click(object sender,EventArgs e)
        {
            if (lvCluster.SelectedItems.Count > 0) {
                开ToolStripMenuItem.PerformClick();
                var item = lvCluster.SelectedItems[0];
                string ExternalId = item.SubItems[0].Text;
                var c = plugin.GetClusterConfig(ExternalId);
                item.SubItems[4].Text = "开";
                FormConfig fc = new FormConfig(plugin,c);
                fc.ShowDialog();
            }
        }

        private void contextMenuStrip1_Opening(object sender,CancelEventArgs e)
        {
            if (lvCluster.SelectedItems.Count > 0) {
                开ToolStripMenuItem.Visible = true;
                关ToolStripMenuItem.Visible = true;
                设置ToolStripMenuItem.Visible = true;
                删除ToolStripMenuItem.Visible = true;
            } else {
                开ToolStripMenuItem.Visible = false;
                关ToolStripMenuItem.Visible = false;
                设置ToolStripMenuItem.Visible = false;
                删除ToolStripMenuItem.Visible = false;
            }
        }

        private void lvCluster_MouseDoubleClick(object sender,MouseEventArgs e)
        {
            设置ToolStripMenuItem.PerformClick();
        }

        private void btnSave_Click(object sender,EventArgs e)
        {
            plugin.gConfig.OnlyAdminEdit = chkOnlyAdminEdit.Checked;
            plugin.gConfig.Save();

            MessageBox.Show("保存成功");
        }

        private void 删除ToolStripMenuItem_Click(object sender,EventArgs e)
        {
            if (MessageBox.Show(string.Format("确定删除{0}个配置吗？",lvCluster.SelectedItems.Count),"提醒",MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                return;

            List<string> ExternalIds = new List<string>();

            lvCluster.BeginUpdate();
            while (lvCluster.SelectedItems.Count > 0) {
                var item = lvCluster.SelectedItems[lvCluster.SelectedItems.Count - 1];
                ExternalIds.Add(item.SubItems[0].Text);
                item.Remove();
            }
            lvCluster.EndUpdate();

            var count = DbUtil.DeleteClusterConfig(ExternalIds);
            MessageBox.Show(string.Format("{0}个设置被删除",count));
        }
    }
}
