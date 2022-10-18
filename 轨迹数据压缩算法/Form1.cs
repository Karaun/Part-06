using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 轨迹数据压缩算法
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "请选择打开的文件";
            ofd.Filter = "txt|*.txt|All|*.*";
            ofd.ShowDialog();

            string path = ofd.FileName;
            if (path == "")
            {
                return;
            }
            using (FileStream FsRead = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            {
                byte[] buffer = new byte[1024 * 1024 * 5];
                int r = FsRead.Read(buffer, 0, buffer.Length);
                txtSource.Text = Encoding.UTF8.GetString(buffer, 0, r);

            }

        }

        private void 导出结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Title = "请选择保存路径";
            ofd.Filter = "txt|*.txt|All|*.*";
            ofd.ShowDialog();

            string path = ofd.FileName;
            if (path == "")
            {
                return;
            }
            using (FileStream FsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(txtResult.Text);
                FsWrite.Write(buffer, 0, buffer.Length);

            }
        }

        private void 计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] line;
            line = txtSource.Text.Split('\n');          
            List<RouteData> RD_list = new List<RouteData>();

            for (int i = 0; i < line.Length; i++)//添加数据
            {
                string[] temp = new string[3];
                temp = line[i].Split(',');
                RouteData RD = new RouteData(i, temp[0], Convert.ToDouble(temp[1]), Convert.ToDouble(temp[2]));
                RD_list.Add(RD);
            }

            List<RouteData> DP_list = RouteData.DP(Convert.ToDouble(textBox1.Text),RD_list);
            RouteData.Sort_list(DP_list);
            txtResult.Text = "------压缩结果 阈值：" + textBox1.Text + "------" + "\r\n";
            txtResult.Text += RouteData.Display(DP_list);
            





        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("KUST：czy", "提示");
        }
    }
}
