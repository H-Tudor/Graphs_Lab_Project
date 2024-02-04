namespace final_exam_prep.DataStructures.Generic.LinkedListBased {
	internal class List<T>: Interfaces.IList<T> where T : IEquatable<T> {
		private LinkedNode<T> current;

		public int Length {
			get {
				if(current == null) {
					return 0;
				} else if(current.Previous == null && current.Next == null) {
					return 1;
				} else {
					int count = 1;

					LinkedNode<T> temp = current;
					while(temp.Previous != null) {
						count++;
						temp = temp.Previous;
					}
					temp = current;
					while(temp.Next != null) {
						count++;
						temp = temp.Next;
					}

					return count;
				}
			} 
		}

		public List() {
			current = null;
		}

		public void AddBeginning(T x) {
			if(current == null) {
				current = new LinkedNode<T>(x);
				return;
			}

			LinkedNode<T> temp = current;
			while(temp.Previous != null) {
				temp = temp.Previous;
			}

			temp.Previous =  new LinkedNode<T>(x);
			temp.Previous.Next = temp;
		}

		public void AddEnding(T x) {
			if(current == null) {
				current = new LinkedNode<T>(x);
				return;
			}

			LinkedNode<T> temp = current;
			while(temp.Next != null) {
				temp = temp.Next;
			}

			temp.Next = new LinkedNode<T>(x);
			temp.Next.Previous = temp;
		}

		public int Count(int index) {
			throw new NotImplementedException();
		}

		public void RemoveAll(T x) {
			if(current == null) {
				return;
			} else if(current.Previous == null && current.Next == null) {
				if (current.Value.Equals(x)) {
					current = null;
				}
				return;
			} else {
				LinkedNode<T> temp = current, next, prev;
				do {
					if(temp.Value.Equals(x)) {
						next = temp.Next;
						prev = temp.Previous;

						next.Previous = prev;
						prev.Next = next;

					}

					temp = temp.Previous;
				} while(temp.Previous != null);

				temp = current;
				do {
					if(temp.Value.Equals(x)) {
						next = temp.Next;
						prev = temp.Previous;

						next.Previous = prev;
						prev.Next = next;

					}
					temp = temp.Next;
				} while(temp.Next != null);
			}
		}

		public void RemoveAt(int x) {
			throw new NotImplementedException();
		}

		public T RemoveBeginning() {
			T value;
			if(current.Previous == null) {
				value = current.Value;
				current = null;
				return value;
			}

			LinkedNode<T> temp = current;
			while(temp.Previous != null) {
				temp = temp.Previous;
			}

			value = temp.Value;
			temp.Next.Previous = null;
			temp = null;
			return value;
		}

		public T RemoveEnding() {
			T value;
			if(current.Next == null) {
				value = current.Value;
				current = null;
				return value;
			}

			LinkedNode<T> temp = current;
			while(temp.Next != null) {
				temp = temp.Next;
			}

			value = temp.Value;
			temp.Previous.Next = null;
			temp = null;
			return value;
		}
	}
}
