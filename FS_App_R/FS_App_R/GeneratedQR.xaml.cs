using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FS_App_R
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeneratedQR : ContentPage
    {
        public GeneratedQR()
        {
            InitializeComponent();

            BindingContext = Application.Current;
        }

        public GeneratedQR(Dictionary<string, object> symptoms, int yesAnswersCount)
        {
            InitializeComponent();

            GenerateQRCode(symptoms, yesAnswersCount);
        }

        private void GenerateQRCode(Dictionary<string, object> symptoms, int yesAnswersCount)
        {
            App app = Application.Current as App;
            Dictionary<string, object> copyProps = new Dictionary<string, object>();

            foreach (var item in app.Properties.ToList())
            {
                if (item.Value.ToString().ToCharArray().All(Char.IsDigit) &&
                    !item.Value.ToString().ToCharArray().All(Char.IsWhiteSpace))
                    copyProps.Add(item.Key, int.Parse(item.Value.ToString()));
                else if (item.Value.ToString().ToCharArray().All(Char.IsWhiteSpace))
                    copyProps.Add(item.Key, "NA");
                else if (item.Value is bool)
                    copyProps.Add(item.Key, Convert.ToInt32(item.Value));
                else if (item.Value.ToString().ToCharArray().All(Char.IsLetter))
                    copyProps.Add(item.Key, item.Value);
                else
                    copyProps.Add(item.Key, item.Value);
            }

            if (!copyProps.ContainsKey("Bus"))
                copyProps.Add("Bus", 0);


            foreach (var item in copyProps.ToList())
            {
                if (item.Key == "plantId")
                    copyProps.Remove(item.Key);
            }

            /*
             * Segun lo visto en las pruebas se debe cambiar la estructura de datos debido a la lentitud del escaner.
             * Por lo que  se decidio cambiar a una lista de objects con los valores de los diccionarios.
             * 02 26 2021 Javier D.
            */
            List<object> userListInfo = new List<object>
            {
                DateTime.Now.Ticks,
                app.Properties["plantId"],
                copyProps["employeeNum"],
                copyProps["employeeName"].ToString().Replace(",", ""),
                copyProps["employeeType"],
                copyProps["IsZF"],
                copyProps["Bus"],
                copyProps["BusNum"].ToString().Replace(",",""),
                copyProps["SeatNum"]
            };

            foreach(var i in symptoms.Values.ToList())
            {
                userListInfo.Add(Convert.ToInt32(i));
            }

            string formattedInfo = "";

            for (int i = 0; i <= userListInfo.Count - 1; i++)
            {
                if (i == userListInfo.Count - 1)
                    formattedInfo += userListInfo[i].ToString();
                else
                    formattedInfo += userListInfo[i].ToString() + ",";
            }
            Console.WriteLine(formattedInfo);
            //app.Properties.Where(p => p.Key != "plantId").ToDictionary(i => i.Key, i => i.Value)
            //Dictionary<string, object> userFinalInfo = new Dictionary<string, object>
            //{
            //    { "datetime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
            //    { "plantId", app.Properties["plantId"] },
            //    { "employeeInfo", copyProps},
            //    { "symptoms", symptoms }
            //};
            //string GeneratedInfoUser = GenerateQRCode();
            //symptoms.Add("TEST", app.Properties);

             //string json = JsonConvert.SerializeObject(userFinalInfo, Formatting.None);

            barcodePlaceholder.BarcodeValue = formattedInfo;

            if (yesAnswersCount > 0)
            {
                lblStatus.Text = "Incorrecto";
                lblStatus.TextColor = Color.Red;
            }
            else
            {
                lblStatus.Text = "Correcto";
                lblStatus.TextColor = Color.Green;
            }
        }
    }

}