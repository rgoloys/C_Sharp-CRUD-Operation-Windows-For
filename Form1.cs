using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml.Linq;
using System.IO;

namespace CRUD
{
    public partial class Form1 : Form
    {
        private XDocument xmldoc;
        private string xmlfile = "EmpDataBased.xml";
        public Form1()
        {
            InitializeComponent();
        }

        private void label_TOTAL_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                xmldoc = XDocument.Load(xmlfile);
                var data = xmldoc.Descendants("Employee").Select(p => new
                {
                    id = p.Element("id").Value,
                    name = p.Element("name").Value,
                    address = p.Element("address").Value,
                    email = p.Element("email").Value,
                    salary = p.Element("salary").Value,
                }).OrderBy(p => p.id).ToList();
                dataGridView1.DataSource = data;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void Add_btn_Click(object sender, EventArgs e)
        {
            XElement emp = new XElement("Employee",
                      new XElement("id", txt_id.Text),
                      new XElement("name", txt_name.Text),
                      new XElement("salary", txt_salary.Text),
                      new XElement("email", txt_email.Text),
                      new XElement("address", txt_address.Text));
            xmldoc.Root.Add(emp);
            xmldoc.Save(xmlfile);
        }
        private void Update_btn_Click(object sender, EventArgs e)
        {
            if (txt_id.Text != "" & txt_name.Text != "" & txt_salary.Text != "" & txt_email.Text != "" & txt_address.Text != "")
            {
                XElement emp = xmldoc.Descendants("Employee").FirstOrDefault(p => p.Element("id").Value == txt_id.Text);
                if (emp != null)
                {
                    emp.Element("fullname").Value = txt_name.Text;
                    emp.Element("salary").Value = txt_salary.Text;
                    emp.Element("email").Value = txt_email.Text;
                    emp.Element("address").Value = txt_address.Text;
                    xmldoc.Save(xmlfile);
                }
            }
            else
            {
                MessageBox.Show("Please Fully Fill up the Form", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Delete_btn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    txt_id.Text = row.Cells[0].Value.ToString();
                    txt_name.Text = row.Cells[1].Value.ToString();
                    txt_salary.Text = row.Cells[2].Value.ToString();
                    txt_email.Text = row.Cells[3].Value.ToString();
                    txt_address.Text = row.Cells[4].Value.ToString();
                }
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    XElement emp = xmldoc.Descendants("Employee").FirstOrDefault(p => p.Element("id").Value == txt_id.Text);
                    Console.WriteLine(emp);
                    if (emp != null)
                    {
                        emp.Remove();
                        xmldoc.Save(xmlfile);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    txt_id.Text = row.Cells[0].Value.ToString();
                    txt_name.Text = row.Cells[1].Value.ToString();
                    txt_salary.Text = row.Cells[2].Value.ToString();
                    txt_email.Text = row.Cells[3].Value.ToString();
                    txt_address.Text = row.Cells[4].Value.ToString();
                }
            }
            else
            {
                MessageBox.Show("Please select a row", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txt_name_TextChanged(object sender, EventArgs e)
        {
           // e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void txt_salary_TextChanged(object sender, EventArgs e)
        {
         //   e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && dataGridView1.CurrentCell.Value != null)
            {

            }
        }
    }
}
