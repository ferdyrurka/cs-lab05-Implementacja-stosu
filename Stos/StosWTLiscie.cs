using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Stos
{
    public class StosWLiscie<T> : IStos<T>
    {
        //lista jednokierunkowa
        private Wezel<T> szczyt = null;

        private int count;

        public T Peek => IsEmpty ? throw new StosEmptyException() : szczyt.data;

        public int Count => count + 1;

        public bool IsEmpty => szczyt == null;

        public void Clear() => count = -1;

        public T Pop()
        {
            if (IsEmpty)
                throw new StosEmptyException();

            count--;
            T data = szczyt.data;
            szczyt = szczyt.next;

            return data;
        }

        public void Push(T value)
        {
            count++;

            szczyt = new Wezel<T>(value, szczyt);
        }

        public T[] ToArray()
        {
            //return tab;  //bardzo źle - reguły hermetyzacji

            //poprawnie:
            T[] temp = new T[count];
            Wezel<T> lastWezel = szczyt;

            for (int i = 0; i < temp.Length; i++) {
                temp[i] = lastWezel.data;
                if (lastWezel.next != null)
                    lastWezel = lastWezel.next;
            }

            return temp;
        }

        internal class Wezel<T>
        {
            public T data { get; }

            public Wezel<T> next { get; }

            public Wezel(T data, Wezel<T> next)
            {
                this.data = data;
                this.next = next;
            }
        }
    }
}
