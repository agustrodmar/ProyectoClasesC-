using System.ComponentModel;
using System.Drawing;
using System.Text;

/**
 * Clase EditorGrafico que actúa como un contenedor para todos los objetos gráficos.
 * Proporciona métodos para agregar nuevos objetos gráficos y para dibujar todos los objetos gráficos que contiene.
 * Posee una relación de asociación IGraficos al usar los objetos que se implementan en la interfaz.
 *
 * @property _graficos Lista de objetos gráficos.
 */
public class EditorGrafico
{
    private List<IGrafico> _graficos = new List<IGrafico>();

    /**
     * Método para agregar un nuevo objeto gráfico a la lista.
     *
     * @param grafico El objeto gráfico a agregar.
     */
    public void AddGrafico(IGrafico grafico)
    {
        _graficos.Add(grafico);
    }

    /**
     * Método para dibujar todos los objetos gráficos en la lista.
     */
    public string DibujarTodo()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var grafico in _graficos)
        {
            sb.AppendLine(grafico.Dibujar());
        }
        return sb.ToString();
    }
}

/**
 * Interfaz IGrafico que define los métodos que van a ser implementados por cualquier clase que represente un gráfico.
 */
public interface IGrafico
{
    /**
     * Método Mover que debe ser implementado por cualquier clase que implemente IGrafico.
     *
     * @param x La nueva coordenada x.
     * @param y La nueva coordenada y.
     * @return Verdadero si el gráfico pudo ser movido, falso en caso contrario.
     */
    public bool Mover(int x, int y);

    /**
     * Método Dibujar que debe ser implementado por cualquier clase que implemente IGrafico.
     */
    public string Dibujar();
}

/**
 * Clase Punto que representa un punto en un espacio bidimensional e implementa la interfaz IGrafico.
 *
 * @property X Coordenada x del punto.
 * @property Y Coordenada y del punto.
 */
public class Punto : IGrafico
{
    public int X { get; set; }
    public int Y { get; set; }

    /**
     * Constructor de Punto que inicializa un nuevo punto con las coordenadas dadas.
     *
     * @param x La coordenada x del punto.
     * @param y La coordenada y del punto.
     */
    public Punto(int x, int y)
    {
        if (x < 0 || x > 800 || y < 0 || y > 600)
        {
            throw new Exception("El punto está fuera de la pantalla.");
        }
        X = x;
        Y = y;
    }

    /**
     * Implementación del método Mover para la clase Punto. Mueve el punto a las nuevas coordenadas si están dentro de la pantalla (800x600).
     *
     * @param x La nueva coordenada x.
     * @param y La nueva coordenada y.
     * @return Verdadero si el punto pudo ser movido, falso en caso contrario.
     */
    public virtual bool Mover(int x, int y)
    {
        if (x < 0 || x > 800 || y < 0 || y > 600) // Si se mueve fuera de pantalla (800x600) devuelve false.
        {
            return false;
        }
        X = x;
        Y = y;
        return true;
    }

    /**
     * Implementación del método Dibujar para la clase Punto. Dibuja el punto en las coordenadas actuales.
     */
    public virtual string Dibujar()
    {
        return $"Dibujando un Punto en ({X}, {Y})";
    }
}

/**
 * Clase GraficoCompuesto que representa un gráfico compuesto por varios otros gráficos.
 * Implementa la interfaz IGrafico.
 *
 * @property _graficos Lista de gráficos que componen este gráfico compuesto.
 */
public class GraficoCompuesto : IGrafico
{
    // Lista de gráficos que componen este gráfico compuesto.
    private List<IGrafico> _graficos = new List<IGrafico>();

    /**
     * Método para añadir un nuevo gráfico a la lista de gráficos.
     *
     * @param grafico El objeto gráfico a agregar.
     */
    public void AddGrafico(IGrafico grafico)
    {
        _graficos.Add(grafico);
    }

