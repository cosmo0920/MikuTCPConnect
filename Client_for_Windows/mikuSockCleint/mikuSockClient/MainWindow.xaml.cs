using System;
using System.Windows;
using System.Windows.Controls;
using System.Text;
using System.Xml;
using System.IO;

namespace mikuSockCleint
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            button1.IsEnabled = false;
            ReadFromXML();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            System.Text.Encoding enc = System.Text.Encoding.UTF8;

            string host = textBox2.Text.Trim(); 
            int port = int.Parse(textBox3.Text);
            try
            {
                System.Net.Sockets.TcpClient tcp =
                  new System.Net.Sockets.TcpClient(host, port);

                System.Net.Sockets.NetworkStream ns = tcp.GetStream();

                string sendMsg = textBox1.Text;
                if (sendMsg == "" || textBox1.Text.Length > 140)
                {
                    textBlock1.Text = "ツイートできる条件を満たしてません！！";
                    tcp.Close();
                    return;
                }
                
                byte[] sendBytes = enc.GetBytes(sendMsg + '\n');
                ns.Write(sendBytes, 0, sendBytes.Length);

                //TCPソケットサーバーからストリームが送られてくる場合に受け取れるように
                if (checkBox1.IsChecked == true) {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                     byte[] resBytes = new byte[256];
                     int resSize;
                     do
                     {
                         resSize = ns.Read(resBytes, 0, resBytes.Length);
                         if (resSize == 0)
                         {
                             textBlock1.Text = "サーバーが切断しました。";
                             return;
                         }
                         ms.Write(resBytes, 0, resSize);
                     } while (ns.DataAvailable);//TODO:RubyのTCPSocketのputsメソッドに対応していない
                     string resMsg = enc.GetString(ms.ToArray());
                     
                    textBlock1.Text = resMsg;
                    ms.Close();
                }
                ns.Close();
                tcp.Close();
                textBlock1.Text = host + "のmikutterでつぶやきました。";
                textBox1.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),"Error");
                Environment.Exit(1);
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            label1.Content = textBox1.Text.Length + "/140";
            if (textBox1.Text.Length > 140)
            {
                button1.IsEnabled = false;
            }
            else
            {
                button1.IsEnabled = true;
            }
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                button1.IsEnabled = false;
            }
            else
            {
                button1.IsEnabled = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveToXML();
        }
        private void SaveToXML()
        {
            string setting = "Setting.xml";
            XmlDocument doc = new XmlDocument();
            XmlElement elem = doc.CreateElement("Setting");
            doc.AppendChild(elem);
            //IPノード
            XmlElement item_elem = doc.CreateElement("item");
            elem.AppendChild(item_elem);
           
            XmlElement name_elem = doc.CreateElement("IP");
            item_elem.AppendChild(name_elem);
           
            XmlNode name_node = doc.CreateNode(XmlNodeType.Text, "", "");
            name_node.Value = textBox2.Text;
            name_elem.AppendChild(name_node);
           
            //Portノード
            item_elem = doc.CreateElement("item");
            elem.AppendChild(item_elem);

            name_node = doc.CreateNode(XmlNodeType.Element, "Port", "");
            name_node.InnerText = textBox3.Text;
            item_elem.AppendChild(name_node);
            doc.Save(setting);
        }
        private void ReadFromXML()
        {
            string setting = "Setting.xml";
            string IP = string.Empty;
            string port = string.Empty;

            if (File.Exists(setting))
            {
                XmlTextReader reader = new XmlTextReader(setting);
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.LocalName)
                        {
                            case "IP":
                                IP = reader.ReadString();
                                break;
                            case "Port":
                                port= reader.ReadString();
                                break;
                            default:
                                break;
                        }
                    }
                }
                textBox2.Text = IP;
                textBox3.Text = port;
            }
        }
    }
}
