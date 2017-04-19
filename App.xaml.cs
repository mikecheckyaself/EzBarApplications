using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
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
using System.Diagnostics;

//using RFID_Reader_UI;


namespace EzBarApplication
{
    
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public static string isConnected;
  
        private const int SOLENOID_PIN1 = 26;
        private GpioPin aSolenoidPin1;
        private const int SOLENOID_PIN2 = 25;
        private GpioPin aSolenoidPin2;
        private const int SOLENOID_PIN3 = 24;
        private GpioPin aSolenoidPin3;
        private const int SOLENOID_PIN4 = 23;
        private GpioPin aSolenoidPin4;
        private const int SOLENOID_PIN5 = 22;
        private GpioPin aSolenoidPin5;
        private const int SOLENOID_PIN6 =21;
        private GpioPin aSolenoidPin6;
        private const int SOLENOID_PIN7= 20;
        private GpioPin aSolenoidPin7;
        private const int SOLENOID_PIN8 = 19;
        private GpioPin aSolenoidPin8;
        private const int SOLENOID_PIN9 = 18;
        private GpioPin aSolenoidPin9;
        private const int SOLENOID_PIN10 = 17;
        private GpioPin aSolenoidPin10;

        private GpioController aController = null;   
  
        public App()
        {
            
            this.InitializeComponent();
            this.Suspending += OnSuspending;
           
        //  InitializeSolenoids();
         
        }

        /*CLASS INCHARGE OF FORMING CONNECTIONS WITH OUR DATABASE*/
        public class ConnectionFactory
        {
          
            /* The ConnectionFactory class is used throughout the program to connect to the database. Instead of having to write out the credentials
             * and create a connection everytime, the line 'var connection = ConnectionFactory.Create();' is used in each function. This line makes
             * the variable 'connection' the connection string to the database.
             */
            public static MySqlConnection Create()
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                System.Text.Encoding.GetEncoding("windows-1254");
                //new I18N.West.CP1250();
                MySqlConnection connection;
                string server = "107.180.48.91",
                    database = "Ezbarsdatabase",
                    user = "CapstoneUser2",
                    password = "4nPy+@TXm_R)";

                string connectionString = @"server=" + server + ";" + "UID=" + user + ";" + "password=" + password + ";" + "database=" + database + "; SslMode=None";
                connection = new MySqlConnection(connectionString);
                
                try
                {

                    connection.Open();

                }
                // Exception Handling
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 0:
                            break;

