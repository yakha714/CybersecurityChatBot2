using System;
using System.Drawing;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace CybersecurityChatbot
{
    public class Form1 : Form
    {
        private TextBox txtUserInput;
        private RichTextBox rtxtChatDisplay;
        private Button btnSend;
        private Button btnClear;
        private Chatbot bot;
        private SpeechSynthesizer speaker;

        public Form1()
        {
            SetupGUI();
            bot = new Chatbot();
            speaker = new SpeechSynthesizer();
            DisplayWelcome();
            speaker.SpeakAsync("Welcome to Cybersecurity Awareness Chatbot");
        }

        private void SetupGUI()
        {
            this.Text = "Cybersecurity Chatbot";
            this.Size = new Size(800, 600);
            this.BackColor = Color.FromArgb(30, 30, 40);
            this.StartPosition = FormStartPosition.CenterScreen;

            rtxtChatDisplay = new RichTextBox();
            rtxtChatDisplay.Location = new Point(20, 20);
            rtxtChatDisplay.Size = new Size(550, 450);
            rtxtChatDisplay.BackColor = Color.FromArgb(20, 20, 30);
            rtxtChatDisplay.ForeColor = Color.White;
            rtxtChatDisplay.Font = new Font("Consolas", 10);
            rtxtChatDisplay.ReadOnly = true;

            txtUserInput = new TextBox();
            txtUserInput.Location = new Point(20, 480);
            txtUserInput.Size = new Size(440, 30);
            txtUserInput.BackColor = Color.FromArgb(40, 40, 50);
            txtUserInput.ForeColor = Color.White;
            txtUserInput.Font = new Font("Consolas", 10);
            txtUserInput.KeyPress += TxtUserInput_KeyPress;

            btnSend = new Button();
            btnSend.Text = "Send";
            btnSend.Location = new Point(470, 478);
            btnSend.Size = new Size(100, 35);
            btnSend.BackColor = Color.FromArgb(0, 120, 215);
            btnSend.ForeColor = Color.White;
            btnSend.Click += BtnSend_Click;

            btnClear = new Button();
            btnClear.Text = "Clear Chat";
            btnClear.Location = new Point(470, 520);
            btnClear.Size = new Size(100, 35);
            btnClear.BackColor = Color.FromArgb(200, 50, 50);
            btnClear.ForeColor = Color.White;
            btnClear.Click += (s, e) => ClearChat();

            Label asciiLabel = new Label();
            asciiLabel.Location = new Point(590, 20);
            asciiLabel.Size = new Size(180, 500);
            asciiLabel.ForeColor = Color.LightGreen;
            asciiLabel.Font = new Font("Consolas", 6);
            asciiLabel.Text = GetASCIIArt();

            this.Controls.Add(rtxtChatDisplay);
            this.Controls.Add(txtUserInput);
            this.Controls.Add(btnSend);
            this.Controls.Add(btnClear);
            this.Controls.Add(asciiLabel);
        }

        private string GetASCIIArt()
        {
            return @"
    +-------------------+
    |  CYBERSECURITY    |
    |     CHATBOT       |
    +-------------------+
    
       __________
      /          \
     |    LOCK    |
     |    ICON    |
      \__________/
    
    Stay Safe Online";
        }

        private void DisplayWelcome()
        {
            AppendText("Chatbot: Hello! I am your Cybersecurity Assistant.", Color.Cyan);
            AppendText("Chatbot: I can help you with passwords, scams, privacy, and more.", Color.Cyan);
            AppendText("Chatbot: Try asking: What is phishing? or Give me password tips", Color.Yellow);
            AppendText("", Color.White);
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            ProcessInput();
        }

        private void TxtUserInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ProcessInput();
                e.Handled = true;
            }
        }

        private void ProcessInput()
        {
            string userMessage = txtUserInput.Text.Trim();
            if (string.IsNullOrEmpty(userMessage))
                return;

            AppendText("You: " + userMessage, Color.White);
            string botResponse = bot.GetResponse(userMessage);
            AppendText("Chatbot: " + botResponse, Color.LightGreen);
            if (bot.ShouldSpeak())
            {
                speaker.SpeakAsync(botResponse);
            }
            txtUserInput.Clear();
        }

        private void ClearChat()
        {
            rtxtChatDisplay.Clear();
            DisplayWelcome();
        }

        private void AppendText(string message, Color color)
        {
            rtxtChatDisplay.SelectionColor = color;
            rtxtChatDisplay.AppendText(message + "\n");
            rtxtChatDisplay.ScrollToCaret();
        }
    }
}