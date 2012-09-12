using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Configuration;
using com.whatididwas.PachubeClient;

namespace WindowsFormsApplication1
{
    public partial class Weather : Form
    {
        public Weather()
        {
            InitializeComponent();
            serialPort1.Open();
            serialPort1.DataReceived += serialPort1_DataReceived;
        }
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string POT = serialPort1.ReadLine();
            this.BeginInvoke(new LineReceivedEvent(LineReceived), POT);
        }
        private delegate void LineReceivedEvent(string POT);
        private void LineReceived(string POT)
        {
            //What to do with the received line here
            //int voltaje; double aux;
            //voltaje = int.Parse(POT);
            //label1.Text = POT;
            //aux = voltaje * (5.0 / 255.0);
            //string valor = Convert.ToString(aux);

            try
            {
                if (POT.Substring(0, 1).Equals(","))
                {
                    String[] post = POT.Split(',');
                    String pascal = "";
                    String humidity = "";
                    String celcius = "";
                    String Fahrenheit = "";
                    String Hg = "";
                    String volts = "";
                    String altitude = "";
                    String dewpoint = "";

                    short TSEntryId = 0;
                    for (int i = 1; i < post.Count(); i++)
                    {
                        String header = post[i].Trim().Substring(0, 1);
                        String value = post[i].Trim().Substring(2);
                        switch (header)
                        {
                            case "P":
                                pascal = value;
                                txtpascal.Text = pascal;
                                break;
                            case "F":
                                Fahrenheit = value;
                                txtFahrenheit.Text = Fahrenheit;
                                break;
                            case "G":
                                Hg = value;
                                txtHg.Text = Hg;
                                break;
                            case "V":
                                volts = value;
                                txtvolts.Text = volts;
                                break;
                            case "A":
                                altitude = value;
                                txtaltitude.Text = altitude;
                                break;
                            case "C":
                                celcius = value;
                                txtcelcius.Text = celcius;
                                break;
                            case "%":
                                humidity = value;
                                txthumidity.Text = humidity;
                                break;
                            case "D":
                                dewpoint = value;
                                //txtcelcius.Text = celcius;
                                break;
                            case "H":
                                Hg = value;
                                //txtcelcius.Text = celcius;
                                break;


                        }//switch
                    }//for
                    Boolean TS = WindowsFormsApplication1.ThingSpeak.SendDataToThingSpeak(Fahrenheit, humidity, Hg, null, null, null, null, null, out TSEntryId);
                   updateCosm("Fahrenheit",Fahrenheit);
                   updateCosm("humidity",humidity);
                   updateCosm("Barometer",Hg);
                }//if
                //  string valor2dec = valor.Substring(0, 4);
                label2.Text = POT;
            }
            catch
            {
                //label2.Text = valor + ".00 v";
            }
            progressBar1.Value = 50;

        }
        public Boolean updateThingspea()
        {
            short TSEntryId = 0;
            Boolean TS = WindowsFormsApplication1.ThingSpeak.SendDataToThingSpeak("1", "2", "3", null, null, null, null, null, out TSEntryId);
            return TS;
        }
        public void updateCosm(String ID, String value) 
        {
            String id_value= ID + "," + value;
            PachubeClient.Send("4rMcCpxQD_lCJtksfL89f6HwmZiSAKw0YlpIKzFoODJnMD0g", "74950", id_value);
        }

        public void updateCosm(String value)
        {
            byte[] postArray = Encoding.ASCII.GetBytes(value);
            WebClient wc = new WebClient();
            wc.Headers.Add("X-PachubeApiKey", "4rMcCpxQD_lCJtksfL89f6HwmZiSAKw0YlpIKzFoODJnMD0g");
            wc.UploadData("http://www.pachube.com/api/74608.csv", "PUT", postArray);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            List<String> tList = new List<String>();
            string PortName = Settings1.Default.comPort;


            
            //serialPort1.PortName = PortName;
            comboBoxComPort.Items.Clear();
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {
                tList.Add(s);
            }

            tList.Sort();
            comboBoxComPort.Items.Add("Select COM port...");
            comboBoxComPort.Items.AddRange(tList.ToArray());
            comboBoxComPort.SelectedIndex = 0;
            comboBoxComPort.Text = PortName;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (serialPort1.IsOpen)
            //{
            //    
            //}
            
           // serialPort1.PortName=comboBoxComPort.Text;
            //serialPort1.Open();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                String oldPort = serialPort1.PortName;
                serialPort1.PortName = comboBoxComPort.Text;
                try
                {
                    serialPort1.Open();
                    checkBox7.Text = "Open";
                }
                catch (Exception ex)
                {
                    serialPort1.PortName = oldPort;
                    checkBox7.Checked = false;
                    MessageBox.Show(ex.Message);
                    
                }

                
            }
            else
            {
                serialPort1.Close();
                checkBox7.Text = "Closed";
            }


        }
    }
}
    