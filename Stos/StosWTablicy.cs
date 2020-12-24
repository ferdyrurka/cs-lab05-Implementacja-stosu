using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Stos
{
    public class StosWTablicy<T> : IStos<T>, IEnumerable<T>
    {
        private T[] tab;
        private int szczyt = -1;

        public StosWTablicy(int size = 10)
        {
            tab = new T[size];
            szczyt = -1;
        }

        public T Peek => IsEmpty ? throw new StosEmptyException() : tab[szczyt];

        public int Count => szczyt + 1;

        public bool IsEmpty => szczyt == -1;

        public void Clear() => szczyt = -1;

        public T Pop()
        {
            if (IsEmpty)
                throw new StosEmptyException();

            szczyt--;
            return tab[szczyt + 1];
        }

        public void Push(T value)
        {
            if (szczyt == tab.Length - 1)
            {
                Array.Resize(ref tab, tab.Length * 2);
            }

            szczyt++;

            TrimExcess();

            tab[szczyt] = value;
        }

        public T[] ToArray()
        {
            //return tab;  //bardzo źle - reguły hermetyzacji

            //poprawnie:
            T[] temp = new T[szczyt + 1];
            for (int i = 0; i < temp.Length; i++)
                temp[i] = tab[i];
            return temp;
        }

        public T this[int index]    
        {
            get
            {
                if (index > szczyt + 1)
                    throw new IndexOutOfRangeException("Sczyt: " + szczyt + " tab length: " + tab.Length);

                return tab[index];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new EnumeratorStosu(this);
        }

        public IEnumerable<T> GetEnumeratorYield
        {
            get
            {
                for (int i = 0; i < (this.szczyt + 1); i++)
                {
                    yield return this[i];
                }
            }
        }

        public IEnumerable<T> GetEnumeratorReverseYield
        {
            get
            {
                for (int i = this.szczyt; i >= 0; i--)
                {
                    yield return this[i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ReadOnlyCollection<T> ToArrayReadOnly()
        {
            return Array.AsReadOnly(tab);
        }

        private void TrimExcess()
        {
            if (IsEmpty)
                return;

            int lengthWithFreeTenPercent = (int)Math.Round((szczyt + 1) * 1.1) + 1;

            Array.Resize(ref tab, lengthWithFreeTenPercent);
        }

        internal class EnumeratorStosu : IEnumerator<T>
        {
            private StosWTablicy<T> stos;

            private int position = -1;

            internal EnumeratorStosu(StosWTablicy<T> stos) =>
                this.stos = stos;

            public T Current => stos.tab[position];

            object IEnumerator.Current => Current;

            public void Dispose() {}

            public bool MoveNext()
            {
                if (position < stos.szczyt)
                {
                    position++;
                    return true;
                }

                return false;
            }

            public void Reset() => position = -1;
        }
    }
}
