using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoticeBot
{
    public partial class Form1 : Form
    {

        KakaotalkActive[] katalkactive = new KakaotalkActive[100];
        public Form1()
        {
            InitializeComponent();

        }

        public void Button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string context = textBox2.Text;
            katalkactive[0] = new KakaotalkActive("우리자기", "테스트");

        }
    }
}
