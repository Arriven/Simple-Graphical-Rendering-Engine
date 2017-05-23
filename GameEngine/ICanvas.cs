using System;
namespace GameEngine
{
	public interface ICanvas
	{
		Color this[uint x, uint y] { get; set;} // x and y in pixels
		Color this[float x, float y] { get; set; } // x and y in range [-1; 1]
		uint Width { get; }
		uint Height { get; }
	}
}
