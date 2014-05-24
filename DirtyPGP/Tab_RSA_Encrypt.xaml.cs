using System.Windows.Controls;
using System.Windows.Media;

namespace DirtyPGP
{
	/// <summary>
	/// Interaction logic for Tab_Encrypt.xaml
	/// </summary>
	public partial class Tab_RSA_Encrypt : UserControl
	{
		private bool intialized = false;

		public Tab_RSA_Encrypt()
		{
			InitializeComponent();

			intialized = true;
		}

		private void ed_encode_before_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> eargs)
		{
			if (!intialized)
				return;

			long b = ed_encode_before.Value.Value;

			long e = ed_Key_e.Value.Value;
			long n = ed_Key_n.Value.Value;

			if (e == 0)
			{
				ell_test_encode.Fill = new SolidColorBrush(Colors.Red);
				memoInfo.Text = "[ERR] e == 0";
				return;
			}

			if (n == 0)
			{
				ell_test_encode.Fill = new SolidColorBrush(Colors.Red);
				memoInfo.Text = "[ERR] N == 0";
				return;
			}

			if (b >= n)
			{
				ell_test_encode.Fill = new SolidColorBrush(Colors.Red);
				memoInfo.Text = "[ERR] b >= N";
				return;
			}

			string dbgout = "";
			string dbgout2 = "";
			long v = RSAHelper.BinaryModuloPow(b, e, n, ref dbgout, ref dbgout2);
			ed_encode_after.Value = v;
			ell_test_encode.Fill = new SolidColorBrush(Colors.Green);


			memoInfo.Text = string.Format("({0}^{1}) % {2}\r\n = {3}\r\n({4})\r\n = {5}", b, e, n, dbgout, dbgout2, v);
		}
	}
}
