
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Windows;


namespace DataBase_Linq_toData
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			var constr = ConfigurationManager.ConnectionStrings["constr"].ToString();
			var datacontext = new dataGridDataContext(constr);

			new Thread(() =>
			{
				var result = from c in datacontext.Tickets
							 select new
							 {
								 c.FirstName,
								 c.SecondName,
								 Source = c.City.Name,
								 Destination = c.City1.Name,
								 DateDeparture = c.Date1,
								 DateArrival = c.Date2,
								 c.Class,
								 c.Price,
								 c.Way
							 };
				dataGrid.Dispatcher.Invoke(() => { dataGrid.ItemsSource = result; });
			}).Start();
		}
	}
}
