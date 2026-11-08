
//struct/class darau public dėl paprastumo dirbti
//visi jų kintamieji nustatyti public, nes kitaip jie būtų default - private
public class Image
{
	public int width;
	public int height;
	private Pixel[] ekranas; //perdarau i vienmati masyva, kad uzimtu maziau atminties ir nebutu error'ų išeinant už ribų
	//ekranas privatus todėl, kad jam skaityti ar rašyti sukūriau specialius metodus, kuriuos ir noriu, kad naudotų programa.

	//konstruktorius
	public Image(int width, int height)
	{
		if (width <= 0 || height <= 0)
		{
			//naudoju paprastą throw, nes try catch metodai yra aukstesnio lygmens funkcijoms
			throw new ArgumentException("Width and height must be positive");
		}

		this.width = width;
		this.height = height;
		ekranas = new Pixel[height * width]; //apkeičiu koordinates, kad būtų lengviau dirbti. Toliau visur masyve naudoju [y,x]
	}

	public void SetPixel(int x, int y, Pixel color)
	{
		//jei prasoma uz ribu:
		if (x < 0 || x >= width || y < 0 || y >= height)
		{
			throw new ArgumentException("The pixel's coordinates must be inside the image's bounds");
		}

		ekranas[y * width + x] = color; //taip iš esmės padaromas 2d masyvas iš 1d
	}

	public Pixel GetPixel(int x, int y)
	{
		if (x < 0 || x >= width || y < 0 || y >= height)
		{
			throw new ArgumentException("The pixel's coordinates must be inside the image's bounds");
		}
		return ekranas[y * width + x]; // grąžinama kopija, nes Pixel yra struct
	}

	public void Clear(Pixel color) //visą ekraną nudažome viena spalva
	{
		Array.Fill(ekranas,color); //ši funkcija būtų 2 ciklai, jei būtų 2d masyvas.
	}
	//default parametrai Clear funkcijai:
	public void Clear()
	{
		Clear(new Pixel(0,0,0,255)); // negalima įrašyti į pagrindinę funkciją, nes yra žodelis "new", kuris yra ne konstanta.
	}

}

public struct Pixel
{
	//koordinačių neapsirašau, nes jos ir taip bus pasiekiamos per ekranas[y*width+x]
	//spalvą darau byte kintamajame, nes jis užima mažai atminties ir iškart turi 0-255 apribojimus
	public byte R, G, B, A; //alpha gali buti nuo 0 iki 1, tačiau apsibrėžiu kaip byte, konvertuoti galiu vėliau
	public Pixel (byte r, byte g, byte b, byte a)
	{
		R = r;
		G = g;
		B = b;
		A = a;
	}

	public void SetColor(byte r, byte g, byte b, byte a)
	{
		R = r;
		G = g;
		B = b;
		A = a;
	}
}


class Program // kaip int main() per c++, csharp neleidzia funkciju apsirasyti po klasiu deklaracija.
{
	static void Main()
	{
		Console.WriteLine("Experiments 1-4 starting...");
		//eksperimentas 1 - pabandyti pagrindines funkcijas
		Console.WriteLine("Experiment 1:");
		Image img1 = new Image(16,16);
		img1.Clear(new Pixel(0,0,0,255));
		//spausdiname turima nuotrauka:
		Print(img1);
		
		//eksperimentas 2 - nustatome kampinių pikselių spalvas
		Console.WriteLine("Experiment 2:");
		img1.SetPixel(0,0,new Pixel(255,0,0,255));
		img1.SetPixel(15,0,new Pixel(0,255,0,255));
		img1.SetPixel(0,15,new Pixel(0,0,255,255));
		img1.SetPixel(15,15,new Pixel(255,255,255,255));
		Print(img1);

		//eksperimentas 3 - užpildome viduriuką atsitiktine tvarka
		Console.WriteLine("Experiment 3:");
		//eksperimentui susikūriu papildomą masyvą 3 bandymui
		Pixel[] RandomColorSet = {new Pixel (255,0,0,255),new Pixel (0,255,0,255),new Pixel (0,0,255,255),new Pixel (255,255,255,255)};
		Random r = new Random();//generuojame random indeksą, kurį naudosime 3 eksperimente
		for(int y = 1; y < img1.height - 1; y++)
		{
			for(int x = 1; x < img1.width - 1; x++)
			{
				//užpildome nuotraukos vidurį atsitiktiniais pikseliais
				img1.SetPixel(x,y,RandomColorSet[r.Next(0,4)]);
			}
		}
		Print(img1);

		//eksperimentas 4 - išvalome nuotrauką. (tikriname, ar Clear metodo default parametrai veikia)
		Console.WriteLine("Experiment 4:");
		img1.Clear();
		Print(img1);
		Console.WriteLine("Experiments 1-4 finished.");
	}

	static void Print (Image img)
	{
		for (int y = 0; y < img.height; y++)
		{
			Console.Write("\n");
			for(int x = 0; x < img.width; x++)
			{
				//teko naudoti .Equals, nes == neveikia
				if (img.GetPixel(x,y).Equals(new Pixel(0,0,0,255))) Console.Write("."); //juoda
				else if(img.GetPixel(x,y).Equals(new Pixel(255,0,0,255))) Console.Write("R");
				else if(img.GetPixel(x,y).Equals(new Pixel(0,255,0,255))) Console.Write("G");
				else if(img.GetPixel(x,y).Equals(new Pixel(0,0,255,255))) Console.Write("B");
				else if(img.GetPixel(x,y).Equals(new Pixel(255,255,255,255))) Console.Write("W");
				else Console.Write("#"); //bet kokia kita spalva
			}
		}
		Console.Write("\n");
	}
}