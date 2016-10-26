using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PrediccionAutos.Paginas
{
	public partial class PaginaAuto : ContentPage
	{
        public async void btnPredecir_Clicked(object sender, EventArgs e)
        {
            price.Text = await Clases.PrediccionAutos.Predecir(make.Text, body.Text, wheel.Text, engine.Text, horsepower.Text, peak.Text, highway.Text);
        }

        public PaginaAuto ()
		{
			InitializeComponent ();
		}
	}
}