    /**
     * Implementación del método Mover para GraficoCompuesto. Mueve todos los gráficos en la lista a las nuevas coordenadas.
     *
     * @param x La nueva coordenada x.
     * @param y La nueva coordenada y.
     * @return Verdadero si todos los gráficos pudieron ser movidos, falso en caso contrario.
     */
    public bool Mover(int x, int y)
    {
        foreach (var grafico in _graficos)
        {
            if (!grafico.Mover(x, y))
            {
                return false;
            }
        }
        return true;
    }

    /**
     * Implementación del método Dibujar para GraficoCompuesto. Dibuja todos los gráficos en la lista.
     */
    public string Dibujar()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var grafico in _graficos)
        {
            sb.AppendLine(grafico.Dibujar());
        }
        return sb.ToString();
    }
}

/**
 * Clase Circulo que representa un círculo en un espacio bidimensional.
 * Hereda de la clase Punto e implementa la interfaz IGrafico.
 *
 * @property Radio Radio del círculo.
 */
public class Circulo : Punto
{
    // Propiedad Radio representa el radio del círculo.
    public int Radio { get; set; }

    /**
     * Constructor de Circulo que inicializa un nuevo círculo con las coordenadas y el radio dados.
     *
     * @param x La coordenada x del círculo.
     * @param y La coordenada y del círculo.
     * @param radio El radio del círculo.
     */
    public Circulo(int x, int y, int radio) : base(x, y)
    {
        if (x - radio < 0 || x + radio > 800 || y - radio < 0 || y + radio > 600)
        {
            throw new Exception("El círculo está fuera de la pantalla.");
        }
        Radio = radio;
    }

    /**
     * Implementación del método Mover para la clase Circulo. Mueve el círculo a las nuevas coordenadas si están dentro de la pantalla (800x600).
     *
     * @param x La nueva coordenada x.
     * @param y La nueva coordenada y.
     * @return Verdadero si el círculo pudo ser movido, falso en caso contrario.
     */
    public override bool Mover(int x, int y)
    {
        if (x - Radio < 0 || x + Radio > 800 || y - Radio < 0 || y + Radio > 600)
        {
            return false;
        }
        X = x;
        Y = y;
        return true;
    }

    /**
     * Implementación del método Dibujar para la clase Circulo. Dibuja el círculo en las coordenadas actuales con el radio dado.
     */
    public override string Dibujar()
    {
        return $"Dibujando un Círculo en ({X}, {Y}) con radio {Radio}";
    }
}

/**
 * Clase Rectangulo que representa un rectángulo en un espacio bidimensional.
 * Hereda de la clase Punto e implementa la interfaz IGrafico.
 *
 * @property Ancho Ancho del rectángulo.
 * @property Alto Alto del rectángulo.
 */
public class Rectangulo : Punto
{
    public int Ancho { get; set; }
    public int Alto { get; set; }

    /**
     * Constructor de Rectangulo que inicializa un nuevo rectángulo con las coordenadas, el ancho y el alto dados.
     *
     * @param x La coordenada x del rectángulo.
     * @param y La coordenada y del rectángulo.
     * @param ancho El ancho del rectángulo.
     * @param alto El alto del rectángulo.
     */
    public Rectangulo(int x, int y, int ancho, int alto) : base(x, y)
    {
        if (x < 0 || x + ancho > 800 || y < 0 || y + alto > 600)
        {
            throw new Exception("El rectángulo está fuera de la pantalla.");
        }
        Ancho = ancho;
        Alto = alto;
    }

    /**
     * Implementación del método Mover para la clase Rectangulo. Mueve el rectángulo a las nuevas coordenadas si están dentro de la pantalla (800x600).
     *
     * @param x La nueva coordenada x.
     * @param y La nueva coordenada y.
     * @return Verdadero si el rectángulo pudo ser movido, falso en caso contrario.
     */
    public override bool Mover(int x, int y)
    {
        if (x < 0 || x + Ancho > 800 || y < 0 || y + Alto > 600)
        {
            return false;
        }
        X = x;
        Y = y;
        return true;
    }

