using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EzBarApplication;
using System.Diagnostics;


namespace EzBarApplication.Model
{
    public class MDrinks
    {
        public string DrinkName { get; set; }
        public int NumOfIngredients { get; set; }
        public string FirstIngredient { get; set; }
        public string secondIngredient { get; set; }
       public string thirdIngredient { get; set; }
      /*   public string fourthIngredient { get; set; }
        public string fifthIngredient { get; set; }*/
        public string getNameOfLiquor { get; set; }
        public string LiquorType { get; set; }
        public string LiquorName { get; set; }
        public string AlcoholPercentage { get; set; }
        public string Mixer { get; set; }
        public string LogoImage { get; set; }
        public string DrinkImage { get; set; }
        public int BottleLocation { get; set; }
       // public void turnItOn { get; set; }
       
    }

    public class MixedDrinkGetter
    {
        public static List<MDrinks> GetDrinks()
        {
            
            
            var numOfDrinks = App.getnumofrecipes();
          //  var AlcoholPercentage = App.getABV(App.getNameOfLiquor(i);
          //  var NOI = App.getNumOfIngredients();
            var aDrink= new List<MDrinks>();

            
               for (int i = 1; i < (numOfDrinks + 1); i++)
                {
                

              //  var setAlcoholPercentage = (App.getABV(App.getNameOfLiquor(i)));

                aDrink.Add(new MDrinks {
                    DrinkName = App.getnameofrecipe(i),
                  //  NumOfIngredients = App.getNumOfIngredients(i),
                      // LiquorType = App.getTypeOfLiquor(i),

                     //  LiquorName = App.getNameOfLiquor(i),
                    //   AlcoholPercentage = App.getABV("*"),
                    FirstIngredient =  App.getFirstIngredient(i),
                    secondIngredient = App.getSecondIngredient(i),        
                    thirdIngredient = App.getThirdIngredient(i),
                    
                    //App.getFirstIngredient(i, App.getRecipieNumber(App.getnameofrecipe(i))),
            /*        secondIngredient = App.getSecondIngredient(i, App.getRecipieNumber(App.getnameofrecipe(i))),
                    thirdIngredient = App.getThirdIngredient(i, App.getRecipieNumber(App.getnameofrecipe(i))),
                    fourthIngredient = App.getThirdIngredient(i, App.getRecipieNumber(App.getnameofrecipe(i))),
                    fifthIngredient = App.getThirdIngredient(i, App.getRecipieNumber(App.getnameofrecipe(i))),*/
                    LogoImage = App.getLogo(App.getTypeOfLiquor(i)),
                    DrinkImage = App.getDrinkImage(i),
                    Mixer = App.getNameOfMixer(i) 
                   });
                    
              
            }



            return aDrink;
        }
    }
}