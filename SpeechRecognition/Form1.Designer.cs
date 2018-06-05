namespace SpeechRecognition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Speech.Recognition;
    using System.Windows.Forms;
    using System.Globalization;

    partial class Form1
    {
        public Button button1;
        public TextBox textBox1;
        private Label label1;
        public TextBox textBox2;

        public void button1_Click(object sender, EventArgs e)
        {
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new CultureInfo("en-US"));
            Grammar dictationGrammar = new DictationGrammar();
            recognizer.LoadGrammar(dictationGrammar);
            try
            {
                Console.WriteLine("Listening...");
                button1.Text = "Speak Now";
                recognizer.SetInputToDefaultAudioDevice();
                recognizer.BabbleTimeout = TimeSpan.FromSeconds(2);
                //recognizer.AudioLevelUpdated +=
                //    new EventHandler<AudioLevelUpdatedEventArgs>(recognizer_AudioLevelUpdated);
                Console.WriteLine("The audio level is now: {0}.", recognizer.AudioLevel);
                RecognitionResult result = recognizer.Recognize();
                textBox2.Text = result.Text;
                System.Collections.ObjectModel.ReadOnlyCollection<RecognizedWordUnit> words = result.Words;
                foreach(RecognizedWordUnit w in words)
                {
                    switch (w.Text)
                    {
                        case "start":
                        case "starts":
                            textBox1.Text = "start the engine";
                            break;
                        case "stop":
                            textBox1.Text = "stop the machine";
                            break;
                        case "previous":
                            textBox1.Text = "previous machine";
                            break;
                        case "next":
                            textBox1.Text = "next machine";
                            break;
                    }
                }
            }
            catch (InvalidOperationException exception)
            {
                textBox1.Text = String.Format("Could not recognize input from default aduio device. Is a microphone or sound card available?\r\n{0} - {1}.", exception.Source, exception.Message);
            }
            finally
            {
                recognizer.UnloadAllGrammars();
            }
        }

        void recognizer_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
        {
            Console.WriteLine("The audio level is now: {0}.", e.AudioLevel);
        }


    }
}