    /**
     * Implementación del método Dibujar para la clase Rectangulo. Dibuja el rectángulo en las coordenadas actuales con el ancho y alto dados.
     */
    public override string Dibujar()
    {
        return $"Dibujando un Rectángulo en ({X}, {Y}) con ancho {Ancho} y alto {Alto}";
    }
}

// EJERCICIO 2

class Program
{
    /**
     * Punto de entrada del programa. Se crean instancias de la clase EditorGrafico, Punto, Circulo, Rectangulo y GraficoCompuesto.
     *
     * @param args Argumentos de línea de comandos.
     */
    static void Main(string[] args)
    {
        try
        {
            // Crear un Punto
            Console.Write("Introduce la coordenada X del punto: ");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Introduce la coordenada Y del punto: ");
            int y = Convert.ToInt32(Console.ReadLine());

            if (x < 0 || x > 800 || y < 0 || y > 600)
            {
                throw new Exception("El punto está fuera de la pantalla.");
            }

            Punto miPunto = new Punto(x, y);

            // Crear un Circulo
            Console.Write("Introduce la coordenada X del círculo: ");
            x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Introduce la coordenada Y del círculo: ");
            y = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Introduce el radio del círculo: ");
            int radio = Convert.ToInt32(Console.ReadLine());

            Circulo miCirculo = new Circulo(x, y, radio);

            // Crear un Rectangulo
            Console.Write("Introduce la coordenada X del rectángulo: ");
            x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Introduce la coordenada Y del rectángulo: ");
            y = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Introduce el ancho del rectángulo");
            int ancho = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Introduce el alto del rectángulo: ");
            int alto = Convert.ToInt32(Console.ReadLine());

            Rectangulo miRectangulo = new Rectangulo(x, y, ancho, alto);

            // Crear un objeto de GraficoCompuesto
            GraficoCompuesto miGraficoCompuesto = new GraficoCompuesto();

            // Añadir los gráficos al GraficoCompuesto
            miGraficoCompuesto.AddGrafico(miPunto);
            miGraficoCompuesto.AddGrafico(miCirculo);
            miGraficoCompuesto.AddGrafico(miRectangulo);

            // Dibujo el Gráfico Compuesto manteniendo el Principio de Responsabilidad única para las clases.
            string mensaje = miGraficoCompuesto.Dibujar();
            Console.WriteLine(mensaje);

            // Creo un EditorGrafico
            EditorGrafico miEditor = new EditorGrafico();

            //Agrego los objetos gráficos al editor
            miEditor.AddGrafico(miPunto);
            miEditor.AddGrafico(miCirculo);
            miEditor.AddGrafico(miRectangulo);

            // Dibujo todos los objetos gráficos manteniendo el Principio de Responsabilidad única para las clases.
            mensaje = miEditor.DibujarTodo();
            Console.WriteLine(mensaje);


            /**
             * Bucle para mover el gráfico o salir. Se pregunta al usuario que es lo que
             * desea introducir. Se llaman a los Métodos Mover y Dibujar. 
             * Se tratan las excepciones necesarias para que el programa finalice si el 
             * usuario introduce inputs inválidos.
             */
            while (true)
            {
                Console.WriteLine("Elige lo que deseas hacer: ");
                Console.WriteLine("1. Mover el gráfico");
                Console.WriteLine("2. Salir");
                int opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Introduce las nuevas coordenadas (x y):");
                        // Crear un Rectangulo
                        Console.Write("Introduce la coordenada X del punto: ");
                        x = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Introduce la coordenada Y del punto: ");
                        y = Convert.ToInt32(Console.ReadLine());
                        if (miGraficoCompuesto.Mover(x, y))
                        {
                            Console.WriteLine("Gráfico movido con éxito.");
                            miGraficoCompuesto.Dibujar();
                        }
                        else
                        {
                            Console.WriteLine("No se pudo mover el gráfico.");
                        }
                        break;
                    case 2:
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Introduzca un valor válido para el dato.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Se produjo un error: {ex.Message}");
        }
    }
}
