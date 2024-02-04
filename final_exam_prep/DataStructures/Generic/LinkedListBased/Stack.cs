namespace final_exam_prep.DataStructures.Generic.LinkedListBased {
	internal class Stack<T> : Interfaces.IStack<T> where T: IEquatable<T> {
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

		public Stack() {
			current = null;
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
