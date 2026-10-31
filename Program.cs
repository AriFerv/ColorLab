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

                bool esSeleccionado = i == indiceColorA;
                bool esColorB = i == indiceColorB;

                Raylib.DrawRectangleRec(tarjeta, esSeleccionado? new Color((byte)210, (byte)230, (byte)250, (byte)255): Color.White);
                Raylib.DrawRectangleLinesEx(tarjeta, esSeleccionado ? 2 : 1, esSeleccionado ? Color.Blue : Color.LightGray);

                Raylib.DrawRectangle((int)tarjeta.X + 10, (int)tarjeta.Y + 10, 40, 40, colorRaylib);
                Raylib.DrawText(c.Nombre, (int)tarjeta.X + 60, (int)tarjeta.Y + 8, 16, Color.Black);

                string info = $"RGB({c.R},{c.G},{c.B})  {ColorAHex(colorRaylib)}";
                Raylib.DrawText(info, (int)tarjeta.X + 60, (int)tarjeta.Y + 30, 13, Color.DarkGray);

                if (Raylib.CheckCollisionPointRec(mouse, tarjeta) && Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    indiceColorA = i;
                }

                if (Raylib.CheckCollisionPointRec(mouse, tarjeta) && Raylib.IsMouseButtonPressed(MouseButton.Right))
                {
                    if (i != indiceColorA) {indiceColorB = i;}
                }

                y += altoTarjeta + 8;

                if (esSeleccionado)
                {
                    Raylib.DrawText("A", (int)tarjeta.X + (int)tarjeta.Width - 20, (int)tarjeta.Y + 5, 18, Color.Blue);
                }

                if (esColorB)
                {
                    Raylib.DrawText("B", (int)tarjeta.X + (int)tarjeta.Width - 20, (int)tarjeta.Y + (int)tarjeta.Height - 25, 18, Color.Maroon);
                }
            }

            var colorA = paleta[indiceColorA];
            var colorB = paleta[indiceColorB];

            int n = 8;
            int anchoMuestra = (int)(lienzo.Width / n);
            int xBase = (int)lienzo.X;
            int yMuestra = (int)(lienzo.Y + lienzo.Height + 40);

            for (int i = 0; i <= 7; i++) {
                double t = i/(double)(n-1);
                    //componente =    inicio + (fin - inicio) * t
                byte rMuestra = (byte)(colorA.R + (colorB.R - colorA.R) * t);
                byte gMuestra = (byte)(colorA.G + (colorB.G - colorA.G) * t);
                byte bMuestra = (byte)(colorA.B + (colorB.B - colorA.B) * t);
                Color colorMuestra = new Color(rMuestra, gMuestra, bMuestra, (byte)255);

                int xMuestra = xBase + i * anchoMuestra;
                Raylib.DrawRectangle(xMuestra, yMuestra, anchoMuestra - 4, 50, colorMuestra);
                Raylib.DrawText($"{t:0.00}", xMuestra, yMuestra + 55, 14, Color.Black);
                Raylib.DrawText($"{rMuestra},{gMuestra},{bMuestra}", xMuestra, yMuestra + 72, 9, Color.DarkGray);
                }

            Raylib.DrawRectangle((int)lienzo.X, (int)lienzo.Y, (int)lienzo.Width, (int)lienzo.Height, colorPintado);
            Raylib.DrawRectangleLines((int)lienzo.X, (int)lienzo.Y, (int)lienzo.Width, (int)lienzo.Height, Color.Black);

            if (Raylib.CheckCollisionPointRec(mouse, lienzo) && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                var seleccionado = paleta[indiceColorA];
                colorPintado = new Color(seleccionado.R, seleccionado.G, seleccionado.B, (byte)255);
            }

            if (Raylib.IsKeyPressed(KeyboardKey.C))
            {
                colorPintado = Color.White;
            }

            byte alphaValor = 120;
            Color colorSemitransparente = new Color(colorA.R, colorA.G, colorA.B, alphaValor);

            Rectangle fondoBlanco = new Rectangle(10, 448, 130, 100);
            Rectangle fondoOscuro = new Rectangle(150, 448, 130, 100);

            Raylib.DrawRectangleRec(fondoBlanco, Color.White);
            Raylib.DrawRectangleLinesEx(fondoBlanco, 1, Color.LightGray);
            Raylib.DrawRectangleRec(fondoOscuro, Color.Black);

            Raylib.DrawRectangle((int)fondoBlanco.X + 20, (int)fondoBlanco.Y + 20, 90, 60, colorSemitransparente);
            Raylib.DrawRectangle((int)fondoOscuro.X + 20, (int)fondoOscuro.Y + 20, 90, 60, colorSemitransparente);

            Raylib.DrawText($"Alpha {alphaValor} sobre dos fondos", 10, 428, 14, Color.Black);
            Raylib.DrawText("Fondo blanco", 10, 553, 14, Color.DarkGray);
            Raylib.DrawText("Fondo oscuro", 150, 553, 14, Color.DarkGray);

            //como van a saber como se usa equisde
            Raylib.DrawText("Clic izq. en tarjeta: color A (pinta)", 10, 362, 11, Color.DarkGray);
            Raylib.DrawText("Clic der. en tarjeta: color B (degradado)", 10, 376, 11, Color.DarkGray);
            Raylib.DrawText("Clic izq. en lienzo: pinta con A", 10, 390, 11, Color.DarkGray);
            Raylib.DrawText("Tecla C: limpia el lienzo", 10, 404, 11, Color.DarkGray);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
    
    static int indiceColorA = 0;
    static int indiceColorB = 1;
    static Color colorPintado = Color.White;
    static Rectangle lienzo = new Rectangle(300, 20, 528, 400);

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