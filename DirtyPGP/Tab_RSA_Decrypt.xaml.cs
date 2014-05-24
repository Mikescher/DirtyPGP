using System.Windows.Controls;
using System.Windows.Media;

namespace DirtyPGP
{
	/// <summary>
	/// Interaction logic for Tab_Decrypt.xaml
	/// </summary>
	public partial class Tab_RSA_Decrypt : UserControl
	{
		private bool intialized = false;

		public Tab_RSA_Decrypt()
		{
			InitializeComponent();

			intialized = true;
		}

		private void ed_decode_before_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> eargs)
		{
			if (!intialized)
				return;

			long b = ed_decode_before.Value.Value;

			long d = ed_Key_d.Value.Value;
			long n = ed_Key_n.Value.Value;

			if (d == 0)
			{
				ell_test_encode.Fill = new SolidColorBrush(Colors.Red);
				memoInfo.Text = "[ERR] d == 0";
				return;
			}

			if (n == 0)
			{
				ell_test_encode.Fill = new SolidColorBrush(Colors.Red);
				memoInfo.Text = "[ERR] N == 0";
				return;
			}

			string dbgout = "";
			string dbgout2 = "";
			long v = RSAHelper.BinaryModuloPow(b, d, n, ref dbgout, ref dbgout2);
			ed_encode_after.Value = v;
			ell_test_encode.Fill = new SolidColorBrush(Colors.Green);


			memoInfo.Text = string.Format("({0}^{1}) % {2}\r\n = {3}\r\n({4})\r\n = {5}", b, d, n, dbgout, dbgout2, v);
		}
	}
}
