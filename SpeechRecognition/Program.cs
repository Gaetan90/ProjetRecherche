using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Windows.Forms;

namespace SpeechRecognition
{
    class Program
    {
        static void Main(string[] args)
        {
            Form1 form1 = new Form1();
            form1.button1.Click += new EventHandler(form1.button1_Click);
            form1.ShowDialog();
        }
    }
}
