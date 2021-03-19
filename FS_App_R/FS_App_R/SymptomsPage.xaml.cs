using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FS_App_R.Models;

namespace FS_App_R
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SymptomsPage : ContentPage
    {
        public ObservableCollection<Question> Symptoms { get; set; }
        public List<Question> SelectedYesItems { get; set; }
        public List<Question> SelectedNoItems { get; set; }
        public List<Question> NotAnsweredQuestions { get; set; }
        public SymptomsPage()
        {
            InitializeComponent();

            SelectedYesItems = new List<Question>();
            SelectedNoItems = new List<Question>();
            NotAnsweredQuestions = new List<Question>();

            GenerateList();

        }
        
        private void GenerateList()
        {
            Symptoms = new ObservableCollection<Question>
            {
                new Question { Id = 1, Name = "Fiebre", Descripton = "", IsCheckedYes = false, IsCheckedNo = false, NombreGrupo = "fever" },
                new Question { Id = 2, Name = "Tos, estornudos", Descripton = "", IsCheckedYes = false, IsCheckedNo = false, NombreGrupo = "cough" },
                new Question { Id = 3, Name = "Malestar general", Descripton = "", IsCheckedYes = false, IsCheckedNo = false, NombreGrupo = "generalDiscomfort" },
                new Question { Id = 4, Name = "Dolor de cabeza", Descripton = "", IsCheckedYes = false, IsCheckedNo = false, NombreGrupo = "headache" },
                new Question { Id = 5, Name = "Dificultad para respirar", Descripton = "", IsCheckedYes = false, IsCheckedNo = false, NombreGrupo = "shortnessBreath" },
                new Question { Id = 6, Name = "Viaje", Descripton = "¿Ha viajado en los últimos 7 dias (viaje de trabajo o personal)?", IsCheckedYes = false, IsCheckedNo = false, NombreGrupo = "traveled" },
                new Question
                {
                    Id = 7,
                    Name = "Contacto",
                    Descripton = "¿Ha tenido contacto con alguna persona sospechosa o confirmada de tener Covid-19 en los últimos 14 días?",
                    IsCheckedYes = false,
                    IsCheckedNo = false,
                    NombreGrupo = "suspiciousContact"
                },
                new Question { Id = 8, Name = "Pérdida de olfato y gusto", Descripton = "", IsCheckedYes = false, IsCheckedNo = false, NombreGrupo = "senseLost" }
            };
            listview2.ItemsSource = Symptoms;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            SelectedYesItems.Clear();
            SelectedNoItems.Clear();
        }

        private void listview2_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            listview2.SelectedItem = null;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Dictionary<string, object> sintomasDic = new Dictionary<string, object>();

            foreach (var item in listview2.ItemsSource)
            {
                Question dataitem = (Question)item;

                if (!dataitem.IsCheckedYes && !dataitem.IsCheckedNo)
                {
                    NotAnsweredQuestions.Add(dataitem);
                }

                if (dataitem.IsCheckedYes)
                {
                    sintomasDic.Add(dataitem.NombreGrupo, true);
                    SelectedYesItems.Add(dataitem);
                }
                else
                {
                    sintomasDic.Add(dataitem.NombreGrupo, false);
                    SelectedNoItems.Add(dataitem);
                }
            }
            //if (SelectedNoItems.Count == 8 && SelectedYesItems.Count > 0)
            //    SelectedYesItems.Clear();

            if (NotAnsweredQuestions.Count > 0)
            {
                NotAnsweredQuestions.Clear();
                await DisplayAlert("Advertencia", "Debes responder todas las preguntas del cuestionario.", "OK");
            }
            else
            {
                await Navigation.PushAsync(new GeneratedQR(sintomasDic, SelectedYesItems.Count));
            }
        }
    }
}