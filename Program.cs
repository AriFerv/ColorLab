Color color = new Color(52, 152, 219, 255);

string hex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
// 52  -> 34
// 152 -> 98
// 219 -> DB
// Resultado: #3498DB
