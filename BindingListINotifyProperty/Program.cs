using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BindingListINotifyProperty
{
    class Market : INotifyPropertyChanged
    {
        #region Prperty i notyfy
        //use of notify property
        private float product;
        public float Product
        {
            get => product;
            set
            {
                if (value.Equals(product)) return;
                product = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Called when Product is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        //1 use of binding list
        public BindingList<float> Prices { get; set; } = new BindingList<float>()
        {
            AllowNew = false,
            RaiseListChangedEvents = true,

        };


    }

    class Program
    {
        static void Main(string[] args)
        {

            var market = new Market();

            market.PropertyChanged += Market_PropertyChanged;
            if (market.Prices != null)
            {
                market.Prices.ListChanged += Prices_ListChanged;
                market.Product = 5f;

                string enteredValue;
                do
                {
                    enteredValue = Console.ReadLine();
                    if (!float.TryParse(enteredValue, out var parsedFloat)) continue;

                    market.Product = parsedFloat;

                    market.Prices?.Add(parsedFloat);
                    if (market.Prices.Count > 1)
                    {
                        market.Prices[market.Prices.Count - 2]++;
                    }

                } while (enteredValue != "0");
            }


            Console.ReadLine();
        }

        private static void Prices_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (!(sender is BindingList<float> bindingList)) return;
            for (var i = 0; i< bindingList.Count;i++)
            {
                Console.WriteLine($"Prices[{i}]:\t{bindingList[i]}");
            }

            Console.WriteLine($"Element with index {e.NewIndex} changed to {bindingList[e.NewIndex]}." +
                              $"{e.PropertyDescriptor}");
        }

        private static void Market_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Market market)
            {
                Console.WriteLine($"This is market. Property name is [{e.PropertyName}]. The new product is {market.Product}");
            }
        }
    }
}
