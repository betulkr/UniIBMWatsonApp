using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Cloud.SDK.Core.Http;
using IBM.Watson.VisualRecognition.v3;
using IBM.Watson.VisualRecognition.v3.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace api
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Kendi API Key ve Service Url'inizi girebilirsiniz. Deneme amaçlı kendi hesabımdaki değerleri girdim.
        public void classifyImageFromUrl(string url)
        {
            IamAuthenticator authenticator = new IamAuthenticator(
            apikey: "frpMt8ewuyNbRVm8c4kd9BdiqKSKxGtRvL9BQ_zFQSG5"
            );

            VisualRecognitionService visualRecognition = new VisualRecognitionService("2018-03-19", authenticator);
            visualRecognition.SetServiceUrl("https://api.us-south.visual-recognition.watson.cloud.ibm.com/instances/726905b9-bd34-48d0-a9af-71b27f3a31a8");

            var result = visualRecognition.Classify(
                url: url
                );

            // Dönen tüm sonuçları consol'da görebilirsiniz.
            Console.WriteLine(result.Response);

            var img = JObject.Parse(result.Response);
            //label6.Text = ( img["images"][0]["classifiers"][0]["classes"][0]["class"]).ToString();

        }

        public void classifyImageFromFile(string fileName)
        {
            float t=6/10;
            IamAuthenticator authenticator = new IamAuthenticator(
            apikey: "BfHmP164OXAf7JERkEwGyVhS_y9UY2TPVdULAhzivREA"
            );

            VisualRecognitionService visualRecognition = new VisualRecognitionService("2018-03-19", authenticator);
            visualRecognition.SetServiceUrl("https://api.us-south.visual-recognition.watson.cloud.ibm.com/instances/45b8e4f6-e8a1-4ef7-9510-df2483784053");

            DetailedResponse<ClassifiedImages> result;
            using (FileStream fs = File.OpenRead(fileName))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    result = visualRecognition.Classify(
                        imagesFile: ms,
                        imagesFilename: fileName,
                        threshold: t,
                        classifierIds: new List<string>()
                        {
                            "kalca_844752752"
                        },
                        owners: new List<string>()
                        {
                            "me"
                        }
                        );
                }
            }

            
            var img = JObject.Parse(result.Response);
            Console.WriteLine(result.Response);
            // JSON'da birden fazla sonuç getiriyor. Ben ilk gelen sonucu label'a yazdırdım. Ama doğruluk oranına göre de yazdırılabilir. 
            //label3.Text = ( img["images"][0]["classifiers"][0]["classes"][0]["class"]).ToString();
            label7.Text=(result.Response);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(opnfd.FileName);
                classifyImageFromFile(opnfd.FileName);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string url = textBox1.Text;
            //classifyImageFromUrl(url);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
