namespace final_exam_prep.DataStructures.Generic.ArrayBased {
	internal class Stack<T> : Interfaces.IStack<T> where T : IEquatable<T> {

		private int n;
		private T[] v;

		public int Length {
			get { return n; }
			private set { }
		}

		public Stack() {
			n = 0;
			v = new T[n];
		}

		public override string ToString() {
			string result = "[ ";

			for(int i = 0; i < n; i++) {
				result += v[i].ToString() + " ";
			}

			result += "]";
			return result;
		}

		public int Count(int index) => n;

		public void AddEnding(T x) {
			n++;
			T[] array = new T[n];

			for(int i = 0; i < n - 1; i++) {
				array[i] = v[i];
			}

			array[n - 1] = x;
			v = array;
		}

		public T RemoveEnding() {
			n--;
			T[] array = new T[n];

			for(int i = 0; i < n; i++) {
				array[i] = v[i];
			}

			T value = v[n];
			v = array;
			return value;
		}
	}
}
