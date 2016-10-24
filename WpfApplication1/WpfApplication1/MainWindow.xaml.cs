using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Xml;


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fileName = "C:\\temp\\Employees.xml";

        List<User> users = new List<User>();



        public MainWindow()
        {
            if (File.Exists(fileName))
            {
                readFile(fileName, users);
            }
   
                InitializeComponent();           
        }
        
        

        private void button_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User();

            newUser.UserId = IdTextBox.Text;
            newUser.UserName = NameTextBox.Text;
            newUser.UserAddress = AddressTextBox.Text;
            newUser.ZipCode = ZipTextBox.Text;
            newUser.HireDate = HireTextBox.Text;
            newUser.TermDate = TermTextBox.Text;

            users.Add(newUser);

            IdTextBox.Clear();
            NameTextBox.Clear();
            AddressTextBox.Clear();
            ZipTextBox.Clear();
            HireTextBox.Clear();
            TermTextBox.Clear();

            mylistView.ItemsSource = null;
            mylistView.ItemsSource = users;

        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            {
                using (XmlWriter writer = XmlWriter.Create("C:\\temp\\Employees.xml"))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("List");

                    foreach (User c in users)
                    {
                        writer.WriteStartElement("Employee");

                        writer.WriteElementString("Id", c.UserId.ToString());
                        writer.WriteElementString("Name", c.UserName.ToString());
                        writer.WriteElementString("Address", c.UserAddress.ToString());
                        writer.WriteElementString("ZipCode", c.ZipCode.ToString());
                        writer.WriteElementString("HireDate", c.HireDate.ToString());
                        writer.WriteElementString("TermDate", c.TermDate.ToString());
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    

                }
            }
        }


        public static void readFile(string file, List<User> a)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            XmlNode comNode = doc.DocumentElement.SelectSingleNode("/List");

            foreach (XmlNode child in comNode.ChildNodes)
            {
                string empId = "";
                string empName = "";
                string empAddress = "";
                string empZip = "";
                string empHireDate = "";
                string empTermDate = "";

                foreach (XmlNode grandChild in child.ChildNodes)
                {
                    switch (grandChild.Name)
                    {
                        case "Id":
                            {
                                empId = grandChild.InnerText;
                                break;
                            }
                        case "Name":
                            {
                                empName = grandChild.InnerText;
                                break;
                            }
                        case "Address":
                            {
                                empAddress = grandChild.InnerText;
                                break;
                            }
                        case "ZipCode":
                            {
                                empZip = grandChild.InnerText;
                                break;
                            }
                        case "HireDate":
                            {
                                empHireDate = grandChild.InnerText;
                                break;
                            }
                        case "TermDate":
                            {
                                empTermDate = grandChild.InnerText;
                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }
                }
                User newUser = new User();

                newUser.UserId = empId;
                newUser.UserName = empName;
                newUser.UserAddress = empAddress;
                newUser.ZipCode = empZip;
                newUser.HireDate = empHireDate;
                newUser.TermDate = empTermDate;

                a.Add(newUser);

                
            }

        }
    }
}
