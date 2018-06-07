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
        private Label label2;
        public TextBox textBox2;
        public bool volume = false;

        public void button1_Click(object sender, EventArgs e)
        {
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new CultureInfo("en-US"));
            Grammar dictationGrammar = new DictationGrammar();
            recognizer.LoadGrammar(dictationGrammar);
            try
            {
                Console.WriteLine("Listening...");
                button1.Text = "Press then speak";
                recognizer.SetInputToDefaultAudioDevice();
                recognizer.BabbleTimeout = TimeSpan.FromSeconds(2);
                recognizer.AudioLevelUpdated +=
                new EventHandler<AudioLevelUpdatedEventArgs>(recognizer_AudioLevelUpdated);
                RecognitionResult result = recognizer.Recognize();
                if(volume == true)
                {
                    textBox2.Text = result.Text;
                    Console.WriteLine("Confiance:" + result.Confidence*100 + "%");
                    System.Collections.ObjectModel.ReadOnlyCollection<RecognizedWordUnit> words = result.Words;
                    foreach (RecognizedWordUnit w in words)
                    {
                        switch (w.Text)
                        {
                            case "Start":
                            case "start":
                            case "starts":
                                textBox1.Text = "start the engine";
                                break;
                            case "Stop":
                            case "stop":
                                textBox1.Text = "stop the machine";
                                break;
                            case "Previous":
                            case "previous":
                                textBox1.Text = "previous machine";
                                break;
                            case "Next":
                            case "next":
                                textBox1.Text = "next machine";
                                break;
                            default:
                                textBox1.Text = "unrecognized action";
                                break;
                        }
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
            if ((e.AudioLevel > 10))
            {
                volume = true;
            }

            Console.WriteLine("The audio level is now: {0}.", e.AudioLevel);
        }
    }
}