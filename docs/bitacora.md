# Bitacora de ColorLab
## Predicción inicial
Al intentar predecir el degradado entre Azul Oscuro y Verde Oscuro, puedo suponer que los tonos mostrados se mantendran oscuros, incrementando levemente su brillo mientras mas va moviendose a verde.
Tambien, un fondo blanco hara los colores mas claros al transparentarse con los colores.

## Avance por pasos
Paso 1: Configuración inicial
Se creó el proyecto de consola con Raylib-cs 8.1.0 y se limpió el .gitignore para que bin/ y obj/ dejaran de rastrearse. Se abrió una ventana con fondo blanco que cierra correctamente.

Paso 2: Paleta de colores
Se creó el struct ColorPaleta (nombre, R, G, B) y un arreglo con los cinco colores de la imagen de referencia que vi. Se dibujó cada color con su muestra, nombre, RGB y hexadecimal.

Paso 3: Función ColorAHex
Se agregó la función dada por la actividad, que usa X2 para escribir cada componente con dos dígitos hexadecimales. El resultado coincide con mi conversión manual: Azul turquesa oscuro (30, 38, 39) = #1E2627.

Paso 4: Pintar y borrar en el lienzo
Clic izquierdo sobre una tarjeta selecciona el color, clic en el lienzo lo pinta y la tecla C lo regresa a blanco.

Paso 5: Rediseño de las tarjetas
Se cambió el diseño de la paleta a tarjetas con borde y resaltado para la seleccionada, para parecerse al ejemplo de la actividad y liberar espacio. Las variables se llaman anchoTarjeta, altoTarjeta, esSeleccionado y tarjeta.

Paso 6: Colores A y B
Se renombró indiceSeleccionado a indiceColorA y se agregó indiceColorB, que se elige con clic derecho (sin permitir que sea igual a A). Cada tarjeta muestra una letra A o B en su esquina.

Paso 7: Cálculo del degradado
Se recorren 8 pasos con t = i / (n - 1), usando (double) para evitar la división entera. R, G y B se interpolan por separado con inicio + (fin - inicio) * t y se convierten a byte.

Paso 8: Dibujo del degradado
Cada paso se dibuja como un cuadro (colorMuestra) en una fila debajo del lienzo, con su valor de t con dos decimales. t va de 0.00 a 1.00. El ancho de cada cuadro se calcula como lienzo.Width / n, así el degradado se ajusta al lienzo.

Paso 9: Ajuste del lienzo
Se cambió el lienzo a Rectangle(300, 20, 528, 400). Como el ancho es múltiplo de 8, la fila del degradado mide lo mismo que el lienzo.

Paso 10: RGB de cada muestra
Se agregó debajo de cada valor de t el RGB de esa muestra, calculado con la interpolación de cada canal.El primer cuadro (t = 0.00) muestra el RGB del color A y el último (t = 1.00) el del color B.

Paso 12: Alpha 120 sobre dos fondos
Se dibujaron un fondo blanco y uno oscuro, y encima de cada uno un cuadro más pequeño con el color A con alpha 120.

Paso 13: Instrucciones del programa
Se colocaron las instrucciones sobre como usar el programa bajo las tarjetas de los colores, asi el programa ahora es utilizable para cualquier persona.

## Errores y Correccines: Sesión 03 · Color Lab
- Objetivo del cambio: elegir el color B con clic derecho sin que sea igual a A, y generar los 8 pasos del degradado.
- Comando o prueba ejecutada: revisión del código antes de correr dotnet run.
- Error exacto o síntoma observado:
  - `if i =! j {indiceColorB = j;}`
  - `for (int i = 0; i =< 7; i++;) {double t = i/(double)(8-1)}`
- Mi hipótesis antes de pedir ayuda: pensé que j pdoria funcionar bien como una segunda variable como i y que se podía comparar contra ella.
- Causa identificada: j no tenia un valor valido o util, solo hay un ciclo y su variable es i. Además `=!` no es un operador en C# (es `!=`), `=<` tampoco (es `<=`), faltaban los paréntesis del if, sobraba un `;` en el for y faltaba el `;` al final de la instrucción, cosas que habia olviado por la falta de practica o simplemente se me escaparon por el cansancio.
- Cambio aplicado: comparar i contra indiceColorA, usar `!=` y `<=`, poner los paréntesis y corregir los `;`.
- Resultado de la prueba: el código compila y el degradado se dibuja con t de 0.00 a 1.00.
- Ayuda utilizada, si la hubo: Claude me señaló cada error que cometi y me guio como si fuera mi tutor para corregirlos.
- Lo que puedo explicar: 
  - Con `(double)`, la división `i / (n - 1)` da decimales. Sin él, C# divide enteros y trunca: `3 / 7` daría 0, `t` valdría 0 casi siempre y 1 solo al final, así que casi todas las muestras serían el color inicial.
  - `i` es la posición de la tarjeta que el `for` revisa en cada vuelta. Solo cambia `indiceColorA` o `indiceColorB` cuando hago clic en esa tarjeta, y la condición `i != indiceColorA` evita que B sea igual a A.
  - En `inicio + (fin - inicio) * t`, con `t = 0` queda el color inicial, con `t = 1` el inicio se cancela y queda el final, y con valores intermedios sale una mezcla proporcional. Por ejemplo, con `t = 0.5` obtengo el punto medio.

## Comparación final
1. ¿Qué coincidió con tu predicción?
Coincidió que el degradado entre Azul turquesa oscuro y Verde oscuro se mantiene oscuro todo el trayecto y sube levemente de brillo hacia el verde. También coincidió que el fondo blanco hace ver más claro el color transparente.

2. ¿Qué color o paso cambió más de lo esperado?
Esperaba que el degradado entre Azul turquesa oscuro y Verde oscuro no cambiara mucho, porque los dos son colores oscuros. Lo que vi fue que el cambio sí se nota un poco, llas primeras muestras son casi grises azuladas y las últimas ya se ven claramente verdes, aunque todas siguen siendo oscuras.

3. ¿La conversión hexadecimal coincidió?
Sí. Convertí a mano 30, 38 y 39 a 1E, 26 y 27, y el programa mostró #1E2627 para Azul turquesa oscuro.

4. ¿Cómo influyó el fondo en la transparencia?
Con alpha 120, el color A se ve distinto según el fondo aunque sus valores RGB no cambian, sobre blanco se ve muy claro y casi se funde con el fondo, y sobre el oscuro se ve gris o casi negro.

Explicación breve:
El color es una mezcla de rojo, verde y azul de 0 a 255. En el degradado, cada canal se interpola por separado con inicio + (fin - inicio) * t, y t va de 0 a 1 en pasos iguales. Con alpha 120, el color final mezcla aproximadamente 47% del color con 53% del fondo, por eso cambia el resultado visible.