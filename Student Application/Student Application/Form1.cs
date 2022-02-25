using System.Text;

namespace Student_Application
{
    public partial class Form1 : Form
    {
        CalculatorGPA NTD = new CalculatorGPA();
        public Form1()
        {
            InitializeComponent();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV(*.csv)|*.csv";
                bool fileError = false;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = dataGridView1.Columns.Count;
                            string column = "";
                            string[] outputCSV = new string[dataGridView1.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                column += dataGridView1.Columns[i].HeaderText.ToString() + ",";
                            }
                            outputCSV[0] += column;
                            for (int i = 1; (i - 1) < dataGridView1.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    outputCSV[i] += dataGridView1.Rows[i - 1].Cells[j].Value.ToString() + ",";
                                }
                            }
                            File.WriteAllLines(saveFileDialog.FileName, outputCSV, Encoding.UTF8);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.Rows.Add();
            dataGridView1.Rows[n].Cells[0].Value = textBox1Id.Text;
            dataGridView1.Rows[n].Cells[1].Value = textBox2Name.Text;
            dataGridView1.Rows[n].Cells[2].Value = comboBox1Major.Text;
            dataGridView1.Rows[n].Cells[3].Value = textBox3GPA.Text;

            string input = this.textBox3GPA.Text;
            string name = this.textBox2Name.Text;

            double dInpu = Convert.ToDouble(input);
            NTD.AddGPA(dInpu, name);

            double gpax = NTD.gatGPAx();
            textBox4GPAx.Text = gpax.ToString();

            double max = NTD.getMax();
            textBox5Max.Text = max.ToString();

            double min = NTD.gatMin();
            textBox6Min.Text = min.ToString();

            textBox1Id.Text = "";
            textBox2Name.Text = "";
            comboBox1Major.Text = "";
            textBox3GPA.Text = "";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV (*.csv) | *.csv";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] readAllLine = File.ReadAllLines(openFileDialog.FileName);

                string readAllText = File.ReadAllText(openFileDialog.FileName);
                for (int i = 0; i < readAllLine.Length; i++)
                {
                    string allDATARAW = readAllLine[i];
                    string[] allDATASplited = allDATARAW.Split(',');
                    this.dataGridView1.Rows.Add(allDATASplited[0], allDATASplited[1], allDATASplited[2]);
                }
            }
        }
    }
}