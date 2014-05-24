using System.Windows.Controls;
using System.Windows.Media;

namespace DirtyPGP
{
	/// <summary>
	/// longeraction logic for Tab_Generate.xaml
	/// </summary>
	public partial class Tab_Generate : UserControl
	{
		private bool intialized = false;

		public event ProvideKeyHandler ProvideKey;
		public delegate void ProvideKeyHandler(long e, long d, long n);

		public Tab_Generate()
		{
			InitializeComponent();

			intialized = true;

			testValues();
		}

		private void ed_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
		{
			testValues();
		}

		private void testValues()
		{
			if (!intialized)
				return;

			SolidColorBrush b_true = new SolidColorBrush(Colors.Green);
			SolidColorBrush b_false = new SolidColorBrush(Colors.Red);

			long p = ed_var_p.Value.Value;
			long q = ed_var_q.Value.Value;
			long n = ed_var_n.Value.Value;
			long m = ed_var_m.Value.Value;
			long e = ed_var_e.Value.Value;
			long d = ed_var_d.Value.Value;

			ell_test_p.Fill = RSAHelper.IsPrime(p) ? b_true : b_false;
			ell_test_q.Fill = RSAHelper.IsPrime(q) ? b_true : b_false;

			ell_test_n.Fill = (n == p * q) ? b_true : b_false;
			ell_test_m.Fill = (m == (p - 1) * (q - 1)) ? b_true : b_false;

			ell_test_e.Fill = RSAHelper.GGT(e, m) == 1 ? b_true : b_false;

			ell_test_d.Fill = (m != 0 && (e * d) % m == 1) ? b_true : b_false;

			bool correct = RSAHelper.IsPrime(p) && RSAHelper.IsPrime(q) && (n == p * q) && (m == (p - 1) * (q - 1)) && (RSAHelper.GGT(e, m) == 1) && (m != 0 && (e * d) % m == 1);

			lblPublicKey.Content = correct ? string.Format("PUBLIC KEY = (e|N) = ({0}|{1})", e, n) : "ERROR";
			lblPrivateKey.Content = correct ? string.Format("PRIVATE KEY = (d|N) = ({0}|{1})", d, n) : "ERROR";
		}
		private void btn_gen_p_Click(object sender, System.Windows.RoutedEventArgs eargs)
		{
			ed_var_p.Value = RSAHelper.GetPrime();
		}

		private void btn_gen_q_Click(object sender, System.Windows.RoutedEventArgs eargs)
		{
			ed_var_q.Value = RSAHelper.GetPrime();
		}

		private void btn_gen_n_Click(object sender, System.Windows.RoutedEventArgs eargs)
		{
			long p = ed_var_p.Value.Value;
			long q = ed_var_q.Value.Value;

			ed_var_n.Value = p * q;
		}

		private void btn_gen_m_Click(object sender, System.Windows.RoutedEventArgs eargs)
		{
			long p = ed_var_p.Value.Value;
			long q = ed_var_q.Value.Value;

			ed_var_m.Value = (p - 1) * (q - 1);
		}

		private void btn_gen_e_Click(object sender, System.Windows.RoutedEventArgs eargs)
		{
			long m = ed_var_m.Value.Value;

			ed_var_e.Value = RSAHelper.GetRandomCoprime(m);
		}

		private void btn_gen_d_Click(object sender, System.Windows.RoutedEventArgs eargs)
		{
			long m = ed_var_m.Value.Value;
			long e = ed_var_e.Value.Value;

			ed_var_d.Value = RSAHelper.GetMultInv(e, m);
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs eargs)
		{
			long n = ed_var_n.Value.Value;
			long e = ed_var_e.Value.Value;
			long d = ed_var_d.Value.Value;

			if (ProvideKey != null)
			{
				ProvideKey(e, d, n);
			}
		}
	}
}
