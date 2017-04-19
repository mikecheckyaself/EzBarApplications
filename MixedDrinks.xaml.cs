using EzBarApplication.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;
using Windows.Devices.Gpio;







// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EzBarApplication
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MixedDrinks : Page
    {
        private const int SOLENOID_PIN1 = 26;
        private GpioPin aSolenoidPin1;
        private string ChosenDrinkName;

        public List<EzBarApplication.Model.MDrinks> Drink;
        public Model.MDrinks MDrink { get { return this.DataContext as Model.MDrinks; } }
        
        public MixedDrinks()
        {
            this.InitializeComponent();
            ((App)Application.Current).InitializeSolenoids();
             Drink = MixedDrinkGetter.GetDrinks();
            this.DataContextChanged += (s, e) => Bindings.Update();
             
        }
      

        private void Get_Button_Click(object sender, RoutedEventArgs e)
        {
           



        }

     
        private void displayResult()
        {

        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        { 
            Frame.Navigate(typeof(interfaceOptions));
            ((App)Application.Current).Release();

        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var MDrinks = (MDrinks)e.ClickedItem;
           DrinkNameContents.Text = MDrinks.DrinkName;
            firstIngredientContent.Text = MDrinks.FirstIngredient;
            secondIngredientContent.Text = MDrinks.secondIngredient;
                ThirdIngredientContent.Text = MDrinks.thirdIngredient;
            ChosenDrinkName = DrinkNameContents.Text;
            // firstIngredientContent.Text = MDrinks.LiquorType;
           // secondIngredientContent.Text = MDrinks.Mixer;
          //  ThirdIngredientContent.Text = MDrinks.getNameOfLiquor;
           /* firstIngredientContent.Text = MDrinks.FirstIngredient;
            secondIngredientContent.Text = MDrinks.secondIngredient;
            ThirdIngredientContent.Text = MDrinks.thirdIngredient;
            FourthIngredientContent.Text = MDrinks.fourthIngredient;
            FifthIngredientContent.Text = MDrinks.fifthIngredient;
            */
            
        }
     
     
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            // ((App)Application.Current).PourADrink((DrinkNameContents.Text).ToString());
          //  ((App)Application.Current).
            ((App)Application.Current).PourADrink(ChosenDrinkName);
        }
    }
}
 // ((App)Application.Current).TurnOnSolenoid(aSolenoidPin1, true);