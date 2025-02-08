using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppNote
{
    public partial class Form1 : Form
    {
        private float fontSizeIncrement = 1.0f;
        private bool autoTextWrapFlag = false;
       
        public Form1()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            autoTextWrapMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;

            // 接收窗体键事件
            this.KeyPreview = true;
            this.KeyDown += Mainform_KeyDown;
        }

        private void Mainform_KeyDown(object sender, KeyEventArgs e)
        {
            // 使用Keys.Oemplus表示加号
            if (e.Control && e.KeyCode == Keys.Oemplus)
            {
                // 阻止默认行为
                e.SuppressKeyPress = true;
                ZoomFontSize(true);
            }

            // 使用Keys.OemMinus表示减号
            if (e.Control && e.KeyCode == Keys.OemMinus)
            {
                e.SuppressKeyPress = true;
                ZoomFontSize(false);
            }
        }

        /// <summary>
        /// 打开txt文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // 文件名只能选择txt
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            // 设置默认过滤器索引为1（即默认显示文本文件）
            openFileDialog.FilterIndex = 1;
            openFileDialog.ShowDialog();

            string path = openFileDialog.FileName;
            // 是否选中文件
            if (path == null)
            {
                return;
            }

            try
            {
                // 文件流读取
                using (FileStream fileStream = new FileStream(path,FileMode.Open,FileAccess.Read))
                {
                    byte[] buffer = new byte[1024 * 1024];
                    int len = fileStream.Read(buffer, 0, buffer.Length);
                    if (len > 0) {
                        textBox.Text = Encoding.UTF8.GetString(buffer,0, len); 
                    } 
                    else
                    {
                        MessageBox.Show("打开的文件是空文件");
                    }
                }

            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.Title = "请选择要保存的文件路径";

            saveFileDialog.ShowDialog();

            string path = saveFileDialog.FileName;
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate,FileAccess.Write))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(textBox.Text);
                    fileStream.Write(bytes, 0, bytes.Length);
                }
                MessageBox.Show("保存成功");
            }
            catch (Exception ex) { 
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 文字是否自动换行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoTextWrap_Click(object sender, EventArgs e)
        {
            autoTextWrapFlag = !autoTextWrapFlag;

            // 控制显示隐藏 是否选中是否换行
            if (autoTextWrapFlag)
            {
                autoTextWrapMenu.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            }
            else
            {
                autoTextWrapMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
               
            }
            textBox.WordWrap = autoTextWrapFlag;
        }

        /// <summary>
        /// 字体设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontOperation_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();   
            fontDialog.ShowDialog();
            textBox.Font = fontDialog.Font;
        }

        /// <summary>
        ///  字体放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomMagnify_Cllick(object sender, EventArgs e)
        {
            ZoomFontSize(true);
        }

        /// <summary>
        ///  字体缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomShrink_Click(object sender, EventArgs e)
        {
            ZoomFontSize(false);
        }

        /// <summary>
        /// 查看note信息打开浏览器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendInfo_Click(object sender, EventArgs e)
        {
            string url = "https://cn.bing.com/search?q=%E8%8E%B7%E5%8F%96%E6%9C%89%E5%85%B3+windows&%E4%B8%AD%E7%9A%84%E8%AE%B0%E4%BA%8B%E6%9C%AC%E7%9A%84%E5%B8%AE%E5%8A%A9&filters=guid:%224466414-zh-hans-dia%22%20lang:%22zh-hans%22&form=T00032&ocid=HelpPane-BingIA";  
            // windows 系统打开url
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                // 在Windows上使用Process.Start直接打开URL
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
           
        }

        /// <summary>
        /// 关于note信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutNote_Click(object sender, EventArgs e)
        {
            AboutNote aboutNote = new AboutNote();
            aboutNote.ShowDialog();
        }


        private void ZoomFontSize(bool bl)
        {

            float newSize = bl ? (textBox.Font.Size + fontSizeIncrement) : (textBox.Font.Size - fontSizeIncrement);
            // 最大字体最小字体
            if (newSize <= 72 && newSize >= 8) { 
                textBox.Font = new Font(textBox.Font.FontFamily, newSize);  
            }

        }
       
    }
}
