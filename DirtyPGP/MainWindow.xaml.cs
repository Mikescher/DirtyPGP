using System.Windows;

namespace DirtyPGP
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			tab_Generate.ProvideKey += tab_Generate_ProvideKey;
		}

		void tab_Generate_ProvideKey(long e, long d, long n)
		{
			tab_PGP_Decrypt.setKey(e, d, n);
			tab_PGP_Encrypt.setKey(e, d, n);
			tab_RSA_Decrypt.setKey(e, d, n);
			tab_RSA_Encrypt.setKey(e, d, n);
		}
	}
}
