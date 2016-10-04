using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Threading;
using System.Speech.Recognition;
using System.Diagnostics;

namespace VoiceRecognitionRobotArm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SpeechSynthesizer sSynth = new SpeechSynthesizer();
        PromptBuilder pBuilder = new PromptBuilder();
        SpeechRecognitionEngine sRecognize = new SpeechRecognitionEngine();
        private void button1_Click(object sender, EventArgs e)
        {
            pBuilder.ClearContent();
            pBuilder.AppendText(textBox1.Text);
            sSynth.Speak(pBuilder);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void button2_Click(object sender, EventArgs e)
        {


            button2.Enabled = false;
            button3.Enabled = true;
            Choices sList = new Choices();
            sList.Add(new string[] { "up", "UP", "down", "DOWN", "OFF", "off", "left", "LEFT","RIGHT", "right", "grap", "let", "exit", "default" });
            Grammar gr = new Grammar(new GrammarBuilder(sList));
            try
            {
                sRecognize.RequestRecognizerUpdate();
                sRecognize.LoadGrammar(gr);
                sRecognize.SpeechRecognized += sRecognize_SpeechRecognized;
                sRecognize.SetInputToDefaultAudioDevice();
                sRecognize.RecognizeAsync(RecognizeMode.Multiple);
                
                //Thread.Sleep(1000);
                //SendKeys.SendWait("^+d");
                //Thread.Sleep(1000);
                //SendKeys.SendWait("^+c");
                //Thread.Sleep(1000);
                //SendKeys.SendWait("{ENTER}");
                //Thread.Sleep(1000);
                //SendKeys.SendWait("^+t");
                //Thread.Sleep(1000);
                // SendKeys.SendWait("LED.hex");
                // Thread.Sleep(1000);
                //SendKeys.SendWait("{ENTER}");
                //Thread.Sleep(4000);

            }

            catch
            {

                return;
            }
        }

        private void sRecognize_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "exit":
                    { textBox1.Text = textBox1.Text + "Exiting..";
                        Application.Exit();
                    }
                    break;
                case "up": case "UP": 
                    {
                        textBox1.Text = textBox1.Text + "\r\n" + e.Result.Text.ToString();

                        SendKeys.SendWait("go 2");
                        SendKeys.SendWait("{ENTER}");
                        Thread.Sleep(1000);
                        SendKeys.SendWait("{ENTER}");
                    }
                    break;
                case "down": case "DOWN":
                    {
                        textBox1.Text = textBox1.Text + "\r\n" + e.Result.Text.ToString();

                        SendKeys.SendWait("go 1");
                        SendKeys.SendWait("{ENTER}");
                        Thread.Sleep(1000);
                        SendKeys.SendWait("{ENTER}");
                    }
                    break;
                case "left": case "LEFT":
                    {
                        textBox1.Text = textBox1.Text + "\r\n" + e.Result.Text.ToString();

                        SendKeys.SendWait("go 3");
                        SendKeys.SendWait("{ENTER}");
                        Thread.Sleep(1000);
                        SendKeys.SendWait("{ENTER}");
                    }break;
                case "right":case "RIGHT":
                
                    {
                        textBox1.Text = textBox1.Text + "\r\n" + e.Result.Text.ToString();

                        SendKeys.SendWait("go 4");
                        SendKeys.SendWait("{ENTER}");
                        Thread.Sleep(1000);
                        SendKeys.SendWait("{ENTER}");
                    }
                    break;
                case "DEFAULT":case "default":
                
                    {
                        textBox1.Text = textBox1.Text + "\r\n" + e.Result.Text.ToString();
                        SendKeys.SendWait("go 0");
                        SendKeys.SendWait("{ENTER}");
                        Thread.Sleep(1000);
                        SendKeys.SendWait("{ENTER}");
                        Thread.Sleep(5000);
                        SendKeys.SendWait("off");
                        SendKeys.SendWait("{ENTER}");
                        Thread.Sleep(1000);
                        SendKeys.SendWait("{ENTER}");
                    }
                    break;


                default: textBox1.Text = textBox1.Text + "\r\n" + "Didn't catch that,repeat..";
                    break;
            }


            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            sRecognize.RecognizeAsyncStop();
            textBox1.Clear();
           
            button2.Enabled = true;
            button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process myProcess;
            myProcess = Process.Start("RoboPlusTerminal.exe");
        }
    }
}
