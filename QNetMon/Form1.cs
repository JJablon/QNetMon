using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QNetMon
{
    public partial class Form1 : Form
    {
        System.Net.NetworkInformation.Ping p;
        Bitmap[] icons = null;
        enum color { black, gray, red, pink, yellow, violet, green, white };
        IPAddress ip = null;
        PingReply reply = null;
        bool error = false;
        float? rtts = null;
        long? rtt = null;

        public Form1()
        {
            p = new Ping();
            InitializeComponent();
            notifyIcon1.Visible = true;
            this.Focus();
            this.Hide();
            this.Visible = false;
            this.SendToBack();
            icons = new System.Drawing.Bitmap[Enum.GetNames(typeof(color)).Length];
            for (int i = 0; i < Enum.GetNames(typeof(color)).Length; i++)
            {
                icons[i] = new Bitmap(imageList1.Images[i], new Size(imageList1.Images[i].Width, imageList1.Images[i].Height));
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ip = null;
            reply = null;
            error = false;
            rtts = null;
            rtt = null;
            try
            {
                ip = new IPAddress(134744072);
                reply = p.Send(ip);
                rtts = (float)((float)reply.RoundtripTime / (float)1000f);
                rtt = reply.RoundtripTime;
                notifyIcon1.Text = "QNetMon - " + rtt.ToString()+" ms";
            }
            catch(Exception )
            {
                error = true;
            }
          


            Bitmap showing = icons[(int)color.white];
            if (rtt != null && ip != null && reply != null&&error == false)
            {
                if (rtt < 100)
                    showing = icons[(int)color.green];
                else if (rtt < 200)
                    showing = icons[(int)color.violet];
                else if (rtt < 300)
                    showing = icons[(int)color.yellow];
                else if (rtt < 500)
                    showing = icons[(int)color.pink];
                else if (rtt < 1500)
                    showing = icons[(int)color.red];
                else if (rtt < 1500)
                    showing = icons[(int)color.gray];
                else

                    showing = icons[(int)color.black];

            }
            else
            {
                //remains default - white
            }
            IntPtr icoPtr = showing.GetHicon();// Get an Hicon for myBitmap.
            Icon ico = Icon.FromHandle(icoPtr);// Create a new icon from the handle.
            notifyIcon1.Icon = ico;


        }
    }
}
