﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;

namespace Ajanotto2
{

    public partial class Form1 : Form
    {
        string[] lista = new string[100];
        int counter = 0;
        private SerialPort myport;
        Stopwatch timer = new Stopwatch();
        string in_data;
        string aika;
        
        public Form1()
        {      
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            label1.Text = "-";
            myport = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One);
            myport.DataReceived += Myport_DataReceived;
            try
            {
                myport.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
        }

        private void Myport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            in_data = myport.ReadLine();
            
            if (!int.TryParse(in_data, out int sdata)) ;
            int valinta = 2;
            if (timer.IsRunning == false)
            {
                valinta = 4;
                button1.BackColor = Color.Green;
            }
            if(timer.IsRunning == true)
            {
                button1.BackColor = Color.Red;
            }
            if (sdata == 0 && valinta == 4)
            {
                timer.Start();
                Console.WriteLine("timer start");
            }
            if (sdata == 0 && valinta == 2)
             {
                 timer.Stop();
                 Console.WriteLine("timer stop");
                 button1.BackColor = Color.Green;
                 TimeSpan ts = timer.Elapsed;
                 string elapsedTime = string.Format("{0:00}:{1:00}", ts.Seconds, ts.Milliseconds);
                 Console.WriteLine(elapsedTime);
                 myport.Close();
                 aika = elapsedTime;
                 this.Invoke(new EventHandler(displaydata_event));
                 Thread.Sleep(100);
                 timer.Reset();
            }
        }

        private void displaydata_event(object sender, EventArgs e)
        {
            label1.Text = aika;

        }


        private void Button3_Click(object sender, EventArgs e)
        {
            lista[counter] = textBox1.Text + ":   " + aika;
            counter++;
            label1.Text = "-";
            textBox2.Text = "";
            for (int i = 0; i < counter; i++)
            {
                textBox2.AppendText(lista[i] + Environment.NewLine);
            }
        }
    }
}
