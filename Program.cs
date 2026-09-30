using Raylib_cs;
namespace ColorLab;

internal static class Programa
{
    public static void Main()
    {
        const int anchoVentana = 850;
        const int altoVentana = 600;

        Raylib.InitWindow(anchoVentana, altoVentana, "Practica 3 - Color Lab");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            int y = 20;
            foreach (var c in paleta)
            {
                Color colorRaylib = new Color(c.R, c.G, c.B, (byte)255);

                Raylib.DrawRectangle(20, y, 40, 40, colorRaylib);

                string info = $"{c.Nombre}  RGB({c.R},{c.G},{c.B})  {ColorAHex(colorRaylib)}";
                Raylib.DrawText(info, 70, y + 10, 18, Color.Black);

                y += 55;
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    struct ColorPaleta
    {
        public string Nombre;
        public byte R, G, B;

        public ColorPaleta(string nombre, byte r, byte g, byte b)
        {
            Nombre = nombre;
            R = r;
            G = g;
            B = b;
        }
    }

    static ColorPaleta[] paleta = new ColorPaleta[]
    {
        new ColorPaleta("Azul turquesa oscuro", 30, 38, 39),
        new ColorPaleta("Verde oscuro", 60, 86, 44),
        new ColorPaleta("Gris azulado", 16, 28, 38),
        new ColorPaleta("Azul hielo", 228, 237, 240),
        new ColorPaleta("Negro", 0, 0, 0),
    };

    static string ColorAHex(Color color)
    {
        return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }

}