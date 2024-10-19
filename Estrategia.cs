
using System;
using System.Collections.Generic;
using System.Numerics;
using tp1;

namespace tpfinal
{

    public class Estrategia
    {

        private string ImprimirProceso(Proceso p)
    	{
        	return "Nombre: " + p.nombre + " Tiempo: " + p.tiempo + " Prioridad: " + p.prioridad + "\n";
    	}

    	public string Consulta1(List<Proceso> datos)
    	{
        	string resultado = "";

        	// Se construye una heap copiando los datos originales
        	var heap = new List<Proceso>(datos);
        
        	// Aplicar buildHeap a cada padre para organizar la heap
        	for (int i = heap.Count / 2 - 1; i >= 0; i--)
        	{
            	buildMinHeap(heap, i);
        	}

        	// Recorrer las hojas (elementos después de los padres)
        	for (int i = heap.Count / 2; i < heap.Count; i++)
        	{
            	resultado += ImprimirProceso(heap[i]);
        	}

        	return resultado;
    	}


         public string Consulta2(List<Proceso> datos)
    	{
        	// Calcular la altura de la heap utilizando Math.Log
        	int altura = (int)Math.Log(datos.Count, 2);

        	string resultado = "La Altura de la Heap es: " + altura;

        	return resultado;
   		}



    	public string Consulta3(List<Proceso> datos)
    	{
        	string resultado = "";

        	// Se construye una heap copiando los datos originales
        	var heap = new List<Proceso>(datos);
        
        	// Aplicar buildHeap a cada padre para organizar la heap
        	for (int i = heap.Count / 2 - 1; i >= 0; i--)
        	{
            	buildMinHeap(heap, i);
        	}

        	// Recorrer los elementos del heap y determinar su nivel
        	for (int i = 0; i < heap.Count; i++)
        	{
            	// Calcular el nivel del nodo actual
            	int nivel = (int)Math.Floor(Math.Log(i + 1, 2));

            	// Imprimir el proceso junto con su nivel
            	resultado += ImprimirProcesoConNivel(heap[i], nivel);
        	}

        	return resultado;
    	}

		private string ImprimirProcesoConNivel(Proceso p, int nivel)
    	{
        	return "Nivel " + nivel + ": " + "Nombre: " + p.nombre + " Tiempo: " + p.tiempo + " Prioridad: " + p.prioridad + "\n";
    	}    	


      
		public void ShortesJobFirst(List<Proceso> datos, List<Proceso> collected)
		{
    		// Limpiar la salida
    		collected.Clear();
    
    		// Estructura de datos auxiliar: MinHeap
    		var minHeap = new List<Proceso>(datos);
    
    		// Convertir la lista en un MinHeap aplicando buildHeap desde la mitad hacia atrás
    		for (int i = minHeap.Count / 2 - 1; i >= 0; i--)
   			{
       			 buildMinHeap(minHeap, i);
    		}
    
    		// Extraer elementos del MinHeap y añadirlos a 'collected' en orden
    		while (minHeap.Count > 0)
    		{
        		// Extraer el menor elemento (la raíz) del MinHeap
        		collected.Add(ExtraerMin(minHeap));
    		}
		}
        
        // Función para construir el MinHeap, ajusta el árbol si es necesario
		private void buildMinHeap(List<Proceso> heap, int i)
		{
    		int n = heap.Count;
    		int menor = i;  // Inicializar el nodo como menor
    		int izq = 2 * i + 1;  // Hijo izquierdo
    		int der = 2 * i + 2;  // Hijo derecho

    		// Comparar el nodo actual con su hijo izquierdo
    		if (izq < n && heap[izq].tiempo < heap[menor].tiempo)
    		{
        		menor = izq;
    		}

    		// Comparar el nodo actual con su hijo derecho
    		if (der < n && heap[der].tiempo < heap[menor].tiempo)
    		{
        		menor = der;
    		}

    		// Si se ha encontrado un hijo menor, intercambiar y continuar ajustando
    		if (menor != i)
    		{
        		var temp = heap[i];
        		heap[i] = heap[menor];
        		heap[menor] = temp;

        		// Continuar ajustando el heap desde la nueva posición
        		buildMinHeap(heap,menor);	
    		}
		}

		// Función para extraer el mínimo elemento (la raíz) del MinHeap
		private Proceso ExtraerMin(List<Proceso> heap)
		{
    		// Extraer la raíz (mínimo elemento)
    		var min = heap[0];

    		// Mover el último elemento a la raíz
    		heap[0] = heap[heap.Count - 1];
    		heap.RemoveAt(heap.Count - 1);

    		// Restaurar la propiedad del MinHeap usando buildHeap
            if (heap.Count > 0)
            {
                buildMinHeap(heap, 0); // Ajustar el nuevo heap
            }

            return min;
        }

		
		
        public void PreemptivePriority(List<Proceso> datos, List<Proceso> collected)
		{
    		// Limpiar la salida
    		collected.Clear();

    		// Estructura de datos auxiliar: MaxHeap
    		var maxHeap = new List<Proceso>(datos);

    		// Convertir la lista en un MaxHeap aplicando buildMaxHeap desde la mitad hacia atrás
    		for (int i = maxHeap.Count / 2 - 1; i >= 0; i--)
    		{
        		buildMaxHeap(maxHeap, i);
    		}

    		// Extraer elementos del MaxHeap y añadirlos a 'collected' en orden
    		while (maxHeap.Count > 0)
    		{
        		// Extraer el mayor elemento (la raíz) del MaxHeap
        		collected.Add(ExtraerMax(maxHeap));
    		}
		}
        

		// Función para construir el MaxHeap, ajusta el árbol si es necesario
		private void buildMaxHeap(List<Proceso> heap, int i)
		{
    		int n = heap.Count; // Número de elementos en el heap
    		int mayor = i;  // Inicializar el nodo como mayor

    		while (true)
    		{
        		int izq = 2 * mayor + 1;  // Hijo izquierdo
        		int der = 2 * mayor + 2;  // Hijo derecho
        		int actual = mayor;  // Nodo actual a comparar

        		// Comparar el nodo actual con su hijo izquierdo según la prioridad
        		if (izq < n && heap[izq].prioridad > heap[actual].prioridad)
        		{
            		actual = izq;
        		}

        		// Comparar el nodo actual con su hijo derecho según la prioridad
        		if (der < n && heap[der].prioridad > heap[actual].prioridad)
        		{
            		actual = der;
        		}

        		// Si no se requiere más intercambio, salir del ciclo
        		if (actual == mayor)
        		{
            		break;
        		}

        		// Intercambiar el nodo actual con el mayor hijo
        		var temp = heap[mayor];
        		heap[mayor] = heap[actual];
        		heap[actual] = temp;

        		// Actualizar el índice "mayor" para continuar ajustando
        		mayor = actual;
    		}
		}
		

		// Función para extraer el máximo elemento (la raíz) del MaxHeap
		private Proceso ExtraerMax(List<Proceso> heap)
		{
    		var max = heap[0];
    		heap[0] = heap[heap.Count - 1];
    		heap.RemoveAt(heap.Count - 1);

    		if (heap.Count > 0)
    		{
        		buildMaxHeap(heap, 0);
    		}

    		return max;
		}
   }
}