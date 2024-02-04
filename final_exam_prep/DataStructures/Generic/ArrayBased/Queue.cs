namespace final_exam_prep.DataStructures.Generic.ArrayBased {
	internal class Queue<T> : Interfaces.IQueue<T> where T : IEquatable<T> {

		private int n;
		private T[] v;

		public int Length {
			get { return n; }
			private set { }
		}

		public Queue() {
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

		public T RemoveBeginning() {
			n--;
			T[] array = new T[n];

			for(int i = 1; i <= n; i++) {
				array[i - 1] = v[i];
			}

			T value = v[0];
			v = array;
			return value;
		}
	}
}
