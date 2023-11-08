using MG.Storage;
using MG.Storage.Service;
using MG.Storage.Service.Impl.QiNiu;

namespace SendSnsTools
{
    public partial class Form1 : Form
    {
        List<string> picList = new List<string>();
        IUploaderService uploaderService = new QiNiuUploaderImpl();
        public Form1()
        {
            InitializeComponent();
            List<ComboBoxItem> items = new() {
    new () { Text = "淘宝活动", Value = 4 },
    new () { Text = "京东活动", Value = 5},
};
            comboBox1.DataSource = items;
        }

        private async void button1_Click(object sender,EventArgs e)
        {
            var t = picList.Count;
            if (t >= 9) {
                MessageBox.Show("最多添加9张图片","提示");
                return;
            }
            var tt = openFileDialog1.ShowDialog(this);
            if (tt == DialogResult.OK) {
                var bytes = File.ReadAllBytes(openFileDialog1.FileName);
                var tt1 = await uploaderService.UploadImage(bytes.ToStream(),string.Concat(Guid.NewGuid().ToString("N"),".jpg"),UploadTypeEnum.社群工具);
                picList.Add(tt1.Msg);
                switch (t) {
                    case 0: {
                        pictureBox1.ImageLocation = openFileDialog1.FileName;
                        break;
                    }
                    case 1: {
                        pictureBox2.ImageLocation = openFileDialog1.FileName;
                        break;
                    }
                    case 2: {
                        pictureBox3.ImageLocation = openFileDialog1.FileName;
                        break;
                    }
                    case 3: {
                        pictureBox4.ImageLocation = openFileDialog1.FileName;
                        break;
                    }
                    case 4: {
                        pictureBox5.ImageLocation = openFileDialog1.FileName;
                        break;
                    }
                    case 5: {
                        pictureBox6.ImageLocation = openFileDialog1.FileName;
                        break;
                    }
                    case 6: {
                        pictureBox7.ImageLocation = openFileDialog1.FileName;
                        break;
                    }
                    case 7: {
                        pictureBox8.ImageLocation = openFileDialog1.FileName;
                        break;
                    }
                    case 8: {
                        pictureBox9.ImageLocation = openFileDialog1.FileName;
                        break;
                    }
                }
            }
        }

        private async void button2_Click(object sender,EventArgs e)
        {
            AddSns addsns = new() {
                itemId = itemId.Text,
                itemUrl = itemUrl.Text,
                sendTime = DateTime.Parse(sendTime.Text),
                content = content.Text,
                commentList = new string[] { commentList.Text },
                itemType = (int)comboBox1.SelectedValue,
                picList = picList.ToArray()
            };
            var t = await addsns.SendAddSns();
            if (t) {
                MessageBox.Show("发送成功");
            } else {
                MessageBox.Show("发送失败,请联系管理员");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender,EventArgs e)
        {
            var ttt = (System.Windows.Forms.ComboBox)sender;
            var text = (ComboBoxItem)ttt.SelectedItem;
            if (text.Value == 4) {
                commentList.Text = $"#随机表情#晚8答题抢红包，30万个荭包5沅起！#淘口令#";
            } else if (text.Value == 5) {
                commentList.Text = $"#随机表情#荭包天天开，京东荭苞至高11111沅任你抢！#短链接#";
            }
        }
    }
}