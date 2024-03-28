// See https://aka.ms/new-console-template for more information
using System.Reflection.Metadata;
using System.Runtime.InteropServices;



int shape;
int length;
int width;
int choice;



while(true)
{
	//gets the first choice- rectangle, triangle or stop
	while (true)
	{
		Console.WriteLine("Enter 1 for a rectangle tower and 2 for a triangular tower");
		if (int.TryParse(Console.ReadLine(), out choice) && (choice==1 ||  choice==2 || choice==3))
			break;
		Console.WriteLine("Illegal input");
	}
	if (choice == 3)
		break;
	shape = choice;

	//gets hight and weight
	while (true)
	{
		Console.WriteLine("Enter length");
		if (int.TryParse(Console.ReadLine(), out length))
			break;
		Console.WriteLine("Illegal input");
	}
	while (true)
	{
		Console.WriteLine("Enter width");
		if (int.TryParse(Console.ReadLine(), out width))
			break;
			Console.WriteLine("Illegal input");
	}

	//prints the appropriate output
	if (shape == 1)//for rectangle
		if (width == length || Math.Abs(width - length) > 5)
			Console.WriteLine("The area of the rectangle is " + width * length);
		else
			Console.WriteLine("The perimeter of the rectangle is " + (2 * width + 2 * length));
	else//for triangle
	{
		while (true)
		{
			Console.WriteLine("Enter 1 to calculate the perimeter of the triangle and 2 to print the triangle");
			if (int.TryParse(Console.ReadLine(), out choice) && (choice==1 ||  choice==2 || choice==3))
				break;
			Console.WriteLine("Illegal input");
		}
		if (choice == 1)
			Console.WriteLine("The perimeter of the triangle is " + (Math.Sqrt(length * length + (width/2.0) * (width/2.0))*2 + width));
		else//prints triangle
		{
			if (width % 2 == 0 || width > 2 * length)
				Console.WriteLine("Sorry, can't print triangle");
			else
			{
				for (int i = 0; i < width/2; i++)//prints head
					Console.Write(' ');
				Console.WriteLine('*');
				int n = 3;//number of * to print
				for (int i = 0; i < (length - 2) % ((width - 3) / 2); i++)//prints additional rows according to the remainder of the division
				{
					for (int j = 0; j < (width-n)/2; j++)
						Console.Write(' ');
					for (int j = 0; j < n; j++)
						Console.Write('*');
					Console.WriteLine();
				}
				while (n < width)//prints middle rows
				{
					for (int i = 0; i < (length - 2) / ((width - 3) / 2); i++)
					{
						for (int j = 0; j < (width-n)/2; j++)
							Console.Write(' ');
						for (int j = 0; j < n; j++)
							Console.Write('*');
						Console.WriteLine();
					}
					n+=2;
				}
				for (int i = 0; i < width; i++)//prints bace
					Console.Write('*');
				Console.WriteLine();
			}
		}
	}




}
