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
    public partial class UserInfo : ContentPage
    {
        private const string EmpNumb = "employeeNum";
        private const string BusNumber = "BusNum";
        private const string SeatNumber = "SeatNum";
        private const string EmpType = "employeeType";
        private bool _InternalEmployee = false; 
        public UserInfo()
        {
            InitializeComponent();

            BindingContext = Application.Current;
        }

        public UserInfo(bool isInternal)
        {
            InitializeComponent();

            BindingContext = Application.Current;

            _InternalEmployee = isInternal; 

            ChangeAppearenceBasedOnType(isInternal);

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (await UserInputValidation())
                await Navigation.PushAsync(new SymptomsPage());
        }

        private void ChangeAppearenceBasedOnType(bool TappedEmployeeType)
        {
            App app = Application.Current as App;

            if (TappedEmployeeType)
            {
                EmpName.IsEnabled = false;
                PEmpType.IsEnabled = false;
                app.Properties["IsZF"] = true;
            }
            else
            {
                EmpNum.IsEnabled = false;
                app.Properties["IsZF"] = false;
            }
        }

        private void switchIsZF_OnChanged(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                BusNum.IsEnabled = true;
                SeatNum.IsEnabled = true;
            }
            else
            {
                BusNum.IsEnabled = false;
                SeatNum.IsEnabled = false;
            }
        }

        private async Task<bool> UserInputValidation()
        {
            if (!ValidateEmployeeNumber(EmpNum.Text, _InternalEmployee))
            {
                await DisplayAlert("Advertencia", "Favor de verificar que solo haya números en el campo: Número de empleado. \nVerifica que la longuitud del Número sea de 6 digitos. \nFavor de ingresar un Número de empleado valido.", "OK");
                return false;
            }
            if (!ValidateBusNumber(BusNum.Text))
            {
                await DisplayAlert("Advertencia", "Favor de verificar que solo haya números en el campo: Número de autobus. \nLa longuitud maxima para el Número debe ser de 7 digitos.", "OK");
                return false;
            }
            if (!ValidateSeatNumber(SeatNum.Text))
            {
                await DisplayAlert("Advertencia", "Favor de verificar que solo haya números en el campo: Número de asiento. \nLa longuitud maxima para el Número debe ser de 5 digitos.", "OK");
                return false;
            }
            if (!ValidatePickers(_InternalEmployee))
            {
                await DisplayAlert("Advertencia", "Favor de verificar que hayas seleccionado una opcion en los campos: Tipo de empleado y Edificio.", "OK");
                return false;
            }
            if (!ValidateEmployeeName(EmpName.Text, _InternalEmployee))
            {
                await DisplayAlert("Advertencia", "Favor de especificar un Nombre de Empleado", "OK");
                return false;
            }
            return true;
        }
        //Agregar segundo parametro para cuando sea empleado
        private bool ValidateEmployeeNumber(string inputString, bool empDiff)
        {
            Application app = App.Current as App;
            //Aqui entra cuando NO es empleado
            if (!empDiff)
            {
                if (inputString.ToCharArray().All(Char.IsWhiteSpace))
                {
                    //Asociar valor 0 con la caja de texto EmployeeNumber.
                    if(app.Properties.ContainsKey(EmpNumb))
                        app.Properties[EmpNumb] = 0;
                }
                return true;
            }
            //En caso de que sea empleado 
            else
            {
                if (inputString.ToCharArray().All(Char.IsWhiteSpace))
                    return false;
                if (inputString.ToCharArray().All(Char.IsDigit) && inputString.Length == 6)
                {
                    int empNumber = Convert.ToInt32(inputString);
                    if (empNumber > 0)
                        return true;
                    else
                        return false;
                }
                return false;
            }
        }
        private bool ValidateBusNumber(string inputString)
        {
            Application app = App.Current as App;
            if (inputString.ToCharArray().All(Char.IsWhiteSpace))
            {
                if (app.Properties.ContainsKey(BusNumber))
                    app.Properties[BusNumber] = 0;
                return true;
            }
            else if (inputString.ToCharArray().Length <= 7)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool ValidateSeatNumber(string inputString)
        {
            Application app = App.Current as App;
            if (inputString.ToCharArray().All(Char.IsWhiteSpace))
            {
                if (app.Properties.ContainsKey(SeatNumber))
                    app.Properties[SeatNumber] = 0;
                return true;
            }
            else if (inputString.ToCharArray().All(Char.IsDigit) && inputString.ToCharArray().Length <= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Se agrego validacion para el nombre de empleado cuando es empleado Externo
        private bool ValidateEmployeeName(string userInput, bool empDiff)
        {
            if (!empDiff)
            {
                if (userInput.ToCharArray().All(Char.IsWhiteSpace))
                    return false;
                else
                    return true;
            }
            return true;
        }
        private bool ValidatePickers(bool empDiff)
        {
            //Cuando el empleado sea en EmployeeType debemos mandar "ZF"
            Application app = App.Current as App;
            if (empDiff)
            {
                if (!PEmpType.IsEnabled)
                {
                    if (app.Properties.ContainsKey(EmpType))
                        app.Properties[EmpType] = "ZF";
                    if (PEdifP.SelectedIndex < 0)
                        return false;
                    return true;
                }
                return true;
            }
            else
            {
                if (PEmpType.IsEnabled && PEdifP.IsEnabled)
                {
                    if (PEdifP.SelectedIndex < 0 || PEmpType.SelectedIndex < 0)
                        return false;
                    return true;
                }
            }
            return true;
        }
    }
}