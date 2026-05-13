using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public class Chatbot
    {
        private Random random;
        private string lastTopic;
        private string userName;

        public Chatbot()
        {
            random = new Random();
            lastTopic = "";
            userName = "";
        }

        public string GetResponse(string input)
        {
            string lowerInput = input.ToLower();

            if (lowerInput.Contains("my name is"))
            {
                ExtractName(input);
                return "Nice to meet you, " + userName + "! I will remember your name. How can I help you with cybersecurity today?";
            }

            if (lowerInput.Contains("another") || lowerInput.Contains("more") || lowerInput.Contains("tell me more"))
            {
                return GetAnotherTip();
            }

            if (lowerInput.Contains("worried") || lowerInput.Contains("scared"))
            {
                return "I understand your concern. Stay safe by never sharing personal information online. Use strong passwords and enable two-factor authentication.";
            }

            if (lowerInput.Contains("password"))
            {
                lastTopic = "password";
                return GetPasswordTip();
            }

            if (lowerInput.Contains("scam") || lowerInput.Contains("phish"))
            {
                lastTopic = "scam";
                return GetScamTip();
            }

            if (lowerInput.Contains("privacy"))
            {
                lastTopic = "privacy";
                return GetPrivacyTip();
            }

            if (IsGreeting(lowerInput))
            {
                return GetGreeting();
            }

            return GetDefaultResponse();
        }

        private void ExtractName(string input)
        {
            int index = input.ToLower().IndexOf("my name is") + 10;
            if (index < input.Length)
            {
                userName = input.Substring(index).Trim();
            }
        }

        private string GetAnotherTip()
        {
            if (lastTopic == "password")
            {
                return GetPasswordTip();
            }
            else if (lastTopic == "scam")
            {
                return GetScamTip();
            }
            else
            {
                return GetPrivacyTip();
            }
        }

        private string GetPasswordTip()
        {
            string[] tips = {
                "Use strong passwords with uppercase, lowercase, numbers, and symbols. Never reuse passwords across different sites.",
                "Enable Two-Factor Authentication on all your important accounts for extra security.",
                "Use a password manager to generate and store unique passwords for each website.",
                "Change your passwords every 3 months and never share them with anyone."
            };
            return tips[random.Next(tips.Length)];
        }

        private string GetScamTip()
        {
            string[] tips = {
                "Never click links in suspicious emails. Hover over them first to see the real web address.",
                "Legitimate companies will never ask for your password or credit card details via email.",
                "If an offer sounds too good to be true, it is probably a scam. Always verify before acting.",
                "Hang up on suspicious calls and call back using the official number from the company website."
            };
            return tips[random.Next(tips.Length)];
        }

        private string GetPrivacyTip()
        {
            string[] tips = {
                "Review your social media privacy settings regularly. Limit who can see your personal information.",
                "Use a VPN when connecting to public WiFi to encrypt your internet traffic.",
                "Check app permissions on your phone. Many apps ask for more access than they actually need.",
                "Be careful what you share online. Cybercriminals use personal information for targeted attacks."
            };
            return tips[random.Next(tips.Length)];
        }

        private string GetGreeting()
        {
            string[] greetings = {
                "Hello! How can I help you with cybersecurity today?",
                "Hi there! Ready to learn about online safety?",
                "Greetings! Ask me about passwords, scams, or privacy.",
                "Hey! Remember - cybersecurity starts with you!"
            };
            return greetings[random.Next(greetings.Length)];
        }

        private string GetDefaultResponse()
        {
            string[] defaults = {
                "I am not sure I understand. Try asking about passwords, scams, or privacy.",
                "Could you rephrase? Try saying 'Tell me about password safety' or 'Give me a scam tip'.",
                "I am still learning. Please ask about specific topics like passwords, scams, or privacy."
            };
            return defaults[random.Next(defaults.Length)];
        }

        private bool IsGreeting(string input)
        {
            string[] greetings = { "hello", "hi", "hey", "greetings", "good morning", "good afternoon" };
            foreach (string g in greetings)
            {
                if (input.Contains(g))
                    return true;
            }
            return false;
        }

        public bool ShouldSpeak()
        {
            return random.Next(3) == 0;
        }
    }
}