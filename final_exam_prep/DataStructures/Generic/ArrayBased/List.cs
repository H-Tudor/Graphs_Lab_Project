namespace final_exam_prep.DataStructures.Generic.ArrayBased {
	internal class List<T> : Interfaces.IList<T> where T : IEquatable<T> {

		private int n;
		private T[] v;

		public T this[int i] {
			get { return v[i]; }
			set { v[i] = value; }
		}

		public int Length {
			get { return n; }
			private set { }
		}

		public List() {
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

		public void AddBeginning(T x) {
			n++;
			T[] array = new T[n];

			for(int i = 1; i < n; i++) {
				array[i] = v[i - 1];
			}

			array[0] = x;
			v = array;
		}

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

		public void RemoveAll(T x) {
			int count = 0;
			for(int i = 0; i < n; i++) {
				if(!v[i].Equals(x)) {
					count++;
				}
			}

			int k = 0;
			T[] array = new T[count];
			for(int i = 0; i < n; i++) {
				if(!v[i].Equals(x)) {
					array[k] = v[i];
					k++;
				}
			}

			n = count;
			v = array;
		}

		public void RemoveAt(int x) {
			int k = 0;
			T[] array = new T[n - 1];

			for(int i = 0; i < n; i++) {
				if(k != x) {
					array[k] = v[i];
					k++;
				}
			}

			n = n - 1;
			v = array;
		}
	}
}
