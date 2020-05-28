using App2.Validations;
using App2.Validations.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App2.ViewModels
{
    public class MainPageViewModel : NotificationObject
    {
        //Atributos
        public ValidableObject<string> Cedula { get; set; }

        public string cedulaGuardada { get; set; }
        private bool enterIsEnable;

        //Commands
        public ICommand ValidateFormCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        //Getters y Setters
        public bool EnterIsEnable
        {
            get { return enterIsEnable; }
            set
            {
                enterIsEnable = value;
                OnPropertyChanged();
            }
        }

        public string CedulaGuardata
        {
            get { return cedulaGuardada; }
            set
            {
                cedulaGuardada = value;
                OnPropertyChanged();
            }
        }


        //Inicializar
        public MainPageViewModel()
        {
            EnterIsEnable = false;
            Cedula = new ValidableObject<string>();
            addValidations();
            ValidateFormCommand = new Command(() => ValidateFormAsync());
            LoginCommand = new Command(async () => await Login(), () => EnterIsEnable);
        }

        private void addValidations()
        {
            Cedula.Validations.Add(new NumericObject<string> { ValidationMessage = "La cedula debe ser de tipo numerico" });
        }

        private async Task ValidateFormAsync()
        {
            bool cedulaValidate = Cedula.Validate();
            //bool emailValidate = Email.Validate();
            EnterIsEnable = cedulaValidate;
            if(EnterIsEnable)
            {
                //Application.Current.Properties["cedula"] = Cedula.Value;
                await SecureStorage.SetAsync("C2", Cedula.Value);
            }
            ((Command)LoginCommand).ChangeCanExecute();
        }

        private async Task Login()
        {
            //CedulaGuardata = Application.Current.Properties["cedula"].ToString();
            CedulaGuardata = await SecureStorage.GetAsync("C2");
        }


    }
}
