namespace final_exam_prep.DataStructures.Generic.LinkedListBased {
	internal class LinkedNode<T> {
		public T Value { get; set; }
		public LinkedNode<T> Previous { get; set; }
		public LinkedNode<T> Next { get; set; }

		public LinkedNode(T value) {
			Value = value;
			Previous = null;
			Next = null;
		}
	}
}