                        case 1045:
                            break;
                    }
                }
                
                return connection;
            }
        }

        /*TO POUR A DRINK THE FUNCTIONS IN THE BLOCK BELOW ARE ALL NECESSARY TO POUR A DRINK*/
        public void PourADrink(string RecipieName)
        {
            int rPK = getRecipieNumber(RecipieName);
            int NOI = getNumOfIngredients(rPK);
            int ingredientCounter = 1;
            IngredientTraversal(NOI, rPK, ingredientCounter);          
         }               
        public static int getRecipieNumber(string recipieName)
        {
            int rPrimaryKey = 0;
            var connection = ConnectionFactory.Create();
            string recipieNumQuerey = "SELECT R_PK FROM  `Recipes` WHERE Recipe_Name = " + "\"" + recipieName + "\";";
            MySqlCommand recipeNumcmd = new MySqlCommand(recipieNumQuerey, connection);      
            rPrimaryKey = Convert.ToInt16(recipeNumcmd.ExecuteScalar());
            return rPrimaryKey;
        }          
        public static int getNumOfIngredients(int numofrecipe) 
        {
            int numOfIngredients = 0;
            var connection = ConnectionFactory.Create();
            string NOIquery = "SELECT Recipes.NOI FROM Recipes WHERE R_PK =" + numofrecipe + " ;";
            MySqlCommand NOIcmd = new MySqlCommand(NOIquery, connection);
            numOfIngredients = Convert.ToInt32(NOIcmd.ExecuteScalar());

            return numOfIngredients;
        }  
        public void IngredientTraversal(int NOI, int rPK, int ingredientCounter)
        {
            if (ingredientCounter == 1)
            {            
                string firstIngredient = getFirstIngredient(ingredientCounter, rPK);
                int firstLocationNumber = getBottleLocation(ingredientCounter, rPK);
                int pinNumber = getPinNumber(firstLocationNumber);
                int firstAmount = getAmount(ingredientCounter, rPK);
               int firstTimeOn = firstAmount * 100;
                GpioPin aPin = aController.OpenPin(pinNumber);
                PourAnIngredient(aPin, firstTimeOn);
                aPin.Dispose();     
            }

            if (ingredientCounter == 2)
            {
                string secondIngredient = getSecondIngredient(ingredientCounter, rPK);
                int secondLocationNumber = getBottleLocation(ingredientCounter, rPK);
                int pinNumber = getPinNumber(secondLocationNumber);
                int secondAmount = getAmount(ingredientCounter, rPK);
                int secondTimeOn = secondAmount * 100;
                GpioPin aPin = aController.OpenPin(pinNumber);
                PourAnIngredient(aPin, secondTimeOn);
                aPin.Dispose();
            }

            if (ingredientCounter == 3)
            {
                string thirdIngredient = getThirdIngredient(ingredientCounter, rPK);
                int thirdLocationNumber = getBottleLocation(ingredientCounter, rPK);
                int pinNumber = getPinNumber(thirdLocationNumber);
                int thirdAmount = getAmount(ingredientCounter, rPK);
                int thirdTimeOn = thirdAmount * 100;
                GpioPin aPin = aController.OpenPin(pinNumber);
                PourAnIngredient(aPin, thirdTimeOn);
                aPin.Dispose();
            }

            /*       if (ingredientCounter == 4)
                   {
                       
                   }
                   if (ingredientCounter == 5)
                   {
                       
                   }
                   */
            if (ingredientCounter != NOI)
            {
                ingredientCounter++;
                IngredientTraversal(NOI, rPK, ingredientCounter);
            }
            else
                ingredientCounter = 0;
            }
        public static string getFirstIngredient(int ingredientCounter, int rPK)
        {
            var connection = ConnectionFactory.Create();
                string firstIngredient = "";
                string firstIngredientquery = "SELECT Ingredient FROM " + ingredientCounter + "st_Ingredient WHERE R_PK =" + rPK + ";";
                MySqlCommand firstcmd = new MySqlCommand(firstIngredientquery, connection);
            firstIngredient = (firstcmd.ExecuteScalar().ToString());
            return firstIngredient;

        }
        public static string getSecondIngredient(int ingredientCounter, int rPK)
        {
            var connection = ConnectionFactory.Create();
            string secondIngredient = "";
            string secondIngredientquery = "SELECT Ingredient FROM " + ingredientCounter + "nd_Ingredient WHERE R_PK =" + rPK + " ;";
            MySqlCommand secondcmd = new MySqlCommand(secondIngredientquery, connection);
            secondIngredient = (secondcmd.ExecuteScalar().ToString());
            return secondIngredient;
        }
        public static string getThirdIngredient(int ingredientCounter, int rPK)
        {
            var connection = ConnectionFactory.Create();
            string thirdIngredient = "";
            string thirdIngredientquery = "SELECT Ingredient FROM " + ingredientCounter + "rd_Ingredient WHERE R_PK =" + rPK + " ;";
            MySqlCommand thirdcmd = new MySqlCommand(thirdIngredientquery, connection);
            thirdIngredient = (thirdcmd.ExecuteScalar().ToString());
            return thirdIngredient;
        }
        public static string getFourthIngredient(int ingredientCounter, int rPK)
        {
            var connection = ConnectionFactory.Create();
            string fourthIngredient = "";
            string fourthIngredientquery = "SELECT Ingredient FROM " + ingredientCounter + "th_Ingredient WHERE R_PK =" + rPK + " ;";
            MySqlCommand fourthcmd = new MySqlCommand(fourthIngredient, connection);
            fourthIngredient = (fourthcmd.ExecuteScalar().ToString());
            return fourthIngredient;
        }
        public static string getFifthIngredient(int ingredientCounter, int rPK)
        {
            var connection = ConnectionFactory.Create();
            string fifthIngredient = "";
            string fifttIngredientquery = "SELECT Ingredient FROM " + ingredientCounter + "th_Ingredient WHERE R_PK =" + rPK + " ;";
            MySqlCommand fifthcmd = new MySqlCommand(fifthIngredient, connection);
            fifthIngredient = (fifthcmd.ExecuteScalar().ToString());
            return fifthIngredient;
        }
        public static int getBottleLocation(int ingredientCounter, int rpk)
        {
            var connection = ConnectionFactory.Create();
            string Liquid_Name = "";

            if (ingredientCounter == 1)
            {
               Liquid_Name = getFirstIngredient(rpk);
            }
            else if (ingredientCounter == 2)
            {
                Liquid_Name = getSecondIngredient(rpk);
            }
            else 
                Liquid_Name = getThirdIngredient(rpk);
           
            int location = 0;
            string locationquery = "SELECT LocationNumber FROM Inventory WHERE Liquid_Name = " + "\"" + Liquid_Name + "\"" + " OR Genre = " + "\"" + Liquid_Name + "\"" + ";";
            MySqlCommand locationcmd = new MySqlCommand(locationquery, connection);
            location = Convert.ToInt32(locationcmd.ExecuteScalar());
            return location;
        }
        public static int getPinNumber(int locationNumber)
        {
            if (locationNumber == 1)
                return SOLENOID_PIN1;           
            if (locationNumber == 2)
                return SOLENOID_PIN2;
            if (locationNumber == 3)
                return SOLENOID_PIN3;
            if (locationNumber == 4)
                return SOLENOID_PIN4;
            if (locationNumber == 5)
                return SOLENOID_PIN5;
            if (locationNumber == 6)
                return SOLENOID_PIN6;
            if (locationNumber == 7)
                return SOLENOID_PIN7;
            if (locationNumber == 8)
                return SOLENOID_PIN8;
            if (locationNumber == 9)
                return SOLENOID_PIN9;
            else 
                return SOLENOID_PIN10;
        }
        public static int getAmount(int ingredientCounter, int rPK)
        {
            var connection = ConnectionFactory.Create();
    
                if (ingredientCounter == 1)
                {
                    int FirstAmount = 0;
                    string firstIngredientAmountQuerey = "SELECT Amount FROM " + ingredientCounter + "st_Ingredient WHERE R_PK =" + rPK + " ;";
                    MySqlCommand firstcmd = new MySqlCommand(firstIngredientAmountQuerey, connection);
                    FirstAmount = Convert.ToInt32(firstcmd.ExecuteScalar());
                    return FirstAmount;
                }
                if (ingredientCounter == 2)
                {
                    int secondAmount = 0;
                    string secondIngredientAmountQuerey = "SELECT Amount FROM " + ingredientCounter + "nd_Ingredient WHERE R_PK =" + rPK + " ;";
                    MySqlCommand secondcmd = new MySqlCommand(secondIngredientAmountQuerey, connection);
                    secondAmount = Convert.ToInt32(secondcmd.ExecuteScalar());
                    return secondAmount;
                }
            if (ingredientCounter == 3)
            {
                int thirdAmount = 0;
                string thirdIngredientAmountQuerey = "SELECT Amount FROM " + ingredientCounter + "rd_Ingredient WHERE R_PK =" + rPK + " ;";
                MySqlCommand thirdcmd = new MySqlCommand(thirdIngredientAmountQuerey, connection);
                thirdAmount = Convert.ToInt32(thirdcmd.ExecuteScalar());
                return thirdAmount;
            }
            else return 0;  
        }
        public static void PourAnIngredient(GpioPin aPin, int timeOn)
        {            
            GpioPinValue On = GpioPinValue.High;
            GpioPinValue Off = GpioPinValue.Low;
            
            var timer = Stopwatch.StartNew();
            aPin.SetDriveMode(GpioPinDriveMode.Output);
            aPin.Write(On);
            while (timer.ElapsedMilliseconds < timeOn){ } 
            aPin.Write(Off);  
            timer.Stop();
        }

         /* THIS BLOCK BELOW IS USED FOR THE LISTS IN MIXED DRINKS AS WELL AS THEIR RESPECTIVE CONTENT*/
        public static string getFirstIngredient(int numofrecipe)
        {
            var connection = ConnectionFactory.Create();
            string firstIngredient = "";
            string firstIngredientquery = "SELECT Ingredient FROM 1st_Ingredient WHERE R_PK =" + numofrecipe + " ;";
            MySqlCommand firstIngredientcmd = new MySqlCommand(firstIngredientquery, connection);
            firstIngredient = (firstIngredientcmd.ExecuteScalar().ToString());
            return firstIngredient;
        }
        public static string getSecondIngredient(int numofrecipe)
        {
            var connection = ConnectionFactory.Create();
            string secondIngredient = "";
            string secondIngredientquery = "SELECT Ingredient FROM 2nd_Ingredient WHERE R_PK =" + numofrecipe + " ;";
            MySqlCommand secondIngredientcmd = new MySqlCommand(secondIngredientquery, connection);
            secondIngredient = (secondIngredientcmd.ExecuteScalar().ToString());
            return secondIngredient;
        }
        public static string getThirdIngredient(int numofrecipe)
        {
            var connection = ConnectionFactory.Create();
            string thirdIngredient = "";
            string thirdIngredientquery = "SELECT Ingredient FROM 3rd_Ingredient WHERE R_PK =" + numofrecipe + " ;";
            MySqlCommand thirdIngredientcmd = new MySqlCommand(thirdIngredientquery, connection);
            thirdIngredient = (thirdIngredientcmd.ExecuteScalar().ToString());
            return thirdIngredient;
        }
        public static string getLogo(string LiquorType)
        {
            string AbsolutePath = "LogoIcons/AbsoluteLogo.jpg";
            string WhiskeyPath = "LogoIcons/JackDanielsLogo.jpg";
            string RumPath = "LogoIcons/BicardiLogo.jpg";

            if (LiquorType == "Whiskey")
            {
                return WhiskeyPath;
            }
            if (LiquorType == "Vodka")
            {
                return AbsolutePath;
            }
              else          //if (LiquorType == "Rum")
                return RumPath; 
        }
        public static string getDrinkImage(int numofrecipe)
        {
            string BeachBum = "DrinkImages/BeachBum.jpg";
            string CranberryVodka = "DrinkImages/CranberryVodka.jpg";
            string WhiskeyCoke = "DrinkImages/WhiskeyCoke.jpg";
            string ScrewDriver = "DrinkImages/ScrewDriver.jpg";

            if(numofrecipe == 1)
            {
                return WhiskeyCoke;
            }
            else if(numofrecipe == 2)
            {
                return ScrewDriver;
            }
            else if(numofrecipe == 3)
            {
                return CranberryVodka;
            }
            else
                return BeachBum;
            
           
            
        }
        public static string getNameOfMixer(int numofrecipe) //The Minimum number here must be 1. If a zero is passed it will break and if a number larger than the number of elements is passed it will break. The First element in the table begins with 1.
        {
            var connection = ConnectionFactory.Create();
            string MixerName = "";
            string MixerNamequery = "SELECT Liquid_Name FROM Inventory WHERE Genre = (SELECT Ingredient FROM 2nd_Ingredient WHERE R_PK =" + numofrecipe + ");";
            MySqlCommand Mixercmd = new MySqlCommand(MixerNamequery, connection);
            MixerName = (Mixercmd.ExecuteScalar().ToString());
            return MixerName;
        }

        /*UNUSED GPIO CONTROL CODE THAT COULD BE USEFUL LATER ON*/
        public void InitializeSolenoids()
        {
            aController = GpioController.GetDefault();
        
            if(aController != null)
            {
                bool success = true;

                success &= InitializeSolenoidPin(SOLENOID_PIN1, out aSolenoidPin1);
                success &= InitializeSolenoidPin(SOLENOID_PIN2, out aSolenoidPin2);
                success &= InitializeSolenoidPin(SOLENOID_PIN3, out aSolenoidPin3);
                success &= InitializeSolenoidPin(SOLENOID_PIN4, out aSolenoidPin4);
                success &= InitializeSolenoidPin(SOLENOID_PIN5, out aSolenoidPin5);
                success &= InitializeSolenoidPin(SOLENOID_PIN6, out aSolenoidPin6);
                success &= InitializeSolenoidPin(SOLENOID_PIN7, out aSolenoidPin7);
                success &= InitializeSolenoidPin(SOLENOID_PIN8, out aSolenoidPin8);
                success &= InitializeSolenoidPin(SOLENOID_PIN9, out aSolenoidPin9);
                success &= InitializeSolenoidPin(SOLENOID_PIN10, out aSolenoidPin10);

            /*    aSolenoidPin1 = aController.OpenPin(SOLENOID_PIN1);
                aSolenoidPin1.Write(GpioPinValue.Low);
                aSolenoidPin1.SetDriveMode(GpioPinDriveMode.Output);

                aSolenoidPin2 = aController.OpenPin(SOLENOID_PIN2);
                aSolenoidPin2.Write(GpioPinValue.Low);
                aSolenoidPin2.SetDriveMode(GpioPinDriveMode.Output);

                aSolenoidPin3 = aController.OpenPin(SOLENOID_PIN3);
                aSolenoidPin3.Write(GpioPinValue.Low);
                aSolenoidPin3.SetDriveMode(GpioPinDriveMode.Output);

                aSolenoidPin4 = aController.OpenPin(SOLENOID_PIN4);
                aSolenoidPin4.Write(GpioPinValue.Low);
                aSolenoidPin4.SetDriveMode(GpioPinDriveMode.Output);

                aSolenoidPin5 = aController.OpenPin(SOLENOID_PIN5);
                aSolenoidPin5.Write(GpioPinValue.Low);
                aSolenoidPin5.SetDriveMode(GpioPinDriveMode.Output);

                aSolenoidPin6 = aController.OpenPin(SOLENOID_PIN6);
                aSolenoidPin6.Write(GpioPinValue.Low);
                aSolenoidPin6.SetDriveMode(GpioPinDriveMode.Output);

                aSolenoidPin7 = aController.OpenPin(SOLENOID_PIN7);
                aSolenoidPin7.Write(GpioPinValue.Low);
                aSolenoidPin7.SetDriveMode(GpioPinDriveMode.Output);

                aSolenoidPin8 = aController.OpenPin(SOLENOID_PIN8);
                aSolenoidPin8.Write(GpioPinValue.Low);
                aSolenoidPin8.SetDriveMode(GpioPinDriveMode.Output);

                aSolenoidPin9 = aController.OpenPin(SOLENOID_PIN9);
                aSolenoidPin9.Write(GpioPinValue.Low);
                aSolenoidPin9.SetDriveMode(GpioPinDriveMode.Output);

                aSolenoidPin10 = aController.OpenPin(SOLENOID_PIN10);
                aSolenoidPin10.Write(GpioPinValue.Low);
                aSolenoidPin10.SetDriveMode(GpioPinDriveMode.Output);
                */
    }

}
        public bool InitializeSolenoidPin(int pin, out GpioPin output)
        {
            bool result = true;
            GpioOpenStatus pinStatus;

            if(aController.TryOpenPin(pin, GpioSharingMode.Exclusive, out output, out pinStatus))
            {
                if(output.IsDriveModeSupported(GpioPinDriveMode.Output))
                {
                    output.SetDriveMode(GpioPinDriveMode.Output);
                    output.Write(GpioPinValue.Low);
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
        public void TurnOnSolenoid(GpioPin pin, bool value)
        {
            if (pin != null)
            {

                GpioPinValue v = value ? GpioPinValue.Low : GpioPinValue.High;
                pin.Write(v);
            }
        }
        public void Release()
        {
            if (aController != null)
            {
                if (aSolenoidPin1 != null)
                {
                    aSolenoidPin1.Dispose();
                }

                if (aSolenoidPin2 != null)
                {
                    aSolenoidPin2.Dispose();
                }
                if (aSolenoidPin3 != null)
                {
                    aSolenoidPin3.Dispose();
                }
                if (aSolenoidPin4 != null)
                {
                    aSolenoidPin4.Dispose();
                }
                if (aSolenoidPin5 != null)
                {
                    aSolenoidPin5.Dispose();
                }
                if (aSolenoidPin6 != null)
                {
                    aSolenoidPin6.Dispose();
                }
                if (aSolenoidPin7 != null)
                {
                    aSolenoidPin7.Dispose();
                }
                if (aSolenoidPin8 != null)
                {
                    aSolenoidPin8.Dispose();
                }
                if (aSolenoidPin9 != null)
                {
                    aSolenoidPin9.Dispose();
                }
                if (aSolenoidPin10 != null)
                {
                    aSolenoidPin10.Dispose();
                }
            }
        }
      
        /*Rarely used Functions*/
        public static string getnameofrecipe(int numofrecipe) //The Minimum number here must be 1. If a zero is passed it will break and if a number larger than the number of elements is passed it will break. The First element in the table begins with 1.
        {
            var connection = ConnectionFactory.Create();
            string recipename = "";
            string recipenamequery = "SELECT Recipe_Name FROM Recipes WHERE R_PK =" + numofrecipe + ";";
            MySqlCommand namecmd = new MySqlCommand(recipenamequery, connection);
            recipename = (namecmd.ExecuteScalar().ToString());
            return recipename;
        }
        public static string getTypeOfLiquor(int numofrecipe) //The Minimum number here must be 1. If a zero is passed it will break and if a number larger than the number of elements is passed it will break. The First element in the table begins with 1.
        {
            var connection = ConnectionFactory.Create();
            string liquortype = "";
            string liquortypequery = "SELECT GENRE FROM Liquids WHERE Liquid_Name = (SELECT Ingredient From 1st_Ingredient Where R_PK = " + numofrecipe + ");";
            MySqlCommand liquorcmd = new MySqlCommand(liquortypequery, connection);
            liquortype = (liquorcmd.ExecuteScalar().ToString());
            return liquortype;
        }
        public static int getnumofrecipes()
        {
            int counter = 0;
            var connection = ConnectionFactory.Create();
            string counterquery = "SELECT COUNT(*) FROM Recipes;";
            MySqlCommand countercmd = new MySqlCommand(counterquery, connection);
            counter = Convert.ToInt32(countercmd.ExecuteScalar());

            return counter;
        }
         /*    public static string getNameOfLiquor(int numofrecipe) //The Minimum number here must be 1. If a zero is passed it will break and if a number larger than the number of elements is passed it will break. The First element in the table begins with 1.
        {
            var connection = ConnectionFactory.Create();
            string liquorName = "";
            string liquorNamequery = "SELECT Ingredient FROM 1st_Ingredient WHERE R_PK =" + numofrecipe + ");"; //SELECT Liquid_Name FROM Inventory WHERE Genre = (
            MySqlCommand liquorcmd = new MySqlCommand(liquorNamequery, connection);
            liquorName = (liquorcmd.ExecuteScalar().ToString());
            return liquorName;
        }*/
        
            

        

       


       
     




        
       
       

        
        public static string getABV(string nameofLiquor) //The Minimum number here must be 1. If a zero is passed it will break and if a number larger than the number of elements is passed it will break. The First element in the table begins with 1.
        {
            var connection = ConnectionFactory.Create();
            string aBV = "0";
            string aBVquery = "SELECT ABV FROM Liquids;";// WHERE Liquid_Name;"; //="  + nameofLiquor + "); "; //(SELECT Liquid_Name FROM Liquids WHERE Liquid_Name =" + nameofLiquor + ");";// (SELECT Liquid_Name FROM Inventory WHERE =" + App.getNameOfLiquor(numofrecipe) + ");";
            MySqlCommand aBVcmd = new MySqlCommand(aBVquery, connection);
            aBV = (aBVcmd.ExecuteScalar().ToString());
            return aBV;
        }
       

      

        
       


        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            InitializeSolenoids();
            Frame rootFrame = Window.Current.Content as Frame;
            
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
