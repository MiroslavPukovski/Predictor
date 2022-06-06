using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace Ultimate_Predictor
{
    public partial class Form1 : Form
    {
        private const string APP_NAME = "ULTIMATE_PREDICTOR";
        private readonly string PREDICTION_CONFIG_PATCH = $"{Environment.CurrentDirectory}\\PredictionsConfig.json";
        private string[] _predictions;
        private Random _random = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = APP_NAME;
            
            try
            {
                var data = File.ReadAllText(PREDICTION_CONFIG_PATCH);
                _predictions = JsonConvert.DeserializeObject<string[]>(data);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if(_predictions == null)
                {
                    Close();
                }
                else if(_predictions.Length == 0)
                {
                    MessageBox.Show("Finito La Comedy");
                    Close();
                }
            }
            
        }

        private async void bPredict_Click(object sender, EventArgs e)
        {
            bPredict.Enabled = false;
           await Task.Run(()=>
            {
                for (int i = 1; i < 100; i++)
                {
                    this.Invoke(new Action(() =>
                    {
                        UpdateProgressBar(i);
                        this.Text = $"{i}%";
                    }));
                    
                    Thread.Sleep(20);
                }
            });
            var index = _random.Next(_predictions.Length);
            var prediction = _predictions[index];

            MessageBox.Show($"{ prediction} !");

            progressBar2.Value = 0;
            this.Text = APP_NAME;
            bPredict.Enabled = true;
        }

        private void UpdateProgressBar(int i)
        {
            if (i == progressBar2.Maximum)
            {
                progressBar2.Maximum = i + 1;
                progressBar2.Value = i + 1;
                progressBar2.Maximum = i;
            }
            else
            {
                progressBar2.Value = i + 1;
            }
            progressBar2.Value = i;
        }
    }
}
