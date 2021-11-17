using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataOfAction
{
    private class Node
    {
        public byte typeOfMethodExeciute;
        public bool updateMethod;
        public SingleMethod queueOfMethod;
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
        <param name="_queueOfMethod">Struktura zawierająca dane do delegatury.</param>
        */
        public Node(byte _typeOfMethodExecute, bool _updateMethod, SingleMethod _queueOfMethod)
        {
            typeOfMethodExeciute = _typeOfMethodExecute;
            updateMethod = _updateMethod;
            queueOfMethod = _queueOfMethod;
        }
        /**
        <summary>
        Metoda dodaje do strukturę SingleMethod.
        </summary>
        <param name="sm">Struktura dodawana do kolejki</param>
        */
        public void AddToQueue(ref SingleMethod sm)
        {
            if (sm.IsParams())
            {
                queueOfMethod.AddAction(sm.GetWithParametr(), sm.GetVariables());
            }
            else
            {
                queueOfMethod.AddAction(sm.GetNoParametr());
            }
        }
        /**
        <summary>
        Metoda dodaje do strukturę SingleMethod.
        </summary>
        <param name="sm">Struktura dodawana do kolejki.</param>
        <param name="_variables">Parametry do wywoływanych metod.</param>
        */
        public void AddToQueue(ref SingleMethod sm, CustomVariable[] _variables)
        {
            queueOfMethod.AddAction(sm.GetWithParametr(), _variables);
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
            bool qIsparams = queueOfMethod.IsParams();
            if (qIsparams == sm.IsParams())
            {
                if (qIsparams)
                {
                    queueOfMethod.RemoveAction(sm.GetWithParametr());
                    if (queueOfMethod.GetWithParametr() == null)
                    {
                        return queueOfMethod;
                    }
                }
                else
                {
                    queueOfMethod.RemoveAction(sm.GetNoParametr());
                    if (queueOfMethod.GetNoParametr() == null)
                    {
                        return queueOfMethod;
                    }
                }
            }
            return null;
        }
        /**
        <summary>
        Określenie czy Node jest pusty.
        </summary>
        */
        public bool QueueOfmethodIsEmpty()
        {
            if (queueOfMethod == null)
            {
                return true;
            }
            if (queueOfMethod.GetNoParametr() == null && queueOfMethod.GetWithParametr() == null)
            {
                return true;
            }
            return false;
        }
    }
    private Node dataActionNodeParametr = null;

    private Stack<SingleMethod> poolingDataMethodNoParam = null;
    private Stack<SingleMethod> poolingDataMethodWithParam = null;
    /**
    <summary>
    Dodanie metody do obsługi akcji.
    </summary>
    <param name="_action">Metoda jaka jest przypisywana do wykonywania.<param>
    <param name="_typeOfMethodExecute">Parametr określający, kiedy dana metoda ma się wywoływać.</param>
    <param name="_updateMethod">Czy metoda ma się wykonywać automatycznie w update.</param>
    <param name="_vatiables">Parametry jakie ma przyjmować metoda.</params>
    */
    public void AddMethod(SingleMethod _action, byte _typeOfMethodExecute, bool _updateMethod, CustomVariable[] _vatiables = null)
    {
        Node node = FindNode(_typeOfMethodExecute);
        if (node != null)
        {
            node.AddToQueue(ref _action);
            return;
        }
        bool isParamsAction = _action.IsParams();
        if (isParamsAction)
        {
            if (poolingDataMethodWithParam != null)
            {
                SingleMethod sm = poolingDataMethodWithParam.Pop();
                sm.UpdateDataMethod(_action, _vatiables);
                AddToTreeOfMethod(ref sm, _typeOfMethodExecute, _updateMethod);
            }
            else
            {
                MethodWithParametr mwp = new MethodWithParametr(_action.GetWithParametr(), _vatiables);
                SingleMethod sm = (SingleMethod)mwp;
                AddToTreeOfMethod(ref sm, _typeOfMethodExecute, _updateMethod);
            }
        }
        else
        {
            if (poolingDataMethodNoParam != null)
            {
                SingleMethod sm = poolingDataMethodNoParam.Pop();
                sm.UpdateDataMethod(_action, _vatiables);
                AddToTreeOfMethod(ref sm, _typeOfMethodExecute, _updateMethod);
            }
            else
            {
                MethodNoneParametr mwp = new MethodNoneParametr(_action.GetNoParametr());
                SingleMethod sm = (SingleMethod)mwp;
                AddToTreeOfMethod(ref sm, _typeOfMethodExecute, _updateMethod);
            }
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
    public void DeleteMethod(SingleMethod _action, byte _typeOfMethodExecute, bool _updateMethod)
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
            if (findIt)
            {
                //Tu trzeba przypisać odpowiednie gałęzie
                AddToTreeOfMethod(ref actualCheckNode.left, actualCheckNode.left.typeOfMethodExeciute, actualCheckNode.left.updateMethod);
                AddToTreeOfMethod(ref actualCheckNode.right, actualCheckNode.right.typeOfMethodExeciute, actualCheckNode.right.updateMethod);
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
    Wywołaj metody zawarte w konkretnym indeksem podanym w parametrze.
    </summary>
    <param name="_typeOfMethodExecute">Parametr określający, jakie metody mają się wywołać.</param>
    */
    public void ExeciuteTypeOfMethod(byte _typeOfMethodExecute)
    {
        Node node = FindNode(_typeOfMethodExecute);
        if(node != null)
        {
            node.queueOfMethod.SaveExeciuteMethod();
        }
    }
    /**
    <summary>
    Metoda dodaje do drzewa.
    </summary>
    <param name="_action">Metoda jaka jest przypisywana do wykonywania.<param>
    <param name="_typeOfMethodExecute">Parametr określający, kiedy dana metoda ma się wywoływać.</param>
    <param name="_updateMethod">Czy metoda ma się wykonywać automatycznie w update.</param>
    */
    private void AddToTreeOfMethod(ref SingleMethod _singleMethod, byte _typeOfMethodExecute, bool _updateMethod)
    {
        if (dataActionNodeParametr == null)
        {
            Node node = new Node(_typeOfMethodExecute, _updateMethod, _singleMethod);
            node.AddToQueue(ref _singleMethod);
            dataActionNodeParametr = node;
            return;
        }
        Node n = FindNode(_typeOfMethodExecute);
        if (n == null)
        {
            Node actualCheckNode = dataActionNodeParametr;
            n = new Node(_typeOfMethodExecute, _updateMethod, _singleMethod);
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
    /**
    <summary>
    Metoda dodaje do drzewa.
    </summary>
    <param name="_action">Metoda jaka jest przypisywana do wykonywania.<param>
    <param name="_typeOfMethodExecute">Parametr określający, kiedy dana metoda ma się wywoływać.</param>
    <param name="_updateMethod">Czy metoda ma się wykonywać automatycznie w update.</param>
    */
    private void AddToTreeOfMethod(ref Node _singleMethod, byte _typeOfMethodExecute, bool _updateMethod)
    {
        Node actualCheckNode = dataActionNodeParametr;
        while (true)
        {
            if (actualCheckNode.typeOfMethodExeciute < _typeOfMethodExecute)
            {
                if (actualCheckNode.left == null)
                {
                    actualCheckNode.left = _singleMethod;
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
                    actualCheckNode.right = _singleMethod;
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
public abstract class SingleMethod
{
    /**
    <summary>
    Aktualizuje dane.
    </summary>
    <param name="_action">Metoda jaka jest przypisywana do wykonywania.<param>
    */
    public virtual void UpdateDataMethod(SingleMethod _action)
    {

    }
    /**
    <summary>
    Aktualizuje dane.
    </summary>
    <param name="_action">Metoda jaka jest przypisywana do wykonywania.<param>
    */
    public virtual void UpdateDataMethod(SingleMethod _action, CustomVariable[] _variables)
    {

    }
    /**
    <summary>
    Czy zawiera parametry?
    </summary>
    */
    public abstract bool IsParams();
    /**
    <summary>
    Bezpieczna metoda wywoływania podpiętych delegatur.
    </summary>
    */
    public abstract bool SaveExeciuteMethod();
    /**
    <summary>
    Funkcja zwraca true jeśli udało się usunąć.
    </summary>
    <param name="_action">Porównywany objekt do usunięcia.<param>
    */
    public abstract bool RemoveIt(SingleMethod _action);
    /**
    <summary>
    Funkcja zwraca true jeśli udało się usunąć.
    </summary>
    <param name="_action">Metoda jaka jest przypisywana do wykonywania.<param>
    */
    public virtual bool RemoveIt(MethodNoneParametr.MethodOfParametr _action)
    {
        return false;
    }
    /**
    <summary>
    Funkcja zwraca true jeśli udało się usunąć.
    </summary>
    <param name="_action">Metoda jaka jest przypisywana do wykonywania.<param>
    */
    public virtual bool RemoveIt(MethodWithParametr.MethodWithParametrIs _action)
    {
        return false;
    }
    /**
    <summary>
    Funkcja zwraca delegaty bez parametrów.
    </summary>
    */
    public virtual MethodNoneParametr.MethodOfParametr GetNoParametr()
    {
        return null;
    }
    /**
    <summary>
    Funkcja zwraca delegaty z parametrami.
    </summary>
    */
    public virtual MethodWithParametr.MethodWithParametrIs GetWithParametr()
    {
        return null;
    }
    public virtual CustomVariable[] GetVariables() { return null; }
    public virtual void AddAction(MethodNoneParametr.MethodOfParametr _action) { }
    public virtual void AddAction(MethodWithParametr.MethodWithParametrIs _action, CustomVariable[] _variables) { }
    public virtual void RemoveAction(MethodNoneParametr.MethodOfParametr _action) { }
    public virtual void RemoveAction(MethodWithParametr.MethodWithParametrIs _action) { }
}
public class MethodNoneParametr : SingleMethod
{
    public delegate void MethodOfParametr();
    public MethodOfParametr methodDoExeciute;
    /**
    <summary>
    Konstruktor struktury obsługi metody.
    </summary>
    <param name="_action">Metoda jaka jest przypisywana do wykonywania.<param>
    */
    public MethodNoneParametr(MethodOfParametr _action)
    {
        methodDoExeciute = _action;
    }
    /**
    <summary>
    Aktualizuje dane.
    </summary>
    <param name="_action">Metoda jaka jest przypisywana do wykonywania.<param>
    */
    public override void UpdateDataMethod(SingleMethod _action)
    {
        methodDoExeciute = _action.GetNoParametr();
    }
    /**
    <summary>
    Czy zawiera parametry?
    </summary>
    */
    public override bool IsParams()
    {
        return false;
    }
    /**
    <summary>
    Bezpieczna metoda wywoływania podpiętych delegatur.
    </summary>
    */
    public override bool SaveExeciuteMethod()
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
    public override bool RemoveIt(SingleMethod _action)
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
    public override bool RemoveIt(MethodOfParametr _action)
    {
        if (_action == methodDoExeciute)
        {
            methodDoExeciute = null;
            return true;
        }
        return false;
    }
    /**
    <summary>
    Funkcja zwraca delegaty bez parametrów.
    </summary>
    */
    public override MethodNoneParametr.MethodOfParametr GetNoParametr()
    {
        return methodDoExeciute;
    }
    /**
    <summary>
    Metoda dodaje akcje do delegatury.
    </summary>
    <param name="_action">Akcja, jaka ma zostać dodana do delegatury (bez parametru).</param>
    */
    public override void AddAction(MethodOfParametr _action)
    {
        methodDoExeciute -= _action;
        methodDoExeciute += _action;
    }
    /**
    <summary>
    Metoda usuwa akcje z delegatury.
    </summary>
    <param name="_action">Akcja, jaka ma zostać usunięta z delegatury (bez parametru).</param>
    */
    public override void RemoveAction(MethodOfParametr _action)
    {
        methodDoExeciute -= _action;
    }
}
public class MethodWithParametr : SingleMethod
{
    public delegate void MethodWithParametrIs(CustomVariable[] variables);
    public MethodWithParametrIs methodDoExeciute;
    private CustomVariable[] variables = null;
    /**
    <summary>
    Konstruktor struktury obsługi metody.
    </summary>
    <param name="_action">Metoda jaka jest przypisywana do wykonywania.<param>
    <param name="_variables">Tablica parametrów do wywoływania</param>
    */
    public MethodWithParametr(MethodWithParametrIs _action, CustomVariable[] _variables)
    {
        methodDoExeciute = _action;
        variables = _variables;
    }
    /**
    <summary>
    Aktualizuje dane.
    </summary>
    <param name="_action">Metoda jaka jest przypisywana do wykonywania.<param>
    <param name="_variables">Tablica parametrów do wywoływania</param>
    */
    public override void UpdateDataMethod(SingleMethod _action, CustomVariable[] _variables)
    {
        methodDoExeciute = _action.GetWithParametr();
        variables = _variables;
    }
    /**
    <summary>
    Czy zawiera parametry?
    </summary>
    */
    public override bool IsParams()
    {
        return true;
    }
    /**
    <summary>
    Bezpieczna metoda wywoływania podpiętych delegatur.
    </summary>
    */
    public override bool SaveExeciuteMethod()
    {
        if (this.methodDoExeciute == null)
            return false;

        methodDoExeciute.Invoke(variables);

        return true;
    }
    /**
    <summary>
    Funkcja zwraca true jeśli udało się usunąć.
    </summary>
    <param name="_action">Porównywany objekt do usunięcia.<param>
    */
    public override bool RemoveIt(SingleMethod _action)
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
    public override bool RemoveIt(MethodWithParametrIs _action)
    {
        if (_action == methodDoExeciute)
        {
            methodDoExeciute = null;
            return true;
        }
        return false;
    }
    /**
    <summary>
    Funkcja zwraca delegaty z parametrami.
    </summary>
    */
    public override MethodWithParametr.MethodWithParametrIs GetWithParametr()
    {
        return methodDoExeciute;
    }
    /**
    <summary>
    Metoda dodaje akcje do delegatury.
    </summary>
    <param name="_action">Akcja, jaka ma zostać dodana do delegatury (bez parametru).</param>
    */
    public override void AddAction(MethodWithParametrIs _action, CustomVariable[] _variables)
    {
        methodDoExeciute -= _action;
        methodDoExeciute += _action;
        variables = _variables;
    }
    /**
    <summary>
    Metoda usuwa akcje z delegatury.
    </summary>
    <param name="_action">Akcja, jaka ma zostać usunięta z delegatury (bez parametru).</param>
    */
    public override void RemoveAction(MethodWithParametrIs _action)
    {
        methodDoExeciute -= _action;
    }
    /**
    <summary>
    Funkcja zwraca parametry. 
    </summary>
    */
    public override CustomVariable[] GetVariables()
    {
        return variables;
    }
}
public class CustomVariable
{
    private System.Object variable;
    /**
    <summary>
    Metoda ustawia wartość.
    </summary>
    */
    public void SetVariable<T>(T _variable)
    {
        if (variable == null)
            variable = new System.Object();
        variable = (object)_variable;
    }
    /**
    <summary>
    Metoda zwraca typ przechowywanej wartości.
    </summary>
    */
    public System.Type GetTypeOfVariable()
    {
        return variable.GetType();
    }
    /**
    <summary>
    Funkcja zwraca wartość variable.
    </summary>
    */
    public T GetVariableValue<T>()
    {
        System.Type tt = GetTypeOfVariable();
        unchecked
        {
            if (tt == typeof(float))
            {
                float ttt = (float)variable;
                return (T)System.Convert.ChangeType(ttt, typeof(T));
            }
            else if (tt == typeof(bool))
            {
                return (T)System.Convert.ChangeType((bool)variable, typeof(T));
            }
            else if (tt == typeof(double))
            {
                return (T)System.Convert.ChangeType((double)variable, typeof(T));
            }
            else if (tt == typeof(byte))
            {
                return (T)System.Convert.ChangeType((byte)variable, typeof(T));
            }
            else if (tt == typeof(sbyte))
            {
                return (T)System.Convert.ChangeType((sbyte)variable, typeof(T));
            }
            else if (tt == typeof(short))
            {
                return (T)System.Convert.ChangeType((short)variable, typeof(T));
            }
            else if (tt == typeof(ushort))
            {
                return (T)System.Convert.ChangeType((ushort)variable, typeof(T));
            }
            else if (tt == typeof(int))
            {
                return (T)System.Convert.ChangeType((int)variable, typeof(T));
            }
            else if (tt == typeof(uint))
            {
                return (T)System.Convert.ChangeType((uint)variable, typeof(T));
            }
            else if (tt == typeof(string))
            {
                return (T)System.Convert.ChangeType((string)variable, typeof(T));
            }
            else if (tt == typeof(char))
            {
                return (T)System.Convert.ChangeType((char)variable, typeof(T));
            }
            else if (tt == typeof(System.Enum))
            {
                return (T)System.Convert.ChangeType((System.Enum)variable, typeof(T));
            }
            return (T)System.Convert.ChangeType(-254, typeof(T));
        }
    }
}