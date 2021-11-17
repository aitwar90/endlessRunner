using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataOfActionNoneParametr
{
    private class Node
    {
        public byte typeOfMethodExeciute;
        public bool updateMethod;
        public Queue<SingleMethod> queueOfMethod;
        public Node left = null;
        public Node right = null;
        public Node()
        {

        }
        /**
        <summary>
        Konstruktor Noda do drzewa akcji.
        </summary>
        <param name="_typeOfMethodExecute">Typ wykonuwania metod w kolejce.</param>
        <param name="_updateMethod">Czy metoda ma się wykonywać uatomatycznie.</param>
        */
        public Node(byte _typeOfMethodExecute, bool _updateMethod)
        {
            typeOfMethodExeciute = _typeOfMethodExecute;
            updateMethod = _updateMethod;
            if (queueOfMethod == null)
                queueOfMethod = new Queue<SingleMethod>();
        }
        /**
        <summary>
        Metoda dodaje do strukturę SingleMethod.
        </summary>
        <param name="sm">Struktura dodawana do kolejki</param>
        */
        public void AddToQueue(ref SingleMethod sm)
        {
            SingleMethod temp = queueOfMethod.Dequeue();
            bool findIt = false;
            Queue<SingleMethod> tQueue = new Queue<SingleMethod>();
            while (true)
            {
                if (temp == sm)
                {
                    findIt = true;
                    tQueue.Enqueue(temp);
                }
                if (QueueOfmethodIsEmpty())
                    break;
                temp = queueOfMethod.Dequeue();
            }
            if (!findIt)
                queueOfMethod.Enqueue(sm);
        }
        /**
        <summary>
        Metoda usuwa podany SingleMethod zwracając go.
        </summary>
        <param name="sm">Struktura usuwana z kolejki</param>
        */
        public SingleMethod DeleteFromQueue(SingleMethod sm)
        {
            if (QueueOfmethodIsEmpty())
                return null;
            SingleMethod singleMethodDeleted = null;
            SingleMethod temp = queueOfMethod.Dequeue();
            Queue<SingleMethod> tQueue = new Queue<SingleMethod>();
            while (true)
            {
                if (!temp.RemoveIt(sm))
                {
                    tQueue.Enqueue(temp);
                }
                else
                {
                    singleMethodDeleted = temp;
                }
                if (QueueOfmethodIsEmpty())
                    break;
                temp = queueOfMethod.Dequeue();
            }
            queueOfMethod = tQueue;
            return singleMethodDeleted;
        }
        /**
        <summary>
        Metoda usuwa podany SingleMethod zwracając go.
        </summary>
        <param name="sm">Struktura usuwana z kolejki</param>
        */
        public SingleMethod DeleteFromQueue(SingleMethod.MethodOfParametr sm)
        {
            if (QueueOfmethodIsEmpty())
                return null;
            SingleMethod singleMethodDeleted = null;
            SingleMethod temp = queueOfMethod.Dequeue();
            Queue<SingleMethod> tQueue = new Queue<SingleMethod>();
            while (true)
            {
                if (!temp.RemoveIt(sm))
                {
                    tQueue.Enqueue(temp);
                }
                else
                {
                    singleMethodDeleted = temp;
                }
                if (QueueOfmethodIsEmpty())
                    break;
                temp = queueOfMethod.Dequeue();
            }
            queueOfMethod = tQueue;
            return singleMethodDeleted;
        }
        /**
        <summary>
        Określenie czy Node jest pusty.
        </summary>
        */
        public bool QueueOfmethodIsEmpty()
        {
            if (queueOfMethod == null ||
            queueOfMethod.Count == 0)
                return true;
            return false;
        }
    }
    private Node dataActionNodeParametr = null;

    private Stack<SingleMethod> poolingDataMethod = null;
    /**
    <summary>
    Dodanie metody do obsługi akcji.
    </summary>
    <param name="_action">Metoda jaka jest przypisywana do wykonywania.<param>
    <param name="_typeOfMethodExecute">Parametr określający, kiedy dana metoda ma się wywoływać.</param>
    <param name="_updateMethod">Czy metoda ma się wykonywać automatycznie w update.</param>
    */
    public void AddMethod(SingleMethod.MethodOfParametr _action, byte _typeOfMethodExecute, bool _updateMethod)
    {
        if (poolingDataMethod != null)
        {
            SingleMethod sm = poolingDataMethod.Pop();
            sm.UpdateDataMethod(_action);
            AddToTreeOfMethod(ref sm, _typeOfMethodExecute, _updateMethod);
        }
        else
        {
            SingleMethod sm = new SingleMethod(_action);
            AddToTreeOfMethod(ref sm, _typeOfMethodExecute, _updateMethod);
        }
    }
    /**
    <summary>
    Usunięcie metody z obsługi akcji.
    </summary>
    <param name="_action">Metoda jaka jest przypisywana do wykonywania.<param>
    <param name="_typeOfMethodExecute">Parametr określający, kiedy dana metoda ma się wywoływać.</param>
    <param name="_updateMethod">Czy metoda ma się wykonywać automatycznie w update.</param>
    */
    public void DeleteMethod(SingleMethod.MethodOfParametr _action, byte _typeOfMethodExecute, bool _updateMethod)
    {
        Node node = FindNode(_typeOfMethodExecute);
        if (node == null)
            return;
        node.DeleteFromQueue(_action);
        if (node.QueueOfmethodIsEmpty()) //Jeśli kolejka jest pusta zaktualizuj drzewo usuwając zbędny Nod
        {
            Node actualCheckNode = dataActionNodeParametr;
            Node parentActual = null;
            bool findIt = false;
            while (true)
            {
                if (actualCheckNode.typeOfMethodExeciute == _typeOfMethodExecute)
                {
                    findIt = true;
                    break;
                }
                else if (actualCheckNode.typeOfMethodExeciute < _typeOfMethodExecute)
                {
                    if (actualCheckNode.left != null)
                    {
                        parentActual = actualCheckNode;
                        actualCheckNode = actualCheckNode.left;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (actualCheckNode.right != null)
                    {
                        parentActual = actualCheckNode;
                        actualCheckNode = actualCheckNode.right;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if(findIt)
            {
                //Tu trzeba przypisać odpowiednie gałęzie
            }
        }
    }
    /**
    <summary>
    Znajdź Node w drzewie.
    </summary>
    <param name="_typeOfMethodExecute">Parametr określający, kiedy dana metoda ma się wywoływać.</param>
    <param name="_updateMethod">Czy metoda ma się wykonywać automatycznie w update.</param>
    */
    private Node FindNode(byte _typeOfMethodExecute)
    {
        if (dataActionNodeParametr == null)
            return null;

        Node actualCheckNode = dataActionNodeParametr;
        while (true)
        {
            if (actualCheckNode.typeOfMethodExeciute == _typeOfMethodExecute)
            {
                return actualCheckNode;
            }
            else if (actualCheckNode.typeOfMethodExeciute < _typeOfMethodExecute)
            {
                if (actualCheckNode.left != null)
                {
                    actualCheckNode = actualCheckNode.left;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (actualCheckNode.right != null)
                {
                    actualCheckNode = actualCheckNode.right;
                }
                else
                {
                    return null;
                }
            }
        }
    }
    /**
    <summary>
    Konstruktor struktury obsługi metody.
    </summary>
    <param name="_action">Metoda jaka jest przypisywana do wykonywania.<param>
    <param name="_typeOfMethodExecute">Parametr określający, kiedy dana metoda ma się wywoływać.</param>
    <param name="_updateMethod">Czy metoda ma się wykonywać automatycznie w update.</param>
    */
    private void AddToTreeOfMethod(ref SingleMethod _singleMethod, byte _typeOfMethodExecute, bool _updateMethod)
    {
        if (dataActionNodeParametr == null)
        {
            Node node = new Node(_typeOfMethodExecute, _updateMethod);
            node.AddToQueue(ref _singleMethod);
            dataActionNodeParametr = node;
            return;
        }
        Node n = FindNode(_typeOfMethodExecute);
        if (n == null)
        {
            Node actualCheckNode = dataActionNodeParametr;
            n = new Node(_typeOfMethodExecute, _updateMethod);
            n.AddToQueue(ref _singleMethod);
            while (true)
            {
                if (actualCheckNode.typeOfMethodExeciute < _typeOfMethodExecute)
                {
                    if (actualCheckNode.left == null)
                    {
                        actualCheckNode.left = n;
                        break;
                    }
                    else
                    {
                        actualCheckNode = actualCheckNode.left;
                    }
                }
                else
                {
                    if (actualCheckNode.right == null)
                    {
                        actualCheckNode.right = n;
                        break;
                    }
                    else
                    {
                        actualCheckNode = actualCheckNode.right;
                    }
                }
            }
        }
    }
}
public class SingleMethod
{
    public delegate void MethodOfParametr();
    public MethodOfParametr methodDoExeciute;
    /**
    <summary>
    Konstruktor struktury obsługi metody.
    </summary>
    <param name="_action">Metoda jaka jest przypisywana do wykonywania.<param>
    */
    public SingleMethod(MethodOfParametr _action)
    {
        methodDoExeciute = _action;
    }
    /**
    <summary>
    Aktualizuje dane.
    </summary>
    <param name="_action">Metoda jaka jest przypisywana do wykonywania.<param>
    */
    public void UpdateDataMethod(MethodOfParametr _action)
    {
        methodDoExeciute = _action;
    }
    /**
    <summary>
    Bezpieczna metoda wywoływania podpiętych delegatur.
    </summary>
    */
    public bool SaveExeciuteMethod()
    {
        if (this.methodDoExeciute == null)
            return false;

        methodDoExeciute.Invoke();

        return true;
    }
    /**
    <summary>
    Funkcja zwraca true jeśli udało się usunąć.
    </summary>
    <param name="_action">Porównywany objekt do usunięcia.<param>
    */
    public bool RemoveIt(SingleMethod _action)
    {
        if (_action == this)
        {
            methodDoExeciute = null;
            return true;
        }
        return false;
    }
    /**
    <summary>
    Funkcja zwraca true jeśli udało się usunąć.
    </summary>
    <param name="_action">Metoda jaka jest przypisywana do wykonywania.<param>
    */
    public bool RemoveIt(MethodOfParametr _action)
    {
        if (_action == methodDoExeciute)
        {
            methodDoExeciute = null;
            return true;
        }
        return false;
    }
}
