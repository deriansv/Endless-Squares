// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


namespace SquareApi.Models
{
    public class Square
    {

        public int Id { get; set; }
        public double SideLength { get; set; }
        public double Area
        {
            get { return SideLength * SideLength; }
        }
    }
}