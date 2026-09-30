using Raylib_cs;
using System.Numerics;
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

            Vector2 mouse = Raylib.GetMousePosition();

            int y = 20;
            for (int i = 0; i < paleta.Length; i++)
            {
                var c = paleta[i];
                Color colorRaylib = new Color(c.R, c.G, c.B, (byte)255);
                Rectangle fila = new Rectangle(10, y - 5, 400, 50);

                Raylib.DrawRectangle(20, y, 40, 40, colorRaylib);
                string info = $"{c.Nombre}  RGB({c.R},{c.G},{c.B})  {ColorAHex(colorRaylib)}";
                Raylib.DrawText(info, 70, y + 10, 18, Color.Black);

                if (Raylib.CheckCollisionPointRec(mouse, fila) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    indiceSeleccionado = i;
                }

                y += 55;
            }

            // Lienzo para pintar
            Raylib.DrawRectangle((int)lienzo.X, (int)lienzo.Y, (int)lienzo.Width, (int)lienzo.Height, colorPintado);
            Raylib.DrawRectangleLines((int)lienzo.X, (int)lienzo.Y, (int)lienzo.Width, (int)lienzo.Height, Color.Black);

            if (Raylib.CheckCollisionPointRec(mouse, lienzo) && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                var seleccionado = paleta[indiceSeleccionado];
                colorPintado = new Color(seleccionado.R, seleccionado.G, seleccionado.B, (byte)255);
            }

            if (Raylib.IsKeyPressed(KeyboardKey.C))
            {
                colorPintado = Color.White;
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
    
    static int indiceSeleccionado = 0;
    static Color colorPintado = Color.White;
    static Rectangle lienzo = new Rectangle(450, 20, 350, 350);

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