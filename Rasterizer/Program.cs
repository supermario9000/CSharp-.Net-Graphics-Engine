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

	//Žemiau pateikiami metodai skirti figūroms braižyti:
	public void DrawLine(int x0, int y0, int x1, int y1, Pixel color, int thickness = 1) // thickness - kokio storumo linija. Pvz. 2 px ar 1 px. Dar nepadaryta, bet numatyta
	{
		//algoritmo idėja: 
		//1. pagal pitagorą apsiskaičiuojame, kokio ilgio turi gautis tiesė (vektorius)
		//2. dedame žingsnį pagal liniją nuo x0, link x1 (kuriame pikselyje linija tuo metu)
		//žingsnis dedamas pagal padalinimą (padalinami dx, dy. Maksimumas - žingsnių skaičius. Todėl mažesnis d(x ar y) turės mažesnius žingsnius)
		//tuomet apvaliname į žemesnę pusę kas žingsnį kiekvieną x ir y.
		//3. kartojame kol baigiasi žingsniai.
		//Pasidarome Pitagoro teoremą (nes apimtų ir vienos ašies ir dviejų ašių tieses)
		int dx = x1 - x0;
		int dy = y1 - y0;
		int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));

		for (int i = 0; i <= steps; i++)
		{
			double t; // apskaičiuojame, kiek toli nuėjome mūsų linijoje
			if(steps == 0)
			{
				t = 0; //padedamas taškas/ vieno taško linija
			}
			else
			{
				t = (double)i / steps; //nuo 0 iki 1. Steps - dx ir dy trupmeninė dalis. Pvz jei dx=7, steps bus 1/7
			}

			int x = (int)Math.Round(x0 + dx * t); //naudojama round, kad būtų piešiama nuo pikselio "centro"
			int y = (int)Math.Round(y0 + dy * t); //t.y. pikselis yra -0.5<x ar y<0.5

			if (x >= 0 && x < width && y >= 0 && y < height)
			{
				ekranas[y * width + x] = color;
			}
		}
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
	const string OutputFolder = "Images";
	static void Main()
	{
		Console.WriteLine("Experiments 1-4 starting...");
		//eksperimentas 1 - pabandyti pagrindines funkcijas
		Console.WriteLine("Experiment 1:");
		Image img1 = new Image(16,16);
		img1.Clear(new Pixel(0,0,0,255));
		//spausdiname turima nuotrauka:
		Print(img1);
		SaveImageAsPPM(img1,"1_eksperimentas");
		
		//eksperimentas 2 - nustatome kampinių pikselių spalvas
		Console.WriteLine("Experiment 2:");
		img1.SetPixel(0,0,new Pixel(255,0,0,255));
		img1.SetPixel(15,0,new Pixel(0,255,0,255));
		img1.SetPixel(0,15,new Pixel(0,0,255,255));
		img1.SetPixel(15,15,new Pixel(255,255,255,255));
		Print(img1);
		SaveImageAsPPM(img1,"2_eksperimentas");

		//eksperimentas 3 - užpildome viduriuką atsitiktine tvarka
		Console.WriteLine("Experiment 3:");
		//eksperimentui susikūriu papildomą masyvą 3 bandymui
		Pixel[] RandomColorSet = {new Pixel (255,0,0,255), new Pixel (0,255,0,255), new Pixel (0,0,255,255), new Pixel (255,255,255,255), new Pixel (123,123,123,255)};
		Random r = new Random();//generuojame random indeksą, kurį naudosime 3 eksperimente
		for(int y = 1; y < img1.height - 1; y++)
		{
			for(int x = 1; x < img1.width - 1; x++)
			{
				//užpildome nuotraukos vidurį atsitiktiniais pikseliais
				img1.SetPixel(x,y,RandomColorSet[r.Next(0,5)]); //naudoju 0-4, nes negrąžina išorinės ribos. T.y. dabar 0<=r<5
			}
		}
		Print(img1);
		SaveImageAsPPM(img1,"3_eksperimentas");

		//eksperimentas 4 - išvalome nuotrauką. (tikriname, ar Clear metodo default parametrai veikia)
		Console.WriteLine("Experiment 4:");
		img1.Clear();
		Print(img1);
		SaveImageAsPPM(img1,"4_eksperimentas");
		Console.WriteLine("Experiments 1-4 finished.");

		//eksperimentai 5-8 - formu braižymas
		//eksperimentas 5 - tikslas: išbandyti, ar veikia linijos nubrėžimas.
		//eksperimentą dalinu į 3 dalis skirtingoms linijoms brėžti.
		//5.a - įstrižos linijos, kai x0<x1 ir y0<y1 brėžimas
		Console.WriteLine("Experiment 5a:");
		img1.DrawLine(1,1,15,13,new Pixel(255,0,0,0)); // nupiešiame liniją
		Print(img1);
		SaveImageAsPPM(img1,"5_eksperimentas_a");
		//5.b - tiesios linijos brėžimas, kai x0>x1, o y0=y1
		Console.WriteLine("Experiment 5b:");
		img1.DrawLine(15,13,1,13,new Pixel(255,0,0,0)); // nupiešiame liniją
		Print(img1);
		SaveImageAsPPM(img1,"5_eksperimentas_b");
		//5.c - brėžimas, kai x0<x1 ir y0>y1
		Console.WriteLine("Experiment 5c:");
		img1.DrawLine(1,13,15,1,new Pixel(255,0,0,0)); // nupiešiame liniją
		Print(img1);
		SaveImageAsPPM(img1,"5_eksperimentas_c");
		//veikia puikiai

		//eksperimentas 6 - kvadrato nubraižymas iš linijų | pagal šį eksperimentą bus daroma kvadrato brėžimo funkcija
		//tikslas - turint 2 taškus nubrėžti keturkampį
		Console.WriteLine("Experiment 5:");
		img1.Clear();
		int testx0 = 0;//virsutinis kairinis
		int testy0 = 0;
		int testx1 = 15;//apatinis dešininis
		int testy1 = 15;
		//taskai 
		//piešiame keturkampį
		//1 cikas x0<x1 y1=y0; 2 ciklas x0=x1 y0<y1; 3 ciklas x0>x1 y0=y1; 4 ciklas x1=x0 y0>y1
		img1.DrawLine(testx0,testy0,testx1,testy0,new Pixel(255,0,0,255));
		img1.DrawLine(testx1,testy0,testx1,testy1,new Pixel(255,0,0,255));
		img1.DrawLine(testx0,testy1,testx1,testy1,new Pixel(255,0,0,255));
		img1.DrawLine(testx0,testy0,testx0,testy1,new Pixel(255,0,0,255));
		Print(img1);
		SaveImageAsPPM(img1, "6_eksperimentas");
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

	static void SaveImageAsPPM(Image img, string fileName)
	{
		//ppm formatas:
		//P3 (arba P6) - headeris, kuris nustato, kokiu formatu rašomi duomenys (ascii/binary)
		//x y - nuotraukos dimensijos (width height)
		//255 - maksimalus spalvos intensyvumas
		//x0y0  x1y0  ...  xny0 - pikseliu spalvu matrica / ekranas
		//x0y1  x1y1  ...  xny1 - du tarpai tarp pikselių.
		//....  ....  ...  ....
		//x0yn  x1yn  ...  xnyn
		
		//Tačiau ppm nepalaiko rgb(A) alpha skaičiaus, todėl jis čia nenaudojamas.
		string path = Path.Combine(OutputFolder, fileName + ".ppm");
		try
		{
			Directory.CreateDirectory(OutputFolder); //metodas automatiškai patikrina, ar egzistuoja aplankas, praleidžia jei taip.
			using StreamWriter writer = new StreamWriter(path);
			writer.WriteLine("P3");
			writer.WriteLine(img.width + " " + img.height);
			writer.WriteLine("255");

			//surašome pačią pikselių matricą
			for(int y = 0; y < img.height; y++)
			{
				for(int x = 0; x < img.width; x++)
				{
					Pixel p = img.GetPixel(x,y);
					writer.Write(p.R + " " + p.G + " " + p.B + "  "); // gale du tarpai, taip atskiriami pikseliai ppm faile
				}
				writer.WriteLine();
			}
		}
		catch(IOException e)
		{
			Console.WriteLine($"Could not save `{path}`: {e.Message}");
		}
		catch(UnauthorizedAccessException e)
		{
			Console.WriteLine($"No permission to write `{path}`: {e.Message}");
		}
	}
}