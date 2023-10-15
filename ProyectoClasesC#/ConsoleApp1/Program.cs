using System.ComponentModel;
using System.Drawing;


/**
 * EditorGrafico
 *      Actúa como un contenedor para todos los objetos gráficos.
 *      Proporciona métodos para agregar nuevos objetos gráficos y para dibujar todos los objetos gráficos que contiene.
 *      Posee una relación de asociación IGraficos al usar los objetos que se implementan en la interfaz.
 */
public class EditorGrafico
{
    private List<IGrafico> _graficos = new List<IGrafico>();

    public void AddGrafico(IGrafico grafico)
    {5
        _graficos.Add(grafico);
    }

    public void DibujarTodo()
    {
        foreach (var grafico in _graficos)
        {
            grafico.Dibujar();
        }
    }
}


/**
 * Interfaz IGrafico
 * Defino los métodos que van a ser implementados por cualquier clase que represente un gráfico.
 * Método Mover:
     * Debe ser implementado por cualquier clase que implemente IGrafico.
     * @return Verdadero si el gráfico pudo ser movido, falso en caso contrario.
 * Método Dibujar:
     * Debe ser implementado por cualquier clase que implemente IGrafico.
 */
public interface IGrafico 
{

    public bool Mover(int x, int y);

    public void Dibujar();
   
}

/**
 * Clase Punto:
    * Representa un punto en un espacio bidimensional.
    * Implementa la interfaz IGrafico.
 */
public class Punto : IGrafico 
{
    public int X { get; set; } // Propiedades X e Y representan las coordenadas del punto.
    public int Y { get; set; }

    /**
    * Constructor de Punto:
        * Inicializa un nuevo punto con las coordenadas dadas.
    */
    public Punto(int x, int y)
    {
        X = x;
        Y = y;
    }


    // Implementación del método Mover para la clase Punto.
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

    // Implementación del método Dibujar para la clase Punto.
    public virtual void Dibujar() 
    {
        Console.WriteLine($"Dibujando un Punto en ({X}, {Y})");
    }

}

/**
 * Clase GraficoCompuesto
 * Representa un gráfico compuesto por varios otros gráficos.
 * Implementa la interfaz IGrafico.
 */
public class GraficoCompuesto : IGrafico
{
    // Lista de gráficos que componen este gráfico compuesto.
    private List<IGrafico> _graficos = new List<IGrafico>();

    /**
     * Método AddGrafico
     * Añade un nuevo gráfico a la lista de gráficos.
     */
    public void AddGrafico(IGrafico grafico)
    {
        _graficos.Add(grafico);
    }

    // Implementación del método Mover para GraficoCompuesto
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

    // Implementación del método Dibujar para GraficoCompuesto
    public void Dibujar()
    {
        foreach (var grafico in _graficos)
        {
            grafico.Dibujar();
        }
    }
}

/**
 * Clase Circulo
 * Representa un círculo en un espacio bidimensional.
 * Hereda de la clase Punto e implementa la interfaz IGrafico.
 */
public class Circulo : Punto
{
    // Propiedad Radio representa el radio del círculo.
    public int Radio { get; set; }

    /**
     * Constructor de Circulo
     * Inicializo un nuevo círculo con las coordenadas y el radio dados.
     */
    public Circulo(int x, int y, int radio) : base(x, y)
    {
        Radio = radio;
    }

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
    public override void Dibujar()
    {
        Console.WriteLine($"Dibujando un Círculo en ({X}, {Y}) con radio {Radio}");
    }
}

/**
 * Clase Rectangulo
 * Representa un rectángulo en un espacio bidimensional.
 * Hereda de la clase Punto e implementa la interfaz IGrafico.
 */
public class Rectangulo : Punto
{
    // Propiedades Ancho y Alto representan las dimensiones del rectángulo.
    public int Ancho { get; set; }
    public int Alto { get; set; }

    /**
     * Constructor de Rectangulo
     * Inicializa un nuevo rectángulo con las coordenadas, el ancho y el alto dados.
     */
    public Rectangulo(int x, int y, int ancho, int alto) : base(x, y)
    {
        Ancho = ancho;
        Alto = alto;
    }

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

    public override void Dibujar()
    {
        Console.WriteLine($"Dibujando un Rectángulo en ({X}, {Y}) con ancho {Ancho} y alto {Alto}");
    }
}

// EJERCICIO 2

class Program
{
    /**
     * Punto de entrada del programa.
     *      Se crean instancias de la clase EditorGrafico, Punto, Circulo, Rectangulo y GraficoCompuesto.
     *      
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

            if (x - radio < 0 || x + radio > 800 || y - radio < 0 || y + radio > 600)
            {
                throw new Exception("El círculo está fuera de la pantalla.");
            }


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

            if (x < 0 || x + ancho > 800 || y < 0 || y + alto > 600)
            {
                throw new Exception("El rectángulo está fuera de la pantalla.");
            }

            Rectangulo miRectangulo = new Rectangulo(x, y, ancho, alto);

            // Crear un objeto de GraficoCompuesto
            GraficoCompuesto miGraficoCompuesto = new GraficoCompuesto();

            // Añadir los gráficos al GraficoCompuesto
            miGraficoCompuesto.AddGrafico(miPunto);
            miGraficoCompuesto.AddGrafico(miCirculo);
            miGraficoCompuesto.AddGrafico(miRectangulo);

            // Dibujar el gráfico compuesto
            miGraficoCompuesto.Dibujar();

            // Creo un EditorGrafico
            EditorGrafico miEditor = new EditorGrafico();

            //Agrego los objetos gráficos al editor
            miEditor.AddGrafico(miPunto);
            miEditor.AddGrafico(miCirculo);
            miEditor.AddGrafico(miRectangulo);

            // Dibujar todos los objetos gráficos
            miEditor.DibujarTodo();


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
