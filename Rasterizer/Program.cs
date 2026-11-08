using System.Collections;
using System.Drawing;

Console.WriteLine("Hello, World!");

Image img = new Image(800, 600);
img.Clear(new Pixel(0, 0, 0, 255));  // opaque black
img.SetPixel(10, 20, new Pixel(255, 0, 0, 255));

//struct/class darau public dėl paprastumo dirbti
//visi jų kintamieji nustatyti public, nes kitaip jie būtų default - private
public class Image
{
	public int width;
	public int height;
	public Pixel[] ekranas; //perdarau i vienmati masyva, kad uzimtu maziau atminties ir nebutu error'ų išeinant už ribų

	//konstruktorius
	public Image(int width, int height)
	{
		this.width = width;
		this.height = height;
		ekranas = new Pixel[height * width]; //apkeičiu koordinates, kad būtų lengviau dirbti. Toliau visur masyve naudoju [y,x]
	}

	public void SetPixel(int x, int y, Pixel color)
	{
		ekranas[y * width + x] = color; //taip iš esmės padaromas 2d masyvas iš 1d
	}

	public Pixel GetPixel(int x, int y)
	{
		return ekranas[y * width + x]; // grąžinama kopija, nes Pixel yra struct
	}

	public void Clear(Pixel color) //visą ekraną nudažome viena spalva
	{
		Array.Fill(ekranas,color); //ši funkcija būtų 2 ciklai, jei būtų 2d masyvas.
	}
}

public struct Pixel
{
	//koordinačių neapsirašau, nes jos ir taip bus pasiekiamos per ekranas[x,y]
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


