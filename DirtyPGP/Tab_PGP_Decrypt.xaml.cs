using System;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace DirtyPGP
{
	/// <summary>
	/// Interaction logic for Tab_Encrypt.xaml
	/// </summary>
	public partial class Tab_PGP_Decrypt : UserControl
	{
		private bool intialized = false;

		public Tab_PGP_Decrypt()
		{
			InitializeComponent();

			intialized = true;
		}

		public void setKey(long e, long d, long n)
		{
			ed_Key_d.Value = d;
			ed_Key_n.Value = n;
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs eargs)
		{
			long d = ed_Key_d.Value.Value;
			long n = ed_Key_n.Value.Value;

			string code = edCryptNumbers.Text;

			StringBuilder readyBuilder = new StringBuilder();
			foreach (int b in code.Split(' ').Where(p => RSAHelper.isInt(p)).Select(p => Convert.ToInt64(p)).ToList())
			{
				string dbgout = "";
				string dbgout2 = "";
				long v = RSAHelper.BinaryModuloPow(b, d, n, ref dbgout, ref dbgout2);

				readyBuilder.AppendFormat("{0:000000} ", v);
			}
			edNumberized.Text = readyBuilder.ToString();

			StringBuilder numberBuilder = new StringBuilder();
			foreach (int b in readyBuilder.ToString().Split(' ').Where(p => RSAHelper.isInt(p)).Select(p => Convert.ToInt64(p)).ToList())
			{
				numberBuilder.AppendFormat("{0:00} ", (b / 10000) % 100);
				numberBuilder.AppendFormat("{0:00} ", (b / 100) % 100);
				numberBuilder.AppendFormat("{0:00} ", (b / 1) % 100);
			}
			edPlainNumbers.Text = numberBuilder.ToString();

			StringBuilder textBuilder = new StringBuilder();
			foreach (int b in numberBuilder.ToString().Split(' ').Where(p => RSAHelper.isInt(p)).Select(p => Convert.ToInt64(p)).ToList())
			{
				if (b == 0)
					textBuilder.Append(' ');
				else
					textBuilder.Append((char)('A' + (b - 1)));
			}

			edPlainText.Text = textBuilder.ToString();
		}
	}
}
