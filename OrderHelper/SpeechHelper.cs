using OrderHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;

namespace OrderHelper
{
    public static class SpeechHelper
    {
        public static async Task Speak(this Order order)
        {

            string[] prices = order.Total.ToString().Trim('0').Split('.');
            if (prices.Length == 0)
                return;
            string contentToSpeak = prices[0] + " dollars ";
            if(prices.Length > 1 && prices[1] != "")
            {
                contentToSpeak += prices[1].PadRight(2, '0');
            }
            contentToSpeak += ", thank you!";
            MediaElement mediaElement = new MediaElement();
            var synth = new SpeechSynthesizer();
            SpeechSynthesisStream stream = 
                await synth.SynthesizeTextToStreamAsync(contentToSpeak);
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }
    }
}
