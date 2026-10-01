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
            int anchoTarjeta = 260;
            int altoTarjeta = 60;

            for (int i = 0; i < paleta.Length; i++)
            {
                var c = paleta[i];
                Color colorRaylib = new Color(c.R, c.G, c.B, (byte)255);
                Rectangle tarjeta = new Rectangle(10, y, anchoTarjeta, altoTarjeta);

                bool esSeleccionado = i == indiceSeleccionado;

                Raylib.DrawRectangleRec(tarjeta, esSeleccionado? new Color((byte)210, (byte)230, (byte)250, (byte)255): Color.White);
                Raylib.DrawRectangleLinesEx(tarjeta, esSeleccionado ? 2 : 1, esSeleccionado ? Color.Blue : Color.LightGray);

                Raylib.DrawRectangle((int)tarjeta.X + 10, (int)tarjeta.Y + 10, 40, 40, colorRaylib);
                Raylib.DrawText(c.Nombre, (int)tarjeta.X + 60, (int)tarjeta.Y + 8, 16, Color.Black);

                string info = $"RGB({c.R},{c.G},{c.B})  {ColorAHex(colorRaylib)}";
                Raylib.DrawText(info, (int)tarjeta.X + 60, (int)tarjeta.Y + 30, 13, Color.DarkGray);

                if (Raylib.CheckCollisionPointRec(mouse, tarjeta) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    indiceSeleccionado = i;
                }

                y += altoTarjeta + 8;
            }

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