using Launcher;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace setings
{
    public partial class Setings : Form
    {
        public Setings()
        {
            InitializeComponent();
        }
        private void From2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Cursor = new Cursor(Cursor.Current.Handle);
                Point location = new Point(Cursor.Position.X, Cursor.Position.Y);
                this.Location = location;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetingClass.dopArguments = textBox1.Text;
            string ram = comboBox1.Text;
            SetingClass.sram = ram;
            SetingClass.javaPath = textBox2.Text;
            SetingClass.forge = checkBoxForge.Checked;
            SetingClass.liteMod = checkBoxLiteMod.Checked;
            SetingClass.buildAutoSync = checkBoxBuildSync.Checked;
            SetingClass.shaders = checkBoxShaders.Checked;
            SetingClass.showConsole = checkBoxConsole.Checked;
            this.DialogResult = DialogResult.OK;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.Text = SetingClass.sram;
            textBox2.Text = SetingClass.javaPath;
            textBox1.Text = SetingClass.dopArguments;
            checkBoxForge.Checked = SetingClass.forge;
            checkBoxShaders.Checked = SetingClass.shaders;
            checkBoxLiteMod.Checked = SetingClass.liteMod;
            checkBoxBuildSync.Checked = SetingClass.buildAutoSync;
            checkBoxConsole.Checked = SetingClass.showConsole;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            openJavaFile.Filter = "Java|java.exe|All Executable|*.exe";
            openJavaFile.ShowDialog();
            if (openJavaFile.FileName != "openJavaFile")
                textBox2.Text = openJavaFile.FileName;
        }
    }
}
