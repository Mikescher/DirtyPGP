using System;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace DirtyPGP
{
	/// <summary>
	/// Interaction logic for Tab_Encrypt.xaml
	/// </summary>
	public partial class Tab_PGP_Encrypt : UserControl
	{
		private bool intialized = false;

		public Tab_PGP_Encrypt()
		{
			InitializeComponent();

			intialized = true;
		}

		public void setKey(long e, long d, long n)
		{
			ed_Key_e.Value = e;
			ed_Key_n.Value = n;
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs eargs)
		{
			string plain = edPlainText.Text;

			StringBuilder numberBuilder = new StringBuilder();
			StringBuilder cryptBuilder = new StringBuilder();

			int count = 0;
			foreach (char chr in plain)
			{
				char cr = char.ToUpper(chr);

				if (cr == ' ')
				{
					numberBuilder.Append("00");
					cryptBuilder.Append("00");
				}
				else if (cr >= 'A' && cr <= 'Z')
				{
					numberBuilder.AppendFormat("{0:00}", (int)(cr - 'A' + 1));
					cryptBuilder.AppendFormat("{0:00}", (int)(cr - 'A' + 1));
				}

				numberBuilder.Append(" ");

				if (count++ == 2)
				{
					cryptBuilder.Append(" ");
					count = 0;
				}
			}

			edNumberized.Text = numberBuilder.ToString();
			edPlainNumbers.Text = cryptBuilder.ToString();


			//###############

			long e = ed_Key_e.Value.Value;
			long n = ed_Key_n.Value.Value;

			StringBuilder readyBuilder = new StringBuilder();
			foreach (int b in cryptBuilder.ToString().Split(' ').Where(p => RSAHelper.isInt(p)).Select(p => Convert.ToInt64(p)).ToList())
			{
				string dbgout = "";
				string dbgout2 = "";
				long v = RSAHelper.BinaryModuloPow(b, e, n, ref dbgout, ref dbgout2);

				readyBuilder.AppendFormat("{0:000000} ", v);
			}

			edCryptNumbers.Text = readyBuilder.ToString();
		}
	}
}
