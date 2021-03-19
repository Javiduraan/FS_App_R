using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FS_App_R
{
    public partial class App : Application
    {
        private const string EmpNum = "employeeNum";
        private const string EmpName = "employeeName";
        private const string EmpType = "employeeType";
        private const string Edification = "plantId";
        private const string BusNum = "BusNum";
        private const string SeatNum = "SeatNum";
        private const string IsInternalEmployee = "IsZF";
        private const string Bus = "Bus";

        public App()
        {
            Device.SetFlags(new string[] { "RadioButton_Experimental" });

            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public string EmpNumber
        {
            get
            {
                if (Properties.ContainsKey(EmpNum))
                    return  Properties[EmpNum].ToString();

                return "";
            }
            set
            {
                Properties[EmpNum] = value;
            }
        }
        public string EmployeeName
        {
            get
            {
                if (Properties.ContainsKey(EmpName))
                    return Properties[EmpName].ToString();

                return "";
            }
            set
            {
                Properties[EmpName] = value;
            }
        }

        public string EmployeeType
        {
            get
            {
                if (Properties.ContainsKey(EmpType))
                    return Properties[EmpType].ToString();

                return "";
            }
            set
            {
                Properties[EmpType] = value;
            }
        }

        public string EdifP
        {
            get
            {
                if (Properties.ContainsKey(Edification))
                    return Properties[Edification].ToString();

                return "";
            }
            set
            {
                Properties[Edification] = value;
            }
        }

        public string BusNumber
        {
            get
            {
                if (Properties.ContainsKey(BusNum))
                    return Properties[BusNum].ToString();

                return "";
            }
            set
            {
                Properties[BusNum] = value;
            }
        }

        public string SeatNumb
        {
            get
            {
                if (Properties.ContainsKey(SeatNum))
                    return Properties[SeatNum].ToString();

                return "";
            }
            set
            {
                Properties[SeatNum] = value;
            }
        }

        public bool InternalEmployee
        {
            get
            {
                if (Properties.ContainsKey(IsInternalEmployee))
                    return (bool)Properties[IsInternalEmployee];

                return false;
            }
            set
            {
                Properties[IsInternalEmployee] = value;
            }
        }
        public bool IsBus
        {
            get
            {
                if (Properties.ContainsKey(Bus))
                    return (bool)Properties[Bus];

                return false;
            }
            set
            {
                Properties[Bus] = value;
            }
        }
    }
}
