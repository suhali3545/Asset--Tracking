
using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

List<Asset> assets = new List<Asset>();
        string input = "";

        Console.WriteLine("Asset Tracking Application");

while (input != "q")
{    // Ask for products
    Console.WriteLine("Enter office (Germany, Sweden, USA): ");
    string office = Console.ReadLine();

    Console.WriteLine("Enter asssetType (Computer, Smartphone): ");
    string assetType = Console.ReadLine();

    Console.WriteLine("Enter brand (Asus,Siemens, Dell, Samsung, Sony, Lenovo, Motorola): ");
    string brand = Console.ReadLine();

    Console.WriteLine("Enter model (Galaxy 10, Xperia 7, X300, X 200, Desktop 900, ROG 500,Optiplex 100, Optiplex 200, Optiplex 300, Brick): ");
    string model = Console.ReadLine();


    Console.WriteLine("Enter product price: ");
    decimal PriceUSD = decimal.Parse(Console.ReadLine());

    Console.WriteLine("Enter purchaseDate (yyyy-mm-dd): ");
    DateTime PurchaseDate = DateTime.Parse(Console.ReadLine());
    decimal exchangeRate = 1.0m;
    if (office == "Germany")
    {
        decimal.TryParse(Console.ReadLine(), out exchangeRate);
    }
    else if (office == "Sweden")
    {
        decimal.TryParse(Console.ReadLine(), out exchangeRate);
    }
    decimal priceLocal = 0;

    // Greate and Add Asset to the list
    assets.Add(new Asset(office, assetType, brand, model, PriceUSD, priceLocal, PurchaseDate, exchangeRate));
    // Ask the user if you want to continue
    Console.WriteLine("Asset added successfully! Enter 'q' to quit or any key to add asset!\n");

    input = Console.ReadLine();

    if (input == "q")
    {
        break;
    }

    Console.WriteLine("-------------------------------------");
    

   


}

Console.WriteLine("-------------------------------------");



 //Sorting products by office, purchaseDate and assetType

List<Asset> sortedassets = assets.OrderBy(Asset => Asset.Office)
                                 .ThenBy(Asset => Asset.purchaseDAte)
                                 .ThenBy(Asset => Asset.AssetType).ToList();
// Output Asset Traking List
Console.WriteLine("Office".PadRight(15) + "AssetType".PadRight(15) + "Brand".PadRight(10) + "Model".PadRight(10) + "Price (USD)".PadRight(15) + "Price (Local)".PadRight(15) + "Purchase Date" );
foreach (Asset Asset in sortedassets)
{
    string status = Asset.GetStatusColor();
    string pricelocal = Asset.GetLocalPrice().ToString("C", CultureInfo.CurrentCulture);
    Console.ForegroundColor = status == "red" ? ConsoleColor.Red : (status == "yellow" ? ConsoleColor.Yellow : ConsoleColor.Green);
    {   // Reset text color.
        Console.ResetColor();
    }
    Console.WriteLine(Asset.Office.PadRight(15) + Asset.AssetType.PadRight(15) + Asset.purchaseDAte.PadRight(10) + Asset.Model.PadRight(10) + Asset.PriceUSD.ToString("F2").PadRight(15) + Asset.PriceLocal.ToString("F2").PadRight(15) + Asset.PurchaseDate );
}

Console.ReadLine();
Console.ReadKey();


class Asset
{
    private DateTime purchaseDate;
    private object priceLocal;

    public Asset(string? office, string? assetType, string? brand, string? model, decimal priceUSD, DateTime purchaseDate, decimal exchangeRate)
    {
        Office = office;
        AssetType = assetType;
        purchaseDAte = brand;
        Model = model;
        PriceUSD = priceUSD;
        this.purchaseDate = purchaseDate;
    }

    public Asset(string? office, string? assetType, string? brand, string? model, decimal priceUSD, object priceLocal, DateTime purchaseDate)
    {
        Office = office;
        AssetType = assetType;
        purchaseDAte = brand;
        Model = model;
        PriceUSD = priceUSD;
        this.priceLocal = priceLocal;
        this.purchaseDate = purchaseDate;
    }

    public Asset(string office, string assetType, string brand, string model, decimal priceUSD, decimal priceLocal, DateTime purchaseDate, decimal exchangeRate)
    {
        Office = office;
        AssetType = assetType;
        purchaseDAte = brand;
        Model = model;
        PriceUSD = priceUSD;
        PriceLocal = priceLocal;
        PurchaseDate = purchaseDate;
        ExchangeRate = exchangeRate;
    }

    public string Office { get; set; }
    public string AssetType { get; set; }
        public string purchaseDAte { get; set; }
        public string Model { get; set; }
        public decimal PriceUSD { get; set; }
        public decimal PriceLocal { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal ExchangeRate { get; private set; }
         
    // calculate the local price based on office location and exchange rate
        public decimal GetLocalPrice()
        {
            //if (Office == "Germany") return PriceUSD * 0.85m;
            //if (Office == "SWeden") return PriceUSD * 10.8m;
            //if (Office == "USD") return PriceUSD;
            return PriceUSD * ExchangeRate;
        }

    // check the Asset status based on its age
    public string GetStatusColor()
    {
            var lifeSpan = 3;

            var endOfLife = PurchaseDate.AddYears(3);
            TimeSpan remainingTime = endOfLife - DateTime.Now;
            if (remainingTime.TotalDays <= 90)
            {
                return "RED";

            }
            else if (remainingTime.TotalDays <= 180)
            {
                return "YELLOW";
            }
            else
            {
                return "NORMAL";
            }
    }

}





