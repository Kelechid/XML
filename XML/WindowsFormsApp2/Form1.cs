using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<studentList>"+
                           "<student>"+
                               "<stn>100</stn>"+
                               "<stname>Bill</stname>" +
                               "<stsurname>Gates</stsurname>" +
                           "</student>"+
                           "<student>" +
                               "<stn>200</stn>" +
                               "<stname>James</stname>" +
                               "<stsurname>Cameroon</stsurname>" +
                           "</student>" +
                        "</studentList>");
            using (XmlTextWriter writer=new XmlTextWriter("C://students.xml",null))
            {
                writer.Formatting = Formatting.Indented;
                doc.Save(writer);
                MessageBox.Show("The XML file is created");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "File Content: ";
            XElement list = XElement.Load("C://students.xml");
            MessageBox.Show(list.ToString());
            foreach(var x in list.Elements("student"))
            {
                label1.Text += ", "+x.Element("stname").Value;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var doc = XDocument.Load("C://students.xml");
            XElement newStudent = new XElement("student",
                new XElement("stn",textBox1.Text),
                new XElement("stname",textBox2.Text),
                new XElement("stsurname",textBox3.Text));
            doc.Element("studentList").Add(newStudent);
            doc.Save("C://students.xml");
            MessageBox.Show("The new student is added");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var doc = XDocument.Load("C://students.xml");
            doc.Descendants("student")
                .Where(x => (string)x.Element("stn") == textBox4.Text)
                .Remove();
            doc.Save("C://students.xml");
            MessageBox.Show("The Student is deleted");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var doc = XDocument.Load("C://students.xml");
            var items=from item in doc.Descendants("student")
                      where item.Element("stn").Value==textBox7.Text
                      select item;
            foreach(XElement X in items)
            {
                X.SetElementValue("stname",textBox6.Text);
                X.SetElementValue("stsurname", textBox5.Text);
            }
            doc.Save("C://students.xml");
        }
    }
}
