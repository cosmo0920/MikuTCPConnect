using System;
using System.Windows;
using System.Windows.Controls;

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
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            System.Text.Encoding enc = System.Text.Encoding.UTF8;

            string host = "<target machine LANIP>";
            int port = 3939;
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
    }
}
