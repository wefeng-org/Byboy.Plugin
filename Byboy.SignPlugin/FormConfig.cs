using Byboy.SignPlugin.DbUtils;
using System.Text;

namespace Byboy.SignPlugin
{
    public partial class FormConfig : Form
    {
        private ClusterConfig c;
        public Plugin plugin { get; set; }
        public ConfigObj config { get; set; }
        public FormConfig()
        {
            InitializeComponent();
        }

        public FormConfig(Plugin plugin,ClusterConfig c)
            : this()
        {
            // TODO: Complete member initialization
            this.plugin = plugin;
            this.c = c;
            this.config = c.ConfigObj;
        }

        private void FormConfig_Load(object sender,EventArgs e)
        {
            chkFirstSign.Checked = config.FirstSign;
            txtCmd.Text = config.Cmd;
            txtDays.Text = string.Join("\r\n",config.Days);
            txtLevels.Text = string.Join("\r\n",config.Levels);
            txtHellos.Text = string.Join("\r\n",config.Hellos);
            numMin.Value = config.Min;
            numMax.Value = config.Max;
            numAdd.Value = config.Add;
            numBegin.Value = config.Begin;
            numBeginExt.Value = config.BeginExtCredits;
            numRepeatMin.Value = config.RepeatMin;
            numRepeatMax.Value = config.RepeatMax;
            txtSignTime.Text = string.Join(",",config.SignTime);
            numTop.Value = config.Top;


            if (cmbType.Items.Count > config.Type)
                cmbType.SelectedIndex = config.Type;

            numRandom.Value = config.Random;
            //cmbRndType.Items.AddRange(plugin.Config.ExtcreditsType.ToArray());
            if (cmbRndType.Items.Count > config.RndType)
                cmbRndType.SelectedIndex = config.RndType;
            numRndMin.Value = config.RndMin;
            numRndMax.Value = config.RndMax;

            //var r = new DataGridViewRow();
            //var c = r.Cells.
            //c.Value = "test";
            //r.Cells.AddRange(c, c, c);
            //dataGridView1.Rows.Add("fdas", "fdsafs", "ffffffff");
        }

        private void button1_Click(object sender,EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            double add = 1 + (double)numAdd.Value / 100;
            for (int i = 0;i < 30;i++) {
                sb.AppendFormat("连续签到：{0}天，奖励加成：{1:#.##}\r\n"
                    ,i + 1,Math.Pow(add,i));
            }

            MessageBox.Show(sb.ToString());
        }

        private void FormConfig_FormClosing(object sender,FormClosingEventArgs e)
        {
            config.FirstSign = chkFirstSign.Checked;
            config.Cmd = txtCmd.Text.Trim();
            config.Days = Util.StrToIntList(txtDays.Text);
            config.Levels = txtLevels.Text.Trim().Split("\r\n".ToCharArray(),StringSplitOptions.RemoveEmptyEntries).ToList();
            config.Hellos = txtHellos.Text.Trim().Split("\r\n".ToCharArray(),StringSplitOptions.RemoveEmptyEntries).ToList();
            config.Type = cmbType.SelectedIndex;
            config.Min = (int)numMin.Value;
            config.Max = (int)numMax.Value;
            config.Add = (int)numAdd.Value;
            config.Begin = (int)numBegin.Value;
            config.BeginExtCredits = (int)numBeginExt.Value;
            config.RepeatMin = (int)numRepeatMin.Value;
            config.RepeatMax = (int)numRepeatMax.Value;
            config.SignTime = Util.StrToIntList(txtSignTime.Text);
            config.Top = (int)numTop.Value;
            config.Random = (int)numRandom.Value;
            config.RndType = cmbRndType.SelectedIndex;
            config.RndMin = (int)numRndMin.Value;
            config.RndMax = (int)numRndMax.Value;

            DbUtil.SaveClusterConfig(c);
        }
    }
}
